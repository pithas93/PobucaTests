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
    public class SmokeTests : JpbBaseTest
    {

        [TestMethod]
        public void Can_Login()
        {
            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to login");
            AssertThat.IsTrue(ContactsPage.IsContactListLoaded, "Failed to load contact list");
        }

        [TestMethod]
        public void Can_Create_A_Simple_Contact()
        {
            ContactCreator.CreateSimpleContact();
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organization list page");
            AssertThat.IsTrue(OrganizationsPage.IsOrganizationListLoaded, "Failed to load organization list");
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            AssertThat.IsTrue(OrganizationViewPage.IsAt, "Failed to show organization detail view page");
            AssertThat.AreEqual(OrganizationViewPage.OrganizationName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.DeleteOrganization().OnlyOrganization();

            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().WithOrganizationName("SiEBEN").Find(), "Organization was not deleted successfully");
        }

        [TestMethod]
        public void Can_Edit_Contact()
        {
            ContactCreator.CreateSimpleContact();
            AssertThat.IsTrue(ContactViewPage.IsAt, "Failed to open contact view");
            ContactCreator.EditSimpleContact();
            AssertThat.IsTrue(ContactViewPage.IsAt, "Failed to open contact view");

        }

        [TestMethod]
        public void Can_Edit_Organization()
        {

            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");

            OrganizationsPage.OpenOrganization();
            AssertThat.IsTrue(OrganizationViewPage.IsAt, "Failed to open organization view");
            EditOrganizationPage.GoTo();
            AssertThat.IsTrue(EditOrganizationPage.IsAt, "Failed to open organization for edit");

        }

        [TestMethod]
        public void Can_Import_Contact_Template()
        {
            
            ContactCreator.ImportSimpleContact();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        [TestMethod]
        public void Can_Import_Organization_Template()
        {
            LeftSideMenu.GoToImports();
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations1.xls").Submit();

            OrganizationsPage.FindOrganization().WithOrganizationName("SiEBEN").Delete(DeleteType.OnlyOrganization);

            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().WithOrganizationName("SiEBEN").Find(), "Previously imported organization failed to be deleted");
        }
    }
}
