'''
Created on 30.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from google.appengine.api import memcache

class FlushHandler(Handler):
    def get(self):
        memcache.flush_all()
        self.redirect('/blog')