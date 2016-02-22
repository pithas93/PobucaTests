using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class OrganizationsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organizations"); } }
        
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
        }

        /// <summary>
        /// Selects an organization from the list. By default selects the first one
        /// </summary>
        public static void SelectOrganization()
        {
            Commands.SelectRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if organizations exists matching given organization name
        /// </summary>
        /// <param name="organization_name"></param>
        /// <returns>True if there is at least one such organization</returns>
        public static bool DoesOrganizationExistWithName(string organization_name)
        {
            //Commands.SearchFor(organization_name);
            return Driver.Instance.FindElements(By.LinkText(organization_name)).Any();

        }
    }
}
