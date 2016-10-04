using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Coworkers;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.CoworkersTests
{
    [TestClass]
    public class MinorCoworkerTests : PbkBaseTest
    {


        /// <summary>
        /// Assert that telephone number links work as prompts to make telephone calls
        /// </summary>
        [TestMethod]
        public void Coworker_Telephone_Numbers_Are_Callable()
        {
            CoworkersPage.OpenFirstCoworker();
            VerifyThat.IsTrue(CoworkerViewPage.IsWorkPhoneCallable, "Coworker work phone is not callable but it should");
            VerifyThat.IsTrue(CoworkerViewPage.IsWorkPhone2Callable, "Coworker work phone 2 is not callable but it should");
            VerifyThat.IsTrue(CoworkerViewPage.IsHomePhoneCallable, "Coworker home phone is not callable but it should");
            VerifyThat.IsTrue(CoworkerViewPage.IsMobilePhoneCallable, "Coworker mobile phone is not callable but it should");

        }


        /// <summary>
        /// Check that a coworker can be shared with a valid email and that the share button enabled.
        /// </summary>
        [TestMethod]
        public void Check_Contact_Is_Shareable_With_Valid_Email()
        {
            CoworkersPage.OpenFirstCoworker();
            AssertThat.IsTrue(CoworkerViewPage.IsCoworkerShareableTo(DummyData.EmailValue), "Though email inserted is of valid syntax, Share button is not enabled.");

        }


        /// <summary>
        /// Check that the filter that checks the validity of the email input in share coworker dialog box, prevents the input of invalid format values
        /// </summary>
        [TestMethod]
        public void Check_Share_Coworker_Email_Input_Filter()
        {
            CoworkersPage.OpenFirstCoworker();
            VerifyThat.IsFalse(CoworkerViewPage.IsCoworkerShareableTo(DummyData.NonsenseValue), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(CoworkerViewPage.IsCoworkerShareableTo("_.@.co"), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(CoworkerViewPage.IsCoworkerShareableTo(DummyData.SimpleWord), "Email field input does not follows email syntaxt but, it was accepted by the filter.");

        }


        /// <summary>
        /// Assert that is possible to call coworker phone numbers that are visible for every contact from the contact list page
        /// </summary>
        [TestMethod]
        public void Call_A_Coworker_Telephone_From_Coworker_List_Page()
        {
            LeftSideMenu.GoToCoworkers();
            AssertThat.IsTrue(CoworkersPage.IsCoworkerMobilePhoneCallable, "Coworker mobile phone is not callable from within contact list page");
            AssertThat.IsTrue(CoworkersPage.IsCoworkerWorkPhoneCallable, "Coworker work phone is not callable from within contact list page");

        }


        /// <summary>
        /// Assert that page paths displayed at the top most part of page, are correct for all pages related to coworkers
        /// </summary>
        [TestMethod]
        public void Assert_That_Page_Paths_Are_Correct()
        {
            LeftSideMenu.GoToCoworkers();

            VerifyThat.IsTrue(CoworkersPage.IsAt, "Coworker page path is not the expected one");

            CoworkersPage.OpenFirstCoworker();
            VerifyThat.IsTrue(CoworkerViewPage.IsAt, "Contact view page path is not the expected one");

        }


        /// <summary>
        /// Assert that email links work as prompts to send emails
        /// </summary>
        [TestMethod]
        public void Contact_Emails_Are_Emailable()
        {
            LeftSideMenu.GoToCoworkers();
            CoworkersPage.OpenFirstCoworker();
            VerifyThat.IsTrue(CoworkerViewPage.IsWorkEmailEmailable, "Work email link is not active but it should");
            VerifyThat.IsTrue(CoworkerViewPage.IsPersonalEmailEmailable, "Personal email link is not active but it should");

        }

        /// <summary>
        ///  Assert that clicking Invite more Co-workers button from within Coworkers list page, navigates to Invite Page
        /// </summary>
        [TestMethod]
        public void Check_That_Invite_Coworkers_Button_Works()
        {
            LeftSideMenu.GoToCoworkers();
            CoworkersPage.ClickInviteCoworkers();
            AssertThat.IsTrue(InvitePage.IsAt, "Browser should be at Invite new coworkers page, but is elsewhere.");
        }

    }
}
