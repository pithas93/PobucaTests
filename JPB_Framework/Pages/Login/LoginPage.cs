using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
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
            try
            {
                var userField = Driver.Instance.FindElement(By.Id("email"));
                var passField = Driver.Instance.FindElement(By.Id("pass"));
                var loginBtn = Driver.Instance.FindElement(By.Id("login_btn"));
                userField.SendKeys(username);
                passField.SendKeys(password);
                loginBtn.Click();
            }
            catch (NoSuchElementException)
            {
               // In case that browser navigates directly to Contacts List Page, it 's not required to login, so do nothing
            }

            // wait for organization list to load
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (WebDriverTimeoutException e)
            {
                //Report.ToLogFile(MessageType.Message, $"Failed to login or did take too long. \n\t{e.Source} \n\t{e.Message} \n\t{e.StackTrace}");
                Report.ToLogFile(MessageType.Exception, "Failed to login or did take too long.", e);
                throw e;

            }
        }
    }
}
