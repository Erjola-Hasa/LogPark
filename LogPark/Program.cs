using log4net;
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
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string Connection =DataAccesLayer.Properties.Settings.Default.Connection;
            string SaveLanguage =Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);


            if (string.IsNullOrEmpty(Connection))
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

