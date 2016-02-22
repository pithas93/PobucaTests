using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests.Chrome
{
    public class JpbBaseTest_Chrome
    {

        [TestInitialize]
        public void SetUp()
        {
            Driver.Initialize(Browser.Chrome);
            LoginPage.GoTo();
            LoginPage.LoginAs("panagiotis@panagof1.com").WithPassword("6AB10F93").Login();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Driver.Close();
        }
    }
}
