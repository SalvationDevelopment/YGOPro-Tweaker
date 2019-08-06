using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace YGOPro_Tweaker
{
    public class YGOProUtils
    {
        private static YGOProUtils INSTANCE = null;
        private Dictionary<int, string> _CardIdToNameCache = new Dictionary<int, string>();

        private YGOProUtils() { }

        public static YGOProUtils getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new YGOProUtils();
            }

            return INSTANCE;
        }

        public string GetCardName(int CardId)
        {
            if (_CardIdToNameCache.ContainsKey(CardId))
            {
                return _CardIdToNameCache[CardId];
            }

            var CardName = _GetCardName(CardId);
            _CardIdToNameCache[CardId] = CardName;

            return CardName;
        }

        private string _GetCardName(int CardID)
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
            string[] expansionDirectories = Directory.GetDirectories(Application.StartupPath + @"\expansions\");
            foreach (string directory in expansionDirectories)
            {
                string[] expansionsDatabaseFileList = Directory.GetFiles(directory, "*.cdb", SearchOption.TopDirectoryOnly);
                foreach (string exp in expansionsDatabaseFileList)
                {
                    using (var conn = new SQLiteConnection(@"Data Source=" + Path.Combine(directory, exp)))
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
            }


            return string.Empty;
        }
    }
}
