using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;


namespace JPB_Framework.UI_Utilities
{
    public class GeneralSearchRecordCommand
    {

        /// <summary>
        /// The search criteria with which the records will be identified
        /// </summary>
        protected string keyword;

        protected RecordType recordType;

        public GeneralSearchRecordCommand()
        {
            keyword = "";
        }

        /// <summary>
        /// Append str to the existing value of keyword that will be used by the search command
        /// </summary>
        /// <param name="str"></param>
        protected void AppendToKeyword(string str)
        {
            if (string.IsNullOrEmpty(keyword)) keyword = str;
            else keyword = keyword + ' ' + str;
        }

        public GeneralSearchRecordCommand ToFindContact(string key)
        {
            AppendToKeyword(key);
            recordType = RecordType.Contact;
            return this;
        }

        public GeneralSearchRecordCommand ToFindOrganization(string key)
        {
            AppendToKeyword(key);
            recordType = RecordType.Organization;
            return this;
        }

        public GeneralSearchRecordCommand ToFindCoworker(string key)
        {
            AppendToKeyword(key);
            recordType = RecordType.Coworker;
            return this;
        }

        public GeneralSearchRecordCommand WithKeyword(string key)
        {
            AppendToKeyword(key);
            recordType = RecordType.General;
            return this;
        }

        public void Search()
        {
            ExecuteSearch(keyword);
        }


        public void Open()
        {

            var resultElements = ExecuteSearch(keyword).FindElements(By.CssSelector("div.ibox-content strong.ng-binding"));


            foreach (var element in resultElements)
            {
                if (keyword.Equals(element.Text)) element.Click();
                return;
            }

            Report.Report.ToLogFile(MessageType.Message, $"There is no record matching keyword='{keyword}'.", null);
        }



        public void AndSeeResultsForContacts()
        {
            try
            {
                ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchcontacts']")).Click();
            }
            catch (NoSuchElementException)
            {
                Report.Report.ToLogFile(MessageType.Message, "General search returned no results for contacts.", null);
            }
        }

        public void AndSeeResultsForOrganizations()
        {
            try
            {
                ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchgroups']")).Click();
            }
            catch (NoSuchElementException)
            {
                Report.Report.ToLogFile(MessageType.Message, "General search returned no results for organizations.", null);
            }
        }

        public void AndSeeResultsForCoworkers()
        {
            try
            {
                ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchusers']")).Click();
            }
            catch (NoSuchElementException)
            {
                Report.Report.ToLogFile(MessageType.Message, "General search returned no results for coworkers.", null);
            }
        }

        private static IWebElement ExecuteSearch(string word)
        {
            var searchBtn = Driver.Instance.FindElement(By.CssSelector("[ng-click*='searching']"));
            var temp = searchBtn.GetAttribute("class");

            if (!temp.Contains("disappear"))
            {
                searchBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }


            var searchbox = Driver.Instance.FindElement(By.CssSelector("input#global-search"));
            temp = searchbox.GetAttribute("value");
            if (!string.IsNullOrEmpty(temp)) searchbox.Clear();
            Driver.Wait(TimeSpan.FromSeconds(2));

            searchbox.SendKeys(word);

            IJavaScriptExecutor exec = Driver.Instance as IJavaScriptExecutor;
            exec.ExecuteScript("arguments[0].scrollIntoView(true);", searchbox);
            try
            {
                Driver.WaitForElementToBeVisible(TimeSpan.FromSeconds(15), "#search-section");
                Driver.WaitForElementToBeInvisible(TimeSpan.FromSeconds(15), "[ng-if='isSearching.Groups || isSearching.Contacts || isSearching.User']");
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Message, $"Waiting for general search to return results, while searching for keyword '{word}', took too long.", null);
                throw e;
            }

            Driver.Wait(TimeSpan.FromSeconds(1));

            return Driver.Instance.FindElement(By.CssSelector("div#search-section"));
        }

        protected enum RecordType
        {
            Contact,
            Organization,
            Coworker,
            General
        }



    }
}
