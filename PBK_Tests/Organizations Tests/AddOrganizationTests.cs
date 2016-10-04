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
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.IsOrganizationCreatedSuccessfully, "Organization was not saved successfully though it should have.");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }
 
        /// <summary>
        /// Assert that is not possible to create an organization without value for organization name
        /// </summary>
        [TestMethod]
        public void Cannot_Leave_Organization_Mandatory_Fields_Empty()
        {
            OrganizationCreator.CreateOrganizationWithoutOrganizationName();
            AssertThat.IsFalse(OrganizationCreator.FirstOrganization.IsOrganizationCreatedSuccessfully, "Organization was created successfully though it should not.");
        }

        /// <summary>
        /// Assert that is not possible to create an organization if at least one field contains a value that exceeds 50 characters
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Organization_With_Overflown_Values()
        {
            OrganizationCreator.CreateOrganizationWithOverflowValues();
            AssertThat.IsFalse(OrganizationCreator.FirstOrganization.IsOrganizationCreatedSuccessfully, "Organization was created successfully though it should not.");
        }

        /// <summary>
        /// Assert that is possible to create an organization that has nonsense values to some of its fields
        /// </summary>
        [TestMethod]
        public void Create_Organization_With_Nonsense_Field_Values()
        {
            OrganizationCreator.CreateOrganizationWithNonsenseValues();
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.IsOrganizationCreatedSuccessfully, "Organization was not saved successfully though it should have.");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Assert that when adding an extra field during organization creation, if the field is left empty, it does not show at organization's view page, after organization has been saved
        /// </summary>
        [TestMethod]
        public void Create_Organization_With_Extra_Null_Fields()
        {
            OrganizationCreator.CreateOrganizationWithNullValuesInExtraFields();
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.IsOrganizationCreatedSuccessfully, "Organization was not saved successfully though it should have.");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

    }
}
