'''
Created on 03.05.2012

@author: Ikari
'''
from controllers.AbstractHandler import Handler
from google.appengine.ext import db
from model.Page import Page
from controllers.utils import escape

class WikiHistoryHandler(Handler):
    def get(self,page_name):
        user = self.get_user()
        if not user:
            self.redirect('/wiki/signup')
            return
        
        edit_link = '/wiki/_edit' + page_name
        view_link = '/wiki' + page_name
        
        page = Page.get_page_by_name(page_name)
        
        if not page:
            self.redirect(edit_link)
            return
        
        editions = []
        eds = list(page.editions)
        eds.reverse()
        num = 1
        for edition_key in eds:
            edition = db.get(edition_key)
            editions.append( (edition.created.strftime('%d %b %Y %H:%M:%S'), escape(edition.content), view_link + '?v=' +  str(num), edit_link + '?v=' +  str(num) ) )
            num+=1
        
        self.render('wiki_history.html', editions = editions, view_page = view_link, edit_page = edit_link)
