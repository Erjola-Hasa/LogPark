using DataAccesLayer;

using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class UserService
    {
        
        public void InsertUser(string firstName, string lastName, string userName, string password, string profile)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
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

        public Users AuthenticateUser(string userName, string password)
        {
            UserRepository userRepository = new UserRepository();
            Users user = userRepository.GetUserByUserName(userName, password);
            return user;

        }
      
    }

}
