
using DataAccesLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class ConfigService
    {
        ConfigRepository _config = new ConfigRepository();

        public bool ConnectToDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            try
            {
               
                bool IsConneted = _config.ConnectDatabase(UserId, Password, ServerName, DatabaseName);
                if (IsConneted == true)
                {


                    return true;
                }
               
            }
              catch (Exception ex) 
            {
               MessageBox.Show(ex.ToString());
    
            }

            return false;


        }

        public bool TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            try
            {
                bool IsConneted = _config.TestDatabase(UserId, Password, ServerName, DatabaseName);
                if (IsConneted == true)
                {


                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
                return false;
            
        }

           
    }
}
