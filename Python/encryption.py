import hashlib
from Crypto import Random
from Crypto.Cipher import AES
import hmac
import tkMessageBox

## Constant values

## Size of master key matches size of aes key and hmac key.
MASTER_KEY_SIZE_BYTES = 32
## Most sources agree 32 bytes is long enough for a salt.
DEFAULT_SALT_SIZE_BYTES = 32
## Required block size for AES...cannot be changed.
AES_BLOCK_SIZE_BYTES = 16
## Required size for AES IV...cannot be changed.
AES_IV_SIZE_BYTES = 16
## Set to take approximately 1 second on my 2014 mid-range laptop.
KEY_DERIVATION_ITERATIONS = 100000

## This is called to generate a random byte array.  An output size may
## be specified.  If not specified, output size defaults to
## DEFAULT_SALT_SIZE_BYTES.
def generate_salt(salt_size_bytes = DEFAULT_SALT_SIZE_BYTES):
    return Random.new().read(salt_size_bytes)

## Take in password and salt derive derive the aes key and the hmac key.
def derive_keys(password, salt):
    ## Derive master key using PBKDF2 (slow derivation process).
    master_key = hashlib.pbkdf2_hmac('sha1', password, salt, 
                     KEY_DERIVATION_ITERATIONS, MASTER_KEY_SIZE_BYTES)
    ## Derive aes key and hmac key from master key.
    ## Use HKDF-Expand
    ## https://tools.ietf.org/html/rfc5869
    aes_key_exp_byte = '\x01'
    hmac_key_exp_byte = '\x02'
    aes_key = calculate_mac_tag(master_key, aes_key_exp_byte)
    hmac_key = calculate_mac_tag(master_key, aes_key + hmac_key_exp_byte)
    return aes_key, hmac_key

## Take in hmac key and message and return mac tag.
def calculate_mac_tag(hmac_key, message):
    hmac_function = hmac.new(hmac_key, message, hashlib.sha256)
    return hmac_function.digest()
  
## Take plaintext and password.  Return key salt, aes iv, ciphertext,
## and mac tag.
def encrypt_then_authenticate(plaintext, password):

    ## Generate a new key salt each time we encrypt. Use default salt size.
    key_salt = generate_salt()
    ## Generate a new aes IV each time we encrypt. Must use required IV size.
    aes_iv = generate_salt(AES_IV_SIZE_BYTES)
    ## Populate aes_key and hmac_key via key derivation function.
    aes_key, hmac_key = derive_keys(password, key_salt)
  
    ## In AES CBC mode the plaintext needs have a length that is a multiple
    ## of the AES block size.  First, figure out how many extra bytes we
    ## need to add. 
    padding_length = (AES_BLOCK_SIZE_BYTES - 
                     (len(plaintext) % AES_BLOCK_SIZE_BYTES))
    ## Now, set plaintext_padded equal to the plaintext plus a string
    ## of padding in which the value in each byte is the total length
    ## of padding needed.
    plaintext_padded = plaintext + chr(padding_length) * padding_length
    
    ## Run encryption on padded plaintext to get ciphertext.
    aes_function = AES.new(aes_key, AES.MODE_CBC, aes_iv)  
    ciphertext = aes_function.encrypt(plaintext_padded)

    ## Run hmac to create a message authentication tag (mac).
    mac_tag = calculate_mac_tag(hmac_key, aes_iv + ciphertext) 
    return key_salt, aes_iv, ciphertext, mac_tag

## First, check that encrypted data matches mac tag.  If it does,
## decrypt and return decrypted data.
def authenticate_then_decrypt(ciphertext, password, key_salt, aes_iv, mac_tag):

    ## Populate aes_key and hmac_key via key derivation function.
    aes_key, hmac_key = derive_keys(password, key_salt)
  
    ## Run hmac to create a message authentication tag (mac).
    calculated_mac_tag = calculate_mac_tag(hmac_key, aes_iv + ciphertext) 

    ## Test if calculated mac tag matches expected mac tag.
    ## If it doesn't it means a bad password was entered or
    ## the data is corrupted.  Exit function.
    hmac_matched = hmac.compare_digest(mac_tag, calculated_mac_tag)
    if not hmac_matched:
        tkMessageBox.showerror("Decryption error!", "Incorrect password" +
                               " or corrupted data.")
        return
 
    ## Mac tag check passed, now run decryption routine.
    aes_function = AES.new(aes_key, AES.MODE_CBC, aes_iv)  
    plaintext = aes_function.decrypt(ciphertext)

    ## We need to "unpad" the result (data to be encrypted is padded to be
    ## a length that is a multiple of the AES block size).  After decrypting
    ## we need to get rid of what was added to the end.  This code first
    ## finds the value in the last character of the string, then removes
    ## x last characters where x is the value of the last character in
    ## the string.
    plaintext_unpadded = plaintext[:-ord(plaintext[-1])]
  
    ## Return unpadded plaintext.
    return plaintext_unpadded

