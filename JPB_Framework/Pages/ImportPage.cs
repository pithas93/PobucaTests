using System;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class ImportPage
    {
        public static bool IsAt => Driver.CheckIfIsAt("Import");

        /// <summary>
        /// Defines the path that contains the files to be imported.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>                                                   
        public static ImportFileCommand FromPath(string filePath)
        {
            
            return new ImportFileCommand();
        }


        public static bool IsImportSuccessMessageShown
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(
                        By.CssSelector("fieldset[ng-show='wizardStepThree'] div[ng-show='showSuccessResult']"));
                var value = element.GetAttribute("class");

                return string.Equals(value, "ng-binding");
            }
        }

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




        public static ImportFileCommand ImportFile()
        {
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
