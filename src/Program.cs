using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncherLite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new MainForm();

            if (Config.OverrideDimensions)
            {
                form.Width = Config.Width;
                form.Height = Config.Height;
            }

            form.Title = Config.Title;
            form.Footer1 = Config.Footer1;
            form.Footer2 = Config.Footer2;
            form.Games = Config.Games;

            Application.Run(form);
        }
    }
}
