using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace YGOPro_Tweaker
{
    public partial class FrmDeckExtractor : Form
    {
        private readonly cReplayManager _replay = new cReplayManager();
        private StringBuilder _playerOneDeck = new StringBuilder();
        private StringBuilder _playerTwoDeck = new StringBuilder();
        private StringBuilder _playerThreeDeck = new StringBuilder();
        private StringBuilder _playerFourDeck = new StringBuilder();

        private readonly List<string> _playerMainDeck = new List<string>();
        private readonly List<string> _playerExtraDeck = new List<string>();
        private readonly List<string> _playerMainDeckText = new List<string>();
        private readonly List<string> _playerExtraDeckText = new List<string>();

        private string _playerOneName;
        private string _playerTwoName;
        private string _playerThreeName;
        private string _playerFourName;
        private int _startLp;
        private int _startHand;
        private int _drawFor;

        public FrmDeckExtractor()
        {
            InitializeComponent();
        }

        private void FrmDeckExtractor_Load(object sender, EventArgs e)
        {
            ResetPlayerName();
        }

        private void ResetPlayerName()
        {
            btnCopyDeckListPlayerOne.Text = "Copy ? Deck List";
            btnSaveDeckListPlayerOne.Text = "Save ? Deck List";

            btnCopyDeckListPlayerTwo.Text = "Copy ? Deck List";
            btnSaveDeckListPlayerTwo.Text = "Save ? Deck List";

            btnCopyDeckListPlayerThree.Text = "Copy ? Deck List";
            btnSaveDeckListPlayerThree.Text = "Save ? Deck List";

            btnCopyDeckListPlayerFour.Text = "Copy ? Deck List";
            btnSaveDeckListPlayerFour.Text = "Save ? Deck List";
        }

        private void ResetPlayerDeck()
        {
            _playerOneDeck = new StringBuilder();
            _playerTwoDeck = new StringBuilder();
            _playerThreeDeck = new StringBuilder();
            _playerFourDeck = new StringBuilder();
        }

        private void ReadBasicData()
        {
            _playerOneName = _replay.ExtractName(_replay.ReadString(40));
            _playerTwoName = _replay.ExtractName(_replay.ReadString(40));
            if (!_replay.SingleMode) //tag, etc
            {
                _playerFourName = _replay.ExtractName(_replay.ReadString(40));
                _playerThreeName = _replay.ExtractName(_replay.ReadString(40));
            }
            _startLp = _replay.DataReader.ReadInt32();
            _startHand = _replay.DataReader.ReadInt32();
            _drawFor = _replay.DataReader.ReadInt32();
            _replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture); //Player 1
        }

        private void ReadDeck(StringBuilder PlayerDeck)
        {
            var numberOfCard = _replay.DataReader.ReadInt32();
            _playerMainDeck.Clear();
            _playerMainDeckText.Clear();
            _playerExtraDeck.Clear();
            _playerExtraDeckText.Clear();


            //textBox1.Text += "----- Main -----" + Environment.NewLine;
            for (var i = 0; i < numberOfCard; i++)
            {
                var cardId = _replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture);
                var cardName = YGOProUtils.getInstance().GetCardName(Convert.ToInt32(cardId));
                if (cardName == "")
                {
                    MessageBox.Show($@"Replay is invalid or old version. Program can not find card from ID.{Environment.NewLine}Card ID : {cardId.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                _playerMainDeck.Add(cardId);

                //add number of card
                var index = _playerMainDeckText.FindIndex(x => x.Contains(cardName));

                if (index > -1)
                {
                    var cardNumber = Convert.ToInt32(_playerMainDeckText[index].Substring(_playerMainDeckText[index].Length - 1, 1));

                    cardNumber++;

                    _playerMainDeckText[index] = (cardName == string.Empty ? "UNKNOWN CARD" : cardName) + " x" + cardNumber.ToString();
                }
                else
                {
                    _playerMainDeckText.Add(cardName + " x1");
                }

            }


            numberOfCard = _replay.DataReader.ReadInt32();

            for (var i = 0; i < numberOfCard; i++)
            {
                var cardId = _replay.DataReader.ReadInt32().ToString(System.Globalization.CultureInfo.InvariantCulture);
                var cardName = YGOProUtils.getInstance().GetCardName(Convert.ToInt32(cardId));
                if (cardName == "")
                {
                    MessageBox.Show($@"Replay is invalid or old version. Program can not find card from ID.{Environment.NewLine}Card ID : {cardId.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                _playerExtraDeck.Add(cardId);

                //add number of card
                var index = _playerExtraDeckText.FindIndex(x => x.Contains(cardName));
                if (index > -1)
                {
                    var CardNumber = Convert.ToInt32(_playerExtraDeckText[index].Substring(_playerExtraDeckText[index].Length - 1, 1));

                    _playerExtraDeckText[index] = (cardName == string.Empty ? "UNKNOWN CARD" : cardName) + " x" + (++CardNumber).ToString();
                }
                else
                {
                    _playerExtraDeckText.Add(cardName + " x1");
                }
            }

            //sort card by id
            _playerMainDeck.Sort();
            _playerExtraDeck.Sort();
            _playerMainDeckText.Sort();
            _playerExtraDeckText.Sort();

            PlayerDeck.AppendLine("#created by YGOPro Tweaker");
            PlayerDeck.AppendLine("#main");

            foreach (var CardID in _playerMainDeck) PlayerDeck.AppendLine(CardID);
            PlayerDeck.AppendLine("#extra");
            foreach (var CardID in _playerExtraDeck) PlayerDeck.AppendLine(CardID);
            PlayerDeck.AppendLine("!side");
        }

        private void btnLoadDeck_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.InitialDirectory = Application.StartupPath + "\\replay";
            OFD.Filter = "YGOPro Replay Files (*.yrp)|*.yrp|All files (*.*)|*.*";
            OFD.FilterIndex = 1;
            OFD.RestoreDirectory = true;

            var ReplayPath = string.Empty;

            if (OFD.ShowDialog() == DialogResult.OK)
                try
                {
                    ReplayPath = OFD.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            else
                return;

            txtReplayPath.Text = ReplayPath;
            ResetPlayerName();
            ResetPlayerDeck();
            _replay.FromFile(ReplayPath);

            ReadBasicData();


            lbStartLP.Text = $"Start Life Point : {_startLp}";
            lbStartHand.Text = $"Start Hand : {_startHand}";
            lbDrawFor.Text = $"Draw For : {_drawFor}";

            listPlayerOneDeckList.Items.Clear();
            listPlayerTwoDeckList.Items.Clear();
            listPlayerThreeDeckList.Items.Clear();
            listPlayerFourDeckList.Items.Clear();

            ReadDeck(_playerOneDeck);
            listPlayerOneDeckList.Items.Add($"{_playerOneName} Deck");
            listPlayerOneDeckList.Items.Add("============ Main Deck ============");
            listPlayerOneDeckList.Items.AddRange(_playerMainDeckText.ToArray());
            listPlayerOneDeckList.Items.Add("============ Extra Deck ============");
            listPlayerOneDeckList.Items.AddRange(_playerExtraDeckText.ToArray());

            btnCopyDeckListPlayerOne.Text = $"Copy {_playerOneName} Deck List";
            btnSaveDeckListPlayerOne.Text = $"Save {_playerOneName} Deck List";

            _playerMainDeck.Clear();
            _playerExtraDeck.Clear();

            ReadDeck(_playerTwoDeck);
            listPlayerTwoDeckList.Items.Add($"{_playerTwoName} Deck");
            listPlayerTwoDeckList.Items.Add("============ Main Deck ============");
            listPlayerTwoDeckList.Items.AddRange(_playerMainDeckText.ToArray());
            listPlayerTwoDeckList.Items.Add("============ Extra Deck ============");
            listPlayerTwoDeckList.Items.AddRange(_playerExtraDeckText.ToArray());

            btnCopyDeckListPlayerTwo.Text = $"Copy {_playerTwoName} Deck List";
            btnSaveDeckListPlayerTwo.Text = $"Save {_playerTwoName} Deck List";

            if (!_replay.SingleMode)
            {
                _playerMainDeck.Clear();
                _playerExtraDeck.Clear();
                ReadDeck(_playerThreeDeck);

                listPlayerThreeDeckList.Items.Add($"{_playerThreeName} Deck");
                listPlayerThreeDeckList.Items.Add("============ Main Deck ============");
                listPlayerThreeDeckList.Items.AddRange(_playerMainDeckText.ToArray());
                listPlayerThreeDeckList.Items.Add("============ Extra Deck ============");
                listPlayerThreeDeckList.Items.AddRange(_playerExtraDeckText.ToArray());

                btnCopyDeckListPlayerThree.Text = $"Copy {_playerThreeName} Deck List";
                btnSaveDeckListPlayerThree.Text = $"Save {_playerThreeName} Deck List";

                //
                _playerMainDeck.Clear();
                _playerExtraDeck.Clear();
                ReadDeck(_playerFourDeck);

                listPlayerFourDeckList.Items.Add($"{_playerFourName} Deck");
                listPlayerFourDeckList.Items.Add("============ Main Deck ============");
                listPlayerFourDeckList.Items.AddRange(_playerMainDeckText.ToArray());
                listPlayerFourDeckList.Items.Add("============ Extra Deck ============");
                listPlayerFourDeckList.Items.AddRange(_playerExtraDeckText.ToArray());

                btnCopyDeckListPlayerFour.Text = $"Copy {_playerFourName} Deck List";
                btnSaveDeckListPlayerFour.Text = $"Save {_playerFourName} Deck List";


            }



            lbPlayer.Text = string.Empty;

            if (_replay.SingleMode)
            {
                lbMode.Text = "Mode : Single or Match" + Environment.NewLine;
                lbPlayer.Text += $"Player 1 : {_playerOneName}{Environment.NewLine}";
                lbPlayer.Text += $"VS{Environment.NewLine}";
                lbPlayer.Text += $"Player 2 : {_playerTwoName}{Environment.NewLine}";
            }
            else
            {
                if (_playerTwoName.Trim().Equals(string.Empty))
                    lbMode.Text = "Mode : 1vs2" + Environment.NewLine;
                else
                    lbMode.Text = "Mode : Tag" + Environment.NewLine;

                lbPlayer.Text += $"Player 1 : {_playerOneName}{Environment.NewLine}";
                if (!_playerTwoName.Trim().Equals(string.Empty))
                //if not 1vs2 mode
                    lbPlayer.Text += $"Player 2 : {_playerTwoName}{Environment.NewLine}";
                lbPlayer.Text += $"VS{Environment.NewLine}";
                lbPlayer.Text += $"Player 3 : {_playerThreeName}{Environment.NewLine}";
                lbPlayer.Text += $"Player 4 : {_playerFourName}{Environment.NewLine}";
            }
            _replay.DataReader.Close();
        }

        private void btnCopyDeckListPlayerOne_Click(object sender, EventArgs e)
        {
            if (_playerOneDeck.Length > 0)
            {
                var sbTmep = new StringBuilder();
                foreach (string value in listPlayerOneDeckList.Items) sbTmep.AppendLine(value);
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerTwo_Click(object sender, EventArgs e)
        {
            if (_playerTwoDeck.Length > 0)
            {
                var sbTmep = new StringBuilder();
                foreach (string value in listPlayerTwoDeckList.Items) sbTmep.AppendLine(value);
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerThree_Click(object sender, EventArgs e)
        {
            if (_playerThreeDeck.Length > 0)
            {
                var sbTmep = new StringBuilder();
                foreach (string value in listPlayerThreeDeckList.Items) sbTmep.AppendLine(value);
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopyDeckListPlayerFour_Click(object sender, EventArgs e)
        {
            if (_playerFourDeck.Length > 0)
            {
                var sbTmep = new StringBuilder();
                foreach (string value in listPlayerFourDeckList.Items) sbTmep.AppendLine(value);
                Clipboard.SetText(sbTmep.ToString());
                MessageBox.Show("Copied To Clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveDeckListPlayerOne_Click(object sender, EventArgs e)
        {
            if (_playerOneDeck.Length > 0)
            {
                // Configure save file dialog box
                var dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK) System.IO.File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(_playerOneDeck.ToString()));
            }
        }

        private void btnSaveDeckListPlayerTwo_Click(object sender, EventArgs e)
        {
            if (_playerTwoDeck.Length > 0)
            {
                // Configure save file dialog box
                var dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK) System.IO.File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(_playerTwoDeck.ToString()));
            }
        }

        private void btnSaveDeckListPlayerThree_Click(object sender, EventArgs e)
        {
            if (_playerThreeDeck.Length > 0)
            {
                // Configure save file dialog box
                var dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK) System.IO.File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(_playerThreeDeck.ToString()));
            }
        }

        private void btnSaveDeckListPlayerFour_Click(object sender, EventArgs e)
        {
            if (_playerFourDeck.Length > 0)
            {
                // Configure save file dialog box
                var dlg = new SaveFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\deck";
                dlg.FileName = "deck.ydk"; // Default file name
                dlg.DefaultExt = ".ydk"; // Default file extension
                dlg.Filter = "YGOPro Deck Files (.ydk)|*.ydk"; // Filter files by extension 

                if (dlg.ShowDialog() == DialogResult.OK) System.IO.File.WriteAllBytes(dlg.FileName, Encoding.UTF8.GetBytes(_playerFourDeck.ToString()));
            }
        }
    }
}
