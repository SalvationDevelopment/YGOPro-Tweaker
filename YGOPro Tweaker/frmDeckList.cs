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
    public partial class frmDeckList : Form
    {
        public frmDeckList()
        {
            InitializeComponent();
        }

        private void frmDeckList_Load(object sender, EventArgs e)
        {
            pbCard.ImageLocation = Application.StartupPath + "\\textures\\cover.jpg";
        }

        string createdBy = string.Empty;

        List<int> PlayerMainDeck = new List<int>();
        List<int> PlayerExtraDeck = new List<int>();
        List<int> PlayerSideDeck = new List<int>();

        List<string> PlayerMainDeckText = new List<string>();
        List<string> PlayerExtraDeckText = new List<string>();
        List<string> PlayerSideDeckText = new List<string>();
        //////////////////////////////////////////////////////

        private void btnLoadDeck_Click(object sender, EventArgs e)
        {
            PlayerMainDeck.Clear();
            PlayerExtraDeck.Clear();
            PlayerSideDeck.Clear();
            PlayerMainDeckText.Clear();
            PlayerExtraDeckText.Clear();
            PlayerSideDeckText.Clear();

            if (getCardID())
            {
                clearDeck();
                getCardText(PlayerMainDeck, PlayerMainDeckText, mainDeck);
                getCardText(PlayerExtraDeck, PlayerExtraDeckText, extraDeck);
                getCardText(PlayerSideDeck, PlayerSideDeckText, sideDeck);
                printList();
                pbCard.ImageLocation = Application.StartupPath + "\\textures\\cover.jpg";
            }
        }

        private void clearDeck()
        {
            PlayerMainDeckText.Clear(); mainDeck.Clear();
            PlayerExtraDeckText.Clear(); extraDeck.Clear();
            PlayerSideDeckText.Clear(); sideDeck.Clear();
        }
        

        private void printList()
        {
            mainDeck.Insert(0, new cardData(-1, String.Format("============ Main Deck ============", PlayerMainDeck.Count.ToString())));
            extraDeck.Insert(0, new cardData(-2, String.Format("============ Extra Deck ============", PlayerExtraDeck.Count.ToString())));
            sideDeck.Insert(0, new cardData(-3, String.Format("============ Side Deck ============", PlayerSideDeck.Count.ToString())));

            allCard = new List<cardData>();
            allCard.AddRange(mainDeck);
            allCard.AddRange(extraDeck);
            allCard.AddRange(sideDeck);

            listDeckList.DataSource = allCard;
            listDeckList.DisplayMember = "Name";
            listDeckList.ValueMember = "ID";

            listDeckList.SelectedValueChanged -= new EventHandler(setPicture);
            listDeckList.SelectedValueChanged += new EventHandler(setPicture);
            listDeckList.Refresh();
        }

        private void setPicture(object sender, EventArgs e)
        {
            if (listDeckList.SelectedIndex != -1
                && (int)listDeckList.SelectedValue != -1
                && (int)listDeckList.SelectedValue != -2
                && (int)listDeckList.SelectedValue != -3
                )
            {

                pbCard.ImageLocation = String.Format(@"{0}\pics\{1}.jpg", Application.StartupPath, listDeckList.SelectedValue.ToString());
            }
        }

        class cardData
        {
            public cardData(int id, string name)
            {
                ID = id;
                Name = name;
            }

            public string Name { get; set; }
            public int ID { get; set; }
        }

        List<cardData> mainDeck = new List<cardData>();
        List<cardData> extraDeck = new List<cardData>();
        List<cardData> sideDeck = new List<cardData>();
        List<cardData> allCard = new List<cardData>();

        private void getCardText(List<int> Deck, List<string> DeckText, List<cardData> CardData)
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                string CardID = Deck[i].ToString();
                string CardName = GetCardName(Convert.ToInt32(CardID));
                if (CardName == "")
                {
                    MessageBox.Show(String.Format("Deck is invalid or old version. Program can not find card from ID.{0}Card ID : {1}", Environment.NewLine, CardID.ToString()
                        ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                //add number of card
                int index = DeckText.FindIndex(x => x.Contains(CardName));

                if (index > -1)
                {
                    int CardNumber = Convert.ToInt32(DeckText[index].Substring(DeckText[index].Length - 1, 1));

                    CardNumber++;
                    DeckText[index] = (CardName + " x" + CardNumber.ToString());

                    CardData[index] = (new cardData(Convert.ToInt32(CardID), (CardName + " x" + CardNumber.ToString())));
                    //CardData.Add(new cardData(Convert.ToInt32(CardID), (CardName + " x" + CardNumber.ToString())));
                }
                else
                {
                    DeckText.Add(CardName + " x1");
                    CardData.Add(new cardData(Convert.ToInt32(CardID), CardName + " x1"));
                }

            }
        }

        private bool getCardID()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.InitialDirectory = Application.StartupPath + "\\deck";
            OFD.Filter = "YGOPro Deck Files (*.ydk)|*.ydk|All files (*.*)|*.*";
            OFD.FilterIndex = 1;
            OFD.RestoreDirectory = true;

            string DeckPath = string.Empty;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DeckPath = OFD.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            else
            {
                return false;
            }

            txtDeckPath.Text = DeckPath;

            string[] lines = System.IO.File.ReadAllLines(DeckPath);
            createdBy = lines[0];
            int flag = 0; // 0 = Main Deck, 1 = Extra Deck, 2 = Side Deck
            int resultParse = 0;
            foreach (string line in lines)
            {
                switch (line)
                {
                    case "#main": flag = 0; break;
                    case "#extra": flag = 1; break;
                    case "!side": flag = 2; break;
                    default: if (int.TryParse(line, out resultParse))
                        {
                            switch (flag)
                            {
                                case 0: PlayerMainDeck.Add(resultParse); break;
                                case 1: PlayerExtraDeck.Add(resultParse); break;
                                case 2: PlayerSideDeck.Add(resultParse); break;
                            }
                        }
                        break;
                }
            }
            return true;
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
            using (var conn = new SQLiteConnection(@"Data Source=expansions\live\prerelease.cdb"))
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
            return string.Empty;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (allCard.Count > 0)
            {
                Clipboard.SetText(getListString());
                MessageBox.Show("Copied To Clipboard.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveToTextFile_Click(object sender, EventArgs e)
        {
            if (allCard.Count > 0)
            {
                SaveFileDialog sfg = new SaveFileDialog();
                sfg.FileName = "decklist.txt"; // Default file name
                sfg.DefaultExt = ".txt"; // Default file extension
                sfg.Filter = "Text File (.txt)|*.txt"; // Filter files by extension 

                if (sfg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(sfg.FileName, Encoding.UTF8.GetBytes(getListString()));
                }
            }
        }

        private string getListString()
        {
            StringBuilder sbdTemp = new StringBuilder();
            foreach (cardData cards in allCard)
            {
                sbdTemp.AppendLine(cards.Name);
            }
            return sbdTemp.ToString();
        }
    }
}
