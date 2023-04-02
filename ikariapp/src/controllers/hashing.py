'''
Created on 27.05.2012

@author: Ikari
'''
import random
import string
import hashlib

def make_salt():
    return ''.join([random.choice(string.letters) for x in range(0,5)])

def make_hash_salt(name,pw,salt=None):
    if not salt:
        salt = make_salt()
    return (hashlib.sha256(name+pw+salt).hexdigest(), salt)

def verify_hash(name,pw,h,salt):        
    return make_hash_salt(name,pw,salt) == (h,salt)