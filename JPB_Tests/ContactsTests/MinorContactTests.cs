using System;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class MinorContactTests : ContactsBaseTest
    {
        

        /// <summary>
        /// Check that clicking a telephone number within a contact, results in showing a dialog to select an app to dial the number or automatically calls the number.
        /// </summary>
        [TestMethod]
        public void Call_A_Contact_Telephone_Number()
        {
            ContactCreator.CreateSimpleContact();
            AssertThat.IsTrue(ContactViewPage.IsMobileNumberCallable, "Contact mobile phone is not callable");
        }

        /// <summary>
        /// Check that a contact can be shared with a valid email and that the share button enabled.
        /// </summary>
        [TestMethod]
        public void Check_Contact_Is_Shareable_With_Valid_Email()
        {
            ContactsPage.OpenFirstContact();
            AssertThat.IsTrue(ContactViewPage.IsContactShareableTo(DummyData.EmailValue), "Though email inserted is of valid syntax, Share button is not enabled.");

        }

        /// <summary>
        /// Check that the filter that checks the validity of the email input in share contact dialog box, prevents the input of invalid format values
        /// </summary>
        [TestMethod]
        public void Check_Share_Contact_Email_Input_Filter()
        {
            ContactsPage.OpenFirstContact();
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo(DummyData.NonsenseValue), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo("_.@.co"), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo(DummyData.SimpleWord), "Email field input does not follows email syntaxt but, it was accepted by the filter.");

        }

        /// <summary>
        /// Asserts that organization name value is locked when a contact is created from within an organization contact list
        /// </summary>
        [TestMethod]
        public void Organization_Value_At_Create_New_Contact_From_Within_Organization_Is_Locked()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.OpenFirstOrganization();
            OrganizationViewPage.CreateContact();
            AssertThat.IsFalse(NewContactPage.IsOrganizationNameEditable, "Organization name field is editable but the contact is being created from within an organization");
        }

        /// <summary>
        /// Assert that is possible to call the contact phone number that is visible for every contact from the contact list page
        /// </summary>
        [TestMethod]
        public void Call_A_Contact_Telephone_From_Contact_List_Page()
        {
            ContactCreator.CreateSimpleContact();
            if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
            ContactsPage.FindContact()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Find();

            AssertThat.IsTrue(ContactsPage.IsContactMobilePhoneCallable, "Contact mobile phone is not callable from within contact list page");
        }

        /// <summary>
        /// Assert that page paths displayed at the top most part of page, are correct for all pages related to contacts
        /// </summary>
        [TestMethod]
        public void Assert_That_Page_Paths_Are_Correct()
        {
            if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
            VerifyThat.IsTrue(ContactsPage.IsAt, "Contact page path is not the expected one");

            ContactsPage.OpenFirstContact();
            VerifyThat.IsTrue(ContactViewPage.IsAt, "Contact view page path is not the expected one");

            EditContactPage.GoTo();
            VerifyThat.IsTrue(EditContactPage.IsAt, "Edit contact page path is not the expected one");

            EditContactPage.ClickSaveContactButton();
            VerifyThat.IsTrue(ContactViewPage.IsAt, "Contact view page path is not the expected one");

            if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
            NewContactPage.GoTo();
            VerifyThat.IsTrue(NewContactPage.IsAt, "New contact page path is not the expected one");

            ContactCreator.CreateSimpleContact();
            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            OrganizationViewPage.FindContactFromOrganizationContactList().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            VerifyThat.IsTrue(ContactViewPage.IsAtFromWithinOrganizationViewPage, "Contact view page path from within organization view page is not the expected one");
            
            EditContactPage.GoTo();
            VerifyThat.IsTrue(EditContactPage.IsAtFromWithinOrganizationViewPage, "Edit contact page path from within organization view page is not the expected one");

            EditContactPage.ClickSaveContactButton();
            VerifyThat.IsTrue(ContactViewPage.IsAtFromWithinOrganizationViewPage, "Contact view page path from within organization view page is not the expected one");
        }

        /// <summary>
        /// Assert that department combo list values are sorted according to their name ascendingly and that their values are not GUID
        /// </summary>
        [TestMethod]
        public void Assert_That_Department_Combo_List_Is_Sorted_Alphabetically()
        {
            NewContactPage.GoTo();
            AssertThat.IsTrue(NewContactPage.IsDepartmentComboListSorted, "Department combo list is not sorted alphabetically");  
        }

        /// <summary>
        /// Assert that country combo list values are sorted according to their name ascendingly and that their values are not GUID
        /// </summary>
        [TestMethod]
        public void Assert_That_Country_Combo_List_Is_Sorted_Alphabetically()
        {
            NewContactPage.GoTo();
            AssertThat.IsTrue(NewContactPage.AreCountryComboListsSorted, "Country combo list is not sorted alphabetically");
        }

        /// <summary>
        /// Assert that comment character limit indicator wothin new/edit contact page, works correctly
        /// </summary>
        [TestMethod]
        public void Assert_Contact_Comment_Field_Character_Limit_Indicator_Works_Correctly()
        {
            
            NewContactPage.GoTo();
            NewContactPage.SetContactComments(" " + DummyData.SimpleText);
            VerifyThat.AreEqual(500 - NewContactPage.CommentsTextLength, NewContactPage.CommentsLimitIndicator,
            $"Comments text length is {500 - NewContactPage.CommentsTextLength} the value indicator is displaying is {NewContactPage.CommentsLimitIndicator}");

            NewContactPage.SetContactComments(DummyData.SimpleText);
            VerifyThat.AreEqual(500 - NewContactPage.CommentsTextLength, NewContactPage.CommentsLimitIndicator,
                $"Comments text length is {500 - NewContactPage.CommentsTextLength} the value indicator is displaying is {NewContactPage.CommentsLimitIndicator}");

        }

    }
}
