using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    public partial class OpenPasteForm : Form
    {
        public OpenPasteForm()
        {
            InitializeComponent();
        }

        private void FindPasteButton_Click(object sender, EventArgs e)
        {
            string keySaltString, aesIVString, cipherTextString, macTagString;
            if (String.IsNullOrEmpty(PasteIDBox.Text))
            {
                MessageBox.Show("Please enter a paste identifier.", "Missing paste identifier!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("Please enter a password.", "Missing password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Form validation suceeded...

            // Call web uri to get the paste data.
            WebServer.GetPaste(PasteIDBox.Text, out keySaltString, out aesIVString, out cipherTextString, out macTagString);
         
            // If all 4 output paramters are not null and not empty, then assume retrieval success.
            if (!String.IsNullOrEmpty(keySaltString) && !String.IsNullOrEmpty(aesIVString) && !String.IsNullOrEmpty(cipherTextString) && !String.IsNullOrEmpty(macTagString))
            {
                byte[] keySalt = Convert.FromBase64String(keySaltString);
                byte[] aesIV = Convert.FromBase64String(aesIVString);
                byte[] cipherText = Convert.FromBase64String(cipherTextString);
                byte[] macTag = Convert.FromBase64String(macTagString);
                // Call decyrption function.
                string plainText = Encryption.AuthenticateThenDecrypt(cipherText, PasswordBox.Text, keySalt, aesIV, macTag);
                
                // If successful decryption, open the main paste edit form.
                if (!String.IsNullOrEmpty(plainText))
                {
                    AddOrEditPasteForm fr = new AddOrEditPasteForm(PasteIDBox.Text, plainText);
                    fr.ShowDialog(this);
                    this.Close();
                }
            }
        }

        private void CancelFindButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
