using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportGmailCsvContactsTests : ContactsBaseTest
    {

        /// <summary>
        /// Assert that importing a gmail csv file that contains 1 contact that has value in every contact field, is successful
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportGmailCsvContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

        /// <summary>
        /// Assert that when importing a gmail csv file that contains no value for the mandatory field, the import is unsuccessful
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Value_In_Mandatory_Field()
        {
            ContactCreator.ImportGmailCsvContactWithoutLastName();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it does not contain value for last name field");
            
        }

        /// <summary>
        /// Assert that when importing a gmail csv file that contains overflow values for at least one field, the import is unsuccessful
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportGmailCsvContactWithOverflowValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it does not contain value for last name field");

        }

        /// <summary>
        /// Assert that when check for duplicate full names is being made, csv files that contain the same contact twice, import only one the two same contacts
        /// </summary>
        [TestMethod]
        public void Import_Contact_File_With_The_Same_Contact_Twice()
        {
            ContactCreator.ImportGmailCsvWithTheSamesContactTwice();
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
            ContactCreator.ImportGmailCsvContactsThatAlreadyExists();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedWithDuplicates, "Contact was imported successfully but it does not contain value for last name field");
            ContactsPage.FindContact()
               .WithFirstName(ContactCreator.FirstContact.FirstName)
               .AndLastName(ContactCreator.FirstContact.LastName)
               .Find();
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel, 1, $"There should be only one contact with name '{ContactCreator.FirstContact.FullName}' being displayed. It seems that the second twin contact was imported successfully");
        }
    }
}
