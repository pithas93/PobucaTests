using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Login
{
    public class LandingPage
    {
        public static string LandingPageUrl => "https://www.justphonebook.com/";
        public static bool IsEmailAlreadyExistsMessageShown {
            get
            {
                try
                {
                    var element = Driver.Instance.FindElement(By.CssSelector("div#toast-container div.toast-message"));
                    return (element.Text.Equals("This email already exists. Is it you? Login to connect!"));
                }
                catch (NoSuchElementException)
                {
                    Report.Report.ToLogFile(MessageType.Message, "Page should be showing a message that informs user that the email provided is already registered.", null);
                    return false;
                }
            }
        }

        public static bool IsEnterCorporateMessageShown
        {
            get
            {
                try
                {
                    var element = Driver.Instance.FindElement(By.CssSelector("div#toast-container div.toast-message"));
                    return (element.Text.Equals("Please enter your work email address!"));
                }
                catch (NoSuchElementException)
                {
                    Report.Report.ToLogFile(MessageType.Message, "Page should be showing a message that informs user that the email provided must be his corporate email.", null);
                    return false;
                }
            }
        }

        public static bool IsEmailValid
        {
            get
            {
                try
                {
                    var element = Driver.Instance.FindElement(By.CssSelector("div#toast-container div.toast-message"));
                    return (element.Text.Equals("Error. PLease Insert a valid e-mail"));
                }
                catch (NoSuchElementException)
                {
                    Report.Report.ToLogFile(MessageType.Message, "Page should be showing a message that informs user that the email provided has invalid form.", null);
                    return false;
                }
            }
        }

        public static void GoTo()
        {
            Driver.NavigateTo(LandingPageUrl);
        }

        public static void SignupWithEmail(string email)
        {
            Driver.Instance.FindElement(By.CssSelector("input#email-top-page-banner")).SendKeys(email);
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.FindElement(By.CssSelector("button#signup-btn-top-page-banner")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }
    }
}
