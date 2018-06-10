using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncherLite
{
    public partial class MainForm : Form
    {
        const int MAX_GAMES = 12;
        readonly Color ITEM_SELECTED_COLOR = Color.Red;
        readonly Color ITEM_UNSELECTED_COLOR = Color.White;

        private List<Game> _games = null;
        private int _selectedGameIndex = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        public string Title
        {
            set
            {
                this.TitleLabel.Text = value;
            }
        }

        public string Footer1
        {
            set
            {
                this.Footer1Label.Text = value;
            }
        }

        public string Footer2
        {
            set
            {
                this.Footer2Label.Text = value;
            }
        }

        public List<Game> Games
        {
            set
            {
                _games = value;
                BuildGameList();
                UpdateSelection();
            }
        }

        private void BuildGameList()
        {
            if (_games == null)
                _games = new List<Game>();

            for (var i = 0; i < MAX_GAMES; i++)
            {
                var label = (Label)Controls.Find($"Game{i+1}Label", false)[0];

                if (i >= _games.Count)
                {
                    label.Visible = false;
                }
                else
                {
                    label.Visible = true;

                    var game = _games[i];
                    label.Text = game.Name;
                }
            }
        }

        private void UpdateSelection()
        {
            for (var i = 0; i < MAX_GAMES; i++)
            {
                var label = (Label)Controls.Find($"Game{i+1}Label", false)[0];

                if (i == _selectedGameIndex)
                    label.ForeColor = ITEM_SELECTED_COLOR;
                else
                    label.ForeColor = ITEM_UNSELECTED_COLOR;
            }
        }
    }
}
