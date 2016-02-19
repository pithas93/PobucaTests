using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class OrganizationsPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organizations"); } }
        
        public static void GoTo()
        {
            var mainMenu = Driver.Instance.FindElement(By.Id("main-menu"));
            var organizationsBtn = mainMenu.FindElement(By.Id("Companies"));
            organizationsBtn.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main-content")));
        }

        
        public static void SelectOrganization()
        {
            Driver.SelectRecordFromListBySequence(1);
        }
    }
}
