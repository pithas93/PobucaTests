using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class ImportOrganizationsWindow
    {
        public static void GoTo()
        {
            Commands.ClickImport();
        }

        public static ImportOrganizationsFileCommand FromPath(string filePath)
        {
            GoTo();
            return new ImportOrganizationsFileCommand(filePath);
        }
    }

    public class ImportOrganizationsFileCommand
    {
        private string filePath;


        public ImportOrganizationsFileCommand(string filePath)
        {
            this.filePath = filePath;
        }

        public ImportOrganizationsFileCommand ImportFile(string contactsXls)
        {
            var BrowseButton = Driver.Instance.FindElement(By.Id("file1"));
            BrowseButton.SendKeys(filePath + contactsXls);
            //            Driver.Wait(TimeSpan.FromSeconds(5));
            var nextBtn = Driver.Instance.FindElement(By.Id("next-step-import-btn"));
            nextBtn.Click();
            return this;
        }


        public void Submit()
        {
            var submitBtn = Driver.Instance.FindElement(By.Id("upload-wizard-btn"));
            submitBtn.Click();

            var finishBtn = Driver.Instance.FindElement(By.CssSelector("fieldset[ng-show='wizardStepThree'] .btn.btn-primary"));
            finishBtn.Click();

        }
    }
}

