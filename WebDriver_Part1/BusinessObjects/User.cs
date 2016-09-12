using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriver_Part1.Utils;

namespace WebDriver_Part1.BusinessObjects
{
    class User
    {
        private const string _defaultUser = "default";
        private string _login;
        private string _password;
        private string _domain;
        
        public User(string username = _defaultUser)
        {
            string[] userFields = ConfigParser.ParseUser(username);
            Login = userFields[0];
            Password = userFields[1];
            Domain = userFields[2];
        }

        public string Login
        {
            private set { _login = value; }
            get { return _login; }
        }

        public string Password
        {
            private set { _password = value; }
            get { return _password; }
        }

        public string Domain
        {
            private set { _domain = value; }
            get { return _domain; }
        }
    }
}
