using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class ContactsOrganizationsBaseTest : JpbBaseTest
    {

        [TestInitialize]
        public void SetUp()
        {
            ContactCreator.Initialize();
            OrganizationCreator.Initialize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ContactCreator.CleanUp();
            OrganizationCreator.CleanUp();
        }

    }
}
