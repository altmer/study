'''
Created on 27.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler

class WikiLogoutHandler(Handler):
    def get(self):
        self.clear_cookie()
        self.redirect("/wiki/")
        
