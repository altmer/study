'''
Created on 27.04.2012

@author: Ikari
'''
import cgi

def escape(s):
    return cgi.escape(s, True) if s else ''