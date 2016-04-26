using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class SmokeGeneralTests : JpbBaseTest
    {
        [TestMethod]
        public void Can_Login()
        {
            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to login");
            AssertThat.IsTrue(ContactsPage.IsContactListLoaded, "Failed to load contact list");
        }
    }

    [TestClass]
    public class SmokeContactTests : ContactsBaseTest
    {

        [TestMethod]
        public void Can_Create_A_Simple_Contact()
        {
            ContactCreator.CreateSimpleContact();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not created successfully but it should");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        

        [TestMethod]
        public void Can_Edit_Contact()
        {
            ContactCreator.CreateSimpleContact();
            ContactCreator.EditSimpleContact();
            AssertThat.IsTrue(ContactCreator.IsContactCreatedSuccessfully, "Contact was not saved successfully after edit");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly after edit");

        }


        [TestMethod]
        public void Can_Import_Contact_Template()
        {          
            ContactCreator.ImportSimpleContact();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        
    }

    [TestClass]
    public class SmokeOrganizationTests : OrganizationsBaseTest
    {

        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            OrganizationCreator.CreateSimpleOrganization();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationCreatedSuccessfully, "Organization was not created successfully but it should");
            AssertThat.IsTrue(OrganizationCreator.AreOrganizationFieldValuesSavedCorrectly, "Organization field values where not saved correctly");

        }

        [TestMethod]
        public void Can_Edit_Organization()
        {
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationCreator.EditSimpleOrganization();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationSavedAfterEdit, "Organization was not saved successfully after edit");
            AssertThat.IsTrue(OrganizationCreator.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly after edit");

        }


        [TestMethod]
        public void Can_Import_Organization_Template()
        {
            OrganizationCreator.ImportSimpleContact();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully");
            AssertThat.IsTrue(OrganizationCreator.AreOrganizationFieldValuesSavedCorrectly, "Organization field values where not saved correctly");

       }
    }
}
