using DataAccesLayer;
using log4net;
using System;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class UserService
    {

        /// <summary>
        /// Initializes a new instance of the UserRepository class
        /// </summary>
        UserRepository userRepository = new UserRepository();


        /// <summary>
        ///  Define a static logger variable
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(UserService));




        /// <summary>
        /// Insert the User in the database with the provided firstname,lastname,userName,password and role
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
                 log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please try again .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        /// <summary>
        /// Retrive the users by username and password  from database  and validate if exist in database or not
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users AuthenticateUser(string userName, string password)
        {
            try
            {
                Users user = userRepository.GetUserByUserName(userName, password);
                if (user != null)
                {

                    if (user.UserName == userName && user.Password == password)
                    {
                        return user;
                    }
                   
                    else
                        return null;

                }
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please verify your credentials.");
            }
            return null;
        }
    }
}