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
    public class ThankYouPage
    {
        public static bool IsAt {
            get
            {
                try
                {
                    var element = Driver.Instance.FindElement(By.CssSelector("h1.white.f40.font-light.m-t-xl"));
                    return (element.Text.Equals("Thank you for signing up!"));
                }
                catch (NoSuchElementException)
                {
                    Report.Report.ToLogFile(MessageType.Message, "It seems that browser is not navigated to thank you page after successfully sending the signup confirmation email.", null);
                    return false;
                }
            }
        }
    }
}
