
using LogPark.DAL;
using System;

namespace LogPark.BLL
{
    public class ConfigService
    {

        public bool ConnectToDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

           ConfigRepository _config= new ConfigRepository();
            bool IsConneted = _config.ConnectDatabase(UserId,Password,ServerName,DatabaseName);
            if (IsConneted == true)
            {


                return true;
            }
            else
            {
                return false;
            }


        }

        public bool TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

            ConfigRepository _config = new ConfigRepository();
             bool IsConneted=  _config.TestDatabase(UserId,Password,ServerName,DatabaseName);
            if (IsConneted == true)
            {


                return true;
            }
            else
            {
                return false;
            }
        }

           
    }
}
