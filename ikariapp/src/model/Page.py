'''
Created on 31.05.2012

@author: Ikari
'''
from google.appengine.ext import db

class Page(db.Model):
    name = db.StringProperty(required = True)
    editions = db.ListProperty(db.Key)
    
    @classmethod
    def get_page_by_name(cls, name):
        pages = list(db.GqlQuery("select * from Page where name='%s'" % name ))
        if len(pages) > 0:
            return pages[0]
        return None
