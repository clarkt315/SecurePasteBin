using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SecurePastebin
{
    internal static class Encryption
    {
        // Size of master key matches size of aes key and hmac key.
        private const int masterKeySizeBytes = 32;
        // Most sources agree 32 bytes is long enough for a salt.
        private const int defaultSaltSizeBytes = 32;
        // Required size for AES IV...cannot be changed.
        private const int aesIVSizeBytes = 16;
        // Set to take approximately 1 second on my 2014 mid-range laptop.
        private const int keyDerivationIterations = 100000;

        // Take in two arrays and return an array that joins array1 | array2.
        private static byte[] CombineArrays(byte[] array1, byte[] array2)
        {
            var combinedArray = new byte[array1.Length + array2.Length];
            array1.CopyTo(combinedArray, 0);
            array2.CopyTo(combinedArray, array1.Length);
            return combinedArray;
        }

        // This is called to generate a random byte array.  An ouput size may be specified.
        // If not specified, output size defaults to defaultSaltSizeBytes.
        public static byte[] GenerateSalt(int saltSizeBytes = defaultSaltSizeBytes)
        {
            using (var randomGenerator = new RNGCryptoServiceProvider())
            {
                var salt = new byte[saltSizeBytes];
                randomGenerator.GetBytes(salt);
                return salt;
            }
        }

        // Take in password and salt and derive the aes key and the hmac key.
        private static void DeriveKeys(string password, byte[] salt, out byte[] aesKey, out byte[] hmacKey)
        {
            // Derive master key using PBKDF2 (slow derivation process).
            byte[] masterKey;
            using (var derivedKey = new Rfc2898DeriveBytes(password, salt, keyDerivationIterations))
            {
                masterKey = derivedKey.GetBytes(masterKeySizeBytes);
            }
            // Derive aes key and hmac key from master key.
            // See HKDF-Expand definition in section 2.3 of https://tools.ietf.org/html/rfc5869
            var aesKeyExpansionByte = new byte[] { 0x01 };
            var hmacKeyExpansionByte = new byte[] { 0x02 };
            aesKey = CalculateMacTag(masterKey, aesKeyExpansionByte);
            hmacKey = CalculateMacTag(masterKey, CombineArrays(aesKey, hmacKeyExpansionByte));
        }

        // Take in hmacKey and message and return mac tag.
        private static byte[] CalculateMacTag(byte[] hmacKey, byte[] message)
        {
            using (var hmac = new HMACSHA256(hmacKey))
            {
                return hmac.ComputeHash(message);
            }
        }

        // Take in plaintext and password. Populate output parameters keySalt, aesIV, cipherText, and macTag.
        public static void EncryptThenAuthenicate(string plainText, string password, out byte[] keySalt, out byte[] aesIV, out byte[] cipherText, out byte[] macTag)
        {
            // Default output params to null
            keySalt = null;
            aesIV = null;
            cipherText = null;
            macTag = null;
            try
            {
                // Generate a new key salt each time we encrypt.  Use default salt size.
                keySalt = GenerateSalt();
                // Generate a new aes IV each time we encrypt.  Must use required IV size.
                aesIV = GenerateSalt(aesIVSizeBytes);

                // Populate aesKey and hmacKey via key derivation function.
                byte[] aesKey, hmacKey;
                DeriveKeys(password, keySalt, out aesKey, out hmacKey);

                // Set up AES encryption provider object.
                using (var aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = aesKey;
                    aesAlg.IV = aesIV;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Create an encrytor to perform the stream transform.
                    var encryptor = aesAlg.CreateEncryptor(aesKey, aesIV);

                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                // Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            cipherText = msEncrypt.ToArray();
                        }
                    }
                }
                // Calculate mac tag.
                macTag = CalculateMacTag(hmacKey, CombineArrays(aesIV, cipherText));
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Encryption failed. {0}", ex.Message), "Encryption Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Takes in cipherText, password, key salt, aes IV, and mac tag.
        // First, authenticates aesIV||cipherText using mac tag.
        // Then, if that succeeds, decrypts and returns plainText.
        public static string AuthenticateThenDecrypt(byte[] cipherText, string password, byte[] keySalt, byte[] aesIV, byte[] macTag)
        {
            try
            {
                // Populate aesKey and hmacKey via key derivation function.
                byte[] aesKey, hmacKey;
                DeriveKeys(password, keySalt, out aesKey, out hmacKey);

                // Calculate mac tag using hmacKey on aesIV||cipherText
                var calcMacTag = CalculateMacTag(hmacKey, CombineArrays(aesIV, cipherText));

                // Compare to see if stored mac tag matches calculated mac tag.
                // Use constant time comparison to prevent timing attacks.
                var mismatch = 0;
                for (var i = 0; i < macTag.Length; i++)
                    mismatch |= macTag[i] ^ calcMacTag[i];

                // If message doesn't authenticate, return error and exit routine.
                if (mismatch != 0)
                    throw new Exception("Incorrect password or corrupted data.");

                // Authentication suceeded, now decrypt...

                // Set up AES encryption provider object.
                using (var aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = aesKey;
                    aesAlg.IV = aesIV;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Create a decrytor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesKey, aesIV);

                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream and return as string.
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Decryption failed. {0}", ex.Message), "Decryption Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
    }
}

