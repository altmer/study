'''
Created on 27.04.2012

@author: Ikari
'''
import webapp2
from views.vars import rot13form
from controllers.utils import escape
import string

class Rot13Handler(webapp2.RequestHandler):
    
    def write_form(self, result = ""):
        self.response.out.write (rot13form % {"result" : escape(result)} )
    
    def get(self):
        self.write_form()
        
    def post(self):
        alphabet = string.lowercase
        
        inp = self.request.get('text')
        res = ''
        
        for i in range(0, len(inp)):
            letter = inp[i]
            
            if not letter.isalpha():
                res = res + letter
                continue
            
            up = letter.isupper()
            
            letter = letter.lower()
            
            ind = alphabet.find(letter)
            newletter = alphabet[(ind + 13) % len(alphabet)]
            
            res = res + (newletter.upper() if up else newletter)
            
        self.write_form(res)