namespace SecurePastebin
{
    partial class MainForm
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
            this.CreatePasteButon = new System.Windows.Forms.Button();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.OpenPasteButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.DeletePasteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreatePasteButon
            // 
            this.CreatePasteButon.Location = new System.Drawing.Point(12, 53);
            this.CreatePasteButon.Name = "CreatePasteButon";
            this.CreatePasteButon.Size = new System.Drawing.Size(105, 23);
            this.CreatePasteButon.TabIndex = 0;
            this.CreatePasteButon.Text = "Create New Paste";
            this.CreatePasteButon.UseVisualStyleBackColor = true;
            this.CreatePasteButon.Click += new System.EventHandler(this.CreatePasteButon_Click);
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionLabel.Location = new System.Drawing.Point(143, 18);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(153, 16);
            this.ActionLabel.TabIndex = 1;
            this.ActionLabel.Text = "What do you want to do?";
            // 
            // OpenPasteButton
            // 
            this.OpenPasteButton.Location = new System.Drawing.Point(123, 53);
            this.OpenPasteButton.Name = "OpenPasteButton";
            this.OpenPasteButton.Size = new System.Drawing.Size(115, 23);
            this.OpenPasteButton.TabIndex = 2;
            this.OpenPasteButton.Text = "Open Existing Paste";
            this.OpenPasteButton.UseVisualStyleBackColor = true;
            this.OpenPasteButton.Click += new System.EventHandler(this.OpenPasteButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(369, 53);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(69, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Exit";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // DeletePasteButton
            // 
            this.DeletePasteButton.Location = new System.Drawing.Point(244, 53);
            this.DeletePasteButton.Name = "DeletePasteButton";
            this.DeletePasteButton.Size = new System.Drawing.Size(119, 23);
            this.DeletePasteButton.TabIndex = 4;
            this.DeletePasteButton.Text = "Delete Existing Paste";
            this.DeletePasteButton.UseVisualStyleBackColor = true;
            this.DeletePasteButton.Click += new System.EventHandler(this.DeletePasteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 102);
            this.Controls.Add(this.DeletePasteButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OpenPasteButton);
            this.Controls.Add(this.ActionLabel);
            this.Controls.Add(this.CreatePasteButon);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Pastebin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreatePasteButon;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Button OpenPasteButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button DeletePasteButton;
    }
}