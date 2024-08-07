#!/usr/bin/env python3
# -*- coding: utf-8 -*-
#
#  Textauswertung.py
#  
#  Copyright 2024  B. Borys
#  


#import requests
#import MySQLdb
#from MySQLdb import Error,IntegrityError
#from math import log2
def TextSplit(mld):
	idSatz=" %satzende "
	mld=mld.replace('#',' ')
	mld=mld.replace('&amp;',' ')
	mld=mld.replace('»',' ')
	mld=mld.replace('«',' ')
	mld=mld.replace('+',' ')
	mld=mld.replace('(',' ')
	mld=mld.replace(')',' ')
	mld=mld.replace('"',' ')
	mld=mld.replace(".",idSatz)
	mld=mld.replace(":",idSatz)
	mld=mld.replace(","," ")
	mld=mld.replace("-",' ')
	mld=mld.replace(" – ",idSatz)
	mld=mld.replace("?",idSatz)
	#print("--1"+mld)
	mld=mld.replace(idSatz+" "+idSatz,idSatz)
	#print("--2"+mld)
	mld=mld.replace(idSatz+"  "+idSatz,idSatz)
	#print("--3"+mld)
	mld=mld.replace( "%satzende  %satzende",idSatz)
	#print("--4"+mld)
	#
	return mld.split()
