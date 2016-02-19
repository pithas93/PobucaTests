﻿using System;
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
            var firstOrganization = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[3]/div[1]"));
            firstOrganization.Click();
        }
    }
}
