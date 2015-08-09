using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Windows.Forms;

using System.Data.SQLite;

namespace YGOPro_Tweaker
{
    public partial class frmDeckExtractor : Form
    {
        cReplayManager Replay = new cReplayManager();
        StringBuilder PlayerOneDeck = new StringBuilder();
        StringBuilder PlayerTwoDeck = new StringBuilder();
        StringBuilder PlayerThreeDeck = new StringBuilder();
        StringBuilder PlayerFourDeck = new StringBuilder();

        List<string> PlayerMainDeck = new List<string>();
        List<string> PlayerExtraDeck = new List<string>();
        List<string> PlayerMainDeckText = new List<string>();
        List<string> PlayerExtraDeckText = new List<string>();

        string PlayerOneName;
        string PlayerTwoName;
        string PlayerThreeName;
        string PlayerFourName;
        int StartLP;
        int StartHand;
        int DrawFor;

        string Start_Life_Point_Text = string.Empty;
        string Start_Hand_Text = string.Empty;
        string Draw_For_Text = string.Empty;

        string Main_Deck_Title = string.Empty;
        string Extra_Deck_Title = string.Empty;
        string Side_Deck_Title = string.Empty;

        string Deck_Owner_Text = string.Empty;
        string Copy_Deck_List_Text  = string.Empty;
        string Save_Deck_List_Text = string.Empty;

        string Mode_Single_or_Match_Text = string.Empty;
        string Mode_1vs2_Text = string.Empty;
        string Mode_Tag_Text = string.Empty;
        string Player1_Text = string.Empty;
        string Player2_Text = string.Empty;
        string Player3_Text = string.Empty;
        string Player4_Text = string.Empty;
        string VS_Text = string.Empty;
        string Copied_To_Clipboard_Text = string.Empty;
        string Error_Text = string.Empty;
        string Information_Text = string.Empty;

        string Replay_Error_Text = string.Empty;

        int currentLanguage = 0; //0 = English, 1 = Thai

        public frmDeckExtractor(int Lenguage)
        {
            InitializeComponent();
            currentLanguage = Lenguage;
        }

        private void frmDeckExtractor_Load(object sender, EventArgs e)
        {
            switch (currentLanguage)
            {
                case 0: //English
                    CLanguage.DeckExtractor.English englishLanguage = new CLanguage.DeckExtractor.English();
                    Start_Life_Point_Text = englishLanguage.Start_Life_Point_Text;
                    Start_Hand_Text = englishLanguage.Start_Hand_Text;
                    Draw_For_Text = englishLanguage.Draw_For_Text;
                    Main_Deck_Title = englishLanguage.Main_Deck_Title;
                    Extra_Deck_Title = englishLanguage.Extra_Deck_Title;
                    Side_Deck_Title = englishLanguage.Side_Deck_Title;
                    Deck_Owner_Text = englishLanguage.Deck_Owner_Text;
                    Copy_Deck_List_Text = englishLanguage.Copy_Deck_List_Text;
                    Save_Deck_List_Text = englishLanguage.Save_Deck_List_Text;
                    Mode_Single_or_Match_Text = englishLanguage.Mode_Single_or_Match_Text;
                    Mode_1vs2_Text = englishLanguage.Mode_1vs2_Text;
                    Mode_Tag_Text = englishLanguage.Mode_Tag_Text;
                    Player1_Text = englishLanguage.Player1_Text;
                    Player2_Text = englishLanguage.Player2_Text;
                    Player3_Text = englishLanguage.Player3_Text;
                    Player4_Text = englishLanguage.Player4_Text;
                    VS_Text = englishLanguage.VS_Text;
                    Copied_To_Clipboard_Text = englishLanguage.Copied_To_Clipboard_Text;
                    Error_Text = englishLanguage.Error_Text;
                    Information_Text = englishLanguage.Information_Text;
                    Replay_Error_Text = englishLanguage.Replay_Error_Text;
                    btnLoadDeck.Text = englishLanguage.Load_Replay_Text;

                    lbMode.Text = String.Format(englishLanguage.Mode_Text, "?");
                    lbStartLP.Text = String.Format(englishLanguage.Start_Life_Point_Text, "?");
                    lbStartHand.Text = String.Format(englishLanguage.Start_Hand_Text, "?");
                    lbPlayer.Text = String.Format(englishLanguage.Player_Text, "?");
                    lbDrawFor.Text = String.Format(englishLanguage.Draw_For_Text, "?");

                    this.Text = englishLanguage.Title;
                    break;
                case 1: //Thai
                    CLanguage.DeckExtractor.Thai thaiLanguage = new CLanguage.DeckExtractor.Thai();
                    Start_Life_Point_Text = thaiLanguage.Start_Life_Point_Text;
                    Start_Hand_Text = thaiLanguage.Start_Hand_Text;
                    Draw_For_Text = thaiLanguage.Draw_For_Text;
                    Main_Deck_Title = thaiLanguage.Main_Deck_Title;
                    Extra_Deck_Title = thaiLanguage.Extra_Deck_Title;
                    Side_Deck_Title = thaiLanguage.Side_Deck_Title;
                    Deck_Owner_Text = thaiLanguage.Deck_Owner_Text;
                    Copy_Deck_List_Text = thaiLanguage.Copy_Deck_List_Text;
                    Save_Deck_List_Text = thaiLanguage.Save_Deck_List_Text;
                    Mode_Single_or_Match_Text = thaiLanguage.Mode_Single_or_Match_Text;
                    Mode_1vs2_Text = thaiLanguage.Mode_1vs2_Text;
                    Mode_Tag_Text = thaiLanguage.Mode_Tag_Text;
                    Player1_Text = thaiLanguage.Player1_Text;
                    Player2_Text = thaiLanguage.Player2_Text;
                    Player3_Text = thaiLanguage.Player3_Text;
                    Player4_Text = thaiLanguage.Player4_Text;
                    VS_Text = thaiLanguage.VS_Text;
                    Copied_To_Clipboard_Text = thaiLanguage.Copied_To_Clipboard_Text;
                    Error_Text = thaiLanguage.Error_Text;
                    Information_Text = thaiLanguage.Information_Text;
                    Replay_Error_Text = thaiLanguage.Replay_Error_Text;
                    btnLoadDeck.Text = thaiLanguage.Load_Replay_Text;

                    lbMode.Text = String.Format(thaiLanguage.Mode_Text, "?");
                    lbStartLP.Text = String.Format(thaiLanguage.Start_Life_Point_Text, "?");
                    lbStartHand.Text = String.Format(thaiLanguage.Start_Hand_Text, "?");
                    lbPlayer.Text = String.Format(thaiLanguage.Player_Text, "?");
                    lbDrawFor.Text = String.Format(thaiLanguage.Draw_For_Text, "?");

                    this.Text = thaiLanguage.Title;
                    break;
                default: break;
            }
            resetPlayerName();
        }

        private void resetPlayerName()
        {
            btnCopyDeckListPlayerOne.Text = String.Format(Copy_Deck_List_Text, "?");
            btnSaveDeckListPlayerOne.Text = String.Format(Save_Deck_List_Text, "?");

            btnCopyDeckListPlayerTwo.Text = String.Format(Copy_Deck_List_Text, "?");
            btnSaveDeckListPlayerTwo.Text = String.Format(Save_Deck_List_Text, "?");

            btnCopyDeckListPlayerThree.Text = String.Format(Copy_Deck_List_Text, "?");
            btnSaveDeckListPlayerThree.Text = String.Format(Save_Deck_List_Text, "?");

            btnCopyDeckListPlayerFour.Text = String.Format(Copy_Deck_List_Text, "?");
            btnSaveDeckListPlayerFour.Text = String.Format(Save_Deck_List_Text, "?");
        }

        private void resetPlayerDeck()
        {
            PlayerOneDeck = new StringBuilder();
            PlayerTwoDeck = new StringBuilder();
            PlayerThreeDeck = new StringBuilder();
            PlayerFourDeck = new StringBuilder();
        }

        private void ReadBasicData()
        {
            PlayerOneName = Replay.ExtractName(Replay.ReadString(40));
            PlayerTwoName = Replay.ExtractName(Replay.ReadString(40));
            if (!Replay.SingleMode) //tag, etc
            {
                PlayerFourName = Replay.ExtractName(Replay.ReadString(40));
                PlayerThreeName = Replay.ExtractName(Replay.ReadString(40));
            }
            StartLP = Replay.DataReader.ReadInt32();
            StartHand = Replay.DataReader.ReadInt32();
            DrawFor = Replay.DataReader.ReadInt32();
            Replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture); //Player 1
        }

        private string GetCardName(int CardID)
        {
            using (var conn = new SQLiteConnection(@"Data Source=cards.cdb"))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT name FROM texts WHERE id LIKE @CardID";
                cmd.Parameters.Add(new SQLiteParameter("@CardID", CardID));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string CardName = reader.GetString(reader.GetOrdinal("name"));
                        return CardName;
                    }
                }
            }
            return string.Empty;
        }

        private void ReadDeck(StringBuilder PlayerDeck)
        {
            int NumberOfCard = Replay.DataReader.ReadInt32();
            PlayerMainDeck.Clear();
            PlayerMainDeckText.Clear();
            PlayerExtraDeck.Clear();
            PlayerExtraDeckText.Clear();


            //textBox1.Text += "----- Main -----" + Environment.NewLine;
            for (int i = 0; i < NumberOfCard; i++)
            {
                string CardID = Replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture);
                string CardName = GetCardName(Convert.ToInt32(CardID));
                if (CardName == "")
                {
                    MessageBox.Show(String.Format(Replay_Error_Text, Environment.NewLine, CardID.ToString()
                        ), Error_Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                PlayerMainDeck.Add(CardID);

                //add number of card
                int index = PlayerMainDeckText.FindIndex(x => x.Contains(CardName));

                if (index > -1)
                {
                    int CardNumber = Convert.ToInt32(PlayerMainDeckText[index].Substring(PlayerMainDeckText[index].Length - 1, 1));

                    CardNumber++;
                    PlayerMainDeckText[index] = (CardName + " x" + CardNumber.ToString());
                }
                else
                {
                    PlayerMainDeckText.Add(CardName + " x1");
                }

            }


            NumberOfCard = Replay.DataReader.ReadInt32();

            for (int i = 0; i < NumberOfCard; i++)
            {
                string CardID = Replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture);
                string CardName = GetCardName(Convert.ToInt32(CardID));
                if (CardName == "")
                {
                    MessageBox.Show(String.Format(Replay_Error_Text, Environment.NewLine, CardID.ToString()
                        ), Error_Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                PlayerExtraDeck.Add(CardID);

                //add number of card
                int index = PlayerExtraDeckText.FindIndex(x => x.Contains(CardName));
                if (index > -1)
                {
                    int CardNumber = Convert.ToInt32(PlayerExtraDeckText[index].Substring(PlayerExtraDeckText[index].Length - 1, 1));

                    PlayerExtraDeckText[index] = (CardName + " x" + (++CardNumber).ToString());
                }
                else
                {
                    PlayerExtraDeckText.Add(CardName + " x1");
                }
            }

            //sort card by id
            PlayerMainDeck.Sort();
            PlayerExtraDeck.Sort();
            PlayerMainDeckText.Sort();
            PlayerExtraDeckText.Sort();

            PlayerDeck.AppendLine("#created by YGOPro Tweaker");
            PlayerDeck.AppendLine("#main");

            foreach (string CardID in PlayerMainDeck)
            {
                PlayerDeck.AppendLine(CardID);
            }
            PlayerDeck.AppendLine("#extra");
            foreach (string CardID in PlayerExtraDeck)
            {
                PlayerDeck.AppendLine(CardID);
            }
            PlayerDeck.AppendLine("!side");
        }


        static int ExtractNumber(string text)
        {
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(text, @"(\d+)");
            if (match == null)
            {
                return 0;
            }

            int value;
            if (!int.TryParse(match.Value, out value))
            {
                return 0;
            }

            return value;
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private void btnLoadDeck_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.InitialDirectory = Application.StartupPath + "\\replay";
            OFD.Filter = "YGOPro Replay Files (*.yrp)|*.yrp|All files (*.*)|*.*";
            OFD.FilterIndex = 1;
            OFD.RestoreDirectory = true;

            string ReplayPath = string.Empty;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ReplayPath = OFD.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            else
            {
                return;
            }

            txtReplayPath.Text = ReplayPath;
            resetPlayerName();
            resetPlayerDeck();
            Replay.FromFile(ReplayPath);

            ReadBasicData();


            lbStartLP.Text = (String.Format(Start_Life_Point_Text, StartLP.ToString()));
            lbStartHand.Text = (String.Format(Start_Hand_Text, StartHand.ToString()));
            lbDrawFor.Text = (String.Format(Draw_For_Text, DrawFor.ToString()));

            listPlayerOneDeckList.Items.Clear();
            listPlayerTwoDeckList.Items.Clear();
            listPlayerThreeDeckList.Items.Clear();
            listPlayerFourDeckList.Items.Clear();

            ReadDeck(PlayerOneDeck);
            listPlayerOneDeckList.Items.Add(String.Format(Deck_Owner_Text, PlayerOneName));
            listPlayerOneDeckList.Items.Add(Main_Deck_Title);
            listPlayerOneDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
            listPlayerOneDeckList.Items.Add(Extra_Deck_Title);
            listPlayerOneDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

            btnCopyDeckListPlayerOne.Text = String.Format(Copy_Deck_List_Text, PlayerOneName);
            btnSaveDeckListPlayerOne.Text = String.Format(Save_Deck_List_Text, PlayerOneName);

            PlayerMainDeck.Clear();
            PlayerExtraDeck.Clear();

            ReadDeck(PlayerTwoDeck);
            listPlayerTwoDeckList.Items.Add(String.Format(Deck_Owner_Text, PlayerTwoName));
            listPlayerTwoDeckList.Items.Add(Main_Deck_Title);
            listPlayerTwoDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
            listPlayerTwoDeckList.Items.Add(Extra_Deck_Title);
            listPlayerTwoDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

            btnCopyDeckListPlayerTwo.Text = String.Format(Copy_Deck_List_Text, PlayerTwoName);
            btnSaveDeckListPlayerTwo.Text = String.Format(Save_Deck_List_Text, PlayerTwoName);

            if (!Replay.SingleMode)
            {
                PlayerMainDeck.Clear();
                PlayerExtraDeck.Clear();
                ReadDeck(PlayerThreeDeck);

                listPlayerThreeDeckList.Items.Add(String.Format(Deck_Owner_Text, PlayerThreeName));
                listPlayerThreeDeckList.Items.Add(Main_Deck_Title);
                listPlayerThreeDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
                listPlayerThreeDeckList.Items.Add(Extra_Deck_Title);
                listPlayerThreeDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

                btnCopyDeckListPlayerThree.Text = String.Format(Copy_Deck_List_Text, PlayerThreeName);
                btnSaveDeckListPlayerThree.Text = String.Format(Save_Deck_List_Text, PlayerThreeName);

                //
                PlayerMainDeck.Clear();
                PlayerExtraDeck.Clear();
                ReadDeck(PlayerFourDeck);

                listPlayerFourDeckList.Items.Add(String.Format(Deck_Owner_Text, PlayerFourName));
                listPlayerFourDeckList.Items.Add(Main_Deck_Title);
                listPlayerFourDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
                listPlayerFourDeckList.Items.Add(Extra_Deck_Title);
                listPlayerFourDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

                btnCopyDeckListPlayerFour.Text = String.Format(Copy_Deck_List_Text, PlayerFourName);
                btnSaveDeckListPlayerFour.Text = String.Format(Save_Deck_List_Text, PlayerFourName);


            }



            lbPlayer.Text = string.Empty;

            if (Replay.SingleMode)
            {
                lbMode.Text = (Mode_Single_or_Match_Text + Environment.NewLine);
                lbPlayer.Text += String.Format(Player1_Text, PlayerOneName, Environment.NewLine);
                lbPlayer.Text += String.Format(VS_Text, Environment.NewLine);
                lbPlayer.Text += String.Format(Player2_Text, PlayerTwoName, Environment.NewLine);
            }
            else
            {
                if (PlayerTwoName.Trim().Equals(string.Empty)) { lbMode.Text = Mode_1vs2_Text + Environment.NewLine; } //if 1vs2 mode
                else { lbMode.Text = Mode_Tag_Text + Environment.NewLine; }

                lbPlayer.Text += String.Format(Player1_Text, PlayerOneName, Environment.NewLine);
                if (!PlayerTwoName.Trim().Equals(string.Empty))
                { //if not 1vs2 mode
                    lbPlayer.Text += String.Format(Player2_Text, PlayerTwoName, Environment.NewLine);
                }
                lbPlayer.Text += String.Format(VS_Text, Environment.NewLine);
                lbPlayer.Text += String.Format(Player3_Text, PlayerThreeName, Environment.NewLine);
                lbPlayer.Text += String.Format(Player4_Text, PlayerFourName, Environment.NewLine);
            }
            Replay.DataReader.Close();
        }

        private void btnCopyDeckListPlayerOne_Click(object sender, EventArgs e)
        {
            if (PlayerOneDeck.Length > 0)
            {
                StringBuilder sbTmep = new StringBuilder();
                foreach (string value in listPlayerOneDeckList.Items)
                {
                    sbTmep.AppendLine(value);
                }
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show(Copied_To_Clipboard_Text, Information_Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerTwo_Click(object sender, EventArgs e)
        {
            if (PlayerTwoDeck.Length > 0)
            {
                StringBuilder sbTmep = new StringBuilder();
                foreach (string value in listPlayerTwoDeckList.Items)
                {
                    sbTmep.AppendLine(value);
                }
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show(Copied_To_Clipboard_Text, Information_Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerThree_Click(object sender, EventArgs e)
        {
            if (PlayerThreeDeck.Length > 0)
            {
                StringBuilder sbTmep = new StringBuilder();
                foreach (string value in listPlayerThreeDeckList.Items)
                {
                    sbTmep.AppendLine(value);
                }
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show(Copied_To_Clipboard_Text, Information_Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerFour_Click(object sender, EventArgs e)
        {
            if (PlayerFourDeck.Length > 0)
            {
                StringBuilder sbTmep = new StringBuilder();
                foreach (string value in listPlayerFourDeckList.Items)
                {
                    sbTmep.AppendLine(value);
                }
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show(Copied_To_Clipboard_Text, Information_Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveDeckListPlayerOne_Click(object sender, EventArgs e)
        {
            if (PlayerOneDeck.Length > 0)
            {
                // Configure save file dialog box
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(PlayerOneDeck.ToString()));
                }
            }
        }

        private void btnSaveDeckListPlayerTwo_Click(object sender, EventArgs e)
        {
            if (PlayerTwoDeck.Length > 0)
            {
                // Configure save file dialog box
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(PlayerTwoDeck.ToString()));
                }
            }
        }

        private void btnSaveDeckListPlayerThree_Click(object sender, EventArgs e)
        {
            if (PlayerThreeDeck.Length > 0)
            {
                // Configure save file dialog box
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(PlayerThreeDeck.ToString()));
                }
            }
        }

        private void btnSaveDeckListPlayerFour_Click(object sender, EventArgs e)
        {
            if (PlayerFourDeck.Length > 0)
            {
                // Configure save file dialog box
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(PlayerFourDeck.ToString()));
                }
            }
        }
    }
}
