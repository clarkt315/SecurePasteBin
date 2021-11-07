using System;
using System.Windows.Forms;

namespace SecurePastebin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CreatePasteButon_Click(object sender, EventArgs e)
        {
            AddOrEditPasteForm fm = new AddOrEditPasteForm();
            fm.ShowDialog(this);
        }

        private void OpenPasteButton_Click(object sender, EventArgs e)
        {
            OpenPasteForm fm = new OpenPasteForm();
            fm.ShowDialog(this);
        }

        private void DeletePasteButton_Click(object sender, EventArgs e)
        {
            DeletePasteForm fm = new DeletePasteForm();
            fm.ShowDialog(this);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
