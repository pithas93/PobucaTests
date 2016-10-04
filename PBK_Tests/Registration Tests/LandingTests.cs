using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Login;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JPB_Tests.Registration_Tests
{

    [TestClass]
    public class LandingTests : PbkBaseTest
    {


        [TestInitialize]
        public new void SetUpTest()
        {
            Report.InitializeReportFile();
            Report.Initialize(TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            Driver.Initialize(browser);

            LandingPage.GoTo();
        }

        [TestMethod]
        public void Signup_With_Email_From_Existing_Domain()
        {
            LandingPage.SignupWithEmail("test@sieben.gr");
            AssertThat.IsTrue(ThankYouPage.IsAt, "Browser should navigate to thank you page after sending sign up confirmation email.");
        }

        [TestMethod]
        public void Signup_With_Email_From_Non_Existing_Domain()
        {
            LandingPage.SignupWithEmail("test@tsatmakaratzali.gr");
            AssertThat.IsTrue(ThankYouPage.IsAt, "Browser should navigate to thank you page after sending sign up confirmation email.");
        }

        [TestMethod]
        public void Signup_With_Email_From_Existing_Account()
        {
            LandingPage.SignupWithEmail("p.mavrogiannis@sieben.gr");
            AssertThat.IsTrue(LandingPage.IsEmailAlreadyExistsMessageShown, "There should be a message informing that a user with the given email, is already registered.");
        }

        [TestMethod]
        public void Signup_With_Email_That_Is_Not_Corporate()
        {
            LandingPage.SignupWithEmail("p.mavrogiannis@hotmail.com");
            VerifyThat.IsTrue(LandingPage.IsEnterCorporateMessageShown, "There should be a message informing that a user with the given email, is already registered.");
        }

        [TestMethod]
        public void Signup_With_Email_Of_Invalid_Form()
        {
            LandingPage.SignupWithEmail("test");
            VerifyThat.IsFalse(LandingPage.IsEmailValid, "There should be a message informing that a user with the given email, is already registered.");
        }
    }
}
