using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

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
    }

}
