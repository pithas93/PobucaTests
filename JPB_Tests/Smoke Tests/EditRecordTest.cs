using JPB_Framework;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
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
            AssertThat.IsTrue(ContactViewPage.IsAt, "Failed to open contact view");
            EditContactPage.GoTo();
            AssertThat.IsTrue(EditContactPage.IsAt, "Failed to open contact for edit");
        }

        [TestMethod]
        public void Can_Edit_Organization()
        {

            OrganizationsPage.GoTo();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");

            OrganizationsPage.OpenOrganization();
            AssertThat.IsTrue(OrganizationViewPage.IsAt, "Failed to open organization view");
            EditOrganizationPage.GoTo();
            AssertThat.IsTrue(EditOrganizationPage.IsAt, "Failed to open organization for edit");

        }

    }
}
