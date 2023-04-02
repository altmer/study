'''
Created on 27.04.2012

@author: Ikari
'''
from controllers.utils import escape
from controllers.validators import valid_username, valid_password, valid_email
from model.User import User
from controllers.hashing import make_hash_salt
from controllers.AbstractHandler import Handler
from google.appengine.ext import db

class SignupHandler (Handler):
    def render_form(self, username='',
                         password='',
                         verify='',
                         email='',
                         username_error='',
                         password_error='',
                         verify_error='',
                         email_error=''):
        
        self.render('signup.html', 
                    username = escape(username), 
                    password = escape(password), 
                    verify = escape(verify),
                    email = escape(email),
                    username_error = username_error,
                    password_error = password_error,
                    verify_error = verify_error,
                    email_error = email_error)
        
    def get(self):
        self.render_form()
    
    def post(self):
        username = self.request.get('username')
        password = self.request.get('password')
        verify = self.request.get('verify')
        email = self.request.get('email')

        username_error = ""
        password_error = ""
        verify_error = ""
        email_error = ""
        
        users = db.GqlQuery("select * from User where name='%s'" % escape(username) )
        
        if users.count(1):
            username_error = "User with such name already exists"

        if not valid_username(username):
            username_error = "That's not valid username"
            
        if not valid_password(password):
            password_error = "That's not valid password"
        
        if password != verify:
            verify_error = "Your passwords didn't match"
            
        if email and not valid_email(email):
            email_error = "That's not valid email"

        if username_error or password_error or verify_error or email_error:
            self.render_form(username, password, verify, email, username_error, password_error, verify_error, email_error)            
        else:
            hash_salt = make_hash_salt(username, password)
            user = User(name = username, email = email, hashpw = hash_salt[0], salt = hash_salt[1])
            user.put()
            
            cookie = '%s|%s' % ( user.key().id(), user.hashpw )
            self.response.headers.add_header('Set-Cookie', 'ikariblog_user=%s; Path=/' % cookie)
            self.redirect("/blog/welcome")

