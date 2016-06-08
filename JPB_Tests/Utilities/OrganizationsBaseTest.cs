using JPB_Framework.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class OrganizationsBaseTest : JpbBaseTest
    {

        [TestInitialize]
        public void SetUp() => OrganizationCreator.Initialize();
        

        [TestCleanup]
        public void CleanUp() => OrganizationCreator.CleanUp();
        

    }
}