using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Import a gmail csv file that contains 1 contact that has value in every contact field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportGmailCsvContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

        /// <summary>
        /// Import a gmail csv file that contains 1 contact that has no value for mandatory field
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Value_In_Mandatory_Field()
        {
            ContactCreator.ImportGmailCsvContactWithoutLastName();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it does not contain value for last name field");
            
        }

        /// <summary>
        /// Import a gmail csv file that contains 1 contact that overflow values for first and last name fields
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportGmailCsvContactWithOverflowValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully but it does not contain value for last name field");

        }
    }
}
