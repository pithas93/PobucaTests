using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests
{
    [TestClass]
    public class LoginTest : JpbBaseTest
    {
     
        [TestMethod]
        public void Can_Go_Login()
        {
           
            Assert.IsTrue(ContactsPage.IsAt, "Failed to login");
        }
     
      
    }
}
