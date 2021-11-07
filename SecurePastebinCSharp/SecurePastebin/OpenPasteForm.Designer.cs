namespace SecurePastebin
{
    partial class OpenPasteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnterPasteIDLabel = new System.Windows.Forms.Label();
            this.PasteIDBox = new System.Windows.Forms.TextBox();
            this.FindPasteButton = new System.Windows.Forms.Button();
            this.CancelFindButton = new System.Windows.Forms.Button();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.EnterPasswordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EnterPasteIDLabel
            // 
            this.EnterPasteIDLabel.AutoSize = true;
            this.EnterPasteIDLabel.Location = new System.Drawing.Point(13, 13);
            this.EnterPasteIDLabel.Name = "EnterPasteIDLabel";
            this.EnterPasteIDLabel.Size = new System.Drawing.Size(271, 13);
            this.EnterPasteIDLabel.TabIndex = 0;
            this.EnterPasteIDLabel.Text = "Enter the unique identifier of the paste you want to open";
            // 
            // PasteIDBox
            // 
            this.PasteIDBox.Location = new System.Drawing.Point(16, 29);
            this.PasteIDBox.MaxLength = 24;
            this.PasteIDBox.Name = "PasteIDBox";
            this.PasteIDBox.Size = new System.Drawing.Size(180, 20);
            this.PasteIDBox.TabIndex = 1;
            // 
            // FindPasteButton
            // 
            this.FindPasteButton.Location = new System.Drawing.Point(16, 115);
            this.FindPasteButton.Name = "FindPasteButton";
            this.FindPasteButton.Size = new System.Drawing.Size(75, 23);
            this.FindPasteButton.TabIndex = 3;
            this.FindPasteButton.Text = "Find Paste";
            this.FindPasteButton.UseVisualStyleBackColor = true;
            this.FindPasteButton.Click += new System.EventHandler(this.FindPasteButton_Click);
            // 
            // CancelFindButton
            // 
            this.CancelFindButton.Location = new System.Drawing.Point(97, 115);
            this.CancelFindButton.Name = "CancelFindButton";
            this.CancelFindButton.Size = new System.Drawing.Size(75, 23);
            this.CancelFindButton.TabIndex = 4;
            this.CancelFindButton.Text = "Cancel";
            this.CancelFindButton.UseVisualStyleBackColor = true;
            this.CancelFindButton.Click += new System.EventHandler(this.CancelFindButton_Click);
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(16, 79);
            this.PasswordBox.MaxLength = 255;
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(250, 20);
            this.PasswordBox.TabIndex = 2;
            // 
            // EnterPasswordLabel
            // 
            this.EnterPasswordLabel.AutoSize = true;
            this.EnterPasswordLabel.Location = new System.Drawing.Point(17, 60);
            this.EnterPasswordLabel.Name = "EnterPasswordLabel";
            this.EnterPasswordLabel.Size = new System.Drawing.Size(159, 13);
            this.EnterPasswordLabel.TabIndex = 5;
            this.EnterPasswordLabel.Text = "Enter password to decrypt paste";
            // 
            // OpenPasteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 150);
            this.Controls.Add(this.EnterPasswordLabel);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.CancelFindButton);
            this.Controls.Add(this.FindPasteButton);
            this.Controls.Add(this.PasteIDBox);
            this.Controls.Add(this.EnterPasteIDLabel);
            this.Name = "OpenPasteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Pastebin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EnterPasteIDLabel;
        private System.Windows.Forms.TextBox PasteIDBox;
        private System.Windows.Forms.Button FindPasteButton;
        private System.Windows.Forms.Button CancelFindButton;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label EnterPasswordLabel;
    }
}