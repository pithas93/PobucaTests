using JPB_Framework;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class LoginTests : JpbBaseTest
    {
     
        [TestMethod]
        public void Can_Login()
        {

            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to login");
            AssertThat.IsTrue(ContactsPage.IsContactListLoaded, "Failed to load contact list");
        }
     
      
    }
}
