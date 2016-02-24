using JPB_Framework;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class EditContactTest : JpbBaseTest
    {
        
        [TestMethod]
        public void Can_Edit_A_Contact()
        {
            ContactsPage.SelectContact();
            Assert.IsTrue(ContactViewPage.IsAt, "Failed to open contact view");
            EditContactPage.GoTo();
            Assert.IsTrue(EditContactPage.IsAt, "Failed to open contact for edit");
        }
        
    }
}
