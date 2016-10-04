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


        public void Select()
        {

            var resultElements = ExecuteSearch(keyword).FindElements(By.CssSelector("div.ibox-content strong.ng-binding"));


            foreach (var element in resultElements)
            {
                if (keyword.Equals(element.Text)) element.Click();
                return;
            }

            Report.Report.ToLogFile(MessageType.Message, $"There is no record matching keyword='{keyword}'.", null);
        }

        public int GetContactCount()
        {
            var resultElements =
                ExecuteSearch(keyword)
                    .FindElements(By.CssSelector("div[ng-if*='results.Contacts.length'] strong.ng-binding"));
            return resultElements.Count;
        }

        public int GetOrganizationCount()
        {
            var resultElements =
                ExecuteSearch(keyword)
                    .FindElements(By.CssSelector("div[ng-if*='results.Groups.length'] strong.ng-binding"));
            return resultElements.Count;
        }

        public int GetCoworkerCount()
        {
            var resultElements =
                ExecuteSearch(keyword)
                    .FindElements(By.CssSelector("div[ng-if*='results.User.length'] strong.ng-binding"));
            return resultElements.Count;
        }

        public void AndSeeResultsForContacts()
        {
            ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchcontacts']")).Click();
        }

        public void AndSeeResultsForOrganizations()
        {
            ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchgroups']")).Click();
        }

        public void AndSeeResultsForCoworkers()
        {
            ExecuteSearch(keyword).FindElement(By.CssSelector("[ng-mousedown*='index.searchusers']")).Click();
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
            Driver.Wait(TimeSpan.FromSeconds(2));

            return Driver.Instance.FindElement(By.CssSelector("div#search-section"));
            //            return Driver.Instance.FindElements(By.CssSelector("div.ibox-content strong.ng-binding"));
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
