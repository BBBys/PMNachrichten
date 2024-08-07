#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  RSSDB3Woerter.py
#  
#  Copyright 2024  <pi@FHEM>
#  


from string import Template
import MySQLdb
from MySQLdb import Error,IntegrityError
from math import log2
from Textauswertung import TextSplit
import numpy

from optparse import OptionParser
parser = OptionParser(description='Wörter zählen, rel. Häufigkeit und Entropie berechnen')
parser.add_option("-v", "--verbose", dest="Dbg",default=False,help="Debug-Mode",action="store_true")
#parser.add_option("-z", "--zurück", dest="Zurk",default=False,help="zurücksetzen (Wortzähler auf 0",action="store_true")

options, arguments = parser.parse_args()
Dbg=options.Dbg


def main(args):
	TTLKENN="<title>"
	MLDKENN="<description>"
	pDb="news"
	pDbHost="localhost"
	pDbUser="news"
	pDbPwd="news"
	pDbTblMld="meldungen"
	pDbTblW1="woerter1"
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

	print(f"{'zählen und Entropie berechnen':*^40}")
	
	sql = f"SELECT * FROM {pDbTblW1} WHERE anzahl>0" 
	#if Dbg: print(sql)
	types=cursorw.execute(sql)
	sql = f"SELECT sum(anzahl) from {pDbTblW1} WHERE anzahl>0" 
	if Dbg: print(sql)
	a=cursorm.execute(sql)
	token=cursorm.fetchone()[0]
	print(f"{types} Types in {token} Token ")
	print(f"Abdeckung {types/token:.2} ")
		
	
	sql = f"SELECT w1,anzahl from {pDbTblW1} WHERE anzahl>0" 
	a=cursorw.execute(sql)
	alle=cursorw.fetchall()
	#if Dbg: print(alle)
	for daten in alle:
		#if Dbg: print(daten)
		relh=daten[1]/token
		#if Dbg: print(relh)
		entr=-log2(relh)
		#if Dbg: print(entr)
		sql = f"update {pDbTblW1} set relh={relh:.4}, entr={entr:.3} WHERE w1='{daten[0]}'" 
		#if Dbg: print(sql)
		a=cursorm.execute(sql)
	db.commit()
	
	#Statistiken
	print(f"{'Statistiken':*^40}")
		
	sql = f"select relh from {pDbTblW1} where (anzahl >0)"
	if Dbg: print(sql)
	nDaten=cursorw.execute(sql)
	if nDaten>0:
		alle=cursorw.fetchall()
	
		# Wörter Häufigkeit
	
		print(f"{'Wörter':-^30}")
		print(f"{'Häufigkeit':^30}")
		maxi=numpy.max(alle)
		mean=numpy.mean(alle)
		median=numpy.median(alle)
		std=numpy.std(alle)
		print(f"Max {maxi:.4f} Mittel {mean:.5f} Median {median:.5f}")
		s3u=max(mean-3*std,0)
		s3o=mean+3*std
		s6u=max(mean-6*std,0)
		s6o=mean+6*std
			
		s = Template('\t3 Sigma ${s3u}...${s3o}\n\t6 Sigma $s6u...$s6o')
		a=s.substitute(s3u=f"{s3u:.3f}", s3o=f"{s3o:.3f}",s6u=f"{s6u:.3f}", s6o=f"{s6o:.3f}")
		print(a)
		print(f"{'häufige (über 3 Sigma)':^30}\n")
		sql = f"select w1,relh from {pDbTblW1} where (relh >{s3o}) order by anzahl DESC"
		if Dbg: print(sql)
		nDaten=cursorw.execute(sql)
		print(f"{nDaten} Wörter")
		alle=cursorw.fetchall()
		for a in alle:
			print("\t",a[0],f"\t{a[1]:.3f}")
		
		print(f"\n\n\n{'Entropie':^30}")
		sql = f"select entr from {pDbTblW1} where (anzahl >0)"
		if Dbg: print(sql)
		nDaten=cursorw.execute(sql)
		alle=cursorw.fetchall()
		maxi=numpy.max(alle)
		mean=numpy.mean(alle)
		median=numpy.median(alle)
		std=numpy.std(alle)
		print(f"Max {maxi:.4f} Mittel {mean:.4f} Median {median:.4f}")
		s3u=max(mean-3*std,0)
		s5u=max(mean-5*std,0)
		s3o=mean+3*std
		s6u=max(mean-6*std,0)
		s6o=mean+6*std
		s = Template('\t3 Sigma ${s3u}...${s3o}\n\t6 Sigma $s6u...$s6o')
		a=s.substitute(s3u=f"{s3u:.3f}", s3o=f"{s3o:.3f}",s6u=f"{s6u:.3f}", s6o=f"{s6o:.3f}")
		print(a)
		print(f"{'unwichtige':^30}")
		print(f"{'(Kriterium 5 Sigma)':^30}")
		sql = f"select w1,relh,entr from {pDbTblW1} where (entr <{s5u}) order by anzahl DESC"
		if Dbg: print(sql)
		nDaten=cursorw.execute(sql)
		print(f"{nDaten} Wörter")
		alle=cursorw.fetchall()
		for a in alle:
			print("\t",a[0])
		print(" ")

	return
			
				
				



			
if __name__ == '__main__':
	import sys
	sys.exit(main(sys.argv))
