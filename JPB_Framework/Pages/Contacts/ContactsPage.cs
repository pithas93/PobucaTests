using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using JPB_Framework.Navigation;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Contacts
{
    public class ContactsPage
    {
        private static readonly string[] filter = { " Favorites", " Department", " Allow Email", " Allow SMS", " Allow Phones", " Orphans" };

        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Contacts");

        /// <summary>
        /// Returns the total number of contacts being displayed in the record list currently displayed.
        /// </summary>
        /// <returns></returns>
        public static int TotalContactsCount => Driver.GetTotalRecordsCount();

        /// <summary>
        /// Check if contact list is loaded properly
        /// </summary>
        public static bool IsContactListLoaded => Driver.CheckIfRecordListIsLoaded();

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their first name, then by their last name, in ascending order
        /// </summary>
        public static bool IsContactListSortedByFirstNameAscending => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.FirstName, SortContactsCommand.SortOrder.Ascending);

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their first name, then by their last name, in descending order
        /// </summary>
        public static bool IsContactListSortedByFirstNameDescending => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.FirstName, SortContactsCommand.SortOrder.Descending);

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their last name, then by their first name, in ascending order
        /// </summary>
        public static bool IsContactListSortedByLastNameAscending => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.LastName, SortContactsCommand.SortOrder.Ascending);

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their last name, then by their first name, in descending order
        /// </summary>
        public static bool IsContactListSortedByLastNameDescending => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.LastName, SortContactsCommand.SortOrder.Descending);

        /// <summary>
        /// The total number of contacts being displayed by the contact list according to the corresponding label on the page
        /// </summary>
        public static int ContactsBeingDisplayed => Driver.GetRecordListCount();

        /// <summary>
        /// The total number of contacts being selected in the contact list according to the corresponding label on the page
        /// </summary>
        public static int ContactsBeingSelected => Driver.GetSelectedRecordsCount();

        /// <summary>
        /// Checks whether or not, the Filter By options have the correct labels and are in the correct order.
        /// </summary>
        public static bool AreFilterByOptionsInCorrectState
        {
            get
            {
                FilterBy();
                var filterByOptions =
                    Driver.Instance.FindElements(
                        By.CssSelector(".checkboxLayer.show div.multiSelectItem.ng-scope.vertical span.ng-binding"));
                int i = 0;
                foreach (var option in filterByOptions)
                {
                    if (filter[i] != option.Text) return false;
                    i++;
                }
                return true;
            }
        }

        /// <summary>
        ///  Checks whether or not the filter by department options are in alphabetical order
        /// </summary>
        public static bool AreFilterByDepartmentsInCorrectState
        {
            get
            {
                FilterBy();
                var filterByOptionList = Driver.Instance.FindElements(By.CssSelector(".checkboxLayer.show .checkBoxContainer .multiSelectItem.ng-scope.vertical"));
                filterByOptionList[1].Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                FilterBy();

                var departmentListBtn = Driver.Instance.FindElement(By.CssSelector("div#department-dropdown .button.multiSelectButton.ng-binding"));
                departmentListBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));

                var departmentsOptionList =
                    Driver.Instance.FindElements(By.CssSelector("div#department-dropdown .checkBoxContainer .multiSelectItem.ng-scope.vertical .ng-binding"));

                int departmentsCount = departmentsOptionList.Count;

                for (int i = 1; i < departmentsCount; i++)
                {
                    string previousDepartmentName = departmentsOptionList[i - 1].Text;
                    string currentDepartmentName = departmentsOptionList[i].Text;
                    if (string.Compare(previousDepartmentName, currentDepartmentName) == 1)
                    {
                        Report.Report.ToLogFile(MessageType.Message,
                                        $"Department:'{previousDepartmentName}' is before department:'{currentDepartmentName}' which is wrong. The department list must be sorted alphabetically.",
                                        null);
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool IsContactPhoneCallable
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(By.CssSelector("font[ng-click='tracePhoneCallsOrganizations()'] a"));
                var href = element.GetAttribute("href");
                var expectedTelephoneLink ="tel:";
                return (href.StartsWith(expectedTelephoneLink));
            }
        }

        /// <summary>
        /// Opens the first contact from the contact list, to view its details
        /// </summary>
        public static void OpenFirstContact()
        {
            if (!IsAt) LeftSideMenu.GoToContacts();
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        /// Issue a search command to find one or more contacts
        /// </summary>
        /// <returns>A search command with upon which you can search additional fields that match first name</returns>
        public static SearchContactCommand FindContact()
        {
            return new SearchContactCommand(LeftSideMenu.GoToContacts);
        }

        /// <summary>
        /// Issue a FilterBy command. Selects the filterby button from contact list page to reveal the filterby options
        /// </summary>
        /// <returns>A command upon which the filterby criteria are being build</returns>
        public static FilterContactsCommand FilterBy()
        {
            if (!IsAt) LeftSideMenu.GoToContacts();
            Commands.ClickFilterBy();
            return new FilterContactsCommand();
        }

        /// <summary>
        /// Issue a SortBy command. Selects the sortby button from contact list page to reveal the sortby options
        /// </summary>
        /// <returns>A command upon which the sortby criteria are being build</returns>
        public static SortContactsCommand SortBy()
        {
            if (!IsAt) LeftSideMenu.GoToContacts();
            Commands.ClickSortBy();
            return new SortContactsCommand();
        }

        /// <summary>
        /// Selects a random number of up to 20 contacts from a list of no more than 40 contacts. 
        /// </summary>
        /// <returns>The count of contacts that where selected</returns>
        public static int SelectRandomNumberOfContacts()
        {
            if (!IsAt) LeftSideMenu.GoToContacts();
            return Commands.SelectRandomNumberOfRecords(0);
        }

    }

}
