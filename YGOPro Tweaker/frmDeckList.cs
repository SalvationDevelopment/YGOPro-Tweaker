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
        int currentLanguage = 0; //0 = English, 1 = Thai
        public frmDeckList(int Lenguage)
        {
            InitializeComponent();
            currentLanguage = Lenguage;
        }

        private void frmDeckList_Load(object sender, EventArgs e)
        {
            switch (currentLanguage)
            {
                case 0: //English
                    CLanguage.DeckList.English englishLanguage = new CLanguage.DeckList.English();
                    Main_Deck_Text = englishLanguage.Main_Deck_Title;
                    Extra_Deck_Text = englishLanguage.Extra_Deck_Title;
                    Side_Deck_Text = englishLanguage.Side_Deck_Title;
                    Error_Text = englishLanguage.Error_Text;
                    Deck_Error_Text = englishLanguage.Deck_Error_Text;
                    Load_Deck_Text = englishLanguage.Load_Deck_Text;
                    Copy_To_Clipboard_Text = englishLanguage.Copy_To_Clipboard_Text;
                    Copied_To_Clipboard_Text = englishLanguage.Copied_To_Clipboard_Text;
                    Information_Text = englishLanguage.Information_Text;
                    this.Text = englishLanguage.Title;
                    break;
                case 1: //Thai
                    CLanguage.DeckList.Thai thaiLanguage = new CLanguage.DeckList.Thai();
                    Main_Deck_Text = thaiLanguage.Main_Deck_Title;
                    Extra_Deck_Text = thaiLanguage.Extra_Deck_Title;
                    Side_Deck_Text = thaiLanguage.Side_Deck_Title;
                    Error_Text = thaiLanguage.Error_Text;
                    Deck_Error_Text = thaiLanguage.Deck_Error_Text;
                    Load_Deck_Text = thaiLanguage.Load_Deck_Text;
                    Copy_To_Clipboard_Text = thaiLanguage.Copy_To_Clipboard_Text;
                    Copied_To_Clipboard_Text = thaiLanguage.Copied_To_Clipboard_Text;
                    Information_Text = thaiLanguage.Information_Text;
                    this.Text = thaiLanguage.Title;
                    break;
                default: break;
            }
            pbCard.ImageLocation = Application.StartupPath + "\\textures\\cover.jpg";
            btnLoadDeck.Text = Load_Deck_Text;
            btnCopyToClipboard.Text = Copy_To_Clipboard_Text;
        }

        string createdBy = string.Empty;

        List<int> PlayerMainDeck = new List<int>();
        List<int> PlayerExtraDeck = new List<int>();
        List<int> PlayerSideDeck = new List<int>();

        List<string> PlayerMainDeckText = new List<string>();
        List<string> PlayerExtraDeckText = new List<string>();
        List<string> PlayerSideDeckText = new List<string>();
        //////////////////////////////////////////////////////
        string Deck_Error_Text = string.Empty;
        string Error_Text = string.Empty;

        string Load_Deck_Text = string.Empty;
        string Copy_To_Clipboard_Text = string.Empty;
        string Copied_To_Clipboard_Text = string.Empty;
        string Information_Text = string.Empty;

        string Main_Deck_Text = string.Empty;
        string Extra_Deck_Text = string.Empty;
        string Side_Deck_Text = string.Empty;

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
            mainDeck.Insert(0, new cardData(-1, String.Format(Main_Deck_Text, PlayerMainDeck.Count.ToString())));
            extraDeck.Insert(0, new cardData(-2, String.Format(Extra_Deck_Text, PlayerExtraDeck.Count.ToString())));
            sideDeck.Insert(0, new cardData(-3, String.Format(Side_Deck_Text, PlayerSideDeck.Count.ToString())));

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
                    MessageBox.Show(String.Format(Deck_Error_Text, Environment.NewLine, CardID.ToString()
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

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (allCard.Count > 0)
            {
                Clipboard.SetText(getListString());
                MessageBox.Show(Copied_To_Clipboard_Text, Information_Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
