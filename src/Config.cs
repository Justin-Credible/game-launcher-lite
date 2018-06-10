using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncherLite
{
    class Config
    {
        public static bool OverrideDimensions
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["OverrideDimensions"]);
            }
        }

        public static int Width
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Width"]);
            }
        }

        public static int Height
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Height"]);
            }
        }

        public static string Title
        {
            get
            {
                return ConfigurationManager.AppSettings["Title"];
            }
        }

        public static string Footer1
        {
            get
            {
                return ConfigurationManager.AppSettings["Footer1"];
            }
        }


        public static string Footer2
        {
            get
            {
                return ConfigurationManager.AppSettings["Footer2"];
            }
        }

        private static List<Game> _games = null;

        public static List<Game> Games
        {
            get
            {
                if (_games != null)
                    return _games;

                // https://stackoverflow.com/a/32637544

                var settingCollection = (NameValueCollection)ConfigurationManager.GetSection("Games");

                var keys = settingCollection.AllKeys;

                _games = new List<Game>();

                foreach (var key in keys)
                {
                    var rawValue = settingCollection[key];

                    var values = rawValue.Split('|');

                    bool isMAME = values[0] == "MAME";
                    string resource = values[1];

                    _games.Add(new Game()
                    {
                        Name = key,
                        IsMAME = isMAME,
                        Resource = resource
                    });
                }

                return _games;
            }
        }
    }
}
