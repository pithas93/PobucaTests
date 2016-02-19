using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class LoginPage
    {

        public static bool IsAt
        {
            get { return Driver.Instance.FindElement(By.Id("loginForm")).Displayed; }
        }

        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress);
            

        }

        public static LoginCommand LoginAs(string username)
        {
            return new LoginCommand(username);
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
            var userField = Driver.Instance.FindElement(By.Id("email"));
            var passField = Driver.Instance.FindElement(By.Id("pass"));
            var loginBtn = Driver.Instance.FindElement(By.Id("login_btn"));
            userField.SendKeys(username);
            passField.SendKeys(password);
            loginBtn.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
        }
    }
}
