using System;
using System.Threading;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class NewContactPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Add Contact"); } }
       
        public static void GoTo()
        {
            var newContactBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[5]/ul/li[2]/a/i"));            
            newContactBtn.Click();
        }

        public static CreateContactCommand CreateContact(string firstName)
        {
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

        public CreateContactCommand withLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public void Create()
        {
            var firstNameField = Driver.Instance.FindElement(By.Id("First Name"));
            var lastNameField = Driver.Instance.FindElement(By.Id("Last Name"));
            var saveBtn = Driver.Instance.FindElement(By.Id("save-entity"));

            firstNameField.SendKeys(firstName);
            lastNameField.SendKeys(lastName);
            Driver.Wait(TimeSpan.FromSeconds(1));
            saveBtn.Click();
        }
    }
}
