using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

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
        public static void SelectContact()
        {
            Commands.SelectRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if contacts exists matching given first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns>A search command with upon which you can search additional fields that match first name</returns>
        public static SearchCommand DoesContactExistWithFirstName(string firstName)
        {
            return new SearchCommand(firstName);
        }
    }

    public class SearchCommand
    {
        private string firstName;
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
        public bool AndLastName(string lastName)
        {
            this.lastName = lastName;
            return Driver.Instance.FindElements(By.LinkText(firstName + lastName)).Any();
        }
    }
}
