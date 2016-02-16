using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;

namespace JPB_Framework
{
    public class LoginPage
    {

        public static void GoTo()
        {
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://jpbstaging.azurewebsites.net");
        }

        public static LoginCommand LoginAs(string username)
        {
            return new LoginCommand(username);
        }

        public static bool IsAt()
        {
            
        }
    }

    public class LoginCommand
    {
        private string username;
        private string password;

        public LoginCommand(string username)
        {
            this.username = username;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Login()
        {
            throw new NotImplementedException();
        }
    }
}
