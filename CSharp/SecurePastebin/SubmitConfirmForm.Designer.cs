namespace SecurePastebin
{
    partial class SubmitConfirmForm
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
            this.PasteIDBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ConfirmSubmitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PasteIDBox
            // 
            this.PasteIDBox.Location = new System.Drawing.Point(153, 33);
            this.PasteIDBox.MaxLength = 24;
            this.PasteIDBox.Name = "PasteIDBox";
            this.PasteIDBox.ReadOnly = true;
            this.PasteIDBox.Size = new System.Drawing.Size(180, 20);
            this.PasteIDBox.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(208, 60);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ConfirmSubmitLabel
            // 
            this.ConfirmSubmitLabel.AutoSize = true;
            this.ConfirmSubmitLabel.Location = new System.Drawing.Point(12, 9);
            this.ConfirmSubmitLabel.Name = "ConfirmSubmitLabel";
            this.ConfirmSubmitLabel.Size = new System.Drawing.Size(473, 13);
            this.ConfirmSubmitLabel.TabIndex = 2;
            this.ConfirmSubmitLabel.Text = "Paste created. Save the unique identifier below.  You will need to use it to retr" +
    "ieve your paste later. ";
            // 
            // SubmitConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 94);
            this.Controls.Add(this.ConfirmSubmitLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PasteIDBox);
            this.Name = "SubmitConfirmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Pastebin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasteIDBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label ConfirmSubmitLabel;
    }
}