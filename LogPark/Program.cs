using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace LogPark
{
    internal static class Program
    {
       

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string Connection = Properties.Settings.Default.Connection;
            string SaveLanguage = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);


            if ((bool)Properties.Settings.Default["FirstRun"] == true)
            {
                Properties.Settings.Default["FirstRun"] = false;
                Properties.Settings.Default.Save();
                DatabaseSettings databaseSettings = new DatabaseSettings();
                databaseSettings.ShowDialog();
            }
            else if (string.IsNullOrEmpty(Connection))
                {
                    DatabaseSettings databaseSettings = new DatabaseSettings();
                    databaseSettings.ShowDialog();
                }
                else
                {
                    Application.Run(new Login());
                }
            

        }
    }
}

