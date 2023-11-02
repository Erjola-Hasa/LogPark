using LogPark.BLL;
using LogPark.DAL;
using Microsoft.VisualBasic;
using System;
using System.Configuration;
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

           
            if ((bool)Properties.Settings.Default["FirstRun"] == true)
            {
                Properties.Settings.Default["FirstRun"] = false;
                //Save setting
                Properties.Settings.Default.Save();

               
                DatabaseSettings databaseSettings = new DatabaseSettings();
                databaseSettings.Show();
            }
            else
            {
                Application.Run(new Login());
            }


        }
    }
}

