#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  RSSDB5WStat.py
#  
#  Copyright 2024  <pi@FHEM>
#  

import numpy
import requests
import MySQLdb
from MySQLdb import Error,IntegrityError
from math import log2
from Textauswertung import TextSplit
from string import Template

from optparse import OptionParser
parser = OptionParser(description='Statistiken')
parser.add_option("-v", "--verbose", dest="Dbg",default=False,help="Debug-Mode",action="store_true")
#parser.add_option("-z", "--zurück", dest="Zurk",default=False,help="zurücksetzen (Wortzähler auf 0",action="store_true")

options, arguments = parser.parse_args()
Dbg=options.Dbg
#Zurk=options.Zurk


def main(args):
	pDb="news"
	pDbHost="localhost"
	pDbUser="news"
	pDbPwd="news"
	pDbTblMld="meldungen"
	pHash=0
	pTitel=2
	pMeldung=3
	pW1=4
	mld=""
	ttl=""
	idSatz=" %satzende "

	db = MySQLdb.connect(pDbHost,pDbUser,pDbPwd,pDb)
	if Dbg: print(db)
	
	cursorm = db.cursor()
	cursorw = db.cursor()
	
	sql = f"select mldentr from {pDbTblMld} where (mldentr >0)"
	if Dbg: print(sql)
	nDaten=cursorw.execute(sql)
	alle=cursorw.fetchall()
	if nDaten>0:
	
		print(f"{'Meldungen':*^30}")
		print(f"{'Entropie':^30}")
		mini=numpy.min(alle)
		maxi=numpy.max(alle)
		mean=numpy.mean(alle)
		median=numpy.median(alle)
		std=numpy.std(alle)
		print(f"Max {maxi:.4f} Mittel {mean:.5f} Median {median:.5f} Min {mini:.5f}")
		s3u=max(mean-3*std,0)
		s3o=mean+3*std
		s6u=max(mean-6*std,0)
		s6o=mean+6*std
		
		
		
		s = Template('\t3 Sigma ${s3u}...${s3o}\n\t6 Sigma $s6u...$s6o')
		a=s.substitute(s3u=f"{s3u:.3f}", s3o=f"{s3o:.3f}",s6u=f"{s6u:.3f}", s6o=f"{s6o:.3f}")
		print(a)
		swo=mean+2*std
		print(f"{'wichtige':^30}\n")
		print(f"Entropie > {swo:.1f}\n")
		sql = f"select titel,meldung,mldentr from {pDbTblMld} where (mldentr >{swo}) order by mldentr DESC"
		if Dbg: print(sql)
		nDaten=cursorw.execute(sql)
		if Dbg: print(nDaten)
		alle=cursorw.fetchall()
		for a in alle:
			print(a[0])
			print(a[1])
			print(f"-----{a[2]:.1f}-----")
		
	
	return
			
				
				



			
if __name__ == '__main__':
	import sys
	sys.exit(main(sys.argv))
