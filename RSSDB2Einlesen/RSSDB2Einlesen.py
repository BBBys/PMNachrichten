#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  RSSDB2.py
#  
#  Copyright 2024  <pi@FHEM>
#  


import requests
import MySQLdb
from MySQLdb import Error,IntegrityError
import zlib
from optparse import OptionParser
parser = OptionParser(description='Meldungen aus Datei in DB. Parameter_ Datei')
parser.add_option("-v", "--verbose", dest="Dbg",default=False,help="Debug-Mode",action="store_true")

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
	f = open(arguments[0],"rt")

	try: 
		db = MySQLdb.connect(pDbHost,pDbUser,pDbPwd,pDb)
		print("Start...")
		if Dbg: print(db)
	except requests.exceptions.ConnectionError as error: 
		print("The error is %s" % error)

	try:
		cursor = db.cursor()
		position=0
		ttl=f.readline()
		mld=f.readline()
		if Dbg: print(mld)
		while(len(mld)>0):
			position=position+1
			if(mld.startswith(TTLKENN)):
				#einen Formatfehler korrigieren
				ttl=mld
				mld=f.readline()
				print(f"Formatfehler bei {position} korrigiert")
				
			ttl=ttl.replace("'","")
			mld=mld.replace("'","")
			if Dbg: print(position)
			if(not ttl.startswith(TTLKENN) or not mld.startswith(MLDKENN)):
				print(f"Formatfehler bei {position}")
				print(ttl)
				print(mld)
				return (-1)
			t1=ttl.split(">")
			t2=t1[1].split("<")
			ttl=t2[0]
			hTtl= zlib.crc32(ttl.encode())
			sql="SELECT hash FROM %s WHERE hash = %d" % (pDbTblMld,hTtl)
			if(Dbg):print(sql)
			a=cursor.execute(sql)
			if(Dbg):print(a)
			if(a>0):
				print("",end="-")
			else:
				print("",end="+")
				t1=mld.split(">")
				t2=t1[1].split("<")
				mld=t2[0]
				#print(mld)
				sql = "INSERT INTO %s (hash,titel,meldung) VALUES (0x%x,'%s','%s')" % (pDbTblMld,hTtl,ttl,mld)
				if(Dbg):print(sql)
				a=cursor.execute(sql)
				assert a>0
			ttl=f.readline()
			mld=f.readline()
		db.commit()            
		print("...fertig")
	except IntegrityError as e:
		if Dbg:print(e)
		return
	except Error as e:
		print(e)
		return   
	return 0










if __name__ == '__main__':
    import sys
    sys.exit(main(sys.argv))
