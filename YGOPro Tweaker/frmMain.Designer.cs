namespace YGOPro_Tweaker
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnDeckList = new System.Windows.Forms.Button();
            this.btnDeckExtractor = new System.Windows.Forms.Button();
            this.lbCredit = new System.Windows.Forms.Label();
            this.llbLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(12, 12);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(374, 47);
            this.btnConfig.TabIndex = 0;
            this.btnConfig.Text = "YGOPro Configuration";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnDeckList
            // 
            this.btnDeckList.Location = new System.Drawing.Point(12, 65);
            this.btnDeckList.Name = "btnDeckList";
            this.btnDeckList.Size = new System.Drawing.Size(374, 47);
            this.btnDeckList.TabIndex = 7;
            this.btnDeckList.Text = "Deck List Builder";
            this.btnDeckList.UseVisualStyleBackColor = true;
            this.btnDeckList.Click += new System.EventHandler(this.btnDeckList_Click);
            // 
            // btnDeckExtractor
            // 
            this.btnDeckExtractor.Location = new System.Drawing.Point(12, 118);
            this.btnDeckExtractor.Name = "btnDeckExtractor";
            this.btnDeckExtractor.Size = new System.Drawing.Size(374, 47);
            this.btnDeckExtractor.TabIndex = 2;
            this.btnDeckExtractor.Text = "Deck Extractor";
            this.btnDeckExtractor.UseVisualStyleBackColor = true;
            this.btnDeckExtractor.Click += new System.EventHandler(this.btnDeckExtractor_Click);
            // 
            // lbCredit
            // 
            this.lbCredit.AutoSize = true;
            this.lbCredit.Location = new System.Drawing.Point(260, 168);
            this.lbCredit.Name = "lbCredit";
            this.lbCredit.Size = new System.Drawing.Size(102, 26);
            this.lbCredit.TabIndex = 10;
            this.lbCredit.Text = "YGOPro Tweaker \r\nDevelop by ekaomk";
            this.lbCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // llbLink
            // 
            this.llbLink.AutoSize = true;
            this.llbLink.Location = new System.Drawing.Point(160, 194);
            this.llbLink.Name = "llbLink";
            this.llbLink.Size = new System.Drawing.Size(227, 13);
            this.llbLink.TabIndex = 3;
            this.llbLink.TabStop = true;
            this.llbLink.Text = "https://github.com/ekaomk/YGOPro-Tweaker";
            this.llbLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbLink_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 214);
            this.Controls.Add(this.lbCredit);
            this.Controls.Add(this.llbLink);
            this.Controls.Add(this.btnDeckExtractor);
            this.Controls.Add(this.btnDeckList);
            this.Controls.Add(this.btnConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main menu - YGOPro Tweaker";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnDeckList;
        private System.Windows.Forms.Button btnDeckExtractor;
        private System.Windows.Forms.Label lbCredit;
        private System.Windows.Forms.LinkLabel llbLink;
    }
}