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
using OpenQA.Selenium.Interactions;
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
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Instructs web driver to check whether or not the record list currently displayed, is sorted by certain field ascendingly or descendingly
        /// </summary>
        /// <param name="field">Defines the field according to which the record list should be sorted. It must be present on record list sort by filters</param>
        /// <param name="order">Defines if the record list should be sorted ascendingly or descendingly</param>
        /// <returns></returns>
        public static bool CheckIfRecordListIsSortedBy(SortRecordsCommand.SortField field, SortRecordsCommand.SortOrder order)
        {
            var recordList = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            int recordListCount = GetRecordListCount();

            // Check if there is at least one record in the record list or else there is no point in continuing
            var recordName = recordList[0].FindElement(By.CssSelector(".font-bold.ng-binding"));
            if (String.IsNullOrEmpty(recordName.Text)) return true;

            // Make page load every single record so that web driver can access them through their WebElements
            while (recordList.Count < recordListCount)
            {
                Actions action = new Actions(Driver.Instance);
                action.MoveToElement(recordList[recordList.Count - 1]);
                action.Perform();
                recordList = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            }

            switch (field)
            {
                case SortRecordsCommand.SortField.FirstName:
                    {
                        for (var i = 0; i < recordListCount; i++)
                        {

                            var currentRecordName = recordList[i].FindElement(By.CssSelector(".font-bold.ng-binding"));
                            var nextRecordName = recordList[i + 1].FindElement(By.CssSelector(".font-bold.ng-binding"));

                            // if there is no next record, there is no point continuing;
                            if (String.IsNullOrEmpty(nextRecordName.Text)) break;

                            if (order == SortOrder.Ascending)
                            {

                                if (String.Compare(currentRecordName.Text, nextRecordName.Text) == 1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordName.Text}' is before contact:'{nextRecordName.Text}' which is wrong. The list must be sorted first by first name, then by last name ascending.",
                                        null);
                                    return false;
                                }
                            }
                            else if (order == SortOrder.Descending)
                            {
                                if (String.Compare(currentRecordName.Text, nextRecordName.Text) == -1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordName.Text}' is before contact:'{nextRecordName.Text}' which is wrong. The list must be sorted first by first name, then by last name descending.",
                                        null);
                                    return false;
                                }
                            }

                        }
                        return true;
                    }
                case SortRecordsCommand.SortField.LastName:
                    {
                        for (var i = 0; i < recordListCount; i++)
                        {

                            var currentRecordName = recordList[i].FindElement(By.CssSelector(".font-bold.ng-binding"));
                            var nextRecordName = recordList[i + 1].FindElement(By.CssSelector(".font-bold.ng-binding"));

                            // Break the string in first and last name and revert their position
                            string currentRecordNameStr = currentRecordName.Text.Split(' ')[1] + ' ' + currentRecordName.Text.Split(' ')[0];
                            string nextRecordNameStr = nextRecordName.Text.Split(' ')[1] + ' ' + nextRecordName.Text.Split(' ')[0];

                            // if there is no next record, there is no point continuing;
                            if (String.IsNullOrEmpty(nextRecordName.Text)) break;

                            if (order == SortOrder.Ascending)
                            {

                                if (String.Compare(currentRecordNameStr, nextRecordNameStr) == 1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordNameStr}' is before contact:'{nextRecordNameStr}' which is wrong. The list must be sorted first by last name, then by first name ascending.",
                                        null);
                                    return false;
                                }
                            }
                            else if (order == SortOrder.Descending)
                            {
                                if (String.Compare(currentRecordNameStr, nextRecordNameStr) == -1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordNameStr}' is before contact:'{nextRecordNameStr}' which is wrong. The list must be sorted first by last name, then by first name descending.",
                                        null);
                                    return false;
                                }
                            }

                        }
                        return true;
                    }
                case SortRecordsCommand.SortField.OrganizationName:
                    {
                        for (var i = 0; i < recordListCount; i++)
                        {

                            var currentRecordName = recordList[i].FindElement(By.CssSelector(".font-bold.ng-binding"));
                            var nextRecordName = recordList[i + 1].FindElement(By.CssSelector(".font-bold.ng-binding"));

                            // if there is no next record, there is no point continuing;
                            if (String.IsNullOrEmpty(nextRecordName.Text)) break;

                            if (order == SortOrder.Ascending)
                            {

                                if (String.Compare(currentRecordName.Text, nextRecordName.Text) == 1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordName.Text}' is before contact:'{nextRecordName.Text}' which is wrong. The list must be sorted first by first name, then by last name ascending.",
                                        null);
                                    return false;
                                }
                            }
                            else if (order == SortOrder.Descending)
                            {
                                if (String.Compare(currentRecordName.Text, nextRecordName.Text) == -1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordName.Text}' is before contact:'{nextRecordName.Text}' which is wrong. The list must be sorted first by first name, then by last name descending.",
                                        null);
                                    return false;
                                }
                            }

                        }
                        return true;
                    }
                case SortRecordsCommand.SortField.City:
                    {
                        for (var i = 0; i < recordListCount; i++)
                        {

                            var currentRecordCity = recordList[i].FindElement(By.CssSelector(".details.font-light.ng-binding.ng-scope"));
                            var nextRecordCity = recordList[i + 1].FindElement(By.CssSelector(".details.font-light.ng-binding.ng-scope"));

                            // if there is no next record, there is no point continuing;
                            if (String.IsNullOrEmpty(nextRecordCity.Text)) break;

                            if (order == SortOrder.Ascending)
                            {

                                if (String.Compare(currentRecordCity.Text, nextRecordCity.Text) == 1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordCity.Text}' is before contact:'{nextRecordCity.Text}' which is wrong. The list must be sorted first by first name, then by last name ascending.",
                                        null);
                                    return false;
                                }
                            }
                            else if (order == SortOrder.Descending)
                            {
                                if (String.Compare(currentRecordCity.Text, nextRecordCity.Text) == -1)
                                {
                                    Report.ToLogFile(MessageType.Message,
                                        $"Contact:'{currentRecordCity.Text}' is before contact:'{nextRecordCity.Text}' which is wrong. The list must be sorted first by first name, then by last name descending.",
                                        null);
                                    return false;
                                }
                            }

                        }
                        return true;
                    }
                default:
                {
                    return false;
                }
            }
            
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
            Thread.Sleep((int)timespan.TotalSeconds * 1000);
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
                var tmp = Driver.Instance.FindElement(By.Id("breadcrumb"));
                var viewLbl = tmp.FindElement(By.LinkText(view));
                return viewLbl.Text == view;
            }
            catch (NoSuchElementException e)
            {
                Report.ToLogFile(MessageType.Exception, $"Browser was expected to be at {view} page, but was not", e);
                return false;
            }
            catch (InvalidOperationException e)
            {
                Report.ToLogFile(MessageType.Exception, $"It is probable that browser didn't loaded properly or in time all of the {view} web page elements", e);
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
                var recordlist = Driver.Instance.FindElement(By.Id("main-content"));
                if (recordlist.Displayed) return true;
                else return false;
            }
            catch (NoSuchElementException e)
            {
                Report.ToLogFile(MessageType.Exception, "Probably at wrong page Or record list is taking time to load", e);
                return false;
            }
            catch (StaleElementReferenceException e)
            {
                Report.ToLogFile(MessageType.Exception, "", e);
                return false;
            }
        }

        /// <summary>
        /// Returns the number of records contained in the record list currently displayed
        /// </summary>
        /// <returns></returns>
        public static int GetRecordListCount()
        {
            var totalRecordsLbl = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[1]/span/span[2]"));
            return int.Parse(totalRecordsLbl.Text);
        }
    }
}
