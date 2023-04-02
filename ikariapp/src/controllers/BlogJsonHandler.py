'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from model.BlogPost import BlogPost
import json

class BlogJsonHandler(Handler):
    def get(self):
        posts = BlogPost.get_all_posts()
        
        res = []
        for post in posts:
            post_json = post.to_json()
            res.append(post_json)                                    
        
        self.response.headers.add_header('Content-Type', 'application/json; charset=utf-8')
        self.write(json.dumps(res))