using DataAccesLayer;
using System;
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
            
               bool IsConneted = _config.ConnectDatabase(UserId, Password, ServerName, DatabaseName);
                if (IsConneted == true)
                {


                    return true;
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
           bool IsConneted = _config.TestDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConneted == true)
            {
                MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            return false;

        }

           
    }
}
