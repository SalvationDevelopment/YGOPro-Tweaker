namespace YGOPro_Tweaker
{
    partial class frmDeckList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeckList));
            this.btnLoadDeck = new System.Windows.Forms.Button();
            this.listDeckList = new System.Windows.Forms.ListBox();
            this.pbCard = new System.Windows.Forms.PictureBox();
            this.txtDeckPath = new System.Windows.Forms.TextBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadDeck
            // 
            this.btnLoadDeck.Location = new System.Drawing.Point(386, 10);
            this.btnLoadDeck.Name = "btnLoadDeck";
            this.btnLoadDeck.Size = new System.Drawing.Size(91, 23);
            this.btnLoadDeck.TabIndex = 0;
            this.btnLoadDeck.Text = "Load Deck";
            this.btnLoadDeck.UseVisualStyleBackColor = true;
            this.btnLoadDeck.Click += new System.EventHandler(this.btnLoadDeck_Click);
            // 
            // listDeckList
            // 
            this.listDeckList.FormattingEnabled = true;
            this.listDeckList.Location = new System.Drawing.Point(12, 38);
            this.listDeckList.Name = "listDeckList";
            this.listDeckList.Size = new System.Drawing.Size(284, 251);
            this.listDeckList.TabIndex = 1;
            // 
            // pbCard
            // 
            this.pbCard.Location = new System.Drawing.Point(302, 38);
            this.pbCard.Name = "pbCard";
            this.pbCard.Size = new System.Drawing.Size(175, 254);
            this.pbCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCard.TabIndex = 2;
            this.pbCard.TabStop = false;
            // 
            // txtDeckPath
            // 
            this.txtDeckPath.Location = new System.Drawing.Point(12, 12);
            this.txtDeckPath.Name = "txtDeckPath";
            this.txtDeckPath.ReadOnly = true;
            this.txtDeckPath.Size = new System.Drawing.Size(368, 20);
            this.txtDeckPath.TabIndex = 3;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Location = new System.Drawing.Point(12, 295);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(465, 23);
            this.btnCopyToClipboard.TabIndex = 4;
            this.btnCopyToClipboard.Text = "Copy To Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // frmDeckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 325);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.txtDeckPath);
            this.Controls.Add(this.pbCard);
            this.Controls.Add(this.listDeckList);
            this.Controls.Add(this.btnLoadDeck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDeckList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deck List Builder - YGOPro Tweaker";
            this.Load += new System.EventHandler(this.frmDeckList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadDeck;
        private System.Windows.Forms.ListBox listDeckList;
        private System.Windows.Forms.PictureBox pbCard;
        private System.Windows.Forms.TextBox txtDeckPath;
        private System.Windows.Forms.Button btnCopyToClipboard;
    }
}