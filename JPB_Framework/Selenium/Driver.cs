using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;

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
                    Instance = new ChromeDriver("C:/Selenium/Chrome_Driver/");
                    break;
                case Browser.Firefox:
                    Instance = new FirefoxDriver();
                    break;
                case Browser.IE:
                    Instance = new InternetExplorerDriver("C:/Selenium/IE_Driver/");
                    break;
                case Browser.Safari:
                    Instance = new SafariDriver();
                    break;
                case Browser.Opera:
                    Instance = new OperaDriver("C:/Selenium/Opera_Driver/");
                    break;
            }
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Instruct web driver to terminate itself
        /// </summary>
        public static void Close()
        {
            Instance.Dispose();
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
        ///  Instructs web driver to check if browser is at a given page.
        /// </summary>
        /// <param name="view">It corresponds to each page 'page link path' which 
        /// is located just below Company Name and the current page title and in 
        /// the webpage has the form 'Home / Contacts' e.t.c. If browser hasn't 
        /// already load webpage content, web driver wait for 1 second and 
        /// checks again</param>
        /// <returns>True if browser is at the given page</returns>
        public static bool CheckIfIsAt(string view)
        {
            try
            {
                var tmp = Driver.Instance.FindElement(By.Id("breadcrumb"));
                var viewLbl = tmp.FindElement(By.LinkText(view));
                return viewLbl.Text == view;
            }
            catch (NoSuchElementException e)
            {
                Report.ToLogFile(MessageType.Exception, $"Browser was expected to be at {view} page, but was not", e);
                return false;
            }
        }

        public static bool CheckIfRecordListIsLoaded()
        {
            try
            {
                var recordlist = Driver.Instance.FindElement(By.Id("main-content"));
                if (recordlist.Displayed) return true;
                else return false;
            }
            catch (NoSuchElementException e)
            {
                Report.ToLogFile(MessageType.Exception, "Probably at wrong page or record list is taking time to load", e);
                return false;
            }
            catch (StaleElementReferenceException e)
            {
                Report.ToLogFile(MessageType.Exception, "", e);
                return false;
            }
        }
    }
}
