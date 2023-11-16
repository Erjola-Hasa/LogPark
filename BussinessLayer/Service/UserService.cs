using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class UserService
    {
        
        /// <summary>
    /// Dipendency Injection UserRepostiory
    /// </summary>
        UserRepository userRepository = new UserRepository();




        /// <summary>
        /// Method to insert User an take Validation about User
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="profile"></param>
        public void InsertUser(string firstName, string lastName, string userName, string password, string profile)
        {
            try
            {
              
                Users existingUser = userRepository.GetUserByUserName(userName, password);
                if (existingUser != null)
                {
                    MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(profile))
                {
                    MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {

                    Users user = new Users
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = userName,
                        Password = Cryptography.Encrypt(password.ToString()),
                        Profile = profile
                    };


                    userRepository.InsertUser(user);
                    MessageBox.Show("Your Account is created ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }






        /// <summary>
        /// Method to Authenticake Users and call the repository method "GetUserByUserName"
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users AuthenticateUser(string userName, string password)
        {
            try
            {
                Users user = userRepository.GetUserByUserName(userName, password);
                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;

        }

    }

}
