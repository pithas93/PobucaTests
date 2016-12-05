using System;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Coworkers;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
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
            if (ContactsPage.IsAt)
            {
                ContactsPage.ResetFilters();
                Commands.ClearSearchbox();
                return;
            }

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
            if (OrganizationsPage.IsAt)
            {
                OrganizationsPage.ResetFilters();
                Commands.ClearSearchbox();
                return;
            }

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
            if (ImportPage.IsAt) return;

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


        /// <summary>
        /// Navigates browser to the Co-workers list page
        /// </summary>
        public static void GoToCoworkers()
        {
            if (CoworkersPage.IsAt)
            {
                CoworkersPage.ResetFilters();
                Commands.ClearSearchbox();
                return;
            }


            try
            {
                var coworkersBtn = Driver.Instance.FindElement(By.CssSelector("#Users"));
                Driver.MoveToElement(coworkersBtn);
                coworkersBtn.Click();

                // wait for organization list to load
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception,
                    "Browser was expected to be in Co-workers Page but is not or page is not loaded properly", e);
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Failed to load Co-workers page on time.", e);
                throw e;
            }
        }

        /// <summary>
        /// Navigates browser to the Profile page
        /// </summary>
        public static void GoToProfile()
        {
            if (ProfilePage.IsAt) return;

            try
            {
                var profileIcon = Driver.Instance.FindElement(By.CssSelector("img[ng-if='$storage.userData.imageUrl']"));
                Driver.MoveToElement(profileIcon);
                profileIcon.Click();

                // wait for organization list to load
                Driver.WaitForElementToBeVisible(TimeSpan.FromSeconds(10),"div.row.contacts-add-update");
            }
            catch (NoSuchElementException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Browser was expected to be in Profile Settings Page but is not or page is not loaded properly", e);
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Exception, "Failed to load Profile Settings Page on time.", e);
                throw e;
            }
        }
    }
}
