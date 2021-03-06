﻿using System;
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

        /// <summary>
        /// Import 1 organization that has values to every field
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Values_In_All_Fields()
        {
            OrganizationCreator.ImportOrganizationWithAllValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import 1 organization that has its mandatory field empty
        /// </summary>
        [TestMethod]
        public void Import_Organization_Without_Mandatory_Field_Value()
        {
            OrganizationCreator.ImportOrganizationWithoutOrganizationName();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was imported successfully but does not contain value for organization name field");
        }

        /// <summary>
        /// Import 1 organization that contains nonsense values to its fields
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Nonsense_Values()
        {
            OrganizationCreator.ImportOrganizationWithNonsenseValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import 1 organization that contains values to its fields that exceed the character limit
        /// </summary>
        [TestMethod]
        public void Import_Organization_With_Overflow_Values()
        {
            OrganizationCreator.ImportOrganizationWithOverflowValues();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was imported successfully but its field values exceed the 50 character limit");
        }

        /// <summary>
        /// Import a template that has less columns than ordinary
        /// </summary>
        [TestMethod]
        public void Import_Template_With_Less_Columns()
        {
            OrganizationCreator.ImportTemplateWithLessColumns();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import a template that has more columns than ordinary
        /// </summary>
        [TestMethod]
        public void Import_Template_With_More_Columns()
        {
            OrganizationCreator.ImportTemplateWithMoreColumns();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Import a template that does not contain the mandatory field column
        /// </summary>
        [TestMethod]
        public void Import_Template_Without_Mandatory_Column()
        {
            OrganizationCreator.ImportTemplateWithoutOrganizationNameColumn();
            AssertThat.IsFalse(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was imported successfully though it should not because it does not contain organization name column");
        }

        /// <summary>
        /// Import a template that has its columns set in a random order
        /// </summary>
        [TestMethod]
        public void Import_Template_With_Columns_In_Random_Order()
        {
            OrganizationCreator.ImportTemplateWithColumnsInRandomOrder();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Organization was not imported successfully thought it should");
            AssertThat.IsTrue(OrganizationCreator.FirstOrganization.AreOrganizationFieldValuesSavedCorrectly, "Organization field values were not saved correctly");
        }

        /// <summary>
        /// Assert that, when 'check for duplicate organization name' filter is checked during import, if template contains an existing organization, that organization is dismissed
        /// </summary>
        [TestMethod]
        public void Import_Organization_That_Already_Exist_Within_Contacts()
        {
            OrganizationCreator.ImportTemplateWithAnExistingOrganization();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedWithDuplicates, "At least one of the 2 duplicate organizations should have been imported but neither did.");

            OrganizationsPage.FindOrganization()
                .WithOrganizationName(OrganizationCreator.FirstOrganization.OrganizationName)
                .Find();
            AssertThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, 1, $"There should be only one organization with name '{OrganizationCreator.FirstOrganization.OrganizationName}' being displayed. It seems that the second twin organization was imported successfully");
        }

        /// <summary>
        /// Assert that, when 'check for duplicate organization name' filter is checked during import, if template contains an organization with the same name twice, only one of them is imported
        /// </summary>
        [TestMethod]
        public void Import_Organizations_With_The_Same_Organization_Twice()
        {
            OrganizationCreator.ImportTemplateWithTwinOrganizations();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedWithDuplicates, "At least one of the 2 duplicate organizations should have been imported but neither did.");

            OrganizationsPage.FindOrganization()
                .WithOrganizationName(OrganizationCreator.FirstOrganization.OrganizationName)
                .Find();
            AssertThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel,1,
                $"There should be only one organziation with name '{OrganizationCreator.FirstOrganization.OrganizationName}' being displayed. It seems that the second twin organization was imported successfully");
        }

        /// <summary>
        /// Assert that when importing a template that contains void lines in between its containing records, the import is successfull
        /// </summary>
        [TestMethod]
        public void Import_Organizations_With_Void_Lines_Between_Organizations()
        {
            OrganizationCreator.ImportTemplateWithVoidLinesBetweenOrganizations();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileImportedSuccessfully, "Contacts were not imported but they should.");
        }

        /// <summary>
        /// Assert that when importing a template that contains organizations with invalid combo field values, the import fails
        /// </summary>
        [TestMethod]
        public void Import_Organizations_With_Invalid_Combo_Box_Values()
        {
            OrganizationCreator.ImportTemplateOrganizationWithInvalidComboValues();
            AssertThat.IsTrue(OrganizationCreator.IsOrganizationFileFailedToImport, "Organization with invalid combo box values was imported but it should not");
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
