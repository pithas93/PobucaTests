using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.UI_Utilities
{
    public class ImportFileCommand
    {
        private string filePath;


        public ImportFileCommand(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Import file of type .xls with given name, containing contact/organization records
        /// </summary>
        /// <param name="fileXls">The name of file containing records for import. Example "Contacts.xls"</param>
        /// <returns></returns>
        public ImportFileCommand ImportFile(string fileXls)
        {
            var BrowseButton = Driver.Instance.FindElement(By.Id("file1"));
            BrowseButton.SendKeys(filePath + fileXls);
            //            Driver.Wait(TimeSpan.FromSeconds(5));
            var nextBtn = Driver.Instance.FindElement(By.Id("next-step-import-btn"));
            nextBtn.Click();
            return this;
        }

        /// <summary>
        /// Submit for import previously selected file 
        /// </summary>
        public void Submit()
        {
            var submitBtn = Driver.Instance.FindElement(By.Id("upload-wizard-btn"));
            submitBtn.Click();

            try
            {
                // Wait till spinner gets invisible and the sucess/failure message is shown
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromMinutes(2));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[ng-show='showSpinner']")));
            }
            catch (WebDriverTimeoutException e)
            {
                Report.ToLogFile(MessageType.Message, "Failed to import file or did take too long.", e);
                throw e;
            }
        }

        public static void CloseImportDialogBox()
        {
            var finishBtn = Driver.Instance.FindElement(By.CssSelector("fieldset[ng-show='wizardStepThree'] .btn.btn-primary"));
            finishBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }
    }
}


