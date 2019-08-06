using System;
using System.Windows.Forms;

namespace YGOPro_Tweaker
{
    public partial class frmMain : Form
    {
        string Agreement = string.Empty;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Set URL to Link Label
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://github.com/YGOProTH/YGOPro-Tweaker";
            llbLink.Links.Add(link);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfig configForm = new frmConfig();
            this.Hide();
            configForm.ShowDialog();
            this.Show();
        }

        private void btnDeckList_Click(object sender, EventArgs e)
        {
            frmDeckList deckListForm = new frmDeckList();
            this.Hide();
            deckListForm.ShowDialog();
            this.Show();
        }

        private void btnDeckExtractor_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show("This software (Deck Extractor) is develop for learn about replay file of YGOPro. Did not have any other purpose, If the user has used in the wrong way. Or cause suffering or damage to themselves and others. We are not responsible in any way.\nIf you press Yes, assume you have read. And accept this", "Agreement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drs == System.Windows.Forms.DialogResult.Yes)
            {
                FrmDeckExtractor deckExtractorForm = new FrmDeckExtractor();
                this.Hide();
                deckExtractorForm.ShowDialog();
                this.Show();
            }
        }

        private void llbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
    }
}
