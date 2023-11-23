using Dapper;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DataAccesLayer
{

    public class UserRepository
    {
        
        /// <summary>
        ///   ConnectionString 
        /// </summary>
        string ConnectionString = Properties.Settings.Default.Connection;






        /// <summary>
        /// Add a new User
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(Users user)
        {
           
                using (var db = new SqlConnection(ConnectionString))
                {
                    db.Open();
                    db.Execute("InsertUser", user, commandType: CommandType.StoredProcedure);
                    db.Close();
                }
            
           

        }






        /// <summary>
        /// Method to select user by  name and password if are correct or no
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>

        public Users GetUserByUserName(string userName, string Password)
        {
           
                using (var db = new SqlConnection(ConnectionString))
                {
                    db.Open();

                    var parameters = new
                    {
                        UserName = userName,
                        Password = Password
                    };
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

                }
            
         
            return null;

        }



    }
}






