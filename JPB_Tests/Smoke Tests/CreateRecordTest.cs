using JPB_Framework;
using JPB_Framework.Pages.Organizations;
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

            Assert.IsTrue(ContactViewPage.IsAt);
            Assert.AreEqual(ContactViewPage.FirstName, "Panagiotis", "First name was not saved correctly");
            Assert.AreEqual(ContactViewPage.LastName, "Mavrogiannis", "Last name was not saved correctly");

            ContactViewPage.DeleteContact().Delete();

            Assert.IsTrue(ContactsPage.IsAt, "Failed to show organizations page");
            Assert.IsFalse(ContactsPage.FindContactWithFirstName("Panagiotis").AndLastName("Mavrogiannis").Find());
        }

        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            OrganizationsPage.GoTo();
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            Assert.IsTrue(OrganizationViewPage.IsAt);
            Assert.AreEqual(OrganizationViewPage.OrganizationName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.DeleteOrganization().OnlyOrganization();

            Assert.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");
            Assert.IsFalse(OrganizationsPage.FindOrganizationWithName("SiEBEN").Find());
        }

    }
}