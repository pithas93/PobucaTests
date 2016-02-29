using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages.Contacts
{
    public class ImportContactsWindow
    {
        public static void ImportFile(string contactsXls)
        {



            var BrowseButton = Driver.Instance.FindElement(By.Id("file1"));
            BrowseButton.SendKeys("D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\" + contactsXls);
            Driver.Wait(TimeSpan.FromSeconds(5));
            var NextBtn = Driver.Instance.FindElement(By.Id("next-step-import-btn"));
            NextBtn.Click();
        }

        public static void GoTo()
        {
            Commands.ClickImport();
        }

        public static ImportContactsFileCommand FromPath(string filePath)
        {
            GoTo();
            return new ImportContactsFileCommand(filePath);
        }
    }

    public class ImportContactsFileCommand
    {
        private string filePath;


        public ImportContactsFileCommand(string filePath)
        {
            this.filePath = filePath;
        }

        public ImportContactsFileCommand ImportFile(string contactsXls)
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
            //Driver.Wait(TimeSpan.FromSeconds(10));

            
            var finishBtn = Driver.Instance.FindElement(By.CssSelector(".showImportContactsModal button.close"));
            finishBtn.Click();

        }
    }
}
