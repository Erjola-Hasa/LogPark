using log4net;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));



        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string Connection =DataAccesLayer.Properties.Settings.Default.Connection;
            string SaveLanguage =Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);


            if ((bool)DataAccesLayer.Properties.Settings.Default["FirstRun"] == true)
            {
                DataAccesLayer.Properties.Settings.Default["FirstRun"] = false;
                DataAccesLayer.Properties.Settings.Default.Save();
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

