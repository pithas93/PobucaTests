using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.UI_Utilities
{
    public class ImportFileCommand
    {
        private string filePath;
        private string fileName;
        private ImportFileType fileType;


        public ImportFileCommand FromPath(string filePath)
        {
            this.filePath = filePath;
            return this;
        }

        /// <summary>
        /// Import file of type .xls with given name, containing contact/organization records
        /// </summary>
        /// <param name="fileXls">The name of file containing records for import. Example "Contacts.xls"</param>
        /// <returns></returns>
        public ImportFileCommand WithFileName(string fileXls)
        {
            fileName = fileXls;
            return this;

        }

        public ImportFileCommand Containing(ImportFileType type)
        {
            fileType = type;
            return this;
        }

        /// <summary>
        /// Submit for import previously selected file 
        /// </summary>
        public void Submit()
        {

            // Select whether you import file with contacts or organizations
            var fileTypeRadios = Driver.Instance.FindElements(By.CssSelector("div[ng-include*='import-inner-choices.min.html'] div[class*='radio']"));
            string dataType;
            if (fileType == ImportFileType.Contacts) dataType = "Contacts";
            else dataType = "Organizations";

            foreach (var fileTypeRadio in fileTypeRadios)
            {
                string radioText = fileTypeRadio.FindElement(By.CssSelector("span.f14")).Text;
                if (radioText != dataType) continue;
                fileTypeRadio.Click();
                break;
            }

            // Click Next button
            Driver.Instance.FindElement(By.CssSelector("button[ng-click*='nextStep'][ng-click*='2']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            // Select file to import
            var browseBtn = Driver.Instance.FindElement(By.CssSelector("input.import-template-file"));
            browseBtn.SendKeys(filePath+fileName);


            // Click Next button
            Driver.Instance.FindElement(By.CssSelector("button[ng-click='wizardStepOne=0;wizardStepTwo=1;wizardStepThree=0;updateStats(1);']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            // Click Submit button
            Driver.Instance.FindElement(By.CssSelector("input#upload-wizard-btn")).Click();

            // Wait for result to be shown on screen
            try
            {
                // Wait till spinner gets invisible and the sucess/failure message is shown
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromMinutes(2));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[ng-show='showSpinner']")));
            }
            catch (WebDriverTimeoutException e)
            {
                Report.Report.ToLogFile(MessageType.Message, "Failed to import file or did take too long.", e);
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

    public enum ImportFileType
    {
        Contacts,
        Organizations
    }
}


