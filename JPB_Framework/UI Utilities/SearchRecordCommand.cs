using System;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.Workflows;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JPB_Framework.UI_Utilities
{
    public class SearchRecordCommand
    {
        protected string keyword;

        public SearchRecordCommand()
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
            if (!OrganizationViewPage.IsAt) Commands.SearchFor(keyword);

            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var record in records)
            {
                var firstAndLastName =
                    record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (keyword.Equals(firstAndLastName)) return true;
                if (firstAndLastName.Equals("") || firstAndLastName == null) break;
            }
            return false;
        }

        /// <summary>
        /// Direct the search command to execute itself and then open the record matching to the keyword. 
        /// Applicable for Contact/Organization lists and contact list within organization view page
        /// </summary>
        public void Open()
        {
            if (!OrganizationViewPage.IsAt) Commands.SearchFor(keyword);

            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var record in records)
            {
                var firstAndLastName =
                    record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;

                if (!keyword.Equals(firstAndLastName)) continue;
                record.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                return;
            }
            Report.Report.ToLogFile(MessageType.Message,
                $"Record with name {keyword} does not exist and so it cannot be opened.", null);
        }

    }

    public class SearchContactCommand : SearchRecordCommand
    {

        /// <summary>
        /// Direct the search command to search for contacts with specific first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public SearchContactCommand WithFirstName(string firstName)
        {
            AppendToKeyword(firstName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public SearchContactCommand AndLastName(string lastName)
        {
            AppendToKeyword(lastName);
            return this;
        }

        /// <summary>
        /// Applicable only for Contacts. Direct the search command to execute itself and then issue a delete command on the contacts returned by the search execution
        /// </summary>
        public void Delete()
        {
            Commands.SearchFor(keyword);
            var selecteddRecordsCount = Commands.SelectRecordsMatching(keyword);
            if (selecteddRecordsCount > 0) new DeleteRecordCommand().Delete();
            else
                Report.Report.ToLogFile(MessageType.Message,
                    $"There are no records matching given keywords and so no records deleted.", null);

        }

    }

    public class SearchOrganizationCommand : SearchRecordCommand
    {

        /// <summary>
        /// Direct the search command to search for organizations with specific organization name
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public SearchOrganizationCommand WithOrganizationName(string organizationName)
        {
            AppendToKeyword(organizationName);
            return this;
        }

        /// <summary>
        /// Applicable only for Organizations. Direct the search command to execute itself and then issue a delete command on the organizations returned by the search execution
        /// </summary>
        /// <param name="option"> Defines if contacts linked to the organization will be also deleted or if the will become orphan</param>
        public void Delete(DeleteType option)
        {
            Commands.SearchFor(keyword);
            var selecteddRecordsCount = Commands.SelectRecordsMatching(keyword);
            if (selecteddRecordsCount > 0)
            {
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
            else
                Report.Report.ToLogFile(MessageType.Message,
                    $"There are no records matching given keywords and so no records deleted.", null);

        }

    }

    public class SearchOrganizationContactListCommand : SearchRecordCommand
    {

        /// <summary>
        /// Direct the search command to search for contacts with specific first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public SearchOrganizationContactListCommand WithFirstName(string firstName)
        {
            AppendToKeyword(firstName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public SearchOrganizationContactListCommand AndLastName(string lastName)
        {
            AppendToKeyword(lastName);
            return this;
        }

        public void Remove()
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var record in records)
            {
                var firstAndLastName =
                    record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (!keyword.Equals(firstAndLastName)) continue;
                var action = new Actions(Driver.Instance);
                action.MoveToElement(record);
                action.Perform();
                record.FindElement(By.CssSelector("div[action='removeRelatedContact(contact)']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                break;
            }
            Report.Report.ToLogFile(MessageType.Message,
                $"Contact with name {keyword} does not exist and so it cannot be removed from organization contact list",
                null);
        }
    }
}

