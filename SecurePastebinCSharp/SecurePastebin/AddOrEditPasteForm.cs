using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    public partial class AddOrEditPasteForm : Form
    {

        // Length of unique identifier to assign each new paste
        private const int pasteIDLengthBytes = 16;

        // When form opens, set this to keep track of whether we are doing an insert of a new paste or an
        // update of an existing paste.
        private string action;

        // This constructor is used when we are adding a new paste.
        public AddOrEditPasteForm()
        {
            InitializeComponent();
            // Generate new identifier for new paste.
            byte[] pasteID = Encryption.GenerateSalt(pasteIDLengthBytes);
            // Store as base-64 string.
            PasteIDBox.Text = Convert.ToBase64String(pasteID);
            action = "insert";
        }

        // This constructor is used when we are editing an existing paste.
        public AddOrEditPasteForm(string pasteIDString, string pasteText)
        {
            InitializeComponent();
            PasteIDBox.Text = pasteIDString;
            PasteTextBox.Text = pasteText;
            action = "update";
        }

        // When user clicks "submit" (either to add a new paste or save changes to an existing paste)...
        private void SubmitPasteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PasteTextBox.Text))
            {
                MessageBox.Show("Please type some text in the paste box.", "Empty paste!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(Password1Box.Text) || String.IsNullOrEmpty(Password2Box.Text))
            {
                MessageBox.Show("Please enter your password in both boxes.", "Missing password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Password1Box.Text != Password2Box.Text)
            {
                MessageBox.Show("Your passwords do not match.", "Password mismatch!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Passed form validation...

            // Encrypt paste text and populate keySalt, aesIV, cipherText, and macTag.
            byte[] keySalt, aesIV, cipherText, macTag;
            Encryption.EncryptThenAuthenicate(PasteTextBox.Text, Password1Box.Text, out keySalt, out aesIV, out cipherText, out macTag);

            // If encryption suceeded...
            if (keySalt != null && aesIV != null && cipherText != null && macTag != null)
            {
                // Call web uri to save paste.
                bool result = WebServer.SavePaste(action, PasteIDBox.Text, Convert.ToBase64String(keySalt), Convert.ToBase64String(aesIV), Convert.ToBase64String(cipherText), Convert.ToBase64String(macTag));

                // If web uri call succeeded...
                if (result == true)
                {
                    // If we created a new paste, show paste creation confirmation form.
                    if (action == "insert")
                    {
                        // Show user the Paste ID
                        SubmitConfirmForm fm = new SubmitConfirmForm(PasteIDBox.Text);
                        fm.ShowDialog(this);
                    }
                    // If we have updated an existing paste, tell user the paste was updated.
                    else if (action == "update")
                    {
                        MessageBox.Show("Paste updated.", "Paste updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.Close();
                }
            }
        }

        private void CancelPasteButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
