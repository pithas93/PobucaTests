using System;
using System.Threading;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace JPB_Framework
{
    public class NewContactPage
    {
        /// <summary>
        /// Check if browser is at contact form page that allows to create a new contact
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Add Contact"); } }

        /// <summary>
        /// Navigates browser, through the available button, to a contact form page that allows to create a new contact
        /// </summary>
        public static void GoTo()
        {
            var newContactBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[5]/ul/li[2]/a/i"));            
            newContactBtn.Click();
            if (!IsAt) Console.WriteLine("Failed to open new contact");
        }

        /// <summary>
        /// Issue a create new contact command with given first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns> A command upon which the parameters for the new contact are specified</returns>
        public static CreateContactCommand CreateContact(string firstName)
        {
            GoTo();
            return new CreateContactCommand(firstName);
        }

        
    }

    public class CreateContactCommand
    {
        private string firstName;
        private string lastName;

        public CreateContactCommand(string firstName)
        {
            this.firstName = firstName;
        }

        /// <summary>
        /// Sets the last name for the new contact
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public CreateContactCommand withLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        /// <summary>
        /// Creates the new contact with given first and last name
        /// </summary>
        public void Create()
        {
            var firstNameField = Driver.Instance.FindElement(By.Id("First Name"));
            var lastNameField = Driver.Instance.FindElement(By.Id("Last Name"));

            firstNameField.SendKeys(firstName);
            lastNameField.SendKeys(lastName);
            Driver.Wait(TimeSpan.FromSeconds(1.5));

            Commands.ClickSave();
            
        }
    }
}
