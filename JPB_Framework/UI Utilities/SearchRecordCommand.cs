using System;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using JPB_Framework.Workflows;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class SearchRecordCommand
    {
        private string keyword;

        public SearchRecordCommand()
        {
            keyword = "";
        }

        /// <summary>
        /// Append str to the existing value of keyword that will be used by the search command
        /// </summary>
        /// <param name="str"></param>
        private void AppendToKeyword(string str)
        {
            if (string.IsNullOrEmpty(keyword)) keyword = str;
            else keyword = keyword + ' ' + str;
        }

        /// <summary>
        /// Direct the search command to search for contacts with dummy first and last name
        /// </summary>
        /// <returns></returns>
        public SearchRecordCommand WithDummyValues()
        {
            AppendToKeyword(DummyData.FirstName);
            AppendToKeyword(DummyData.LastName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public SearchRecordCommand WithFirstName(string firstName)
        {
            AppendToKeyword(firstName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public SearchRecordCommand AndLastName(string lastName)
        {
            AppendToKeyword(lastName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for organizations with specific organization name
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public SearchRecordCommand WithOrganizationName(string organizationName)
        {
            AppendToKeyword(organizationName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for records with specific keywords in their name
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SearchRecordCommand ContainingKeyword(string key)
        {
            AppendToKeyword(key);
            return this;
        }

        /// <summary>
        /// Direct the search command to execute itself
        /// </summary>
        /// <returns>Returns true if at least one record exists in the record list after the search execution</returns>
        public bool Find()
        {
            Commands.SearchFor(keyword);
            return Commands.FindIfRecordExists(keyword);
        }

        /// <summary>
        /// Applicable only for Contacts. Direct the search command to execute itself and then issue a delete command on the contacts returned by the search execution
        /// </summary>
        public void Delete() 
        {
            Commands.SearchFor(keyword);
            Commands.SelectRecordsMatching(keyword);
            new DeleteRecordCommand().Delete();
        }

        /// <summary>
        /// Applicable only for Organizations. Direct the search command to execute itself and then issue a delete command on the organizations returned by the search execution
        /// </summary>
        /// <param name="option"> Defines if contacts linked to the organization will be also deleted or if the will become orphan</param>
        public void Delete(DeleteType option)
        {
            Commands.SearchFor(keyword);
            Commands.SelectRecordsMatching(keyword);
            var command = new DeleteRecordCommand();
            switch (option)
            {
                case DeleteType.OnlyOrganization:
                    {
                        command.OnlyOrganization();
                        break;
                    }
                case DeleteType.WithContacts:
                    {
                        command.WithContacts();
                        break;
                    }
                default:
                    {
                        command.OnlyOrganization();
                        break;
                    }
            }

        }

        /// <summary>
        /// Direct the search command to execute itself and then open the record matching to the keyword. 
        /// Applicable for Contact/Organization lists and contact list within organization view page
        /// </summary>
        public void Open()
        {
            if (!OrganizationViewPage.IsAt) Commands.SearchFor(keyword);

            var contacts = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var contact in contacts)
            {
                var firstAndLastName = contact.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (!keyword.Equals(firstAndLastName)) continue;
                contact.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                break;
            }
        }
    }

}
