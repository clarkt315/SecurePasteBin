using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace SecurePastebin
{
    internal static class WebServer
    {
        private const string website = "https://xxxxxx_insert_your_site_here_xxxxxxx.com/";

        // These are the maximum allowed lengths for each field stored in the database.
        // Values are larger than the size of the respective byte arrays because the
        // data stored in the DB is base64 encoded.
        private const int keySaltLength = 44;
        private const int aesIVLength = 24;
        private const int cipherTextLength = 65535;
        private const int macTagLength = 44;

        // Call web uri to get data for a paste id.
        internal static void GetPaste(string pasteID, out string keySalt, out string aesIV, out string cipherText, out string macTag)
        {
            keySalt = null;
            aesIV = null;
            cipherText = null;
            macTag = null;
            string uri = String.Concat(website, "getpaste.php");
            WebClient webClient = new WebClient();
            NameValueCollection submittedData = new NameValueCollection();
            submittedData["pasteID"] = pasteID;
            try
            {
                byte[] responseBytes = webClient.UploadValues(uri, "POST", submittedData);
                string responseFromServer = Encoding.UTF8.GetString(responseBytes);

                // If blank response then specified paste id was not found.
                if (String.IsNullOrEmpty(responseFromServer))
                {
                    throw new Exception("Paste unique identifer not found.");
                }

                // Make sure that the total length of data returned by the server is not too long
                // (+ 3 is for delimiters)
                if (responseFromServer.Length > keySaltLength + aesIVLength + cipherTextLength + macTagLength + 3)
                {
                    throw new Exception("Response from server is too long.");
                }

                // Check to see if response is in expected format, which is:
                // keySalt|aesIV|cipherText|macTag
                // All 4 values are base64 encoded, so the only valid characters are letters (upper and lower case)
                // numbers and /+=
                Match match = Regex.Match(responseFromServer, "^([a-zA-Z0-9/+=]+)[|]([a-zA-Z0-9/+=]+)[|]([a-zA-Z0-9/+=]+)[|]([a-zA-Z0-9/+=]+)$");
                if (!match.Success)
                {
                    throw new Exception("Received invalid data from server.");
                }

                // Found properly-formatted data...

                // The 4 values returned from the uri call (keySalt, aesIV, cipherText, macTag) are separated by the pipe character.
                String[] returnedValues = responseFromServer.Split('|');

                // Make sure there are 4 elements in the array of strings.
                if (returnedValues.Length != 4)
                {
                    throw new Exception("Received invalid data from server.");
                }

                // Assign elements in array of strings to their variables.
                keySalt = returnedValues[0];
                aesIV = returnedValues[1];
                cipherText = returnedValues[2];
                macTag = returnedValues[3];

                // Make sure keySalt is not too long
                if (keySalt.Length > keySaltLength)
                {
                    throw new Exception("keySalt is too long.");
                }
                // Make sure aesIV is not too long
                if (aesIV.Length > aesIVLength)
                {
                    throw new Exception("aesIV is too long.");
                }
                // Make sure cipherText is not too long
                if (cipherText.Length > cipherTextLength)
                {
                    throw new Exception("cipherText is too long.");
                }
                // Make sure macTag is not too long
                if (macTag.Length > macTagLength)
                {
                    throw new Exception("macTag is too long.");
                }
                // If we reach the end of the function with no errors then we know the data is formatted correctly.
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Get data failed. Error: {0}", ex.Message), "Get data error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                webClient.Dispose();
            }
        }

        // Call web uri to insert/update a paste in the database.
        internal static bool SavePaste(string action, string pasteID, string keySalt, string aesIV, string cipherText, string macTag)
        {
            string uri = String.Concat(website, "savepaste.php");
            WebClient webClient = new WebClient();
            NameValueCollection submittedData = new NameValueCollection();
            submittedData["action"] = action;
            submittedData["pasteID"] = pasteID;
            submittedData["keySalt"] = keySalt;
            submittedData["aesIV"] = aesIV;
            submittedData["cipherText"] = cipherText;
            submittedData["macTag"] = macTag;
            try
            {
                // Upload the paste data.
                byte[] responseBytes = webClient.UploadValues(uri, "POST", submittedData);
                string responseFromServer = Encoding.UTF8.GetString(responseBytes);
                if (responseFromServer.Trim() == "1")
                {
                    return true;
                }
                else if (responseFromServer.Trim() == "0")
                {
                    throw new Exception(String.Format("The database reports 0 rows affected.", pasteID));
                }
                else
                {
                    throw new Exception(String.Format("Received bad response from web server: {0}", responseFromServer));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Save failed. Error: {0}", ex.Message), "Save Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                webClient.Dispose();
            }
        }

        // Call web uri to delete a given paste.
        internal static bool DeletePaste(string pasteID)
        {
            string uri = String.Concat(website, "deletepaste.php");
            WebClient webClient = new WebClient();
            NameValueCollection submittedData = new NameValueCollection();
            submittedData["pasteID"] = pasteID;
            try
            {
                byte[] responseBytes = webClient.UploadValues(uri, "POST", submittedData);
                string responseFromServer = Encoding.UTF8.GetString(responseBytes);
                if (responseFromServer.Trim() == "1")
                {
                    return true;
                }
                else if (responseFromServer.Trim() == "0")
                {
                    throw new Exception(String.Format("Paste identifier not found."));
                }
                else
                {
                    throw new Exception(String.Format("Received bad response from web server: {0}", responseFromServer));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Delete failed. Error: {0}", ex.Message), "Delete Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                webClient.Dispose();
            }
        }

    }
}
