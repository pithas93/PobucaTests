using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Can_Go_Login_Page()
        {
            LoginPage.GoTo();
            Assert.IsTrue(LoginPage.IsAt, "Is not at Login Page");

            LoginPage.LoginAs("panagiotis@panagof1.com").WithPassword("6AB10F93").Login();
            Assert.IsTrue(ContactsPage.IsAt, "Failed to login");
        }
    }
}
