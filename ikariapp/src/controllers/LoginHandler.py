'''
Created on 27.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from controllers.utils import escape
from google.appengine.ext import db
from controllers.hashing import verify_hash

class LoginHandler(Handler):
    def render_form(self, username='',password='',error=''):
        self.render('login.html', 
                    username = escape(username), 
                    password = escape(password), 
                    error = error)
    
    def get(self):
        self.render_form()
    
    def post(self):
        username = self.request.get('username')
        password = self.request.get('password')
        
        users = db.GqlQuery("select * from User where name='%s'" % escape(username) )
        
        error = ''
        
        if users.count(1):
            user = users[0]
            if not verify_hash(username, password, user.hashpw, user.salt):
                error = 'Invalid password'
        else:
            error='Invalid login'
            
        if error:
            self.render_form(username, password, error)
        else:
            cookie = '%s|%s' % ( user.key().id(), user.hashpw )
            self.response.headers.add_header('Set-Cookie', 'ikariblog_user=%s; Path=/' % str(cookie) )
            self.redirect("/blog/welcome")
            