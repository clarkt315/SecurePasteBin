#!/usr/bin/python
# -*- coding: utf-8 -*-

## Python libraries
import Tkinter
import tkMessageBox
import base64
## Application files
import encryption
import webserver

## Constants
PASTE_ID_LENGTH_BYTES = 16

## Create the GUI window and set to grid layout
root = Tkinter.Tk()
root.grid()

## When user clicks exit, quit app.
def exit_app():
    root.quit()

## Need to call this to clear existing widgets when we switch what form
## the user should see. 
def clear_form():
    for widget in root.grid_slaves():
        widget.grid_forget() 


##########################      Forms     ###############################

## This form loads when the app is launched.  It gives the user 4 choices
## of actions to perform.
def main_form():
    clear_form()
    action_label = Tkinter.Label(text="What do you want to do?")
    action_label.grid(column=0,row=0, columnspan=4, pady=4)
    create_paste_button = Tkinter.Button(text=u"Create New Paste",
        command=lambda: add_edit_paste_form("insert"))
    create_paste_button.grid(column=0,row=1)
    open_paste_button = Tkinter.Button(text=u"Open Existing Paste",
        command=open_paste_form)
    open_paste_button.grid(column=1,row=1)
    delete_paste_button = Tkinter.Button(text=u"Delete Existing Paste",
        command=delete_paste_form)
    delete_paste_button.grid(column=2,row=1)
    exit_button = Tkinter.Button(text=u"Exit",
        command=exit_app)
    exit_button.grid(column=3,row=1)

## This form functions both for adding pastes and editing pastes.
## The action parameter determines how it should function.
## The paste_id_string and plaintext parameters are only relevant
## when using the form to edit a paste.
def add_edit_paste_form(action, paste_id_string = None, plaintext = None):
    clear_form()
    paste_id_label = Tkinter.Label(text="Paste unique identifier")
    paste_id_label.grid(column=0,row=0,sticky="W")
    paste_id_box = Tkinter.Entry(width=30)
    paste_id_box.grid(column=0,row=1,sticky="W")
    paste_label = Tkinter.Label(text="Enter your paste text below." +
                                " Use CTRL-C to copy and CTRL-V to paste.")
    paste_label.grid(column=0,row=2,sticky="W")
    paste_box = Tkinter.Text(height=30, width=100, background="white")
    paste_box.grid(column=0,row=3,sticky="W")

    ## Scrollbars are separate widgets in python and it takes a lot of code
    ## to set them up.
    yscrollbar = Tkinter.Scrollbar()
    yscrollbar.grid(column=1,row=3,sticky="NS")
    yscrollbar.config(command=paste_box.yview)
    xscrollbar = Tkinter.Scrollbar()
    xscrollbar.grid(column=0, row=4, sticky="EW")
    xscrollbar.config(command=paste_box.xview, orient="horizontal")
    paste_box.config(xscrollcommand=xscrollbar.set, wrap="none")    
    paste_box.config(yscrollcommand=yscrollbar.set)

    password_label = Tkinter.Label(text="Enter your password twice")
    password_label.grid(column=0,row=5,sticky="W")
    password1_box = Tkinter.Entry(width=30, background="white", show="*")
    password1_box.grid(column=0,row=6,sticky="W")
    password2_box = Tkinter.Entry(width=30, background="white", show="*")
    password2_box.grid(column=0,row=7,sticky="W")

    ## The strip() functions removes whitespace.  We use this on the paste id
    ## and password fields.  The encode() function on the paste text is
    ## necessary in case someone includes unicode characters in the paste.
    submit_button = Tkinter.Button(text=u"Submit",
        command=lambda: save_paste(action,
            paste_id_box.get().strip(),
            paste_box.get("1.0","end").encode('utf-8').strip(),
            password1_box.get().strip(),
            password2_box.get().strip()))
    submit_button.grid(column=0,row=8,sticky="W")

    cancel_button = Tkinter.Button(text=u"Cancel",
                                   command=main_form)
    cancel_button.grid(column=0,row=8,sticky="E")

    ## If the form was called in insert mode, generate a new paste identifier
    ## and put its value into the paste id box.
    if (action == "insert"):
        paste_id = encryption.generate_salt(PASTE_ID_LENGTH_BYTES)
        paste_id_string = base64.b64encode(paste_id)
        paste_id_box.insert(0, paste_id_string)
        paste_id_box.config(state="readonly")
    ## If the form was called in update mode, set the paste id and plaintext
    ## to the values retrieved from the database.
    if (action == "update"):
        paste_id_box.insert(0, paste_id_string)
        paste_id_box.config(state="readonly")
        paste_box.insert(1.0, plaintext)

## This form lets a user open an existing paste by entering the paste
## identifier and the password to decrypt the paste.
def open_paste_form():
    clear_form()
    paste_id_label = Tkinter.Label(text="Enter the unique identifier of " +
                                   "the paste you want to open")
    paste_id_label.grid(column=0,row=0,sticky="W")
    paste_id_box = Tkinter.Entry(width=30, background="white")
    paste_id_box.grid(column=0,row=1,sticky="W")
    password_label = Tkinter.Label(text="Enter password to decrypt paste")
    password_label.grid(column=0,row=2,sticky="W")
    password_box = Tkinter.Entry(width=30, background="white",show="*")
    password_box.grid(column=0,row=3,sticky="W")
    open_button = Tkinter.Button(text=u"Find Paste",
        command=lambda: get_paste(paste_id_box.get().strip(),
        password_box.get().strip()))
    open_button.grid(column=0,row=4,sticky="W")
    cancel_button = Tkinter.Button(text=u"Cancel", command=main_form)
    cancel_button.grid(column=0,row=4,sticky="E")

## This form lets a user enter a paste identifier to delete that paste
## from the database.
def delete_paste_form():
    clear_form()
    paste_id_label = Tkinter.Label(text="Enter the unique identifier of " +
                                   "the paste you want to delete")
    paste_id_label.grid(column=0,row=0,sticky="W")
    paste_id_box = Tkinter.Entry(width=30, background="white")
    paste_id_box.grid(column=0,row=1,sticky="W")
    delete_button = Tkinter.Button(text=u"Delete Paste",
        command=lambda: delete_paste(paste_id_box.get().strip()))
    delete_button.grid(column=0,row=2,sticky="W")
    cancel_button = Tkinter.Button(text=u"Cancel", command=main_form)
    cancel_button.grid(column=0,row=2,sticky="E")

## This form displays after a user creates a paste.  It tells the user the
## paste was created and shows the paste identifier so the user can make
## a note of it.
def submit_confirmation_form(paste_id_string):
    clear_form()
    paste_submitted_label = Tkinter.Label(
        text="Paste created. Save the unique identifier below. You will " +
             "need to use it to retrieve your paste later.")
    paste_submitted_label.grid(column=0,row=0)
    paste_id_box = Tkinter.Entry(width=30)
    paste_id_box.grid(column=0,row=1)
    paste_id_box.insert(0, paste_id_string)
    paste_id_box.config(state="readonly")
    copy_label = Tkinter.Label(text="Highlight and press CTRL-C to copy.")
    copy_label.grid(column=0,row=2)
    close_button = Tkinter.Button(text=u"Close",command=main_form)
    close_button.grid(column=0,row=3)

## This is a very simple form that displays a given message and gives the
## user an OK button to close the form.
def message_form(message):
    clear_form()
    message_label = Tkinter.Label(text=message)
    message_label.grid(column=0,row=0, ipadx=10, ipady=10)
    close_button = Tkinter.Button(text=u"OK", command=main_form)
    close_button.grid(column=0,row=1, ipadx=10, ipady=10)



#######################    Application logic   ########################

## This function takes in a paste id and a password.  It tries to find
## that paste in the database and use the password to decrypt it.
def get_paste(paste_id_string, password):

    ## Error checking:
    if not paste_id_string:
        tkMessageBox.showerror("Missing paste identifier!",
                               "Please enter a paste identifier.")
        return
    if not password:
        tkMessageBox.showerror("Missing password!", "Please enter a" +
                               " password.")
        return
    
    ## Call the webserver get paste routine to find the paste.  Pass in a
    ## paste id and get back the key salt, aes iv, cipher text, and mac tag.
    key_salt_string, aes_iv_string, ciphertext_string, mac_tag_string = (
        webserver.get_paste(paste_id_string))

    ## If all of the returned fields have values...
    if (key_salt_string and aes_iv_string and ciphertext_string and
        mac_tag_string):
        
        ## Convert the base64-encoded values back into byte arrays
        key_salt_bytes = base64.b64decode(key_salt_string)
        aes_iv_bytes = base64.b64decode(aes_iv_string)
        ciphertext_bytes = base64.b64decode(ciphertext_string)
        mac_tag_bytes = base64.b64decode(mac_tag_string)

        ## Try to decrypt the paste
        plaintext = encryption.authenticate_then_decrypt(ciphertext_bytes,
            password, key_salt_bytes, aes_iv_bytes, mac_tag_bytes)

        ## If decryption is successful...
        if plaintext:
            ## Display the paste in the add_edit_paste_form
            add_edit_paste_form("update", paste_id_string, plaintext)

## This function saves a paste to the database.  It takes in an action
## (insert or update) a paste id, the text of the paste,
## and two password entries (which need to match).
def save_paste(action, paste_id_string, plaintext, password1, password2):

    ## Error checking:
    if not plaintext:
        tkMessageBox.showerror("Empty paste!", "Please type some text in" +
                               " the paste box.")
        return
    if not password1 or not password2:
        tkMessageBox.showerror("Missing password!", "Please enter your" +
                               " password in both boxes.")
        return
    if password1 != password2:
        tkMessageBox.showerror("Password mismatch!", "Your passwords do" +
                               " not match.")
        return

    ## Encrypt the plaintext with the supplied password.
    ## The encryption routine returns the values that need to be stored
    ## in the database (key salt, aes iv, ciphertext, mac tag)
    key_salt, aes_iv, ciphertext, mac_tag = (
        encryption.encrypt_then_authenticate(plaintext, password1))

    ## Make sure all returned values are non-empty.
    if not key_salt or not aes_iv or not ciphertext or not mac_tag:
        tkMessageBox.showerror("Encryption failed!", "Encryption failed.")
        return
    
    ## Base64 encode the returned values.
    key_salt_string = base64.b64encode(key_salt)
    aes_iv_string = base64.b64encode(aes_iv)
    ciphertext_string = base64.b64encode(ciphertext)
    mac_tag_string = base64.b64encode(mac_tag)

    ## Call the webserver save paste routine to save to the database
    result = webserver.save_paste(action, paste_id_string, key_salt_string,
        aes_iv_string, ciphertext_string, mac_tag_string)

    ## If result is true, then the save was successful, so go on..
    if result:
        ## If we have inserted a new paste, show submit_confirmation_form
        if (action == "insert"):
            submit_confirmation_form(paste_id_string)
        ## If we have updated an existing paste, just tell the user the
        ## paste was updated.
        if (action == "update"):
            message_form("Paste updated.")        

## This function deletes a paste from the database.
def delete_paste(paste_id_string):

    ## Error checking:
    if not paste_id_string:
        tkMessageBox.showerror("Missing paste identifier!",
                               "Please enter a paste identifier.")
        return

    ## Call the webserver delete paste routine to delete the paste.
    result = webserver.delete_paste(paste_id_string)

    ## If the result was successful, then tell user paste was deleted.
    if result:
        message_form("Paste deleted.")

## This code at the very end is what actually launches the main form
## when the program starts.
main_form()
root.title('Secure Pastebin')
root.mainloop()



