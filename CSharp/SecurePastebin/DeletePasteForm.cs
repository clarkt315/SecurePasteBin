using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    public partial class DeletePasteForm : Form
    {
        public DeletePasteForm()
        {
            InitializeComponent();
        }

        private void DeletePasteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PasteIDBox.Text))
            {
                MessageBox.Show("Please enter a paste identifier.", "Missing paste identifier!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Form validation suceeded...

            // Call web uri to delete the paste.
            bool result = WebServer.DeletePaste(PasteIDBox.Text);
            if (result == true)
            {
                MessageBox.Show("Paste deleted.","Paste deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
 
        private void CancelDeleteButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
