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
    public class EditContactTests : JpbBaseTest
    {

        // Edit every contact field of a contact - normal case
        [TestMethod]
        public void Edit_Every_Contact_Field_From_Existing_Contact()
        {
            ContactCreator.CreateContactWithAllValues();
            ContactCreator.EditContactAlteringAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }
        // Edit every contact field of a contact from within organization contact list
        [TestMethod]
        public void Edit_Contact_From_Within_Organization_Contact_List()
        {
            ContactCreator.CreateSimpleContactWithOrganization();
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.FindOrganization().WithOrganizationName(ContactCreator.OrganizationName).Find();
            OrganizationsPage.OpenOrganization();
            OrganizationViewPage.FindContactFromContactList().WithFirstName(ContactCreator.FirstName).AndLastName(ContactCreator.LastName).Open();

            ContactCreator.EditSimpleContactWithOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }

        // Edit contact with mandatory field value deletion
        [TestMethod]
        public void Edit_Contact_And_Delete_Mandatory_Field_Value()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactRemovingLastName();
            AssertThat.IsFalse(ContactCreator.IsContactSavedAfterEdit, "Contact was saved after edit, without last name which is mandatory.");

        }

        // Edit contact and assign character overflow values
        [TestMethod]
        public void Edit_Contact_And_Assign_Overflow_Values()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactAssigningOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactSavedAfterEdit, "Contact was saved after edit but, it has values in first and last name that exceed the 50 character limit.");

        }

        // Edit contact and assign nonsense values
        [TestMethod]
        public void Edit_Contact_And_Assign_Nonsense_Values()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditContactAssigningNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");

        }

        // Edit contact and link contact with non existant organization
        [TestMethod]
        public void Edit_Contact_And_Assign_Invalid_Organization()
        {
            ContactCreator.CreateSimpleContactWithOrganization();
            ContactCreator.EditContactAssigningInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");

        }

        // Edit contact so that contact becomes orphaned
        [TestMethod]
        public void Edit_Contact_And_Delete_Organization_Value()
        {
            ContactCreator.CreateSimpleContactWithOrganization();
            ContactCreator.EditContactRemovingOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactSavedAfterEdit, "Contact was not saved after edit but, it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact fields have not the expected values after the edit.");
        }

    }
}
