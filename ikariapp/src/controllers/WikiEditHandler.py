'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from model.PageEdition import PageEdition
from google.appengine.ext import db
from model.Page import Page
import logging
from controllers.utils import escape

class WikiEditHandler(Handler):
    def get(self, page_name):
        user = self.get_user()
        v = self.request.get('v')
        if v and v.isdigit():
            v = int(v)
        else:
            v = 1
        if not user:
            self.redirect('/wiki/signup')
            return
        
        page = Page.get_page_by_name(page_name)
        
        content = ''
        if page:
            content = db.get(page.editions[-v]).content
        
        self.render('wiki_edit.html', content = escape(content) )

    def post(self, page_name):
        content = self.request.get('content')
        user = self.get_user()
        if not user:
            self.redirect('/wiki/signup')
            return
        
        page = Page.get_page_by_name(page_name)
        
        if not page:
            page = Page(name = page_name, editions = [])
            
        page_edition = PageEdition(content = content, author = user)
        page_edition.put()
        
        page.editions.append(page_edition.key())
        page.put()
        
        self.redirect('/wiki'+page_name)
        