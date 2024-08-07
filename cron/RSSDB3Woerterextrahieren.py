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
parser = OptionParser(description='Wörter aus Meldungen in DB extrahieren')
parser.add_option("-v", "--verbose", dest="Dbg",default=False,help="Debug-Mode",action="store_true")
parser.add_option("-z", "--zurück", dest="Zurk",default=False,help="zurücksetzen (Wortzähler auf 0",action="store_true")

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
	if(Zurk):
		sql = f"UPDATE {pDbTblW1} SET anzahl=0" 
		if Dbg: print(sql)
		a=cursorw.execute(sql)
		print(f"{a} Wörter zurückgesetzt")
		sql = f"UPDATE {pDbTblMld} SET w1=0" 
		if Dbg: print(sql)
		a=cursorm.execute(sql)
		print(f"{a} Meldungen zurückgesetzt")
		db.commit()
		print("...Neuanfang notwendig")
		return
	
	sql = "select * from %s where (w1 = false)"%(pDbTblMld)
	if Dbg: print(sql)
	nDaten=cursorm.execute(sql)
	if nDaten>0:
		print("%d Meldungen zu bearbeiten"%(nDaten))
		daten=cursorm.fetchone()
		while nDaten>0:
			nDaten=nDaten-1
			hash=daten[pHash]
			mld=(daten[ pTitel]+idSatz+daten[pMeldung]).lower()
			if(Dbg):print(mld)
			woerter=TextSplit(mld)
			if(Dbg):print(woerter)
			
			for wort in woerter:
				sql = "select anzahl from %s where (w1 = '%s')"%(pDbTblW1,wort)
				if Dbg: print(sql)
				nW1=cursorw.execute(sql)
				#if Dbg: print(nW1)
				assert(nW1<2)
				if(nW1<1):
					sql = "INSERT INTO %s (w1,anzahl) VALUES ('%s',1)" % (pDbTblW1,wort)
					if(Dbg):print(sql)
					a=cursorw.execute(sql)
					assert a == 1
				else:
					daten=cursorw.fetchone()
					anz=daten[0]+1
					if Dbg: print(anz)
					sql = "UPDATE %s SET anzahl='%d' WHERE w1='%s'" % (pDbTblW1,anz,wort)
					if Dbg:print (sql)
					a=cursorw.execute(sql)
					if Dbg:print (a)
					assert a == 1
				#else if--------------------
			#for wort----------------------
			# Meldung als bearbeitet markieren
			sql = "UPDATE %s SET w1=True WHERE hash='%s'" % (pDbTblMld,hash)
			if Dbg:print (sql)
			a=cursorw.execute(sql)
			#if Dbg:print (a)
			assert a == 1
			daten=cursorm.fetchone()
		
		
		
		#wuile-----------------------------
		print(f"{'DB Commit':*^40}")
		db.commit()            
	
	else:
		print("keine Meldungen zu bearbeiten")
	#if meldungen
	
	print(f"{'zählen':*^40}")
	
	sql = f"SELECT * FROM {pDbTblW1} WHERE anzahl>0" 
	#if Dbg: print(sql)
	types=cursorw.execute(sql)
	sql = f"SELECT sum(anzahl) from {pDbTblW1} WHERE anzahl>0" 
	if Dbg: print(sql)
	a=cursorm.execute(sql)
	token=cursorm.fetchone()[0]
	print(f"{types} Types in {token} Token ")
	print(f"Abdeckung {types/token:.2} ")
		
	
	
	return
			
				
				



			
if __name__ == '__main__':
	import sys
	sys.exit(main(sys.argv))
