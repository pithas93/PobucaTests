using System;
using JPB_Framework.Navigation;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class ImportPage
    {
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Import");

        /// <summary>
        /// Defines the path that contains the files to be imported.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>                                                   
        public static ImportFileCommand FromPath(string filePath)
        {
            
            return new ImportFileCommand();
        }

        /// <summary>
        /// Returns true if the successful import message is being shown
        /// </summary>
        public static bool IsImportSuccessMessageShown
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(
                        By.CssSelector("fieldset[ng-show='wizardStepThree'] div[ng-show='showSuccessResult']"));
                var value = element.GetAttribute("class");

                return string.Equals(value, "");
            }
        }

        /// <summary>
        /// Returns true if the "Duplicate contacts found. Import partially completed!" is being shown
        /// </summary>
        public static bool IsImportWithDuplicatesMessageShown
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(
                        By.CssSelector("fieldset[ng-show='wizardStepThree'] div[ng-show='duplicateReport']"));
                var value = element.GetAttribute("class");

                return string.Equals(value, "");
            }
        }

        /// <summary>
        /// Returns true if the failed import message is being shown
        /// </summary>
        public static bool IsImportFailedMessageShown
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(
                        By.CssSelector("fieldset[ng-show='wizardStepThree'] div[ng-show='errorReport']"));
                var msgIsShown = element.GetAttribute("class");

                return string.IsNullOrEmpty(msgIsShown);
            }
        }



        /// <summary>
        /// Issue an import file command. The file can either contain contacts or organizations
        /// </summary>
        /// <returns></returns>
        public static ImportFileCommand ImportFile()
        {
            if (!IsAt) LeftSideMenu.GoToImports();

            var elements =
                Driver.Instance.FindElements(By.CssSelector("div.radio.m-b-md label"));

            foreach (var webElement in elements)
            {
                webElement.FindElement(By.CssSelector("span.f14"));
                if (webElement.Text != "Excel, CSV file") continue;
                webElement.FindElement(By.CssSelector("ng-pristine.ng-valid.ng-touched")).Click();
            }

            var nextBtn = Driver.Instance.FindElement(By.CssSelector("button[ng-click*='nextStep'][ng-click*='1']"));
            nextBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));

            return new ImportFileCommand();
        }
    }
}
