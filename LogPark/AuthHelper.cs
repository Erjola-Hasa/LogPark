using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPark
{
    public static class AuthHelper
    {
        private static string loggedInUserRole;

        public static void SetLoggedInUserRole(string role)
        {
            loggedInUserRole = role;
        }

        public static string GetLoggedInUserRole()
        {
            return loggedInUserRole;
        }
    }
}