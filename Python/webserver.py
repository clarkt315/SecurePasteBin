import requests
import re
import tkMessageBox


WEBSITE = "https://xxxxxx_insert_your_site_here_xxxxxxx.com/"

## These are the maximum allowed lengths for each field stored in the
## database.  Values are larger than the size of the respective byte
## arrays because the data stored in the DB is base64 encoded.
KEY_SALT_LENGTH = 44
AES_IV_LENGTH = 24
CIPHERTEXT_LENGTH = 65535
MAC_TAG_LENGTH = 44

## Call web uri to get data for a paste id.
def get_paste(paste_id):
    uri = WEBSITE + "getpaste.php"
    post_data = {'pasteID':paste_id}
    response = requests.post(uri, post_data)
    response_text = response.text.strip()
    
    ## If blank response then specified paste id was not found.
    if not response_text:
        tkMessageBox.showerror("Paste not found!", "Paste unique identifier" +
                               " not found.")
        return None, None, None, None

    ## Make sure total length of data returned by the server is not too long
    elif (len(response_text) > KEY_SALT_LENGTH + AES_IV_LENGTH +
          CIPHERTEXT_LENGTH + MAC_TAG_LENGTH):
        tkMessageBox.showerror("Web response error!", "Message from server" +
                               " too long.")
        return None, None, None, None

    ## Check to see if response is in expected format, which is:
    ## keySalt|aesIV|cipherText|macTag
    ## All 4 values are base64 encoded, so the only valid characters
    ## are letters (upper and lower case), numbers and /+=
    pattern = re.compile("^([a-zA-Z0-9/+=]+)[|]([a-zA-Z0-9/+=]+)[|]" +
                     "([a-zA-Z0-9/+=]+)[|]([a-zA-Z0-9/+=]+)$")
    match = pattern.match(response_text)
    if not match:
        tkMessageBox.showerror("Web response error!", "Received invalid" +
                               " data from server.")
        return None, None, None, None

    ## The 4 values returned from the uri call are separated by |
    key_salt, aes_iv, ciphertext, mac_tag = response_text.split('|')

    ## Make sure key_salt is not too long
    if len(key_salt) > KEY_SALT_LENGTH:
        tkMessageBox.showerror("Web response error!", "keySalt is too long.")
        return None, None, None, None

    ## Make sure aes_iv is not too long
    if len(aes_iv) > AES_IV_LENGTH:
        tkMessageBox.showerror("Web response error!", "aesIV is too long.")
        return None, None, None, None

    ## Make sure ciphertext is not too long
    if len(ciphertext) > CIPHERTEXT_LENGTH:
        tkMessageBox.showerror("Web response error!","ciphertext is too long.")
        return None, None, None, None

    ## Make sure mac_tag is not too long
    if len(mac_tag) > MAC_TAG_LENGTH:
        tkMessageBox.showerror("Web response error!", "mac_tag is too long.")
        return None, None, None, None

    ## If we reach here it means that we've passed all validation tests.
    return key_salt, aes_iv, ciphertext, mac_tag

## Call web uri to insert/update a paste in the database.
def save_paste(action, paste_id, key_salt, aes_iv, ciphertext, mac_tag):
    uri = WEBSITE + "savepaste.php"
    post_data = {'action':action, 'pasteID':paste_id, 'keySalt':key_salt,
                 'aesIV':aes_iv, 'cipherText':ciphertext, 'macTag':mac_tag}
    response = requests.post(uri, post_data)
    response_text = response.text.strip()
    if response_text == "1":
        return True
    elif response_text == "0":
        tkMessageBox.showerror("Save error!", "Saved failed. The database" +
                               " reports 0 rows affected.")
        return False
    else:
        tkMessageBox.showerror("Save error!", "Saved failed. Received bad " +
                               "response from web server: %s" % response_text)
        return False
        
## Call web uri to delete a paste from the database.
def delete_paste (paste_id):
    uri = WEBSITE + "deletepaste.php"
    post_data = {'pasteID':paste_id}
    response = requests.post(uri, post_data)
    response_text = response.text.strip()
    if response_text == "1":
        return True
    elif response_text == "0":
        tkMessageBox.showerror("Delete error!", "Paste ID not found.")
        return False
    else:
        tkMessageBox.showerror("Delete error!", "Saved failed. Received bad " +
                               "response from web server: %s" % response_text)
        return False

