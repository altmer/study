'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
import hmac

SECRET = 'imsosecret'

class TempCookieHandler(Handler):
    def hash_str(self, s):
        return hmac.new(SECRET, s).hexdigest()
    
    def make_secure_val(self, s):
        return '%s|%s' % (s, self.hash_str(s) ) 
    
    def check_secure_val(self,h):
        s = h.split('|')[0]
        return s if self.make_secure_val(s) == h else None
    
    def get(self):
        self.response.headers['Content-Type'] = 'text/plain'
        
        visits = 0
        visits_cookie_val = self.request.cookies.get('visits', '0')
        
        if visits_cookie_val:
            cookie_val = self.check_secure_val(visits_cookie_val)
            if cookie_val:
                visits = int(cookie_val)
        
        visits += 1
        new_cookie_val = self.make_secure_val( str(visits) )
            
        self.response.headers.add_header('Set-Cookie', 'visits=%s' % new_cookie_val)
        if visits > 10000:
            self.write("Thanks!")
        else:
            self.write("You've been here %s times" % visits)
