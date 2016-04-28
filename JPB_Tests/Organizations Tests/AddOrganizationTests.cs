using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class AddOrganizationTests : OrganizationsBaseTest
    {
 
        /// <summary>
        /// Create 1 organization with all its fields filled
        /// </summary>
        [TestMethod]
        public void Create_Organization_With_All_Fields_Filled()
        {
            OrganizationCreator.CreateOrganizationWithAllValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationCreatedSuccessfully, "Organization was not saved successfully though it should have.");
            AssertThat.IsTrue(OrganizationCreator.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        // Assert that is not possible to create an organization without value for organization name

        // Assert that is not possible to create an organization if at least one field contains a value that exceeds 50 characters

        // Assert that is possible to create an organization that has nonsense values to some of its fields

        // Assert that when adding an extra field during organization creation, if the field is left empty, it does not show at organization's view page, after organization has been saved

    }
}
