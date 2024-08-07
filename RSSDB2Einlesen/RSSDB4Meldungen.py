#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  RSSDB4Meldungen.py
#  
#  Copyright 2024  <pi@FHEM>
#  


import requests
import MySQLdb
from MySQLdb import Error,IntegrityError
from math import log2
from Textauswertung import TextSplit

from optparse import OptionParser
parser = OptionParser(description='Entropie für Meldungen in DB berechnen')
parser.add_option("-v", "--verbose", dest="Dbg",default=False,help="Debug-Mode",action="store_true")
parser.add_option("-z", "--zurück", dest="Zurk",default=False,help="zurücksetzen (Meldungsemtropie auf 0",action="store_true")

options, arguments = parser.parse_args()
Dbg=options.Dbg
Zurk=options.Zurk


def main(args):
	TTLKENN="<title>"
	MLDKENN="<description>"
	pDb="news"
	pDbHost="localhost"
	pDbUser="news"
	pDbPwd="news"
	pDbTblMld="meldungen"
	pDbTblW1="woerter1"
	pTitel=0
	pMeldung=1
	pHash=2
	pW1=4
	mld=""
	ttl=""
	idSatz=" %satzende "

	db = MySQLdb.connect(pDbHost,pDbUser,pDbPwd,pDb)
	if Dbg: print(db)
	
	cursorm = db.cursor()
	cursorw = db.cursor()
	if(Zurk):
		# sql = f"UPDATE {pDbTblW1} SET anzahl=0" 
		# if Dbg: print(sql)
		# a=cursorw.execute(sql)
		# print(f"{a} Wörter zurückgesetzt")
		# sql = f"UPDATE {pDbTblMld} SET w1=0" 
		# if Dbg: print(sql)
		# a=cursorm.execute(sql)
		# print(f"{a} Meldungen zurückgesetzt")
		# db.commit()
		return "nicht implementiert"
	
	sql = f"select titel,meldung,hash from {pDbTblMld} "
	if Dbg: print(sql)
	nDaten=cursorm.execute(sql)
	if nDaten>0:
		print("%d Meldungen zu bearbeiten"%(nDaten))
		daten=cursorm.fetchone()
		while nDaten>0:
			nDaten=nDaten-1
			hash=daten[ pHash]
			mld=(daten[ pTitel]+idSatz+daten[pMeldung]).lower()
			#if(Dbg):print(mld)
			woerter=TextSplit(mld)
			#if(Dbg):print(woerter)
			mldentr=0
			for wort in woerter:
				sql = f"select entr from {pDbTblW1} where (w1 = '{wort}')"
				#if Dbg: print(sql)
				nW1=cursorw.execute(sql)
				#if Dbg: print(nW1)
				if(nW1!=1):
					print(f"Fehler Anzahl Einträge {wort} = {nW1}")
				else:
					
					daten=cursorw.fetchone()
					mldentr+=daten[0]
				#else if--------------------
			#for wort----------------------
			# Meldung bearbeitet 
			if Dbg: print(mldentr)
			sql = f"UPDATE {pDbTblMld} SET mldentr={mldentr} WHERE hash={hash}" 
			#if Dbg:print (sql)
			a=cursorw.execute(sql)
			#if Dbg:print (a)
			assert a == 1
			daten=cursorm.fetchone()
			print("",end="+")
		
		
		
		#wuile-----------------------------
		db.commit()            
	
	
	return
			
				
				



			
if __name__ == '__main__':
	import sys
	sys.exit(main(sys.argv))
