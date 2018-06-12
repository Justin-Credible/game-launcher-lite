using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace GameLauncherLite
{
    public partial class MainForm : Form
    {
        const int MAX_GAMES = 12;

        private List<Game> _games = null;

        private int _selectedGameIndex = 0;

        private bool _waiting = false;

        private System.Timers.Timer _timer;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Properties

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
                Render();
            }
        }

        #endregion

        #region Private Methods

        private void Render()
        {
            if (_waiting)
            {
                TitleLabel.Visible = false;
                Footer1Label.Visible = false;
                Footer2Label.Visible = false;

                for (var i = 0; i < MAX_GAMES; i++)
                {
                    var label = (Label)Controls.Find($"Game{i + 1}Label", false)[0];
                    label.Visible = false;
                }

                var game = Config.Games[_selectedGameIndex];

                Game4Label.Visible = true;
                Game4Label.Text = game.Name;
                Game4Label.ForeColor = Config.WaitingTextColor;

                Game6Label.Visible = true;
                Game6Label.Text = Config.WaitingText;
                Game6Label.ForeColor = Config.WaitingTextColor;
            }
            else
            {
                TitleLabel.Visible = true;
                Footer1Label.Visible = true;
                Footer2Label.Visible = true;

                TitleLabel.ForeColor = Config.TitleColor;
                Footer1Label.ForeColor = Config.Footer1Color;
                Footer2Label.ForeColor = Config.Footer2Color;

                for (var i = 0; i < MAX_GAMES; i++)
                {
                    var label = (Label)Controls.Find($"Game{i + 1}Label", false)[0];

                    if (i >= _games.Count)
                    {
                        label.Visible = false;
                        continue;
                    }
                    else
                    {
                        label.Visible = true;

                        var game = _games[i];
                        label.Text = game.Name;
                    }

                    if (i == _selectedGameIndex)
                        label.ForeColor = Config.ItemSelectedColor;
                    else
                        label.ForeColor = Config.ItemUnselectedColor;
                }
            }
        }

        private void LaunchGame(Game game)
        {
            _timer.Stop();
            _waiting = true;

            this.Render();

            var binary = game.IsMAME ? Config.MAME : game.Resource;
            var arguments = game.IsMAME ? game.Resource : null;

            if (game.IsMAME && !String.IsNullOrEmpty(Config.MAME_Arguments))
                arguments += " " + Config.MAME_Arguments;

            var workingDirectory = Path.GetDirectoryName(binary);

            var processStartInfo = new ProcessStartInfo()
            {
                FileName = binary,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
            };

            var process = new Process
            {
                StartInfo = processStartInfo,
            };

            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;

            process.Start();
            process.WaitForExit();
        }

        private void RestartTimerIfApplicable()
        {
            _timer.Stop();

            if (Config.AutoStartOnIdle != null)
                _timer.Start();
        }

        #endregion

        #region Event Handlers

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_waiting)
                return;

            if (e.KeyCode == Config.Up)
            {
                RestartTimerIfApplicable();

                if (_selectedGameIndex == 0)
                    return;

                _selectedGameIndex--;
                Render();
            }
            else if (e.KeyCode == Config.Down)
            {
                RestartTimerIfApplicable();

                if (_selectedGameIndex >= (Config.Games.Count - 1))
                    return;

                _selectedGameIndex++;
                Render();
            }
            else if (e.KeyCode == Config.Select)
            {
                var game = Config.Games[_selectedGameIndex];
                LaunchGame(game);
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                // Sanity check; this shouldn't be possible unless there is a race condition between threads.
                if (_waiting)
                    return;

                if (Config.AutoStartGame != null)
                    LaunchGame(Config.AutoStartGame);
            });
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _timer = new System.Timers.Timer
            {
                AutoReset = false
            };

            _timer.Elapsed += Timer_Elapsed;

            if (Config.AutoStartOnIdle != null)
                _timer.Interval = Config.AutoStartOnIdle.Value * 1000;

            if (Config.AutoStartGame != null)
            {
                LaunchGame(Config.AutoStartGame);
            }
            else if (Config.AutoStartOnIdle != null)
            {
                _timer.Start();
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                Render();
                _waiting = false;

                if (Config.AutoStartOnIdle != null)
                    _timer.Start();
            });
        }

        #endregion
    }
}
