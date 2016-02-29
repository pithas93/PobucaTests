using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JPB_Framework
{
    public class ContactsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Contacts"); } }
        
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
        public static SearchCommand FindContactWithFirstName(string firstName)
        {
            Commands.SearchFor(firstName);
            Driver.Wait(TimeSpan.FromSeconds(3));
            return new SearchCommand(firstName);
        }

        public static void SelectContactWithFirstName(string panagiotis)
        {
            throw new NotImplementedException();
        }
    }

    public class SearchCommand
    {
        private readonly string firstName;
        private string lastName;

        public SearchCommand(string firstName)
        {
            this.firstName = firstName;
        }

        /// <summary>
        /// Check if there is at least one contact matching given first and last name
        /// </summary>
        /// <param name="lastName"> Last name of contact</param>
        /// <returns>True if there is at least one contact</returns>
        public SearchCommand AndLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public bool Find()
        {
            return Driver.Instance.FindElements(By.LinkText(firstName + lastName)).Any();
        }

        public void Delete()
        {

            var contacts = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
//            var contacts = Driver.Instance.FindElements(By.CssSelector(".font-bold.ng-binding"));
            Debug.WriteLine("Count = " + firstName + " " + lastName);

            foreach (var contact in contacts)
            {
                var contactName = Driver.Instance.FindElement(By.CssSelector(".font-bold.ng-binding"));
                if (contactName.Text.Equals(firstName + " " + lastName))
                {
                    Actions action = new Actions(Driver.Instance);
                    action.MoveToElement(contact);
                    action.Perform();
                    contact.FindElement(By.CssSelector(".icheckbox")).Click();
                    
                }
            }

        }
    }
}
