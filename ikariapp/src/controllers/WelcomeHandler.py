'''
Created on 27.05.2012

@author: Ikari
'''
from google.appengine.ext import db
from controllers.AbstractHandler import Handler

class WelcomeHandler(Handler):
    
    def get(self):        
        cookie = self.request.cookies.get('ikariblog_user')
        
        if not cookie or cookie =='':
            self.redirect('/blog/signup')

        if cookie:        
            user_id,hashpw = cookie.split('|')
        
            key = db.Key.from_path('User', int(user_id) )        
            user = db.get(key)
            
            if user.hashpw == hashpw:
                self.response.out.write("<h1>Welcome, %s</h1>" % user.name)
            
        else:
            self.redirect('/blog/signup')        
