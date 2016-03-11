﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organizations"); } }

        public static bool OrganizationListIsLoaded { get { return Driver.CheckIfRecordListIsLoaded(); } }

        /// <summary>
        /// Navigates browser to the organizations list page
        /// </summary>
        public static void GoTo()
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

        /// <summary>
        /// Selects an organization from the list. By default selects the first one
        /// </summary>
        public static void OpenOrganization()
        {
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if organizations exists matching given organization name
        /// </summary>
        /// <param name="organization_name"></param>
        /// <returns>True if there is at least one such organization</returns>
        public static SearchOrganizationCommand FindOrganization()
        {
            return new SearchOrganizationCommand();

        }
    }

    public class SearchOrganizationCommand
    {
        private string organizationName;

        public SearchOrganizationCommand WithName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        public bool Find()
        {
            Commands.SearchFor(organizationName);
            return Commands.FindIfRecordExists(organizationName);
        }


        public void Delete()
        {
            Commands.SearchFor(organizationName);
            Commands.SelectRecordsMatching(organizationName);
//            var organizations = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
//
//            foreach (var organization in organizations)
//            {
//                var organizationName = organization.FindElement(By.CssSelector(".font-bold.ng-binding"));
//                if (organizationName.Text.Equals(organization_name))
//                {
//                    Commands.SelectRecord(organization);
//
//                }
//                else break;
//            }

//            var deleteCmd = new DeleteRecordCommand();
            new DeleteRecordCommand().OnlyOrganization();
            
            
        }

    }

}












//        public static bool DoesOrganizationExistWithName(string organization_name)
//        {
//            //Commands.SearchFor(organization_name);
//            return Driver.Instance.FindElements(By.LinkText(organization_name)).Any();
//
//        }
//    }
//}
