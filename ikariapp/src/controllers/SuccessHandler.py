'''
Created on 27.04.2012

@author: Ikari
'''
import webapp2

class SuccessHandler(webapp2.RequestHandler):
    def get(self):
        self.response.out.write("Thanks")