using DataAccesLayer;
using log4net;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class ConfigService
    {

        /// <summary>
        /// Define a private  _config variable
        /// </summary>
        private ConfigRepository _config;
        /// <summary>
        /// Initializes a new instance of the ConfigRepository  
        /// </summary>
     
        public ConfigService()
        {
            _config = new ConfigRepository();
        }

        /// <summary>
        ///  Define a static logger variable
        /// </summary>

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigService));

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
                    MessageBox.Show("Your Connection is Successful ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }catch(Exception ex)
            
            {
                log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)

            {
                log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return false;

        }

           
    }
}
