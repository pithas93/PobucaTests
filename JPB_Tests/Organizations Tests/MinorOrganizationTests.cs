using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
