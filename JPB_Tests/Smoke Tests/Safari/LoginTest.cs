using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests.Safari
{
    [TestClass]
    public class LoginTest : JpbBaseTest_Safari
    {
     
        [TestMethod]
        public void Can_Go_Login()
        {
           
            Assert.IsTrue(ContactsPage.IsAt, "Failed to login");
        }
     
      
    }
}
