using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests.Safari
{
    [TestClass]
    public class CreateContactTest : JpbBaseTest_Safari
    {

        [TestMethod]
        public void Can_Create_A_Simple_Contact()
        {
            NewContactPage.GoTo();
            Assert.IsTrue(NewContactPage.IsAt, "Failed to open new contact");
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
