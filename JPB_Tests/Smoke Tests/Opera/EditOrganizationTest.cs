using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests.Opera
{
    [TestClass]
    public class EditOrganizationTest : JpbBaseTest_Opera
    {

        [TestMethod]
        public void Can_Edit_Organization()
        {

            OrganizationsPage.GoTo();
            Assert.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");

            OrganizationsPage.SelectOrganization();
            Assert.IsTrue(OrganizationViewPage.IsAt, "Failed to open organization view");
            EditOrganizationPage.GoTo();
            Assert.IsTrue(EditOrganizationPage.IsAt, "Failed to open organization for edit");

        }

    }
}
