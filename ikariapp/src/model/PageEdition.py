'''
Created on 31.05.2012

@author: Ikari
'''
from google.appengine.ext import db
from model.User import User

class PageEdition(db.Model):
    content = db.StringProperty(required = False, multiline=True)
    created = db.DateTimeProperty(auto_now_add = True)
    author = db.ReferenceProperty(User, required = True)