using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests
{
    [TestClass]
    public class CreateOrganizationTest : JpbBaseTest
    {
        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            OrganizationsPage.GoTo();
            Assert.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");
            NewOrganizationPage.GoTo();
            Assert.IsTrue(NewOrganizationPage.IsAt, "Failed to open new organization");
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            Assert.IsTrue(OrganizationViewPage.IsAt);
            Assert.AreEqual(OrganizationViewPage.FirstName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.Delete().OnlyOrganization();
        }

    }
}
