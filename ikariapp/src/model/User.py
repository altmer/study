'''
Created on 27.05.2012

@author: Ikari
'''

from google.appengine.ext import db

class User(db.Model):
    name = db.StringProperty(required = True)
    hashpw = db.StringProperty(required = True)
    email = db.StringProperty(required = False)
    salt = db.StringProperty(required = True)