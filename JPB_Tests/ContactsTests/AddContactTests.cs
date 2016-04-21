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
    public class AddContactTests : JpbBaseTest
    {



        /// <summary>
        /// Create 1 contact that has values in all its fields
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_All_Fields_Filled()
        {
            ContactCreator.CreateContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactSavedSuccessfully,"Contact was not saved successfully but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

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
            AssertThat.IsTrue(ContactCreator.IsContactSavedSuccessfully, "Contact was not saved successfully but it should.");
            OrganizationViewPage.FindContactFromOrganizationContactList().WithFirstName(ContactCreator.FirstName).AndLastName(ContactCreator.LastName).Open();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Create 1 contact that has no value assigned in last name field
        /// </summary>
        [TestMethod]
        public void Create_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.CreateContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.IsContactSavedSuccessfully, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        /// <summary>
        /// Create 1 contact that has values that exceed 50 characters limit
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Overflown_Field_Values()
        {
            ContactCreator.CreateContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactSavedSuccessfully, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        /// <summary>
        /// Create 1 contact that has nonsense values assigned to its fields
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Nonsense_Field_Values()
        {
            ContactCreator.CreateContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactSavedSuccessfully, "Contact was not saved successfully but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Create 1 contact assigning an invalid organization name value to the respective field
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Invalid_Organization()
        {
            ContactCreator.CreateContactWithInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactSavedSuccessfully, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");
        }

        /// <summary>
        /// Create 1 contact and before saving, add some extra fields and left them empty on save
        /// </summary>
        [TestMethod]
        public void Create_Contact_With_Extra_Null_Fields()
        {
            ContactCreator.CreateContactWithNullValues();
            AssertThat.IsTrue(ContactCreator.IsContactSavedSuccessfully, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

    }
}
