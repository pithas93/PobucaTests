using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        public static bool ContactListIsLoaded { get { return Driver.CheckIfRecordListIsLoaded(); } }


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
        public static SearchContactCommand FindContact()
        {
            return new SearchContactCommand();
        }

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
    }

    public class SearchContactCommand
    {
        private string firstName;
        private string lastName;


        public SearchContactCommand WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        /// <summary>
        /// Check if there is at least one contact matching given first and last name
        /// </summary>
        /// <param name="lastName"> Last name of contact</param>
        /// <returns>True if there is at least one contact</returns>
        public SearchContactCommand AndLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public bool Find()
        {
            Commands.SearchFor($"{firstName} {lastName}");
            return Commands.FindIfRecordExists($"{firstName} {lastName}");
        }


        /// <summary>
        /// Selects every contact matching with given first and last name and then deletes them though the delete button
        /// </summary>
        public void Delete()
        {
            Commands.SearchFor($"{firstName} {lastName}");

            Commands.SelectRecordsMatching($"{firstName} {lastName}");
//            var contacts = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
//
//            foreach (var contact in contacts)
//            {
//                var contactName = contact.FindElement(By.CssSelector(".font-bold.ng-binding"));
//                if (contactName.Text.Equals(firstName + " " + lastName))
//                {
//                    //Commands.SelectRecord(contact);
//                    Actions action = new Actions(Driver.Instance);
//                    action.MoveToElement(contact);
//                    action.Perform();
//                    contact.FindElement(By.CssSelector(".icheckbox")).Click();
//                }
//                else break;
//            }
//
//            var deleteCmd = new DeleteRecordCommand();
                        new DeleteRecordCommand().Delete();
//            deleteCmd.Delete();

        }

        
    }
}
