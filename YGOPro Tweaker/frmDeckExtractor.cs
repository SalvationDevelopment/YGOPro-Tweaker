using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Windows.Forms;

using System.Data.SQLite;
using System.IO;

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

        public frmDeckExtractor()
        {
            InitializeComponent();
        }

        private void frmDeckExtractor_Load(object sender, EventArgs e)
        {
            resetPlayerName();
        }

        private void resetPlayerName()
        {
            btnCopyDeckListPlayerOne.Text = String.Format("Copy {0} Deck List", "?");
            btnSaveDeckListPlayerOne.Text = String.Format("Save {0} Deck List", "?");

            btnCopyDeckListPlayerTwo.Text = String.Format("Copy {0} Deck List", "?");
            btnSaveDeckListPlayerTwo.Text = String.Format("Save {0} Deck List", "?");

            btnCopyDeckListPlayerThree.Text = String.Format("Copy {0} Deck List", "?");
            btnSaveDeckListPlayerThree.Text = String.Format("Save {0} Deck List", "?");

            btnCopyDeckListPlayerFour.Text = String.Format("Copy {0} Deck List", "?");
            btnSaveDeckListPlayerFour.Text = String.Format("Save {0} Deck List", "?");
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
            // Normal
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
                conn.Close();
            }

            // Expansions
            string[] expansionsDatabaseFileList = Directory.GetFiles(Application.StartupPath + @"\expansions\live\", "*.cdb", SearchOption.TopDirectoryOnly);
            foreach (string exp in expansionsDatabaseFileList)
            {
                using (var conn = new SQLiteConnection(@"Data Source=expansions\live\" + Path.GetFileName(exp)))
                using (var cmd = conn.CreateCommand())
                {
                    try { conn.Open(); } catch { return string.Empty; }
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
                    conn.Close();
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
                    MessageBox.Show(String.Format("Replay is invalid or old version. Program can not find card from ID.{0}Card ID : {1}", Environment.NewLine, CardID.ToString()
                        ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                PlayerMainDeck.Add(CardID);

                //add number of card
                int index = PlayerMainDeckText.FindIndex(x => x.Contains(CardName));

                if (index > -1)
                {
                    int CardNumber = Convert.ToInt32(PlayerMainDeckText[index].Substring(PlayerMainDeckText[index].Length - 1, 1));

                    CardNumber++;

                    PlayerMainDeckText[index] = ((CardName == string.Empty ? "UNKNOW CARD": CardName) + " x" + CardNumber.ToString());
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
                    MessageBox.Show(String.Format("Replay is invalid or old version. Program can not find card from ID.{0}Card ID : {1}", Environment.NewLine, CardID.ToString()
                        ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                PlayerExtraDeck.Add(CardID);

                //add number of card
                int index = PlayerExtraDeckText.FindIndex(x => x.Contains(CardName));
                if (index > -1)
                {
                    int CardNumber = Convert.ToInt32(PlayerExtraDeckText[index].Substring(PlayerExtraDeckText[index].Length - 1, 1));

                    PlayerExtraDeckText[index] = ((CardName == string.Empty ? "UNKNOW CARD" : CardName) + " x" + (++CardNumber).ToString());
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


            lbStartLP.Text = (String.Format("Start Life Point : {0}", StartLP.ToString()));
            lbStartHand.Text = (String.Format("Start Hand : {0}", StartHand.ToString()));
            lbDrawFor.Text = (String.Format("Draw For : {0}", DrawFor.ToString()));

            listPlayerOneDeckList.Items.Clear();
            listPlayerTwoDeckList.Items.Clear();
            listPlayerThreeDeckList.Items.Clear();
            listPlayerFourDeckList.Items.Clear();

            ReadDeck(PlayerOneDeck);
            listPlayerOneDeckList.Items.Add(String.Format("{0} Deck", PlayerOneName));
            listPlayerOneDeckList.Items.Add("============ Main Deck ============");
            listPlayerOneDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
            listPlayerOneDeckList.Items.Add("============ Extra Deck ============");
            listPlayerOneDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

            btnCopyDeckListPlayerOne.Text = String.Format("Copy {0} Deck List", PlayerOneName);
            btnSaveDeckListPlayerOne.Text = String.Format("Save {0} Deck List", PlayerOneName);

            PlayerMainDeck.Clear();
            PlayerExtraDeck.Clear();

            ReadDeck(PlayerTwoDeck);
            listPlayerTwoDeckList.Items.Add(String.Format("{0} Deck", PlayerTwoName));
            listPlayerTwoDeckList.Items.Add("============ Main Deck ============");
            listPlayerTwoDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
            listPlayerTwoDeckList.Items.Add("============ Extra Deck ============");
            listPlayerTwoDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

            btnCopyDeckListPlayerTwo.Text = String.Format("Copy {0} Deck List", PlayerTwoName);
            btnSaveDeckListPlayerTwo.Text = String.Format("Save {0} Deck List", PlayerTwoName);

            if (!Replay.SingleMode)
            {
                PlayerMainDeck.Clear();
                PlayerExtraDeck.Clear();
                ReadDeck(PlayerThreeDeck);

                listPlayerThreeDeckList.Items.Add(String.Format("{0} Deck", PlayerThreeName));
                listPlayerThreeDeckList.Items.Add("============ Main Deck ============");
                listPlayerThreeDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
                listPlayerThreeDeckList.Items.Add("============ Extra Deck ============");
                listPlayerThreeDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

                btnCopyDeckListPlayerThree.Text = String.Format("Copy {0} Deck List", PlayerThreeName);
                btnSaveDeckListPlayerThree.Text = String.Format("Save {0} Deck List", PlayerThreeName);

                //
                PlayerMainDeck.Clear();
                PlayerExtraDeck.Clear();
                ReadDeck(PlayerFourDeck);

                listPlayerFourDeckList.Items.Add(String.Format("{0} Deck", PlayerFourName));
                listPlayerFourDeckList.Items.Add("============ Main Deck ============");
                listPlayerFourDeckList.Items.AddRange(PlayerMainDeckText.ToArray());
                listPlayerFourDeckList.Items.Add("============ Extra Deck ============");
                listPlayerFourDeckList.Items.AddRange(PlayerExtraDeckText.ToArray());

                btnCopyDeckListPlayerFour.Text = String.Format("Copy {0} Deck List", PlayerFourName);
                btnSaveDeckListPlayerFour.Text = String.Format("Save {0} Deck List", PlayerFourName);


            }



            lbPlayer.Text = string.Empty;

            if (Replay.SingleMode)
            {
                lbMode.Text = ("Mode : Single or Match" + Environment.NewLine);
                lbPlayer.Text += String.Format("Player 1 : {0}{1}", PlayerOneName, Environment.NewLine);
                lbPlayer.Text += String.Format("VS{0}", Environment.NewLine);
                lbPlayer.Text += String.Format("Player 2 : {0}{1}", PlayerTwoName, Environment.NewLine);
            }
            else
            {
                if (PlayerTwoName.Trim().Equals(string.Empty)) { lbMode.Text = "Mode : 1vs2" + Environment.NewLine; } //if 1vs2 mode
                else { lbMode.Text = "Mode : Tag" + Environment.NewLine; }

                lbPlayer.Text += String.Format("Player 1 : {0}{1}", PlayerOneName, Environment.NewLine);
                if (!PlayerTwoName.Trim().Equals(string.Empty))
                { //if not 1vs2 mode
                    lbPlayer.Text += String.Format("Player 2 : {0}{1}", PlayerTwoName, Environment.NewLine);
                }
                lbPlayer.Text += String.Format("VS{0}", Environment.NewLine);
                lbPlayer.Text += String.Format("Player 3 : {0}{1}", PlayerThreeName, Environment.NewLine);
                lbPlayer.Text += String.Format("Player 4 : {0}{1}", PlayerFourName, Environment.NewLine);
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
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
