'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from model.BlogPost import BlogPost
from google.appengine.api import memcache
import time
import logging

POSTS_KEY = 'all_posts'
POSTS_TIME_KEY = 'all_posts_time'

class BlogHandler(Handler):
    def get(self):
        posts = memcache.get(POSTS_KEY)
        t = memcache.get(POSTS_TIME_KEY)

        if not posts or not t:
            posts = BlogPost.get_all_posts()
            t = time.time()
            memcache.set(POSTS_KEY, posts)
            memcache.set(POSTS_TIME_KEY, t)

        t = int (time.time() - t)
            
        self.render('blog.html', posts = posts, posts_generated = t)
