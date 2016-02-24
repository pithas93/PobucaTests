using JPB_Framework;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class CreateContactTest : JpbBaseTest
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
            Assert.IsFalse(ContactsPage.DoesContactExistWithFirstName("Panagiotis").AndLastName("Mavrogiannis"));
        }

    }
}