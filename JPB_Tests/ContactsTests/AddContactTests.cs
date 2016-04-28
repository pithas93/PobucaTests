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
    public class AddContactTests : ContactsBaseTest
    {



        /// <summary>
        /// Create 1 contact that has values in all its fields
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_All_Fields_Filled()
        {
            ContactCreator.CreateContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully,"Contact was not saved successfully but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values were not saved correctly");

        }

        /// <summary>
        /// Create 1 contact from within an organization view page. The contact has first, last and organization name field values
        /// </summary>
        [TestMethod]
        public void Create_Contact_From_Within_Organization()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.OpenOrganization();
            ContactCreator.CreateSimpleContactFromWithinOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not saved successfully but it should.");
            OrganizationViewPage.FindContactFromOrganizationContactList().WithFirstName(ContactCreator.FirstName).AndLastName(ContactCreator.LastName).Open();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values were not saved correctly");

        }

        /// <summary>
        /// Assert that is not possible to create a contact without assinging value to last name field which is mandatory
        /// </summary>
        [TestMethod]
        public void Create_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.CreateContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.IsContactCreatedSuccessfully, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        /// <summary>
        /// Assert that is not possible to create a contact if at least one field contains a value that exceeds 50 characters
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Overflown_Field_Values()
        {
            ContactCreator.CreateContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactCreatedSuccessfully, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        /// <summary>
        /// Assert that is possible to create a contact that has nonsense values assigned to its fields
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Nonsense_Field_Values()
        {
            ContactCreator.CreateContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not saved successfully but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values were not saved correctly");

        }

        /// <summary>
        /// Assert that during contact creation, assigning a non-existant organization to the respective field, results in the field value being auto-cleared
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Invalid_Organization()
        {
            ContactCreator.CreateContactWithInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");
        }

        /// <summary>
        /// Assert that when adding an extra field during contact creation, if the field is left empty, it does not show at contact's view page, after contact has been saved
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Extra_Null_Fields()
        {
            ContactCreator.CreateContactWithNullValues();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values were not saved correctly");
        }

    }
}
