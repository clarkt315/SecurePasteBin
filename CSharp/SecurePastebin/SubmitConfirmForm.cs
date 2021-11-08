using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    public partial class SubmitConfirmForm : Form
    {
        public SubmitConfirmForm(string pasteIDString)
        {
            InitializeComponent();
            PasteIDBox.Text = pasteIDString;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
