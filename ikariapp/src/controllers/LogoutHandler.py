'''
Created on 27.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler

class LogoutHandler(Handler):
    def get(self):
        self.response.headers.add_header('Set-Cookie', 'ikariblog_user=; Path=/')
        self.redirect("/blog/signup")
        
