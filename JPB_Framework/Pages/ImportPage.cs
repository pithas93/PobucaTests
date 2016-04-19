using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class ImportPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Import"); } }

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
        /// Commands the browser to download a contact template file from within the import contacts dialog window
        /// </summary>
        public static void DownloadTemplateFile()
        {
            //            GoTo();
            Driver.Instance.FindElement(By.CssSelector("a[ng-href='import/Contacts.xls']")).Click();
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

            return new ImportFileCommand();
        }
    }
}
