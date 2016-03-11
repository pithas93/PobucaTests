using System.Text;
using JPB_Framework;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class CreateRecordTest : JpbBaseTest
    {

        [TestMethod]
        public void Can_Create_A_Simple_Contact()
        {


            NewContactPage.CreateContact("Panagiotis").withLastName("Mavrogiannis").Create();

            AssertThat.IsTrue(ContactViewPage.IsAt, "Failed to show contact detail view page");
            AssertThat.AreEqual(ContactViewPage.FirstName, "Panagiotis", "First name was not saved correctly");
            AssertThat.AreEqual(ContactViewPage.LastName, "Mavrogiannis", "Last name was not saved correctly");

            ContactViewPage.DeleteContact().Delete();

            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to show contacts list page");
            VerifyThat.IsFalse(ContactsPage.FindContact().WithFirstName("Panagiotis").AndLastName("Mavrogiannis").Find(), "Contact was not deleted successfully");
            

        }

        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            OrganizationsPage.GoTo();
            AssertThat.IsTrue(OrganizationsPage.IsAt,"Failed to show organization list page");
            AssertThat.IsTrue(OrganizationsPage.OrganizationListIsLoaded,"Failed to load organization list");
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            AssertThat.IsTrue(OrganizationViewPage.IsAt, "Failed to show organization detail view page");
            AssertThat.AreEqual(OrganizationViewPage.OrganizationName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.DeleteOrganization().OnlyOrganization();

            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().WithName("SiEBEN").Find(), "Organization was not deleted successfully");
        }

    }
}