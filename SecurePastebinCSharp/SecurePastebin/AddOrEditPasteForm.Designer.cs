namespace SecurePastebin
{
    partial class AddOrEditPasteForm
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
            this.PasteTextBox = new System.Windows.Forms.TextBox();
            this.PasteLabel = new System.Windows.Forms.Label();
            this.Password1Box = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.Password2Box = new System.Windows.Forms.TextBox();
            this.SubmitPasteButton = new System.Windows.Forms.Button();
            this.CancelPasteButton = new System.Windows.Forms.Button();
            this.PasteIDLabel = new System.Windows.Forms.Label();
            this.PasteIDBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PasteTextBox
            // 
            this.PasteTextBox.AcceptsReturn = true;
            this.PasteTextBox.AcceptsTab = true;
            this.PasteTextBox.Location = new System.Drawing.Point(12, 87);
            this.PasteTextBox.MaxLength = 32768;
            this.PasteTextBox.Multiline = true;
            this.PasteTextBox.Name = "PasteTextBox";
            this.PasteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PasteTextBox.Size = new System.Drawing.Size(864, 272);
            this.PasteTextBox.TabIndex = 0;
            this.PasteTextBox.WordWrap = false;
            // 
            // PasteLabel
            // 
            this.PasteLabel.AutoSize = true;
            this.PasteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasteLabel.Location = new System.Drawing.Point(12, 69);
            this.PasteLabel.Name = "PasteLabel";
            this.PasteLabel.Size = new System.Drawing.Size(117, 15);
            this.PasteLabel.TabIndex = 1;
            this.PasteLabel.Text = "Enter your paste text";
            // 
            // Password1Box
            // 
            this.Password1Box.Location = new System.Drawing.Point(15, 380);
            this.Password1Box.MaxLength = 255;
            this.Password1Box.Name = "Password1Box";
            this.Password1Box.PasswordChar = '*';
            this.Password1Box.Size = new System.Drawing.Size(250, 20);
            this.Password1Box.TabIndex = 2;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(12, 362);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(149, 15);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "Enter your password twice";
            // 
            // Password2Box
            // 
            this.Password2Box.Location = new System.Drawing.Point(15, 406);
            this.Password2Box.MaxLength = 255;
            this.Password2Box.Name = "Password2Box";
            this.Password2Box.PasswordChar = '*';
            this.Password2Box.Size = new System.Drawing.Size(250, 20);
            this.Password2Box.TabIndex = 4;
            // 
            // SubmitPasteButton
            // 
            this.SubmitPasteButton.Location = new System.Drawing.Point(12, 432);
            this.SubmitPasteButton.Name = "SubmitPasteButton";
            this.SubmitPasteButton.Size = new System.Drawing.Size(98, 23);
            this.SubmitPasteButton.TabIndex = 5;
            this.SubmitPasteButton.Text = "Submit Paste";
            this.SubmitPasteButton.UseVisualStyleBackColor = true;
            this.SubmitPasteButton.Click += new System.EventHandler(this.SubmitPasteButton_Click);
            // 
            // CancelPasteButton
            // 
            this.CancelPasteButton.Location = new System.Drawing.Point(125, 432);
            this.CancelPasteButton.Name = "CancelPasteButton";
            this.CancelPasteButton.Size = new System.Drawing.Size(75, 23);
            this.CancelPasteButton.TabIndex = 6;
            this.CancelPasteButton.Text = "Cancel";
            this.CancelPasteButton.UseVisualStyleBackColor = true;
            this.CancelPasteButton.Click += new System.EventHandler(this.CancelPasteButton_Click);
            // 
            // PasteIDLabel
            // 
            this.PasteIDLabel.AutoSize = true;
            this.PasteIDLabel.Location = new System.Drawing.Point(15, 13);
            this.PasteIDLabel.Name = "PasteIDLabel";
            this.PasteIDLabel.Size = new System.Drawing.Size(109, 13);
            this.PasteIDLabel.TabIndex = 7;
            this.PasteIDLabel.Text = "Paste unique identifer";
            // 
            // PasteIDBox
            // 
            this.PasteIDBox.Location = new System.Drawing.Point(18, 30);
            this.PasteIDBox.MaxLength = 24;
            this.PasteIDBox.Name = "PasteIDBox";
            this.PasteIDBox.ReadOnly = true;
            this.PasteIDBox.Size = new System.Drawing.Size(180, 20);
            this.PasteIDBox.TabIndex = 8;
            // 
            // AddOrEditPasteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 467);
            this.Controls.Add(this.PasteIDBox);
            this.Controls.Add(this.PasteIDLabel);
            this.Controls.Add(this.CancelPasteButton);
            this.Controls.Add(this.SubmitPasteButton);
            this.Controls.Add(this.Password2Box);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.Password1Box);
            this.Controls.Add(this.PasteLabel);
            this.Controls.Add(this.PasteTextBox);
            this.Name = "AddOrEditPasteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Pastebin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasteTextBox;
        private System.Windows.Forms.Label PasteLabel;
        private System.Windows.Forms.TextBox Password1Box;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox Password2Box;
        private System.Windows.Forms.Button SubmitPasteButton;
        private System.Windows.Forms.Button CancelPasteButton;
        private System.Windows.Forms.Label PasteIDLabel;
        private System.Windows.Forms.TextBox PasteIDBox;
    }
}

