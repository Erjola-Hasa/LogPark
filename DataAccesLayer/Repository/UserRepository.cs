using Dapper;
using System.Data;
using System.Data.SqlClient;



namespace DataAccesLayer
{

    public class UserRepository
    {
        // private string ConnectionString;

        //  MyConnection db = new MyConnection();
        //  Config df = new Config();

        string ConnectionString = Properties.Settings.Default.Connection;

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
            ///<summary>
            /// Select User by UserName and password
            ///</summary>

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






