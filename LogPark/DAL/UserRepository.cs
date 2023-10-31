using Dapper;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Windows.Forms;

namespace LogPark.DAL
{
    public class UserRepository
    {
        MyConnection db = new MyConnection();
        


      

        public void InsertUser(Users user)
        {
            db.con.Open();
           

            db.con.Execute("InsertUser", user, commandType: CommandType.StoredProcedure);

            db.con.Close();
        }


        public Users GetUserByUserName(string userName, string Password)
        {
            db.con.Open();
            var parameters = new
            {
                UserName = userName,
                password = Password
             };

            // string query = "SELECT UserName, Password, Profile FROM Users WHERE UserName=@UserName";
            var result = db.con.ExecuteReader("SelectUserName", parameters, commandType: CommandType.StoredProcedure);
            {
                while (result.Read())
                {
                    Users users = new Users();
                    {
                        users.UserName = userName;
                        users.Password = Password;
                        users.Profile = result.GetString(4);
                    };
                    db.con.Close();
                    return users;
                }
            }

        
          
            return null;


          
        }

    }
}

       
 



