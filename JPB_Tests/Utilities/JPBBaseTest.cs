using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class JpbBaseTest
    {

        [TestInitialize]
        public void SetUp()
        {
            Driver.Initialize(Browser.Firefox);
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
