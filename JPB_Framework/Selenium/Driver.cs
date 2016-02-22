using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Opera;

namespace JPB_Framework
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static string BaseAddress { get { return "https://jpbstaging.azurewebsites.net"; } }

        /// <summary>
        /// Instantiates a Firefox web driver singleton that drives browser through pages
        /// </summary>
        public static void Initialize(Browser type)
        {
            switch (type)
            {
                case Browser.Chrome:
                    Instance = new ChromeDriver();
                    break;
                case Browser.Firefox:
                    Instance = new FirefoxDriver();
                    break;
                case Browser.IE:
                    Instance = new InternetExplorerDriver();
                    break;
                case Browser.Safari:
                    Instance = new SafariDriver();
                    break;
                case Browser.Opera:
                    Instance = new OperaDriver();
                    break;
            }
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Instruct web driver to terminate itself
        /// </summary>
        public static void Close()
        {
            //Instance.Close();
        }

        /// <summary>
        /// Instructs web driver to wait for a given timespan
        /// </summary>
        /// <param name="timespan"></param>
        public static void Wait(TimeSpan timespan)
        {
            Thread.Sleep( (int) timespan.TotalSeconds * 1000);
        }

        /// <summary>
        ///  Instructs web driver to check if browser is at a given page
        /// </summary>
        /// <param name="View">It corresponds to each page 'page link path' which 
        /// is located just below Company Name and the current page title and in 
        /// the webpage has the form 'Home / Contacts' e.t.c. If browser hasn't 
        /// already load webpage content, web driver wait for 1 second and 
        /// checks again</param>
        /// <returns>True if browser is at the given page</returns>
        public static bool CheckIfIsAt(string View)
        {

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
    
    public enum Browser
    {
        Chrome, Firefox, IE, Safari, Opera
    }
}
