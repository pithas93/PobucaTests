using JPB_Framework;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Login;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class JpbBaseTest
    {
        public const string Username = "panagiotis@panagof1.com";
        public const string Password = "6AB10F93";
        public const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
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
        public void SetUp()
        {
            Report.Initialize(TestContext.FullyQualifiedTestClassName,TestContext.TestName);
            Driver.Initialize(Browser.Firefox);
            ContactCreator.Initialize();
            LoginPage.GoTo();
            LoginPage.LoginAs(Username).WithPassword(Password).Login();
            TakeTourWindow.Close();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ContactCreator.CleanUp();
//            LeftSideMenu.GoToContacts();
//            AssertThat.AreEqual(ContactsPage.ContactsBeingDisplayed, 200, $"Contact created by the test, was not deleted or was not found to be deleted! Current contact count is {ContactsPage.ContactsBeingDisplayed}");
            Report.Finalize(TestContext.FullyQualifiedTestClassName, TestContext.TestName, TestContext.CurrentTestOutcome);           
            Driver.Close();
        }

    }
}
