
using DataAccesLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class ConfigService
    {

        /// <summary>
        /// Initializes a new instance of the ConfigRepository class 
        /// </summary>
        ConfigRepository _config = new ConfigRepository();


        /// <summary>
        /// Connect with Database with specifik ServerName,DatabaseName,UserId and Password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
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






        /// <summary>
        /// Method to Test if CoonectionString  is correct or not 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
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
                File.AppendAllText("error.log", ex.ToString());
                MessageBox.Show("An error has occurred. Please verify you credintial.");
                //MessageBox.Show(ex.ToString());
            }
           
                return false;
            
        }

           
    }
}
