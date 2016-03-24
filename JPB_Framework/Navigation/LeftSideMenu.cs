using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Navigates browser to the organizations list page
        /// </summary>
        public static void GoToOrganizations()
        {
            try
            {
                var mainMenu = Driver.Instance.FindElement(By.Id("main-menu"));
                var organizationsBtn = mainMenu.FindElement(By.Id("Companies"));
                organizationsBtn.Click();

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
}
