using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncherLite
{
    class Config
    {
        private static List<Game> _games = null;

        private static Keys? _up;
        private static Keys? _down;
        private static Keys? _select;

        private static Color? _titleColor = null;
        private static Color? _waitingTextColor = null;
        private static Color? _itemUnselectedColor = null;
        private static Color? _itemSelectedColor = null;
        private static Color? _footer1Color= null;
        private static Color? _footer2Color = null;

        public static AutoScaleMode AutoScaleMode
        {
            get
            {
                if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["AutoScaleMode"]))
                    return AutoScaleMode.None;
                else
                    return (AutoScaleMode)Enum.Parse(typeof(AutoScaleMode), ConfigurationManager.AppSettings["AutoScaleMode"]);
            }
        }

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

        public static string WaitingText
        {
            get
            {
                return ConfigurationManager.AppSettings["WaitingText"];
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

        public static Color TitleColor
        {
            get
            {
                if (_titleColor.HasValue)
                    return _titleColor.Value;

                _titleColor = Color.FromName(ConfigurationManager.AppSettings["TitleColor"]);

                return _titleColor.Value;
            }
        }

        public static Color WaitingTextColor
        {
            get
            {
                if (_waitingTextColor.HasValue)
                    return _waitingTextColor.Value;

                _waitingTextColor = Color.FromName(ConfigurationManager.AppSettings["WaitingTextColor"]);

                return _waitingTextColor.Value;
            }
        }

        public static Color ItemUnselectedColor
        {
            get
            {
                if (_itemUnselectedColor.HasValue)
                    return _itemUnselectedColor.Value;

                _itemUnselectedColor = Color.FromName(ConfigurationManager.AppSettings["ItemUnselectedColor"]);

                return _itemUnselectedColor.Value;
            }
        }

        public static Color ItemSelectedColor
        {
            get
            {
                if (_itemSelectedColor.HasValue)
                    return _itemSelectedColor.Value;

                _itemSelectedColor = Color.FromName(ConfigurationManager.AppSettings["ItemSelectedColor"]);

                return _itemSelectedColor.Value;
            }
        }

        public static Color Footer1Color
        {
            get
            {
                if (_footer1Color.HasValue)
                    return _footer1Color.Value;

                _footer1Color = Color.FromName(ConfigurationManager.AppSettings["Footer1Color"]);

                return _footer1Color.Value;
            }
        }

        public static Color Footer2Color
        {
            get
            {
                if (_footer2Color.HasValue)
                    return _footer2Color.Value;

                _footer2Color = Color.FromName(ConfigurationManager.AppSettings["Footer2Color"]);

                return _footer2Color.Value;
            }
        }

        public static string MAME
        {
            get
            {
                return ConfigurationManager.AppSettings["MAME"];
            }
        }

        public static string MAME_Arguments
        {
            get
            {
                return ConfigurationManager.AppSettings["MAME_Arguments"];
            }
        }

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

        public static Keys Up
        {
            get
            {
                if (_up.HasValue)
                    return _up.Value;

                _up = (Keys)Enum.Parse(typeof(Keys), ConfigurationManager.AppSettings["Up"]);

                return _up.Value;
            }
        }

        public static Keys Down
        {
            get
            {
                if (_down.HasValue)
                    return _down.Value;

                _down = (Keys)Enum.Parse(typeof(Keys), ConfigurationManager.AppSettings["Down"]);

                return _down.Value;
            }
        }

        public static Keys Select
        {
            get
            {
                if (_select.HasValue)
                    return _select.Value;

                _select = (Keys)Enum.Parse(typeof(Keys), ConfigurationManager.AppSettings["Select"]);

                return _select.Value;
            }
        }

        public static Game AutoStartGame
        {
            get
            {
                var rawValue = ConfigurationManager.AppSettings["AutoStartGame"];

                if (String.IsNullOrEmpty(rawValue))
                    return null;

                var index = int.Parse(rawValue);

                return Games[index];
            }
        }

        public static int? AutoStartOnIdle
        {
            get
            {
                var rawValue = ConfigurationManager.AppSettings["AutoStartOnIdle"];

                if (String.IsNullOrEmpty(rawValue))
                    return null;

                return int.Parse(rawValue);
            }
        }
    }
}
