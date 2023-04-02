'''
Created on 26.04.2012

@author: Ikari
'''
import re

months = ['January',
          'February',
          'March',
          'April',
          'May',
          'June',
          'July',
          'August',
          'September',
          'October',
          'November',
          'December']
          
def valid_month(month):
    month = month.capitalize()
    return month if month in months else None

def valid_day(day):
    if day and day.isdigit():
        day = int(day)
        if day >= 1 and day <= 31:
            return day
    return None

def valid_year(year):
    if year and year.isdigit():
        year = int(year)
        if year >= 1900 and year <= 2020:
            return year
    return None

def valid_username(username):
    return re.match( r"^[a-zA-Z0-9_-]{3,20}$", username)

def valid_password(password):
    return re.match( r"^.{3,20}$", password)

def valid_email(email):
    return re.match( r"^[\S]+@[\S]+\.[\S]+$", email)