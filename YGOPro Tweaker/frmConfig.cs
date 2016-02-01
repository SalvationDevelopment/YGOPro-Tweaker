using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;
using System.Net;

namespace YGOPro_Tweaker
{
    public partial class frmConfig : Form
    {
        CConf _CConf = new CConf(Application.StartupPath + @"\system.conf");
        string fullVarlue = string.Empty;
        string[] splitValue = { };

        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //check text font file exist
            if (!System.IO.File.Exists(Application.StartupPath + "\\" + txtTextFontPath.Text))
            {
                MessageBox.Show("Font Path : " + txtTextFontPath.Text + " Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //check number font file exist
            if (!System.IO.File.Exists(Application.StartupPath + "\\" + txtNumberFontPath.Text))
            {
                MessageBox.Show("Font Path : " + txtNumberFontPath.Text + " Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Save Config
            saveConfig();
            MessageBox.Show("Save Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            loadConfig();
            MessageBox.Show("Reset Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //First Load
            loadConfig();
        }


        private void loadConfig()
        {
            //Load Language Config
            switch (_CConf.readConfig(CConf.configVar.language))
            {
                case "en":
                    { rbEnglish.Checked = true; break; }
                case "de":
                    { rbGerman.Checked = true; break; }
                case "es":
                    { rbSpanish.Checked = true; break; }
                case "th":
                    { rbThai.Checked = true; break; }
            }
            //Load Direct3D Config
            if (_CConf.readConfig(CConf.configVar.use_d3d).Equals("0")) { rbDirect3DDisable.Checked = true; }
            else { rbDirect3DEnable.Checked = true; }
            //Load Skin Config
            if (_CConf.readConfig(CConf.configVar.use_skin).Equals("0")) { rbSkinDisable.Checked = true; }
            else { rbSkinEnable.Checked = true; }
            //Load Anti-aliasing Config
            int AntiAliasing = Convert.ToInt16(_CConf.readConfig(CConf.configVar.antialias));
            trackAntiAliasing.Value = AntiAliasing;
            lbAntiAliasing.Text = AntiAliasing.ToString();
            //Load Error Log Config
            if (_CConf.readConfig(CConf.configVar.errorlog).Equals("0")) { rbErrorLogDisable.Checked = true; }
            else { rbErrorLogEnable.Checked = true; }
            //Read NickName Config
            txtNickName.Text = _CConf.readConfig(CConf.configVar.nickname);
            //Read Text Font Config
            fullVarlue = _CConf.readConfig(CConf.configVar.textfont);
            splitValue = fullVarlue.Split(' ');
            txtTextFontPath.Text = splitValue[0];
            txtTextFontSize.Text = splitValue[1];
            //Read Number Font Config
            fullVarlue = _CConf.readConfig(CConf.configVar.numfont);
            splitValue = fullVarlue.Split(' ');
            txtNumberFontPath.Text = splitValue[0];
            //Read Fullscreen Config
            if (_CConf.readConfig(CConf.configVar.fullscreen).Equals("0")) { rbScreenSettingWindowed.Checked = true; }
            else { rbScreenSettingFullscreen.Checked = true; }
            //Read Sound Config
            if (_CConf.readConfig(CConf.configVar.enable_sound).Equals("0")) { rbSoundDisable.Checked = true; }
            else { rbSoundEnable.Checked = true; }
            //Read Music Config
            if (_CConf.readConfig(CConf.configVar.enable_music).Equals("0")) { rbMusicDisable.Checked = true; }
            else { rbMusicEnable.Checked = true; }
            //Read Auto Card Placing Config
            if (_CConf.readConfig(CConf.configVar.auto_card_placing).Equals("0")) { rbAutoCardPlacingDisable.Checked = true; }
            else { rbAutoCardPlacingEnable.Checked = true; }
            //Read Random Card Placing Config
            if (_CConf.readConfig(CConf.configVar.random_card_placing).Equals("0")) { rbRandomCardPlacingDisable.Checked = true; }
            else { rbRandomCardPlacingEnable.Checked = true; }
            //Read Auto Chain Order Config
            if (_CConf.readConfig(CConf.configVar.auto_chain_order).Equals("0")) { rbAutoChainOrderDisable.Checked = true; }
            else { rbAutoChainOrderEnable.Checked = true; }
            //Read No Delay For Chain Config
            if (_CConf.readConfig(CConf.configVar.no_delay_for_chain).Equals("0")) { rbNoDelayForChainDisable.Checked = true; }
            else { rbNoDelayForChainEnable.Checked = true; }
            //Read Mute Opponent Config
            if (_CConf.readConfig(CConf.configVar.mute_opponent).Equals("0")) { rbMuteOpponentDisable.Checked = true; }
            else { rbMuteOpponentEnable.Checked = true; }
            //Read Mute Spectators Config
            if (_CConf.readConfig(CConf.configVar.mute_spectators).Equals("0")) { rbMuteSpectatorsDisable.Checked = true; }
            else { rbMuteSpectatorsEnable.Checked = true; }
            //Read Volume Config
            int Volume = Convert.ToInt16(_CConf.readConfig(CConf.configVar.volume));
            trackVolume.Value = Volume;
            lbVolume.Text = Volume.ToString();
            //Read Background Config
            switch (_CConf.readConfig(CConf.configVar.background))
            {
                case "0": { rbBackGroundOption0.Checked = true; break; }
                case "1": { rbBackGroundOption1.Checked = true; break; }
                case "2": { rbBackGroundOption2.Checked = true; break; }
                case "3": { rbBackGroundOption3.Checked = true; break; }
            }
        }

        private void saveConfig()
        {
            //Save Language Config
            if (rbEnglish.Checked)
            {
                _CConf.writeConfig(CConf.configVar.language, "en");
            }
            else if (rbGerman.Checked)
            {
                _CConf.writeConfig(CConf.configVar.language, "de");
            }
            else if (rbSpanish.Checked)
            {
                _CConf.writeConfig(CConf.configVar.language, "es");
            }
            else if (rbThai.Checked)
            {
                _CConf.writeConfig(CConf.configVar.language, "th");
            }
            //Save Direct3D Config
            if (rbDirect3DEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_d3d, "1");
            }
            else if (rbDirect3DDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_d3d, "0");
            }
            //Save Skin Config
            if (rbSkinEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_skin, "1");
            }
            else if (rbSkinDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_skin, "0");
            }
            //Save Anti-aliasing Config
            _CConf.writeConfig(CConf.configVar.antialias, trackAntiAliasing.Value.ToString());
            //Save Error Log Config
            if (rbErrorLogEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.errorlog, "1");
            }
            else if (rbErrorLogDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.errorlog, "0");
            }
            //Save NickName Config
            _CConf.writeConfig(CConf.configVar.nickname, txtNickName.Text);
            //Save Text Font Config
            _CConf.writeConfig(CConf.configVar.textfont, (txtTextFontPath.Text + " " + txtTextFontSize.Text));
            //Save Number Font Config
            _CConf.writeConfig(CConf.configVar.numfont, txtNumberFontPath.Text);
            //Save Fullscreen Config
            if (rbScreenSettingFullscreen.Checked)
            {
                _CConf.writeConfig(CConf.configVar.fullscreen, "1");
            }
            else if (rbScreenSettingWindowed.Checked)
            {
                _CConf.writeConfig(CConf.configVar.fullscreen, "0");
            }
            //Save Sound Config
            if (rbSoundEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.enable_sound, "1");
            }
            else if (rbSoundDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.enable_sound, "0");
            }
            //Save Music Config
            if (rbMusicEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.enable_music, "1");
            }
            else if (rbMusicDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.enable_music, "0");
            }
            //Save Auto Card Placing Config
            if (rbAutoCardPlacingEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.auto_card_placing, "1");
            }
            else if (rbAutoCardPlacingDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.auto_card_placing, "0");
            }
            //Save Random Card Placing Config
            if (rbRandomCardPlacingEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.random_card_placing, "1");
            }
            else if (rbRandomCardPlacingDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.random_card_placing, "0");
            }
            //Save No Delay For Chain Config
            if (rbAutoChainOrderEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.auto_chain_order, "1");
            }
            else if (rbAutoChainOrderDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.auto_chain_order, "0");
            }
            //Save No Delay For Chain Config
            if (rbNoDelayForChainEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.no_delay_for_chain, "1");
            }
            else if (rbNoDelayForChainDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.no_delay_for_chain, "0");
            }
            //Save Mute Opponent Config
            if (rbMuteOpponentEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.mute_opponent, "1");
            }
            else if (rbMuteOpponentDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.mute_opponent, "0");
            }
            //Save Mute Spectators Config
            if (rbMuteSpectatorsEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.mute_spectators, "1");
            }
            else if (rbMuteSpectatorsDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.mute_spectators, "0");
            }
            //Save Volume Config
            _CConf.writeConfig(CConf.configVar.volume, trackVolume.Value.ToString());
            //Save Background Config
            if (rbBackGroundOption0.Checked)
            {
                _CConf.writeConfig(CConf.configVar.background, "0");
            }
            else if (rbBackGroundOption1.Checked)
            {
                _CConf.writeConfig(CConf.configVar.background, "1");
            }
            else if (rbBackGroundOption2.Checked)
            {
                _CConf.writeConfig(CConf.configVar.background, "2");
            }
            else if (rbBackGroundOption3.Checked)
            {
                _CConf.writeConfig(CConf.configVar.background, "3");
            }
            //Save Direct3D Config
            if (rbDirect3DEnable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_d3d, "1");
            }
            else if (rbDirect3DDisable.Checked)
            {
                _CConf.writeConfig(CConf.configVar.use_d3d, "0");
            }
        }

        private void trackAntiAliasing_Scroll(object sender, EventArgs e)
        {
            lbAntiAliasing.Text = trackAntiAliasing.Value.ToString();
        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            lbVolume.Text = trackVolume.Value.ToString();
        }

        private void rbSkinEnable_CheckedChanged(object sender, EventArgs e)
        {
            rbDirect3DEnable.Checked = true;
        }

        private void rbDirect3DDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDirect3DDisable.Checked) { rbSkinDisable.Checked = true; rbDirect3DDisable.Checked = true; }
        }

        private void loadDefault()
        {
            rbEnglish.Checked = true;
            rbDirect3DDisable.Checked = true;
            rbSkinDisable.Checked = true;
            trackAntiAliasing.Value = 0;
            rbErrorLogEnable.Checked = true;
            txtNickName.Text = "Player";
            txtTextFontPath.Text = "fonts/ARIALUNI.TTF";
            txtTextFontSize.Text = "14";
            txtNumberFontPath.Text = "fonts/arialbd.ttf";
            rbScreenSettingWindowed.Checked = true;
            rbSoundEnable.Checked = true;
            rbMusicEnable.Checked = true;
            rbAutoCardPlacingEnable.Checked = true;
            rbRandomCardPlacingDisable.Checked = true;
            rbNoDelayForChainDisable.Checked = true;
            rbMuteOpponentDisable.Checked = true;
            rbMuteSpectatorsDisable.Checked = true;
            trackVolume.Value = 25;
            rbBackGroundOption0.Checked = true;
            MessageBox.Show("Default config has filled. You can save it safe now", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            loadDefault();
        }
        private void txtTextFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
