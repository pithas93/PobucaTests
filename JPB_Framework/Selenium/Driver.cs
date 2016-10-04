using System;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Selenium
{
    public class Driver
    {
        public const int DriverTimeout = 10;

        public static IWebDriver Instance { get; set; }

        /// <summary>
        /// Returns a string that identifies the webpage currently being displayed by the browser
        /// </summary>
        public static string GetCurrentPage
        {
            get
            {
                try
                {
                    var breadcrumb = Instance.FindElement(By.CssSelector("#breadcrumb"));
                    return breadcrumb.Text;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
                catch (InvalidOperationException)
                {
                    return string.Empty;
                }
            }
        }


        public static void NavigateTo(string url)
        {
            Instance.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Instantiates a web driver singleton that drives the selected browser through pages
        /// </summary>
        public static void Initialize(Browser type)
        {

            switch (type)
            {
                case Browser.Chrome:
                    {
                        Instance = new ChromeDriver("C:/Selenium/Chrome_Driver/");
                        break;
                    }
                case Browser.Firefox:
                    {
                        Instance = new FirefoxDriver();


                        break;
                    }
                case Browser.IE:
                    {
                        var o = new InternetExplorerOptions { RequireWindowFocus = true, EnablePersistentHover = false };
                        Instance = new InternetExplorerDriver("C:/Selenium/IE_Driver/", o);
                        break;
                    }
                case Browser.Safari:
                    {
                        Instance = new SafariDriver();
                        break;
                    }
                case Browser.Opera:
                    {
                        Instance = new OperaDriver("C:/Selenium/Opera_Driver/");
                        break;
                    }
            }


            Instance.Manage().Window.Maximize();
            TurnOnWait();

        }

        /// <summary>
        /// Instruct web driver to terminate itself
        /// </summary>
        public static void Close()
        {
            Instance.Dispose();
        }

        /// <summary>
        /// Instructs web driver to restart itself
        /// </summary>
        /// <param name="type"></param>
        public static void Reinitialize(Browser type)
        {
            Close();
            Initialize(type);
        }

        /// <summary>
        /// Deactivate driver wait timeout for a specific action
        /// </summary>
        /// <param name="action"></param>
        public static void NoWait(Action action)
        {
            TurnOffWait();
            action();
            TurnOnWait();
        }

        /// <summary>
        /// Instructs web driver to wait for a given timespan
        /// </summary>
        /// <param name="timespan"></param>
        public static void Wait(TimeSpan timespan)
        {
            Thread.Sleep((int)timespan.TotalSeconds * 1000);
        }

        public static void WaitForElementToBeVisible(TimeSpan timespan, string cssSelector)
        {
            var wait = new WebDriverWait(Driver.Instance, timespan);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
            //            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(cssSelector)));
        }

        public static void WaitForElementToBeInvisible(TimeSpan timespan, string cssSelector)
        {
            var wait = new WebDriverWait(Driver.Instance, timespan);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(cssSelector)));
        }


        /// <summary>
        /// Turn on driver wait timeout. The timeout is 10 seconds
        /// </summary>
        private static void TurnOnWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(DriverTimeout));
        }

        /// <summary>
        /// Turn off driver wait timeout.
        /// </summary>
        private static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        }

        /// <summary>
        /// Instructs browser to move to a specific web element
        /// </summary>
        /// <param name="element"></param>
        public static void MoveToElement(IWebElement element)
        {
            var action = new Actions(Instance);
            action.MoveToElement(element);
            action.Perform();
        }

        /// <summary>
        /// Instructs web driver to check whether or not the record list currently displayed, is sorted by certain field ascendingly or descendingly
        /// </summary>
        /// <param name="field">Defines the field according to which the record list should be sorted. It must be present on record list sort by filters</param>
        /// <param name="order">Defines if the record list should be sorted ascendingly or descendingly</param>
        /// <returns></returns>
        public static bool CheckIfRecordListIsSortedBy(string field, string order)
        {
            var recordList = Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            int recordListCount = Commands.TotalRecordsCount();

            string labelCssSelector = string.Empty;
            if (field.Equals(SortRecordsCommand.SortField.FirstName) ||
                field.Equals(SortRecordsCommand.SortField.LastName) || field.Equals(SortRecordsCommand.SortField.OrganizationName))
                labelCssSelector = ".font-bold.ng-binding";
            else if (field.Equals(SortRecordsCommand.SortField.City)) labelCssSelector = ".details.font-light.ng-binding.ng-scope";
            else if (field.Equals(SortRecordsCommand.SortField.Profession)) labelCssSelector = "font[ng-if='group.accountTypeID']";

            // Check if there is at least one record in the record list or else there is no point in continuing
            var recordName = recordList[0].FindElement(By.CssSelector(".font-bold.ng-binding"));
            if (string.IsNullOrEmpty(recordName.Text)) return true;

            // Make page load every single record so that web driver can access them through their WebElements
            while (recordList.Count < recordListCount)
            {
                Driver.MoveToElement(recordList[recordList.Count - 1]);
                recordList = Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            }

            for (var i = 0; i < recordListCount - 1; i++)
            {
                string currentRecordLabel, nextRecordLabel;
                try
                {
                    currentRecordLabel = recordList[i].FindElement(By.CssSelector(labelCssSelector)).Text;
                }
                catch (NoSuchElementException)
                {
                    currentRecordLabel = string.Empty;
                }

                try
                {
                    nextRecordLabel = recordList[i + 1].FindElement(By.CssSelector(labelCssSelector)).Text;
                }
                catch (NoSuchElementException)
                {
                    nextRecordLabel = string.Empty;
                }


                if (field.Equals(SortRecordsCommand.SortField.LastName))
                {
                    // break the string in first and last name and revert their position
                    currentRecordLabel = RevertFirstLastName(currentRecordLabel);
                    nextRecordLabel = RevertFirstLastName(nextRecordLabel);

                }

                // if the field examined is the record name (first/last/organization name) AND is empty, there is no next record and so there is no point continuing;
                if (string.IsNullOrEmpty(nextRecordLabel) && (field.Equals(SortRecordsCommand.SortField.FirstName) ||
                field.Equals(SortRecordsCommand.SortField.LastName) || field.Equals(SortRecordsCommand.SortField.OrganizationName))) break;

                if (
                    (order.Equals(SortRecordsCommand.SortOrder.Ascending) && string.Compare(currentRecordLabel, nextRecordLabel) == 1)
                    ||
                    (order.Equals(SortRecordsCommand.SortOrder.Descending) && string.Compare(currentRecordLabel, nextRecordLabel) == -1)
                )
                {
                    Report.Report.ToLogFile(MessageType.Message, $"Record with {field} = '{currentRecordLabel}' is before record with {field} = '{nextRecordLabel}' which is wrong. The list must be sorted by {field} {order}.", null);
                    return false;
                }
            }
            return true;

        }



        /// <summary>
        ///  Instructs web driver to check if browser is at a given page.
        /// </summary>
        /// <param name="view">It corresponds to each page 'page link path' which 
        /// is located just below Company Name And the current page title And in 
        /// the webpage has the form 'Home / Contacts' e.t.c. If browser hasn't 
        /// already load webpage content, web driver wait for 1 second And 
        /// checks again</param>
        /// <returns>True if browser is at the given page</returns>
        public static bool CheckIfIsAt(string view)
        {
            try
            {
                var breadcrumb = Instance.FindElement(By.CssSelector("#breadcrumb"));
                return breadcrumb.Text == view;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Instructs web driver to check if record list is loaded
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfRecordListIsLoaded()
        {
            try
            {
                var recordlist = Instance.FindElement(By.Id("main-content"));
                if (recordlist.Displayed) return true;
                else return false;
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Probably at wrong page Or record list is taking time to load", e);
                return false;
            }
            catch (StaleElementReferenceException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "", e);
                return false;
            }
        }





        private static string RevertFirstLastName(string fullName)
        {
            var finalString = new StringBuilder();
            var str = fullName.Split(' ');
            var lastName = str[str.Length - 1];
            finalString.Append(lastName + ' ');
            for (var i = 0; i < str.Length - 1; i++) finalString.Append(str[i] + ' ');
            return finalString.ToString();
        }



    }
}
