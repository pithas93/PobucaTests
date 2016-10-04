using System;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class TakeTourWindow
    {
        /// <summary>
        ///  If "Take A Tour to JustPhoneBook" window has shown up, close it, else do nothing
        /// </summary>
        public static void Close()
        {
            
            try
            {
                IWebElement element = null;
                Driver.NoWait(
                    () => element = Driver.Instance.FindElement(By.CssSelector("div#step-0"))
                    );
                element.FindElement(By.CssSelector("button#DenyCurrentTour")).Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (NoSuchElementException)
            {
                
            }

        }
    }
}
