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


        // 1. Create contact with all fields filled
        [TestMethod]
        public void Create_Contact_With_All_Fields_Filled()
        {
            ContactCreator.CreateContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 2. Create contact from within existing organization
        [TestMethod]
        public void Create_Contact_From_Within_Organization()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.OpenOrganization();
            ContactCreator.CreateSimpleContactFromWithinOrganization();
            OrganizationViewPage.FindContactFromContactList().WithFirstName(ContactCreator.FirstName).AndLastName(ContactCreator.LastName).Open();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 3. Create contact without assigning values in mandatory field
        [TestMethod]
        public void Create_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.CreateContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.ContactWasCreated, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        // 4. Create contact assinging field values that exceed character overflow limit
        [TestMethod]
        public void Create_Contact_With_Overflown_Field_Values()
        {
            ContactCreator.CreateContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.ContactWasCreated, "Contact was created successfully though last name field was left null. Defect spotted!");

        }

        // 5. Create contact with nonsense field values
        [TestMethod]
        public void Create_Contact_With_Nonsense_Field_Values()
        {
            ContactCreator.CreateContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 8. Create contact linked with non-existent organization
        [TestMethod]
        public void Create_Contact_With_Invalid_Organization()
        {
            ContactCreator.CreateContactWithInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.ContactWasCreated, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");
        }

        // 10. Add an extra field and save contact without assigning value to the added field
        [TestMethod]
        public void Create_Contact_With_Extra_Null_Fields()
        {
            ContactCreator.CreateContactWithNullValues();

            AssertThat.IsTrue(ContactCreator.ContactWasCreated, "Contact was not created successfully though it should. Defect spotted!");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

    }
}
