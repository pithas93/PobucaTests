using JPB_Framework;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class LoginTest : JpbBaseTest
    {
     
        [TestMethod]
        public void Can_Login()
        {

            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to login");
            AssertThat.IsTrue(ContactsPage.ContactListIsLoaded, "Failed to load contact list");
        }
     
      
    }
}
