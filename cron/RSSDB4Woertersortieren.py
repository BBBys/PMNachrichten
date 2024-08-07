#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  RSSDB3Woerter.py
#  
#  Copyright 2024  <pi@FHEM>
#  


import requests
import MySQLdb
from MySQLdb import Error,IntegrityError
from math import log2
from Textauswertung import TextSplit

from optparse import OptionParser
parser = OptionParser(description='Wörter zählen, rel. Häufigkeit und Entropie berechnen, Wörter in Stoppliste und Zählliste sortieren')
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
	
	
	return
			
				
				



			
if __name__ == '__main__':
	import sys
	sys.exit(main(sys.argv))
