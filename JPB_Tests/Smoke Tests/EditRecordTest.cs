using JPB_Framework;
using JPB_Framework.Pages.Organizations;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class EditRecordTest : JpbBaseTest
    {
        
        [TestMethod]
        public void Can_Edit_Contact()
        {
            ContactsPage.OpenContact();
            Assert.IsTrue(ContactViewPage.IsAt, "Failed to open contact view");
            EditContactPage.GoTo();
            Assert.IsTrue(EditContactPage.IsAt, "Failed to open contact for edit");
        }

        [TestMethod]
        public void Can_Edit_Organization()
        {

            OrganizationsPage.GoTo();
            Assert.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");

            OrganizationsPage.OpenOrganization();
            Assert.IsTrue(OrganizationViewPage.IsAt, "Failed to open organization view");
            EditOrganizationPage.GoTo();
            Assert.IsTrue(EditOrganizationPage.IsAt, "Failed to open organization for edit");

        }

    }
}
