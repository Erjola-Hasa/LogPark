
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

           ConfigService _config= new ConfigService();
            _config.ConnectToDatabase(UserId,Password,ServerName,DatabaseName);
             return false;
           


        }

        public bool TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

            ConfigService _config=new ConfigService();
            _config.ConnectToDatabase(UserId,Password,ServerName,DatabaseName); 
            return false;
        }
           
    }
}
