using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportContactsTests : JpbBaseTest
    {
        //  Test not implemented because downloading files through automation is not suggested
        // 1. Check that contacts template is successfully downloaded. 

        // 2. Import contacts - all contact fields are filled 
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts2.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();

            AssertThat.IsTrue(ContactsPage.FindDummyContact(),"Contact was not imported successfully");
            ContactsPage.OpenContact();
            VerifyThat.IsTrue(ContactViewPage.AreContactFieldValuesImportedCorrectly, "Contact field values are not the same with those in the import file.");

            ContactViewPage.DeleteContact();
        }

        // 3. Import contacts - only mandatory fields are filled
        [TestMethod]
        public void Import_Contacts_With_Only_Mandatory_Fields_Filled()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts3.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();
           
            AssertThat.IsTrue(ContactsPage.FindDummyContact(), "Contact was not imported successfully.");
            ContactsPage.DeleteDummyContact();
        }

        // 4. Import contacts - only mandatory fields are left unfilled
        [TestMethod]
        public void Import_Contacts_With_Mandatory_Fields_Unfilled()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts4.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();           
        }

        // 5. Import contacts - only mandatory fields are filled. Organization field takes existent value

        // 6. Import contacts - only mandatory fields are filled. Organization field takes nonexistent value
        [TestMethod]
        public void Import_Contacts_With_Nonexistent_Organization()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts6.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();
        }

        // 7. Import contacts - Fields are filled with nonsense values
        [TestMethod]
        public void Import_Contacts_With_Nonsense_Values()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts7.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();
        }

        // 8. Import contacts - Fields are filled with values so that it cause field character overflow
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts8.xls").Submit();
            AssertThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            ImportContactsWindow.CloseImportDialogBox();
        }

        // 9. Import contacts - Test the max imported contact threshold

        // 10. Import contacts - Birthdate field contains invalid for date

        // 11. Import contacts - Template contains less columns than the original template

        // 12. Import contacts - Template contains more columns that the original template

        // 13. Import contacts - Template contains null rows between normal contact rows

        // 14. Immport contacts - Template does not contain the mandatory field column

        // 15. Import ontacts - Template contains duplicate contacts

        // 16. Import contacts - Template contains columns in different order than that of the original template

    }
}
