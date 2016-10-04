﻿using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{

    [TestClass]
    public class EditContactTests : ContactsBaseTest
    {
        

        /// <summary>
        /// Edit every contact field of a contact - normal case
        /// </summary>
        [TestMethod]
        public void Edit_Every_Contact_Field_From_Existing_Contact()
        {
            ContactCreator.CreateContactWithAllValues();
            ContactCreator.EditContactAlteringAllValues(ContactCreator.FirstContact);
            AssertThat.IsTrue(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }

        /// <summary>
        /// Edit every contact field of a contact from within organization contact list
        /// </summary>
        [TestMethod]
        public void Edit_Contact_From_Within_Organization_Contact_List()
        {
            ContactCreator.CreateSimpleContact();
            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            OrganizationViewPage.FindContactFromOrganizationContactList().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();

            ContactCreator.EditSimpleContactWithOrganization(ContactCreator.FirstContact);
            AssertThat.IsTrue(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }

        /// <summary>
        /// Cannot save contact during edit after deletion of mandatory field value
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Contact_After_Leaving_Mandatory_Fields_Empty()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactRemovingLastName(ContactCreator.FirstContact);
            AssertThat.IsFalse(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was saved after edit, without last name which is mandatory.");

        }

        /// <summary>
        /// Cannot save contact during edit after assigning overflow values to at least one field
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Contact_After_Assigning_OverFlown_Field_Values()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactAssigningOverflowValues(ContactCreator.FirstContact);
            AssertThat.IsFalse(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was saved after edit but, it has values in first and last name that exceed the 50 character limit.");

        }

        /// <summary>
        /// Edit contact and assign nonsense values
        /// </summary>
        [TestMethod]
        public void Edit_Contact_And_Assign_Nonsense_Values()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactAssigningNonsenseValues(ContactCreator.FirstContact);
            AssertThat.IsTrue(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }

        /// <summary>
        /// During contact edit, changing organization to a non existance results in organization name being empty after save
        /// </summary>
        [TestMethod]
        public void Cannot_Assign_Invalid_Organization_After_Edit()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactAssigningInvalidOrganization(ContactCreator.FirstContact);
            AssertThat.IsTrue(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");

        }

        /// <summary>
        /// Edit contact so that contact becomes orphaned
        /// </summary>
        [TestMethod]
        public void Edit_Contact_And_Delete_Organization_Value()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactRemovingOrganization(ContactCreator.FirstContact);
            AssertThat.IsTrue(ContactCreator.FirstContact.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");
        }

        /// <summary>
        /// Remove a contact from within an organization contact list thus rendering it orphan
        /// </summary>
        [TestMethod]
        public void Remove_Contact_From_Organization_Contact_List()
        {
            ContactCreator.CreateSimpleContact();
            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            OrganizationViewPage.FindContactFromOrganizationContactList().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Remove();

            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.FirstContact.OrganizationName).Open();
            AssertThat.IsFalse(
                OrganizationViewPage.
                FindContactFromOrganizationContactList()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Find(),
                $"Contact {ContactCreator.FirstContact.FullName} was supposed to be removed but is still a contact of organization {ContactCreator.FirstContact.OrganizationName}."
                );
        }


    }
}
