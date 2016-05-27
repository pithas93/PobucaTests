using System;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class SearchRecordCommand
    {
        /// <summary>
        /// The action that navigate browser to a given page in order to execute the search command
        /// </summary>
        protected Action navigateCommand { get; set; }

        /// <summary>
        /// The search criteria with which the records will be identified
        /// </summary>
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
            navigateCommand?.Invoke();

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
        /// Direct the search command to execute itself and then open the first record matching to the keyword. 
        /// Applicable for Contact/Organization lists and contact list within organization view page
        /// </summary>
        public void Open()
        {
            navigateCommand?.Invoke();

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

        /// <summary>
        /// Direct the search command to execute itself and then select the records matching to the keyword. 
        /// Applicable for Contact/Organization list pages.
        /// </summary>
        public void Select()
        {
            navigateCommand?.Invoke();

            Commands.SearchFor(keyword);

            Commands.SelectRecordsMatching(keyword);

        }
    }

    public class SearchContactCommand : SearchRecordCommand
    {

        /// <summary>
        /// Instructs the search command that the search command will be executed on contact list page
        /// </summary>
        public SearchContactCommand(Action navigateAction)
        {
            navigateCommand = navigateAction;
        }

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
            navigateCommand?.Invoke();

            Commands.SearchFor(keyword);
            var selecteddRecordsCount = Commands.SelectRecordsMatching(keyword);
            if (selecteddRecordsCount > 0) new DeleteRecordCommand().Delete();
            else
                Report.Report.ToLogFile(MessageType.Message,
                    $"There are no records matching given keywords '{keyword}' and so no records deleted.", null);

        }

    }

    public class SearchOrganizationCommand : SearchRecordCommand
    {

        /// <summary>
        /// Instructs the search command that the search command will be executed on organization list page
        /// </summary>
        public SearchOrganizationCommand(Action navigateAction)
        {
            navigateCommand = navigateAction;
        }

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
            navigateCommand();

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
        private int sequence;

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

        /// <summary>
        /// Direct the search command to find a contact from its position inside the contact list
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public SearchOrganizationContactListCommand BySequence(int sequence)
        {
            this.sequence = sequence;
            return this;
        }

        /// <summary>
        /// Applicable only for contacts within organization view page contact list. Directs the search command to be executed and remove any contacts matching the search criteria.
        /// </summary>
        public void Remove()
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            try
            {
                if (!sequence.Equals(null))
                {
                    Commands.ClickContactRemoveButton(records[sequence - 1]);
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Report.Report.ToLogFile(MessageType.Message, "You choose to remove a contact that does not exist, probably because there are not that many contacts in the list.", null);
            }

            foreach (var record in records)
            {
                var firstAndLastName =
                    record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (!keyword.Equals(firstAndLastName)) continue;
                Commands.ClickContactRemoveButton(record);
                return;
            }
            Report.Report.ToLogFile(MessageType.Message,
                $"Contact with name {keyword} does not exist within organization contact list and so it cannot be removed",
                null);
        }

        /// <summary>
        /// Applicable only for contacts within organization view page contact list. Directs the search command to be executed and make primary the contact that matches the search criteria.
        /// </summary>
        public void MakePrimaryContact()
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            try
            {
                if (!sequence.Equals(null))
                {
                    Commands.ClickContactPrimaryButton(records[sequence - 1]);
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Report.Report.ToLogFile(MessageType.Message, "You choose to make primary a contact that does not exist, probably because there are not that many contacts in the list.", null);
            }

            foreach (var record in records)
            {
                var firstAndLastName =
                    record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (!keyword.Equals(firstAndLastName)) continue;
                Commands.ClickContactPrimaryButton(record);
                return;
            }
            Report.Report.ToLogFile(MessageType.Message,
                $"Contact with name {keyword} does not exist within organization contact list and so it cannot be made primary",
                null);
        }


    }
}

