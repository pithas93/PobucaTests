using System;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages.Contacts
{
    /// <summary>
    /// Workflow class that handles the actions regarding the import contacts dialog window
    /// </summary>
    public class ImportContactsWindow
    {
        /// <summary>
        /// Opens the import contacts dialog window
        /// </summary>
        public static void GoTo()
        {
            Commands.ClickImport();
        }

        /// <summary>
        /// Defines the path that contains the files to be imported.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ImportFileCommand FromPath(string filePath)
        {
            GoTo();
            return new ImportFileCommand(filePath);
        }

        /// <summary>
        /// Commands the browser to download a contact template file from within the import contacts dialog window
        /// </summary>
        public static void DownloadTemplateFile()
        {
            GoTo();
            Driver.Instance.FindElement(By.CssSelector("a[ng-href='import/Contacts.xls']")).Click();
        }

        public static bool IsImportSuccessMessageShown {
            get
            {
                var element =
                    Driver.Instance.FindElement(
                        By.CssSelector("fieldset[ng-show='wizardStepThree'] div[ng-show='showSuccessResult']"));
                var value = element.GetAttribute("class");

                if (string.Equals(value, "ng-binding")) return true;
                return false;
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

                if (string.IsNullOrEmpty(msgIsShown)) return true;
                return false;
            }
        }


        public static void CloseImportDialogBox()
        {           
            ImportFileCommand.CloseImportDialogBox();
        }
    }

}
