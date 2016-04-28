using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
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
                Driver.MoveToElement(contactsBtn);
                contactsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Browser was expected to be in Contacts Page but is not or page is not loaded properly", e);
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Failed to load Contacts Page on time.", e);
                throw e;
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
                Driver.MoveToElement(organizationsBtn);
                organizationsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception,
                    "Browser was expected to be in Organization Page but is not or page is not loaded properly", e);
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Failed to load Organizations page on time.", e);
                throw e;
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
                Driver.MoveToElement(importsBtn);
                importsBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("import-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Browser was expected to be in Imports Page but is not or page is not loaded properly", e);
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Failed to load Imports Page on time.", e);
                throw e;
            }
        }
    }
}
