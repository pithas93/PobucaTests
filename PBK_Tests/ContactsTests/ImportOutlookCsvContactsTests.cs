using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportOutlookCsvContactsTests : ContactsBaseTest
    {

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that has value in every contact field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportOutlookCsvContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that has no value for mandatory field
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Value_In_Mandatory_Field()
        {
            ContactCreator.ImportOutlookCsvContactWithoutLastName();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it does not contain value for last name field");

        }

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that overflow values for first and last name fields
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportOutlookCsvContactWithOverflowValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it contains overflow values in its fields");

        }

        /// <summary>
        /// Assert that when check for duplicate full names is being made, csv files that contain the same contact twice, import only one the two same contacts
        /// </summary>
        [TestMethod]
        public void Import_Contact_File_With_The_Same_Contact_Twice()
        {
            ContactCreator.ImportOutlookCsvWithTheSamesContactTwice();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedWithDuplicates, "Contact was imported successfully but it does not contain value for last name field");

            ContactsPage.FindContact()
               .WithFirstName(ContactCreator.FirstContact.FirstName)
               .AndLastName(ContactCreator.FirstContact.LastName)
               .Find();
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel, 1, $"There should be only one contact with name '{ContactCreator.FirstContact.FullName}' being displayed. It seems that the second twin contact was imported successfully");
        }

        /// <summary>
        /// Assert that when check for duplicate full names is being made, csv files that contain the same contact twice, import only one the two same contacts
        /// </summary>
        [TestMethod]
        public void Import_Contact_That_Already_Exists()
        {
            ContactCreator.ImportOutlookCsvContactsThatAlreadyExists();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedWithDuplicates, "Contact was imported successfully but it does not contain value for last name field");
            ContactsPage.FindContact()
               .WithFirstName(ContactCreator.FirstContact.FirstName)
               .AndLastName(ContactCreator.FirstContact.LastName)
               .Find();
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel, 1, $"There should be only one contact with name '{ContactCreator.FirstContact.FullName}' being displayed. It seems that the second twin contact was imported successfully");
        }
    }
}
