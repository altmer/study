'''
Created on 26.04.2012

@author: Ikari
'''
from model.Art import Art
from google.appengine.ext import db
from controllers.AbstractHandler import Handler


class MainHandler(Handler):
    def render_front(self, title="",art="",error=""):
        arts = db.GqlQuery("select * from Art order by created desc")
        
        self.render('front.html',title=title,art=art,error=error,arts=arts)
    
    def get(self):
        self.render_front()
        
    def post(self):
        title = self.request.get('title')
        art = self.request.get('art')
        
        if title and art:
            a = Art(title = title, art = art)
            a.put()
            
            self.redirect("/")
        else:
            error = 'title and art are required'
            self.render_front(title, art, error)
        
