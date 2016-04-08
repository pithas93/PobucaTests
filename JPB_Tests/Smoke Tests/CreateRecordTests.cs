using System.Text;
using JPB_Framework;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class CreateRecordTests : JpbBaseTest
    {

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
            AssertThat.IsTrue(OrganizationsPage.IsAt,"Failed to show organization list page");
            AssertThat.IsTrue(OrganizationsPage.IsOrganizationListLoaded,"Failed to load organization list");
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            AssertThat.IsTrue(OrganizationViewPage.IsAt, "Failed to show organization detail view page");
            AssertThat.AreEqual(OrganizationViewPage.OrganizationName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.DeleteOrganization().OnlyOrganization();

            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().WithOrganizationName("SiEBEN").Find(), "Organization was not deleted successfully");
        }

    }
}