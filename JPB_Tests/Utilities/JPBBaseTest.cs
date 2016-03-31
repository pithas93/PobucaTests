using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class JpbBaseTest
    {
        public const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
//        public const string ImportFilePath = "C:\\Users\\Panagof\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        public const string Username = "panagiotis@panagof1.com";
        public const string Password = "6AB10F93";
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
            Driver.Initialize(Browser.Chrome);
            LoginPage.GoTo();
            LoginPage.LoginAs(Username).WithPassword(Password).Login();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Report.Finalize(TestContext.FullyQualifiedTestClassName, TestContext.TestName, TestContext.CurrentTestOutcome);
            Driver.Close();
        }

    }
}
