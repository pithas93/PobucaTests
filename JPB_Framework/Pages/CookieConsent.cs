using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class CookieConsent
    {
        /// <summary>
        ///  If cookie consent banner has shown up, close it, else do nothing
        /// </summary>
        public static void Close()
        {

            try
            {
                IWebElement element = null;
                Driver.NoWait(
                    () => element = Driver.Instance.FindElement(By.CssSelector("div.cc_banner-wrapper"))
                    );
                element.FindElement(By.CssSelector("[data-cc-event='click:dismiss']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (NoSuchElementException)
            {

            }

        }
    }
}
