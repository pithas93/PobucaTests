﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages
{
    public class UpperToolBar
    {
        public static void Logout()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("div.navbar-more-menu.dropdown "));
            element.FindElement(By.CssSelector("a.dropdown-toggle")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            element.FindElement(By.CssSelector("li[ng-click='logout();']")).Click();

            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(15));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("form#loginForm")));
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Failed to load login screen on time.", null);
                throw e;

            }
        }
    }
}
