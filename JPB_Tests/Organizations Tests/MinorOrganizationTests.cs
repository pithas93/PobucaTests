using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class MinorOrganizationTests : ContactsOrganizationsBaseTest
    {

        /// <summary>
        /// Assert that inside organization view page, the organization's field value is a link that drives browser to prompt user for a telephone call
        /// </summary>
        [TestMethod]
        public void Call_An_Organization_Telephone_Number()
        {
            OrganizationCreator.CreateSimpleOrganization();
            AssertThat.IsTrue(OrganizationViewPage.IsPhoneNumberCallable, "Organization telephone number is not callable");
        }

        /// <summary>
        /// Assert that a contact from orgnization's contact list, can be made primary
        /// </summary>
        [TestMethod]
        public void Make_A_Contact_Primary_From_Within_Organization()
        {
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationViewPage.AddContactsToContactList().Randomly(2).Add();

            OrganizationViewPage.FindContactFromOrganizationContactList().BySequence(1).MakePrimaryContact();
            AssertThat.AreEqual(OrganizationViewPage.PrimaryContact, OrganizationViewPage.GetContactFullNameBySequence(1), $"Contact '{OrganizationViewPage.GetContactFullNameBySequence(1)}' was not made primary though it should");

            OrganizationViewPage.FindContactFromOrganizationContactList().BySequence(2).MakePrimaryContact();
            AssertThat.AreEqual(OrganizationViewPage.PrimaryContact, OrganizationViewPage.GetContactFullNameBySequence(2), $"Contact '{OrganizationViewPage.GetContactFullNameBySequence(2)}' was not made primary though it should");
        }

        /// <summary>
        /// Assert that an orphan contact can be added to an organization's contact list
        /// </summary>
        [TestMethod]
        public void Add_Contact_To_Organization_Contact_List()
        {
            ContactCreator.CreateSimpleOrphanContact();
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationViewPage.AddContactsToContactList().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Add();

            OrganizationsPage.FindOrganization().WithOrganizationName(OrganizationCreator.FirstOrganization.OrganizationName).Open();

            AssertThat.IsTrue(
                OrganizationViewPage.FindContactFromOrganizationContactList()
                    .WithFirstName(ContactCreator.FirstContact.FirstName)
                    .AndLastName(ContactCreator.FirstContact.LastName)
                    .Find(),
                $"Contact {ContactCreator.FirstContact.FullName} does not exist within organization {OrganizationCreator.FirstOrganization.OrganizationName} contacts, although it was added previously");

            OrganizationViewPage.FindContactFromOrganizationContactList().BySequence(1).Remove();

            AssertThat.IsFalse(
                OrganizationViewPage.FindContactFromOrganizationContactList()
                    .WithFirstName(ContactCreator.FirstContact.FirstName)
                    .AndLastName(ContactCreator.FirstContact.LastName)
                    .Find(),
                $"Contact {ContactCreator.FirstContact.FullName} exists within organization {OrganizationCreator.FirstOrganization.OrganizationName} contacts, although it was removed previously");

            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();

            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Contact {ContactCreator.FirstContact.FullName} is not orphan though it should because it was removed from organization '{OrganizationCreator.FirstOrganization.OrganizationName}' contacts");
        }

        /// <summary>
        /// Check that a contact can be shared with a valid email and that the share button enabled.
        /// </summary>
        [TestMethod]
        public void Check_Organization_Is_Shareable_With_Valid_Email()
        {
            OrganizationsPage.OpenFirstOrganization();
            AssertThat.IsTrue(OrganizationViewPage.IsOrganizationShareableTo(DummyData.EmailValue), "Though email inserted is of valid syntax, Share button is not enabled.");

        }

        /// <summary>
        /// Check that the filter that checks the validity of the email input in share contact dialog box, prevents the input of invalid format values
        /// </summary>
        [TestMethod]
        public void Check_Share_Organization_Email_Input_Filter()
        {
            OrganizationsPage.OpenFirstOrganization();
            VerifyThat.IsFalse(OrganizationViewPage.IsOrganizationShareableTo(DummyData.NonsenseValue), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(OrganizationViewPage.IsOrganizationShareableTo("_.@.co"), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(OrganizationViewPage.IsOrganizationShareableTo(DummyData.SimpleWord), "Email field input does not follows email syntaxt but, it was accepted by the filter.");

        }

        /// <summary>
        /// Assert that when deleting an organization, choosing "Delete All" option, besides organization, also deletes the contacts linked to organization
        /// </summary>
        [TestMethod]
        public void Delete_Organization_Along_With_Its_Linked_Contacts()
        {
            ContactCreator.CreateSimpleOrphanContact();
            ContactCreator.CreateSimpleOrphanContact();
            ContactCreator.CreateSimpleOrphanContact();

            OrganizationCreator.CreateSimpleOrganization();

            OrganizationViewPage.AddContactsToContactList()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Add();
            OrganizationViewPage.AddContactsToContactList()
                .WithFirstName(ContactCreator.SecondContact.FirstName)
                .AndLastName(ContactCreator.SecondContact.LastName)
                .Add();
            OrganizationViewPage.AddContactsToContactList()
                .WithFirstName(ContactCreator.ThirdContact.FirstName)
                .AndLastName(ContactCreator.ThirdContact.LastName)
                .Add();

            OrganizationViewPage.DeleteOrganization().WithContacts();

            VerifyThat.IsFalse(
                OrganizationsPage.FindOrganization().WithOrganizationName(OrganizationCreator.FirstOrganization.OrganizationName).Find(),
            $"Organization with name {OrganizationCreator.FirstOrganization.OrganizationName} should be deleted but, it still exists"
                );

            VerifyThat.IsFalse(
                ContactsPage.FindContact().ContainingKeyword(ContactCreator.FirstContact.FullName).Find(),
                $"Contact with name {ContactCreator.FirstContact.FullName} should be deleted along with its organization but, it still exists"
                );
            VerifyThat.IsFalse(
                ContactsPage.FindContact().ContainingKeyword(ContactCreator.SecondContact.FullName).Find(),
                $"Contact with name {ContactCreator.FirstContact.FullName} should be deleted along with its organization but, it still exists"
                );
            VerifyThat.IsFalse(
                ContactsPage.FindContact().ContainingKeyword(ContactCreator.ThirdContact.FullName).Find(),
                $"Contact with name {ContactCreator.FirstContact.FullName} should be deleted along with its organization but, it still exists"
                );
        }

        /// <summary>
        /// Assert that is possible to link a contact that is already linked to an organization, to another organization through organization view page "Add existing contacts" button
        /// </summary>
        [TestMethod]
        public void Add_Existing_No_Orphan_Contact_To_An_Organization()
        {
            ContactCreator.CreateSimpleContact();
            OrganizationCreator.CreateSimpleOrganization();

            OrganizationViewPage.AddContactsToContactList()
                .UncheckingOrphanCheckbox()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Add();

            AssertThat.IsTrue(
               OrganizationViewPage.FindContactFromOrganizationContactList()
               .WithFirstName(ContactCreator.FirstContact.FirstName)
               .AndLastName(ContactCreator.FirstContact.LastName)
               .Find(),
               $"Contact with name {ContactCreator.FirstContact.FullName} should be linked to organization {OrganizationViewPage.OrganizationName} but it is not."
               );

            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            AssertThat.IsFalse(
                OrganizationViewPage.FindContactFromOrganizationContactList()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Find(),
                $"Contact with name {ContactCreator.FirstContact.FullName} should not belong to organization {OrganizationViewPage.OrganizationName} but it is still linked with it."
                );
        }
    }
}
