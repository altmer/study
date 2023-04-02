'''
Created on 03.05.2012

@author: Ikari
'''

import webapp2
import os
import jinja2
from google.appengine.ext import db

template_dir = os.path.join(os.path.dirname(__file__),'..', 'templates')
jinja_env = jinja2.Environment(loader = jinja2.FileSystemLoader(template_dir), autoescape = False)

class Handler(webapp2.RequestHandler):
    def set_cookie(self,user):
        cookie = '%s|%s' % ( user.key().id(), user.hashpw )
        self.response.headers.add_header('Set-Cookie', 'ikariblog_user=%s; Path=/' % str(cookie) )

    def clear_cookie(self):
        self.response.headers.add_header('Set-Cookie', 'ikariblog_user=; Path=/')

    def write(self, *a, **kw):
        self.response.out.write(*a, **kw)
    
    def render_str(self, template, **params):
        t = jinja_env.get_template(template)
        return t.render(params)
    
    def render(self, template, **kw):
        self.write(self.render_str(template, **kw))

    def get_user(self):
        cookie = self.request.cookies.get('ikariblog_user')
        
        if not cookie or cookie =='':
            return None

        if cookie:        
            user_id,hashpw = cookie.split('|')
        
            key = db.Key.from_path('User', int(user_id) )        
            user = db.get(key)
            
            if user.hashpw == hashpw:
                return user
        return None
