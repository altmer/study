'''
Created on 03.05.2012

@author: Ikari
'''
from google.appengine.ext import db
import json
import time

class BlogPost(db.Model):
    subject = db.StringProperty(required = True)
    content = db.TextProperty(required = True)
    created = db.DateTimeProperty(auto_now_add = True)
    
    @classmethod
    def get_all_posts(cls):
        return list ( db.GqlQuery('select * from BlogPost order by created desc') )
    
    @classmethod
    def get_by_id(cls, ident):
        key = db.Key.from_path('BlogPost', int(ident) )
        return db.get(key) 
    
    def to_json(self):
        d = self.__dict__
        
        content = d['_content']
        subj = d['_subject']        
        created_date = d['_created']
        
        created_str = time.strftime('%d-%m-%Y %H:%M:%S', created_date.timetuple())
        
        return  json.loads('{ "content" : "%s", "subject" : "%s", "created" : "%s" }' % (content, subj, created_str) ) 