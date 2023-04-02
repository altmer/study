'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from google.appengine.ext import db
from model.BlogPost import BlogPost
import json

class GetPostJsonHandler(Handler):
    
    def get(self, **kwargs):
        ident = kwargs['id']        
        post = BlogPost.get_by_id(ident)
        
        self.response.headers.add_header('Content-Type', 'application/json; charset=utf-8')
        self.write( json.dumps( post.to_json() ) )            
        