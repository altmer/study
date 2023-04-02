'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from google.appengine.ext import db
import logging
from model.Page import Page

class WikiHandler(Handler):
    def get(self,page_name):
        user = self.get_user()

        v = self.request.get('v')
        if v and v.isdigit():
            v = int(v)
        else:
            v = 1
        
        edit_link = '/wiki/_edit' + page_name
        history_link = '/wiki/_history' + page_name
        
        page = Page.get_page_by_name(page_name)
        
        if not page:
            logging.error('redirecting')
            self.redirect(edit_link)
            return
        
        content = db.get(page.editions[-v]).content
        self.render('wiki.html', content = content, logged = user, edit_page = edit_link, history_link = history_link)
