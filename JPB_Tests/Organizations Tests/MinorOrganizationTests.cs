using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class MinorOrganizationTests : OrganizationsBaseTest
    {
        /// <summary>
        /// Assert that inside organization view page, the organization's field value is a link that drives browser to prompt user for a telephone call
        /// </summary>
        [TestMethod]
        public void Call_An_Organization_Telephone_Number()
        {
            OrganizationCreator.CreateSimpleOrganization();
            AssertThat.IsTrue(OrganizationViewPage.IsPhoneNumberCallable, "Organization telephone number is not callable");
        }


    }
}
