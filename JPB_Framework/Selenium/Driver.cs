using System;
using System.Collections.ObjectModel;
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
using OpenQA.Selenium.Safari;

namespace JPB_Framework.Selenium
{
    public class Driver
    {
        public const int DriverTimeout = 10;

        public static IWebDriver Instance { get; set; }
        public static string BaseAddress => "https://jpbstaging.azurewebsites.net";

        /// <summary>
        /// Instantiates a web driver singleton that drives the selected browser through pages
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
            Wait(TimeSpan.FromSeconds(2));
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
            int recordListCount = GetRecordListCount();

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

        private static string RevertFirstLastName(string fullName)
        {
            var finalString = new StringBuilder();
            var str = fullName.Split(' ');
            var firstName = str[0];
            for (var i = 1; i < str.Length; i++) finalString.Append(str[i] + ' ');
            finalString.Append(firstName);
            return finalString.ToString();
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
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, $"Browser was expected to be at {view} path, but was not", e);
                return false;
            }
            catch (InvalidOperationException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, $"It is probable that browser didn't loaded properly or in time all of the {view} web page elements", e);
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

        /// <summary>
        /// Returns the number of records contained in the record list currently displayed
        /// </summary>
        /// <returns></returns>
        public static int GetRecordListCount()
        {
            var totalRecordsLbl = Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[1]/span/span[2]"));
            return int.Parse(totalRecordsLbl.Text);
        }

        /// <summary>
        /// Returns the value of label displaying the number of records selected in the record list currently displayed
        /// </summary>
        /// <returns></returns>
        public static int GetSelectedRecordsCount()
        {
            var selectedRecordsLbl = Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[1]/span/span[1]"));
            return int.Parse(selectedRecordsLbl.Text);
        }

        /// <summary>
        ///  Returns the total number of records being displayed in the record list currently displayed.
        /// </summary>
        /// <returns></returns>
        public static int GetTotalRecordsCount()
        {
            var recordList = Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            //            int recordListCount = GetRecordListCount();

            // Check if there is at least one record in the record list or else there is no point in continuing
            var recordName = recordList[0].FindElement(By.CssSelector(".font-bold.ng-binding"));
            if (string.IsNullOrEmpty(recordName.Text)) return 0;

            // Make page load every single record so that web driver can access them through their WebElements
            var newRecordListCount = recordList.Count;
            int previousRecordListCount;
            do
            {
                // Navigate to the last record list item
                Driver.MoveToElement(recordList[newRecordListCount - 1]);

                // After the record list has load the extra, not shown previously records, get the new record list count
                recordList = Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

                // Save the previousRecordListCount
                previousRecordListCount = newRecordListCount;

                // Save the newRecordListCount
                newRecordListCount = recordList.Count;

                if (previousRecordListCount > newRecordListCount)
                {
                    Report.Report.ToLogFile(MessageType.Message, "It seems that there is somethign wrong while counting records from the list", null);
                    throw new Exception();
                }

                // There is no change in the newRecordListCount, we have probably reached its bottom
            } while (previousRecordListCount != newRecordListCount);

            return newRecordListCount;
        }

        /// <summary>
        /// Returns true if the given list of text values is sorted alphabetically.
        /// Method also returns false in case one of the text values is a GUID.
        /// </summary>
        /// <param name="valuesList"></param>
        /// <returns></returns>
        public static bool CheckIfListIsSorted(ReadOnlyCollection<IWebElement> valuesList)
        {
            string guid_pattern = @"\A\{[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}\}\z";
            Regex reg = new Regex(guid_pattern, RegexOptions.IgnoreCase);
            Match match;
            match = reg.Match(valuesList[0].Text);
            if (match.Success)
            {
                Report.Report.ToLogFile(MessageType.Message, "Guid value found inside given list values!", null);
                return false;
            }

            for (var i = 1; i < valuesList.Count; i++)
            {
                var previousDepartment = valuesList[i - 1].Text;
                var currentDepartment = valuesList[i].Text;
                match = reg.Match(currentDepartment);
                if (match.Success)
                {
                    Report.Report.ToLogFile(MessageType.Message, "Guid value found inside given list values!", null);
                    return false;
                }

                if (string.Compare(previousDepartment, currentDepartment) == 1) return false;
            }
            return true;
        }
    }
}
