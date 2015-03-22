using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YGOPro_Tweaker
{
    public partial class frmMain : Form
    {
        string Deck_Extractor_Agreement_Text = string.Empty;
        string Agreement = string.Empty;
        int currentLanguage = 0; //0 = English, 1 = Thai
        public frmMain(int Lenguage)
        {
            InitializeComponent();
            currentLanguage = Lenguage;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Set URL to Link Label
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://bitbucket.org/ekaomk/ygopro-tweaker";
            llbLink.Links.Add(link);

            switch (currentLanguage)
            {
                case 0: setEnglishLanguage(); break;
                case 1: setThaiLanguage(); break;
                default: break;
            }
        }

        private void setEnglishLanguage()
        {
            CLanguage.Main.English mainEnglish = new CLanguage.Main.English();
            this.Text = mainEnglish.Title;
            Deck_Extractor_Agreement_Text = mainEnglish.Deck_Extractor_Agreement_Text;
            Agreement = mainEnglish.Agreement_Text;
            lbCredit.Text = mainEnglish.Credit;
        }

        private void setThaiLanguage()
        {
            CLanguage.Main.Thai mainThai = new CLanguage.Main.Thai();
            this.Text = mainThai.Title;
            ////////////////////////////////////////
            Deck_Extractor_Agreement_Text = mainThai.Deck_Extractor_Agreement_Text;
            Agreement = mainThai.Agreement_Text;
            btnConfig.Text = mainThai.Config;
            btnDeckList.Text = mainThai.Deck_List;
            btnDeckExtractor.Text = mainThai.Deck_Extractor;
            lbCredit.Text = mainThai.Credit;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfig configForm = new frmConfig(currentLanguage);
            this.Hide();
            configForm.ShowDialog();
            this.Show();
        }

        private void btnDeckList_Click(object sender, EventArgs e)
        {
            frmDeckList deckListForm = new frmDeckList(currentLanguage);
            this.Hide();
            deckListForm.ShowDialog();
            this.Show();
        }

        private void btnDeckExtractor_Click(object sender, EventArgs e)
        {
            DialogResult drs = MessageBox.Show(Deck_Extractor_Agreement_Text, Agreement, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drs == System.Windows.Forms.DialogResult.Yes)
            {
                frmDeckExtractor deckExtractorForm = new frmDeckExtractor(currentLanguage);
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
