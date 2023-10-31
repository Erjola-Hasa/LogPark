using LogPark.BLL;
using Microsoft.VisualBasic;
using System;

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
            
            Application.Run(new Login());


        }
    }
}

