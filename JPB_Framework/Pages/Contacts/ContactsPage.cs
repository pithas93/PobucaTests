using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class ContactsPage
    {


        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Contacts"); } }

        /// <summary>
        /// Check if contact list is loaded properly
        /// </summary>
        public static bool IsContactListLoaded { get { return Driver.CheckIfRecordListIsLoaded(); } }

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their first name, then by their last name, in ascending order
        /// </summary>
        public static bool IsContactListSortedByFirstNameAscending { get { return Driver.CheckIfRecordListIsSortedBy(SortRecordsCommand.SortField.FirstName, SortRecordsCommand.SortOrder.Ascending); } }

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their first name, then by their last name, in descending order
        /// </summary>
        public static bool IsContactListSortedByFirstNameDescending { get { return Driver.CheckIfRecordListIsSortedBy(SortRecordsCommand.SortField.FirstName, SortRecordsCommand.SortOrder.Descending); } }

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their last name, then by their first name, in ascending order
        /// </summary>
        public static bool IsContactListSortedByLastNameAscending { get { return Driver.CheckIfRecordListIsSortedBy(SortRecordsCommand.SortField.LastName, SortRecordsCommand.SortOrder.Ascending); } }

        /// <summary>
        /// Checks whether or not, the contacts are sorted firstly by their last name, then by their first name, in descending order
        /// </summary>
        public static bool IsContactListSortedByLastNameDescending { get { return Driver.CheckIfRecordListIsSortedBy(SortRecordsCommand.SortField.LastName, SortRecordsCommand.SortOrder.Descending); } }

        /// <summary>
        /// The total number of contacts being displayed by the contact list
        /// </summary>
        public static int ContactsBeingDisplayed { get { return Driver.GetRecordListCount(); } }

        public static int ContactsBeingSelected { get { return Driver.GetSelectedRecordsCount(); } }


        /// <summary>
        /// Selects a contact from the list. By default selects the first one
        /// </summary>
        public static void OpenContact()
        {
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if contacts exists matching given first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns>A search command with upon which you can search additional fields that match first name</returns>
        public static SearchRecordCommand FindContacts()
        {
            return new SearchRecordCommand();
        }

        /// <summary>
        /// Navigate browser to contact list page
        /// </summary>
        public static void GoTo()
        {
            try
            {
                var mainMenu = Driver.Instance.FindElement(By.Id("main-menu"));
                var contactsBtn = mainMenu.FindElement(By.Id("Contacts"));
                contactsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (WebDriverTimeoutException e)
            {
                Report.ToLogFile(MessageType.Message, "", e);
                throw e;
            }
            catch (NoSuchElementException e)
            {
                Report.ToLogFile(MessageType.Message, "", e);
                throw e;
            }
        }

        /// <summary>
        /// Issue a FilterBy command. Selects the filterby button from contact list page to reveal the filterby options
        /// </summary>
        /// <returns>A command upon which the filterby criteria are being build</returns>
        public static FilterRecordCommand FilterBy()
        {
            Commands.ClickFilterBy();
            return new FilterRecordCommand();
        }

        /// <summary>
        /// Issue a SortBy command. Selects the sortby button from contact list page to reveal the sortby options
        /// </summary>
        /// <returns>A command upon which the sortby criteria are being build</returns>
        public static SortRecordsCommand SortBy()
        {
            Commands.ClickSortBy();
            return new SortRecordsCommand();
        }

        public static int SelectRandomNumberOfContacts()
        {
            return Commands.SelectRandomNumberOfRecords();
        }
    }
}
