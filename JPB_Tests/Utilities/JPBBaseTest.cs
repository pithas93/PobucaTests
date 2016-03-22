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
        

        [TestInitialize]
        public void SetUp()
        {
            var t = TestContext;
            Report.Initialize(TestContext.FullyQualifiedTestClassName,TestContext.TestName);
            Driver.Initialize(Browser.Firefox);
            LoginPage.GoTo();
            LoginPage.LoginAs("panagiotis@panagof1.com").WithPassword("6AB10F93").Login();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Report.Finalize(TestContext.FullyQualifiedTestClassName, TestContext.TestName, TestContext.CurrentTestOutcome);
            Driver.Close();
        }

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
    }
}
