'''
Created on 26.04.2012

@author: Ikari
'''

form = """
<form method="post">
    What is your birthday?
    <br>
    <label>Month
        <input type="text" name="month" value="%(month)s" />
    </label>

    <label>Day
        <input type="text" name="day" value="%(day)s" />
    </label>

    <label>Year
        <input type="text" name="year" value="%(year)s" />
    </label>
    
    <div style="color : red">%(error)s</div>

    <br>
    <br>

    <input type="submit" />
</form>
"""

rot13form = """
<form method="post">
    <h1>Enter some text to ROT13:</h1>
    <br/>
    
    <textarea name="text">
    %(result)s
    </textarea>

    <br/>
    <br/>

    <input type="submit" />
</form>
"""

login_form = """
"""