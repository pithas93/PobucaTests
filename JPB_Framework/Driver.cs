using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static string BaseAddress { get { return "https://jpbstaging.azurewebsites.net"; } }

        public static void Initialize()
        {
            Instance = new FirefoxDriver();
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        public static void Close()
        {
            //Instance.Close();
        }

        public static void Wait(TimeSpan timespan)
        {
            Thread.Sleep( (int) timespan.TotalSeconds * 1000);
        }

        public static bool CheckIfIsAt(string View)
        {
            //my-navbar-header
            try
            {
                var tmp = Driver.Instance.FindElement(By.Id("breadcrumb"));
                var ViewLbl = tmp.FindElement(By.LinkText(View));
                return ViewLbl.Text == View;
            }
            catch (StaleElementReferenceException e)
            {
                Driver.Wait(TimeSpan.FromSeconds(1));
                var tmp = Driver.Instance.FindElement(By.Id("breadcrumb"));
                var ViewLbl = tmp.FindElement(By.LinkText(View));
                return ViewLbl.Text == View;
            }
            
        }
    }
}
