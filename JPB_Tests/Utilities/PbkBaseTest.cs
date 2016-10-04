using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Login;
using JPB_Framework.Report;
using JPB_Framework.Selenium;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JPB_Tests.Utilities
{
    public class PbkBaseTest
    {
        public const Browser browser = Browser.Firefox;
        public const string Username = "panagiotis@panagof.com";
        public const string Password = "568bedd1!";
//        public const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        public const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestInitialize]
        public void SetUpTest()
        {
            Report.InitializeReportFile();
            Report.Initialize(TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            Driver.Initialize(browser);

            LoginPage.GoTo();

            // In case browser is already logged in, there is no need to login
            if (!ContactsPage.IsAt)
                LoginPage.LoginAs(Username).WithPassword(Password).Login();

            CookieConsent.Close();
            TakeTourWindow.Close();
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            Driver.Close();
            Report.Finalize(TestContext.CurrentTestOutcome);
        }
    }
}
