using System;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages.Login
{
    public class LoginPage
    {
        public static string BaseAddress => "https://pobucaapp-staging.azurewebsites.net";

        public static bool IsAt => Driver.Instance.FindElement(By.Id("loginForm")).Displayed;

        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        public static LoginCommand LoginAs(string username)
        {
            return new LoginCommand(username);
        }


    }

    public class LoginCommand
    {
        private readonly string username;
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

            Driver.Wait(TimeSpan.FromSeconds(5));

            if (!ContactsPage.IsAt)
            {
                Report.Report.ToLogFile(MessageType.Message, "Failed to login probably.", null);
                Report.Report.AbruptFinalize();
                throw new WebDriverTimeoutException();
            }

            if (!ContactsPage.IsContactListLoaded)
            {
                Report.Report.ToLogFile(MessageType.Message, "Contact list is not loading or it does take too long to load contents", null);
                Report.Report.AbruptFinalize();
                throw new WebDriverTimeoutException();
            }

        }
    }
}

