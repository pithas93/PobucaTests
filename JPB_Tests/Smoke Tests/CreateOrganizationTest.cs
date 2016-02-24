﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class CreateOrganizationTest : JpbBaseTest
    {
        [TestMethod]
        public void Can_Create_A_Simple_Organization()
        {
            OrganizationsPage.GoTo();
            NewOrganizationPage.CreateOrganization("SiEBEN").Create();

            Assert.IsTrue(OrganizationViewPage.IsAt);
            Assert.AreEqual(OrganizationViewPage.OrganizationName, "SiEBEN", "Organization name was not saved correctly");

            OrganizationViewPage.DeleteOrganization().OnlyOrganization();

            Assert.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations page");
            Assert.IsFalse(OrganizationsPage.DoesOrganizationExistWithName("SiEBEN"));
        }

    }
}
