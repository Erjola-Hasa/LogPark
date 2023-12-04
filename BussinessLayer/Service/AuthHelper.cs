namespace LogPark
{
    public static class AuthHelper
    {


        private static string loggedInUserRole;
        /// <summary>
        /// Method to Set the Role of the users
        /// </summary>
        /// <param name="role"></param>
        public static void SetLoggedInUserRole(string role)
        {
            loggedInUserRole = role;
        }



        /// <summary>
        /// Get the role of the users 
        /// </summary>
        /// <returns></returns>
        public static string GetLoggedInUserRole()
        {
            return loggedInUserRole;
        }
    }



}