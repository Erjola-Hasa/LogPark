
using LogPark.DAL;
using System;

namespace LogPark.BLL
{
    public class ConfigService
    {
       

        public ConfigService()
        {
           
        }


        public bool ConnectToDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

           ConfigRepository _config= new ConfigRepository();
            _config.ConnectDatabase(UserId,Password,ServerName,DatabaseName);
             return true;
           


        }

        public bool TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

            ConfigRepository _config = new ConfigRepository();
            _config.ConnectDatabase(UserId,Password,ServerName,DatabaseName); 
            return true;
        }
           
    }
}
