'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from google.appengine.ext import db
from model.BlogPost import BlogPost
from google.appengine.api import memcache
import time

POST_KEY = 'post_%s'
POST_TIME_KEY = 'post_time_%s'

class GetPostHandler(Handler):
    
    def render_post(self,post=None,error="",post_generated=None):
        self.render('blogpost.html', post=post, error=error,post_generated=post_generated)
    
    def get(self, **kwargs):
        ident = kwargs['id']
        
        post = memcache.get(POST_KEY % ident)
        t = memcache.get(POST_TIME_KEY % ident)
        
        if not post or not t:
            post = BlogPost.get_by_id(ident)
            t = time.time()
            memcache.set(POST_KEY % ident, post)
            memcache.set(POST_TIME_KEY % ident, t)

        t = int (time.time() - t)
        
        if not post:
            error = 'No such post'
            self.render_post(error=error)
        else:
            self.render_post(post = post, post_generated = t)
            
        