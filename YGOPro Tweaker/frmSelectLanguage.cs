using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YGOPro_Tweaker
{
    public partial class frmSelectLanguage : Form
    {
        CConf _CConf = new CConf(Application.StartupPath + @"\system.conf");
        public frmSelectLanguage()
        {
            InitializeComponent();
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            _CConf.writeConfig(CConf.configVar.language, "en");
            frmMain mainForm = new frmMain(0);
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }

        private void btnThai_Click(object sender, EventArgs e)
        {
            _CConf.writeConfig(CConf.configVar.language, "th");
            frmMain mainForm = new frmMain(1);
            this.Hide();
            mainForm.ShowDialog();
            this.Close();
        }
    }
}
