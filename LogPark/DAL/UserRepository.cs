using Dapper;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.RegSvrEnum;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime;
using System.Windows.Forms;

namespace LogPark.DAL
{

    public class UserRepository
    {
       // private string ConnectionString;

        //  MyConnection db = new MyConnection();
      //  Config df = new Config();
      
       string ConnectionString=Properties.Settings.Default.Connection;


       


        public void InsertUser(Users user)
        {
           
                using (var db = new SqlConnection(ConnectionString))
                {
                    db.Open();



                    db.Execute("InsertUser", user, commandType: CommandType.StoredProcedure);

                    db.Close();
                }

        }
        


        public Users GetUserByUserName(string userName, string Password)
        {
            if(ConnectionString == null)
            {
                DatabaseSettings dbSetting = new DatabaseSettings();
                dbSetting.ShowDialog();
            }
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                var parameters = new
                {
                    UserName = userName,
                    password = Password
                };

                // string query = "SELECT UserName, Password, Profile FROM Users WHERE UserName=@UserName";
                var result = db.ExecuteReader("SelectUserName", parameters, commandType: CommandType.StoredProcedure);
                {
                    while (result.Read())
                    {
                        Users users = new Users();
                        {
                            users.UserName = userName;
                            users.Password = Password;
                            users.Profile = result.GetString(4);
                        };
                        db.Close();
                        return users;
                    }
                }



                return null;



            }

        }

       
        
    }
}

       
 



