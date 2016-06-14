using System;
using System.Management.Instrumentation;
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
            VerifyThat.IsTrue(ContactsPage.IsAt, "Contact page path is not the expected one");

            ContactsPage.OpenFirstContact();
            VerifyThat.IsTrue(ContactViewPage.IsAt, "Contact view page path is not the expected one");

            EditContactPage.GoTo();
            VerifyThat.IsTrue(EditContactPage.IsAt, "Edit contact page path is not the expected one");

            EditContactPage.ClickSaveContactButton();
            VerifyThat.IsTrue(ContactViewPage.IsAt, "Contact view page path is not the expected one");

            LeftSideMenu.GoToContacts();
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

            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            OrganizationViewPage.CreateContact();
            VerifyThat.IsTrue(NewContactPage.IsAtFromWithinOrganizationViewPage, "Contact view page path from within organization view page is not the expected one");

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
        /// Assert that comment character limit indicator within new/edit contact page, works correctly
        /// </summary>
        [TestMethod]
        public void Assert_Contact_Comment_Field_Character_Limit_Indicator_Works_Correctly()
        {

            NewContactPage.GoTo();
            NewContactPage.SetComments(" " + DummyData.SimpleText);
            VerifyThat.AreEqual(500 - NewContactPage.CommentsTextLength, NewContactPage.CommentsLimitIndicator,
            $"Comments text length is {500 - NewContactPage.CommentsTextLength} the value indicator is displaying is {NewContactPage.CommentsLimitIndicator}");

            NewContactPage.SetComments(DummyData.SimpleText);
            VerifyThat.AreEqual(500 - NewContactPage.CommentsTextLength, NewContactPage.CommentsLimitIndicator,
                $"Comments text length is {500 - NewContactPage.CommentsTextLength} the value indicator is displaying is {NewContactPage.CommentsLimitIndicator}");

        }

        /// <summary>
        /// Assert that a contact is made favorite either from its contact view page or by the contact list page
        /// </summary>
        [TestMethod]
        public void Make_A_Contact_Favorite()
        {
            ContactCreator.CreateSimpleContact();
            ContactViewPage.SetContactFavorite(false);
            AssertThat.IsFalse(ContactViewPage.IsContactFavorite, "Contact should have been set as un-favorite, but it is still favorite");

            LeftSideMenu.GoToContacts();
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).CheckFavorite();
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();

            AssertThat.IsTrue(ContactViewPage.IsContactFavorite, "Contact should have been set as favorite, but it is still un-favorite");

            ContactViewPage.SetContactFavorite(false);

            LeftSideMenu.GoToContacts();
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            AssertThat.IsFalse(ContactViewPage.IsContactFavorite, "Contact should have been set as favorite, but it is still un-favorite");
        }

        /// <summary>
        /// Assert that organization name within contact view, navigates to the organization view of the organization name
        /// </summary>
        [TestMethod]
        public void Organization_Name_Navigates_To_Organization_View()
        {
            ContactCreator.CreateSimpleContact();
            ContactViewPage.ClickOrganizationName();
            AssertThat.AreEqual(ContactCreator.FirstContact.OrganizationName, OrganizationViewPage.OrganizationName,
                $"Browser should navigate to '{ContactCreator.FirstContact.OrganizationName}' organization view but, it did not");
        }

        /// <summary>
        /// Assert that email links work as prompts to send emails
        /// </summary>
        [TestMethod]
        public void Contact_Emails_Are_Emailable()
        {
            ContactCreator.CreateContactWithAllValues();
            VerifyThat.IsTrue(ContactViewPage.IsWorkEmailEmailable, "Work email link is not active but it should");
            VerifyThat.IsTrue(ContactViewPage.IsPersonalEmailEmailable, "Personal email link is not active but it should");
            VerifyThat.IsTrue(ContactViewPage.IsOtherEmailEmailable, "Other email link is not active but it should");
        }

        /// <summary>
        /// Assert that telephone number links work as prompts to make telephone calls
        /// </summary>
        [TestMethod]
        public void Contact_Telephone_Numbers_Are_Callable()
        {
            ContactCreator.CreateContactWithAllValues();
            VerifyThat.IsTrue(ContactViewPage.IsMobilePhoneCallable, "Contact mobile phone is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsMobilePhone2Callable, "Contact mobile phone 2 is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsWorkPhoneCallable, "Contact work phone is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsWorkPhone2Callable, "Contact work phone 2 is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsHomePhoneCallable, "Contact home phone is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsHomePhone2Callable, "Contact home phone is not callable but it should");
            VerifyThat.IsTrue(ContactViewPage.IsOtherPhoneCallable, "Contact other phone is not callable but it should");
        }

        /// <summary>
        /// Assert that clicking on either of the addresses, redirectes browser to a google maps page that shows the clicked address on map
        /// </summary>
        [TestMethod]
        public void Contact_Address_Links_Navigate_To_Google_Maps()
        {
            ContactCreator.CreateContactWithAllValues();
            VerifyThat.IsTrue(ContactViewPage.IsHomeAddressLinkActive, "Work address link does not navigate to google maps");
            VerifyThat.IsTrue(ContactViewPage.IsOtherAddressLinkActive, "Work address link does not navigate to google maps");
            VerifyThat.IsTrue(ContactViewPage.IsWorkAddressLinkActive, "Work address link does not navigate to google maps");
        }

        /// <summary>
        /// Assert that when assinging values to first and last name that matche those of another existing contact, an indicator informs the user accordingly
        /// </summary>
        [TestMethod]
        public void Duplicate_Contact_Indicator()
        {
            ContactCreator.CreateSimpleContact();
            LeftSideMenu.GoToContacts();
            NewContactPage.GoTo();
            NewContactPage.SetFirstName(ContactCreator.FirstContact.FirstName);
            NewContactPage.SetLastName(ContactCreator.FirstContact.LastName);
            AssertThat.IsTrue(NewContactPage.IsPossibleDuplicateAlertShown, 
                "There should be an alert at the top of the page that informs for the possibility of a duplicate contact");

        }

    }
}
