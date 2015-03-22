namespace YGOPro_Tweaker
{
    partial class frmSelectLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectLanguage));
            this.btnThai = new System.Windows.Forms.Button();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnThai
            // 
            this.btnThai.Image = global::YGOPro_Tweaker.Properties.Resources.Thailand_Flag_icon;
            this.btnThai.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThai.Location = new System.Drawing.Point(173, 12);
            this.btnThai.Name = "btnThai";
            this.btnThai.Size = new System.Drawing.Size(155, 73);
            this.btnThai.TabIndex = 1;
            this.btnThai.Text = "ไทย";
            this.btnThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThai.UseVisualStyleBackColor = true;
            this.btnThai.Click += new System.EventHandler(this.btnThai_Click);
            // 
            // btnEnglish
            // 
            this.btnEnglish.Image = global::YGOPro_Tweaker.Properties.Resources.United_Kingdom_flag_icon;
            this.btnEnglish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnglish.Location = new System.Drawing.Point(12, 12);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(155, 73);
            this.btnEnglish.TabIndex = 0;
            this.btnEnglish.Text = "English";
            this.btnEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnglish.UseVisualStyleBackColor = true;
            this.btnEnglish.Click += new System.EventHandler(this.btnEnglish_Click);
            // 
            // frmSelectLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 94);
            this.Controls.Add(this.btnEnglish);
            this.Controls.Add(this.btnThai);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSelectLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Language : เลือกภาษา";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnThai;
        private System.Windows.Forms.Button btnEnglish;
    }
}