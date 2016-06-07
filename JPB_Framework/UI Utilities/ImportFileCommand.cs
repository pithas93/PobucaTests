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
        private bool checkForDuplicateFullName;
        private bool checkForDuplicateEmail;
        private ImportFileType fileType;

        public ImportFileCommand()
        {
            filePath = string.Empty;
            fileName = string.Empty;
            checkForDuplicateFullName = false;
            checkForDuplicateEmail = false;
            fileType = ImportFileType.Contacts;
        }

        /// <summary>
        /// Informs browser in which computer path it will search for the file to import
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ImportFileCommand FromPath(string filePath)
        {
            this.filePath = filePath;
            return this;
        }

        /// <summary>
        /// Informs browser what is the name of the file to be imported. 
        /// File must be of .xls type or else there will be a failure message 
        /// and it must contain contact or organization records
        /// </summary>
        /// <param name="fileXls">The name of file containing records for import. Example "Contacts.xls"</param>
        /// <returns></returns>
        public ImportFileCommand WithFileName(string fileXls)
        {
            fileName = fileXls;
            return this;

        }

        /// <summary>
        /// Informs browser which type of records will be contained inside import file.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ImportFileCommand Containing(ImportFileType type)
        {
            fileType = type;
            return this;
        }

        /// <summary>
        /// Instructs import command to check for duplicate contacts base on a given field
        /// </summary>
        /// <param name="field">The field according which the duplicate contact will be determined. For example ImportField.FullName</param>
        /// <returns></returns>
        public ImportFileCommand CheckingForDuplicate(ImportField field)
        {
            switch (field)
            {
                case ImportField.FullName:
                    {
                        checkForDuplicateFullName = true;
                        break;
                    }
                case ImportField.Email:
                    {
                        checkForDuplicateEmail = true;
                        break;
                    }
                default:
                    {
                        checkForDuplicateFullName = true;
                        break;
                    }
            }
            return this;
        }


        /// <summary>
        /// Instruct browser to execute an import file command with previously defined options.
        /// Details rearding file path, file name and whether it contains contacts or organizations, 
        /// must have been defined or else the command will fail.
        /// </summary>
        public void Submit()
        {
            // Check if file path and name have been set
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
            {
                Report.Report.ToLogFile(MessageType.Message, "File path and name have not been set.", null);
                return;
            }

            // Select whether you import file with contacts or organizations
            var fileTypeRadios = Driver.Instance.FindElements(By.CssSelector("div[ng-include*='import-inner-choices.min.html'] div[class*='radio']"));
            string dataType;
            if (fileType == ImportFileType.Contacts) dataType = "Contacts";
            else dataType = "Organizations";

            foreach (var fileTypeRadio in fileTypeRadios)
            {
                string radioText = fileTypeRadio.FindElement(By.CssSelector("label")).Text;
                if (radioText != dataType) continue;
                fileTypeRadio.FindElement(By.CssSelector("label")).Click();
                break;
            }

            // Click Next button
            Driver.Instance.FindElement(By.CssSelector("button[ng-click*='nextStep'][ng-click*='2']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            // Select file to import
            var browseBtn = Driver.Instance.FindElement(By.CssSelector("input.import-template-file"));            
            browseBtn.SendKeys(filePath + fileName);


            // Click Next button
            Driver.Instance.FindElement(By.CssSelector("button[ng-click='wizardStepOne=0;wizardStepTwo=1;wizardStepThree=0;updateStats(1);']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            // Check duplicate checkboxes accordingly
            if (checkForDuplicateFullName) Driver.Instance.FindElement(By.CssSelector("input#contactFullName")).Click();
            if (checkForDuplicateEmail) Driver.Instance.FindElement(By.CssSelector("input#contactEmail")).Click();
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

    }

    public enum ImportFileType
    {
        Contacts,
        Organizations
    }

    public enum ImportField
    {
        FullName,
        Email
    }
}


