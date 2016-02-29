using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static bool IsAt
        {
            get { return Driver.CheckIfIsAt("Organizations"); }
        }

        /// <summary>
        /// Navigates browser to the organizations list page
        /// </summary>
        public static void GoTo()
        {
            var mainMenu = Driver.Instance.FindElement(By.Id("main-menu"));
            var organizationsBtn = mainMenu.FindElement(By.Id("Companies"));
            organizationsBtn.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
            if (!IsAt) Console.WriteLine("Failed to open new contact");
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
        public static SearchOrganizationCommand FindOrganizationWithName(string organization_name)
        {
            Commands.SearchFor(organization_name);
            Driver.Wait(TimeSpan.FromSeconds(3));
            return new SearchOrganizationCommand(organization_name);

        }
    }

    public class SearchOrganizationCommand
    {
        private string organization_name;

        public SearchOrganizationCommand(string organization_name)
        {
            this.organization_name = organization_name;
        }

        public bool Find()
        {
            return Driver.Instance.FindElements(By.LinkText(organization_name)).Any();
        }

        public void Delete()
        {

            var organizations = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            foreach (var organization in organizations)
            {
                var organizationName = organization.FindElement(By.CssSelector(".font-bold.ng-binding"));
                if (organizationName.Text.Equals(organization_name))
                {
                    Actions action = new Actions(Driver.Instance);
                    action.MoveToElement(organization);
                    action.Perform();
                    organization.FindElement(By.CssSelector(".icheckbox")).Click();
                }
                else break;
            }

            Driver.Instance.FindElement(By.CssSelector("a[ng-click='showDeleteModal=1;']")).Click();
            var deleteOnlyOrganizationBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[2]"));
            deleteOnlyOrganizationBtn.Click();
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
