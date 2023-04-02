'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from model.BlogPost import BlogPost

class NewPostHandler(Handler):
    def render_newpost(self, subject="", content="",error=""):        
        self.render('newpost.html',subject=subject,content=content,error=error)
        
    def get(self):
        self.render_newpost()
    
    def post(self):
        subject = self.request.get('subject')
        content = self.request.get('content')
        
        if subject and content:
            post = BlogPost(subject = subject, content = content)
            post.put()
            
            self.redirect("/blog/%s" % str( post.key().id() ) )
        else:
            error = 'subject and content are required'
            self.render_newpost(subject = subject, content = content, error = error)            
