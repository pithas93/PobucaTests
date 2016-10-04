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
    public class EditOrganizationTests : OrganizationsBaseTest
    {

        /// <summary>
        /// Edit an existing organization, assigning values to its fields that are nonsense
        /// </summary>
        [TestMethod]
        public void Edit_Organization_And_Assign_Nonsense_Values()
        {
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationCreator.EditOrganizationAssigningNonsenseValues(OrganizationCreator.FirstOrganization);
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.IsOrganizationSavedAfterEdit, "Organization was not saved successfully after edit though, it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly after edit");
        }

        /// <summary>
        /// Edit an existing organization, assigning new values for every field
        /// </summary>
        [TestMethod]
        public void Edit_Every_Organization_Field_From_Existing_Organization()
        {
            OrganizationCreator.CreateOrganizationWithAllValues();
            OrganizationCreator.EditOrganizationAlteringAllValues(OrganizationCreator.FirstOrganization);
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.IsOrganizationSavedAfterEdit, "Organization was not saved successfully after edit though, it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly after edit");
        }

        /// <summary>
        /// Assert that is not possible saving changes in an existing organization, if organization name field is left empty
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Organization_After_Leaving_Mandatory_Fields_Empty()
        {
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationCreator.EditOrganizationRemovingOrganizationName(OrganizationCreator.FirstOrganization);
            AssertThat.IsFalse(OrganizationCreator.FirstOrganization.IsOrganizationSavedAfterEdit, "Organization was saved successfully after edit though it should not because its name was left empty");
        }

        /// <summary>
        /// Assert that is not possible saving changes in an existing organization, if at at least one field has its value exceeding the 50-character limit
        /// </summary>
        [TestMethod]
        public void Cannot_Save_Organization_After_Assigning_Overflown_Values()
        {
            OrganizationCreator.CreateSimpleOrganization();
            OrganizationCreator.EditOrganizationAssigningOverflowValues(OrganizationCreator.FirstOrganization);
            AssertThat.IsFalse(OrganizationCreator.FirstOrganization.IsOrganizationSavedAfterEdit, "Organization was saved successfully after edit though it should not because its field values exceed the character limit");
        }


    }
}
