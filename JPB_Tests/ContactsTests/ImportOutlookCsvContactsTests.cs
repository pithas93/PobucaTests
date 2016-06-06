using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportOutlookCsvContactsTests
    {

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that has value in every contact field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportOutlookCsvContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");
        }

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that has no value for mandatory field
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Value_In_Mandatory_Field()
        {
            ContactCreator.ImportOutlookCsvContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully but it does not contain value for last name field");

        }

        /// <summary>
        /// Import an outlook csv file that contains 1 contact that overflow values for first and last name fields
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportOutlookCsvContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully but it does not contain value for last name field");

        }
    }
}
