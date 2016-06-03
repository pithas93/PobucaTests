using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class ImportOrganizationTests : OrganizationsBaseTest
    {
        // 15. Import organizations - Template contains duplicate organizations
        //          -an h timh einai idia gia dyo epafes tote 8a eisagetai h mia apo tis 2 kai oles oi ypoloipes epityxws
        // oti de dexetai arxeia me koukoutoukou times se combo box

        /// <summary>
        /// Import 1 organization that has values to every field
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Values_In_All_Fields()
        {
            OrganizationCreator.ImportOrganizationWithAllValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import 1 organization that has its mandatory field empty
        /// </summary>
        [TestMethod]
        public void Import_Organization_Without_Mandatory_Field_Value()
        {
            OrganizationCreator.ImportOrganizationWithoutOrganizationName();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was imported successfully but does not contain value for organization name field");
        }

        /// <summary>
        /// Import 1 organization that contains nonsense values to its fields
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Nonsense_Values()
        {
            OrganizationCreator.ImportOrganizationWithNonsenseValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import 1 organization that contains values to its fields that exceed the character limit
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Overflow_Values()
        {
            OrganizationCreator.ImportOrganizationWithOverflowValues();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was imported successfully but its field values exceed the 50 character limit");
        }

        /// <summary>
        /// Import a template that has less columns than ordinary
        /// </summary>
        [TestMethod]
        public void Import_Template_With_Less_Columns()
        {
            OrganizationCreator.ImportTemplateWithLessColumns();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import a template that has more columns than ordinary
        /// </summary>
        [TestMethod]
        public void Import_Template_With_More_Columns()
        {
            OrganizationCreator.ImportTemplateWithMoreColumns();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import a template that does not contain the mandatory field column
        /// </summary>
        [TestMethod]
        public void Import_Template_Without_Mandatory_Column()
        {
            OrganizationCreator.ImportTemplateWithoutOrganizationNameColumn();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was imported successfully though it should not because it does not contain organization name column");
        }

        /// <summary>
        /// Import a template that has its columns set in a random order
        /// </summary>
        [TestMethod]
        public void Import_Template_With_Columns_In_Random_Order()
        {
            OrganizationCreator.ImportTemplateWithColumnsInRandomOrder();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import 1 organization with value for Primary Contact field that belongs to another organization
        /// </summary>
//        [TestMethod]
//        public void Import_Organization_With_Primary_Contact_That_Belongs_To_Another_Organization()
//        {
//            OrganizationCreator.ImportOrganizationWithPrimaryContactThatBelongsToAnotherOrganization();
//            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
//
//            OrganizationsPage.FindOrganization().WithOrganizationName(OrganizationCreator.OrganizationName).Open();
//            AssertThat.AreEqual(OrganizationViewPage.PrimaryContact, "", "Previously imported organization has as primary contact, a contact that is linked to another existing organization");
//
//            ContactsPage.FindContact().ContainingKeyword(OrganizationCreator.PrimaryContact).Open();
//            OrganizationsPage.FindOrganization().WithOrganizationName(ContactViewPage.OrganizationName).Open();
//
//            AssertThat.AreEqual(OrganizationCreator.PrimaryContact, OrganizationViewPage.PrimaryContact, $"Previously imported organization should have set contact '{OrganizationCreator.PrimaryContact}' as primary in its organization, but it did not.");
//
//        }

        /// <summary>
        /// Import 1 organization with value for Primary Contact field that does not exist in contact list
        /// </summary>
//        [TestMethod]
//        public void Import_Organization_With_Primary_Contact_That_Does_Not_Exist()
//        {
//            OrganizationCreator.ImportOrganizationWithPrimaryContactThatDoesNotExist();
//            AssertThat.IsTrue(OrganizationCreator.IsOrganizationImportedSuccessfully, "Organization was not imported successfully thought it should");
//
//            OrganizationsPage.FindOrganization().WithOrganizationName(OrganizationCreator.OrganizationName).Open();
//            AssertThat.AreEqual(OrganizationViewPage.PrimaryContact, "", "Previously imported organization has as primary contact, a contact that is linked to another existing organization");
//
//        }
    }
}
