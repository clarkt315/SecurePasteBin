namespace SecurePastebin
{
    partial class DeletePasteForm
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
            this.EnterPasteIDLabel = new System.Windows.Forms.Label();
            this.DeletePasteButton = new System.Windows.Forms.Button();
            this.CancelDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PasteIDBox
            // 
            this.PasteIDBox.Location = new System.Drawing.Point(12, 30);
            this.PasteIDBox.MaxLength = 24;
            this.PasteIDBox.Name = "PasteIDBox";
            this.PasteIDBox.Size = new System.Drawing.Size(180, 20);
            this.PasteIDBox.TabIndex = 0;
            // 
            // EnterPasteIDLabel
            // 
            this.EnterPasteIDLabel.AutoSize = true;
            this.EnterPasteIDLabel.Location = new System.Drawing.Point(12, 12);
            this.EnterPasteIDLabel.Name = "EnterPasteIDLabel";
            this.EnterPasteIDLabel.Size = new System.Drawing.Size(276, 13);
            this.EnterPasteIDLabel.TabIndex = 1;
            this.EnterPasteIDLabel.Text = "Enter the unique identifier of the paste you want to delete";
            // 
            // DeletePasteButton
            // 
            this.DeletePasteButton.Location = new System.Drawing.Point(15, 61);
            this.DeletePasteButton.Name = "DeletePasteButton";
            this.DeletePasteButton.Size = new System.Drawing.Size(91, 23);
            this.DeletePasteButton.TabIndex = 2;
            this.DeletePasteButton.Text = "Delete Paste";
            this.DeletePasteButton.UseVisualStyleBackColor = true;
            this.DeletePasteButton.Click += new System.EventHandler(this.DeletePasteButton_Click);
            // 
            // CancelDeleteButton
            // 
            this.CancelDeleteButton.Location = new System.Drawing.Point(112, 61);
            this.CancelDeleteButton.Name = "CancelDeleteButton";
            this.CancelDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.CancelDeleteButton.TabIndex = 3;
            this.CancelDeleteButton.Text = "Cancel";
            this.CancelDeleteButton.UseVisualStyleBackColor = true;
            this.CancelDeleteButton.Click += new System.EventHandler(this.CancelDeleteButton_Click);
            // 
            // DeletePasteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 96);
            this.Controls.Add(this.CancelDeleteButton);
            this.Controls.Add(this.DeletePasteButton);
            this.Controls.Add(this.EnterPasteIDLabel);
            this.Controls.Add(this.PasteIDBox);
            this.Name = "DeletePasteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Pastebin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasteIDBox;
        private System.Windows.Forms.Label EnterPasteIDLabel;
        private System.Windows.Forms.Button DeletePasteButton;
        private System.Windows.Forms.Button CancelDeleteButton;
    }
}