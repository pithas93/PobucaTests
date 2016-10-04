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
        public static string BaseAddress => "https://jpbstaging.azurewebsites.net";

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

            // wait for contact list to load
            try
            {
                Driver.WaitForElementToBeVisible(TimeSpan.FromSeconds(15), "div.groups-main-content");
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Failed to login or did take too long.", null);
                throw e;
            }
        }
    }
}



//                try
//                {
//                    LoginPage.LoginAs(Username).WithPassword(Password).Login();
//                }
//                catch (WebDriverTimeoutException)
//                {
//                    Report.ToLogFile(MessageType.Message,
//                        "Reseting browser because the test failed to initialize properly.", null);
//                    Driver.Reinitialize(browser);
//                    LoginPage.GoTo();
//                    LoginPage.LoginAs(Username).WithPassword(Password).Login();
//                }