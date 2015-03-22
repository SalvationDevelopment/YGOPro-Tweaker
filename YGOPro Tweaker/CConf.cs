using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;

namespace YGOPro_Tweaker
{
    public class CConf
    {
        private string _filePath;

        public enum configVar
        {
            language,
            use_d3d,
            use_skin,
            antialias,
            errorlog,
            nickname,
            gamename,
            lastdeck,
            lastdeckai,
            lastscriptai,
            textfont,
            numfont,
            serverport,
            lastip,
            lastport,
            fullscreen,
            enable_sound,
            enable_music,
            random_card_placing,
            auto_card_placing,
            auto_chain_order,
            no_delay_for_chain,
            mute_opponent,
            mute_spectators,
            volume,
            background
        }

        public string readConfig(configVar config)
        {
            string text = File.ReadAllText(_filePath, Encoding.UTF8);
            Match match = Regex.Match(text, "(?m)^" + config.ToString() + "(.+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string result = match.Groups[1].Value.ToString(); //(?m) = multiline
                if (result.IndexOf("").Equals(0))
                { result = result.Remove(0, 1); }
                result = result.Replace("=", ""); //remove =
                return result.Trim();
            }
            return null;
        }

        public void writeConfig(configVar config, string value)
        {
            File.WriteAllText(_filePath, Regex.Replace(File.ReadAllText(_filePath),
                "(?m)^" + config.ToString() + "(=| .+|/ )$",
                config.ToString() + " = " + value));
        }

        public CConf(string filePath)
        {
            _filePath = filePath;
        }

        public string filePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public void changeFilePath(string filePath)
        {
            _filePath = filePath;
        }

        ~CConf() { }
    }
}
