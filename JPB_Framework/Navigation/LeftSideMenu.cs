using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Navigation
{
    public class LeftSideMenu
    {

        /// <summary>
        /// Navigate browser to contact list page
        /// </summary>
        public static void GoToContacts()
        {
            try
            {
                var contactsBtn = Driver.Instance.FindElement(By.CssSelector("#Contacts"));
                var action = new Actions(Driver.Instance);
                action.MoveToElement(contactsBtn);
                action.Perform();
                contactsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Browser was expected to be in Contacts Page but is not or page is not loaded properly", e);
            }

        }

        /// <summary>
        /// Navigates browser to the organizations list page
        /// </summary>
        public static void GoToOrganizations()
        {
            try
            {
                var organizationsBtn = Driver.Instance.FindElement(By.CssSelector("#Companies"));
                var action = new Actions(Driver.Instance);
                action.MoveToElement(organizationsBtn);
                action.Perform();
                organizationsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Browser was expected to be in Organization Page but is not or page is not loaded properly", e);
            }
        }

        /// <summary>
        /// Navigates browser to the import records page
        /// </summary>
        public static void GoToImports()
        {
            try
            {
                var importsBtn = Driver.Instance.FindElement(By.CssSelector("#importEntButton"));
                var action = new Actions(Driver.Instance);
                action.MoveToElement(importsBtn);
                action.Perform();
                importsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("import-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Browser was expected to be in Imports Page but is not or page is not loaded properly", e);
            }
        }
    }
}
