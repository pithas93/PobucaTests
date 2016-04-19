using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Selenium;
using JPB_Framework.Workflows;
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
            ContactCreator.ImportContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        // 4. Import contacts - only mandatory fields are left unfilled
        [TestMethod]
        public void Import_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.ImportContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was created successfully though last name field was left null. Defect spotted!");


            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts4.xls").Submit();
            //            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            VerifyThat.IsFalse(ContactsPage.FindContact().WithFirstName("Panagiotis").Find(), "Contact was imported successfully though it has no value in the last name which is mandatory");
            //            if (ContactsPage.FindContact().WithFirstName("Panagiotis").Find())
            //                ContactsPage.FindContact().WithFirstName("Panagiotis").Delete();
        }

        // 5. Import contacts - only mandatory fields are filled. Organization field takes existent value

        // 6. Import contacts - only mandatory fields are filled. Organization field takes nonexistent value
        [TestMethod]
        public void Import_Contacts_With_Nonexistent_Organization()
        {
            ContactCreator.ImportContactWithInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            LeftSideMenu.GoToContacts();
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstName).AndLastName(ContactCreator.LastName).Open();
            AssertThat.AreEqual(ContactViewPage.OrganizationName, "", $"Organization ought to be null but it has value = '{ContactViewPage.OrganizationName}' which is invalid. Defect spotted!");


            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts6.xls").Submit();
            //            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            VerifyThat.IsFalse(ContactsPage.FindDummyContacts(), "Contact was imported successfully though it is linked to a non-existent organization.");
            //            if (ContactsPage.FindDummyContacts())
            //                ContactsPage.DeleteDummyContacts();
        }

        // 7. Import contacts - Fields are filled with nonsense values
        [TestMethod]
        public void Import_Contacts_With_Nonsense_Values()
        {
            ContactCreator.ImportContactWithNonsenseValues();

//            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts7.xls").Submit();
//            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
//            ImportContactsWindow.CloseImportDialogBox();
//
//            AssertThat.IsTrue(ContactsPage.FindContact().WithFirstName("#$@#$").AndLastName("%@#$").Find(), "Contact was not imported successfully.");
//            ContactsPage.FindContact().WithFirstName("#$@#$").AndLastName("%@#$").Delete();

        }

        // 8. Import contacts - Fields are filled with values so that it cause field character overflow
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportContactWithOverflowValues();

//            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts8.xls").Submit();
//            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
//            ImportContactsWindow.CloseImportDialogBox();
//
//            VerifyThat.IsFalse(ContactsPage.FindContact().WithFirstName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").AndLastName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").Find(), "Contact was imported successfully thought its field values exceed the character overflow limit.");
//            if (ContactsPage.FindContact().WithFirstName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").AndLastName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").Find())
//                ContactsPage.FindContact().WithFirstName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").AndLastName("qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890").Delete();
        }

        // 9. Import contacts - Test the max imported contact threshold

        // 10. Import contacts - Birthdate field contains invalid for date
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_1()
        {
            ContactCreator.ImportContactWithInvalidBirthdate1();

//            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts10.xls").Submit();
//            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
//            ImportContactsWindow.CloseImportDialogBox();
//
//            VerifyThat.IsFalse(ContactsPage.FindDummyContacts(),"Contacts were imported successfully thought with invalid birthdate");
//            if (ContactsPage.FindDummyContacts())
//                ContactsPage.DeleteDummyContacts();
        }

        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_2()
        {
            ContactCreator.ImportContactWithInvalidBirthdate2();

            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts10.xls").Submit();
            //            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            VerifyThat.IsFalse(ContactsPage.FindDummyContacts(),"Contacts were imported successfully thought with invalid birthdate");
            //            if (ContactsPage.FindDummyContacts())
            //                ContactsPage.DeleteDummyContacts();
        }

        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_3()
        {
            ContactCreator.ImportContactWithInvalidBirthdate3();

            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts10.xls").Submit();
            //            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            VerifyThat.IsFalse(ContactsPage.FindDummyContacts(),"Contacts were imported successfully thought with invalid birthdate");
            //            if (ContactsPage.FindDummyContacts())
            //                ContactsPage.DeleteDummyContacts();
        }

        // 11. Import contacts - Template contains less columns than the original template
        [TestMethod]
        public void Import_Contacts_Template_Containing_Less_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithLessColumns();
//            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts11.xls").Submit();
//            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
//            ImportContactsWindow.CloseImportDialogBox();
//
//            AssertThat.IsTrue(ContactsPage.FindDummyContacts(), "Contact was not imported successfully.");
//            ContactsPage.DeleteDummyContacts();
        }

        // 12. Import contacts - Template contains more columns that the original template
        [TestMethod]
        public void Import_Contacts_Template_Containing_More_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithMoreColumns();
//            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts12.xls").Submit();
//            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
//            ImportContactsWindow.CloseImportDialogBox();
//
//            AssertThat.IsTrue(ContactsPage.FindDummyContacts(), "Contact was not imported successfully.");
//            ContactsPage.DeleteDummyContacts();
        }

        // 14. Immport contacts - Template does not contain the mandatory field column
        [TestMethod]
        public void Import_Contacts_Template_Without_Mandatory_Column()
        {
            ContactCreator.ImportTemplateWithoutMandatoryColumn();
            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts14.xls").Submit();
            //            VerifyThat.IsTrue(ImportContactsWindow.IsImportFailedMessageShown, "There should be an error message informing user that the file import has failed. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            VerifyThat.IsFalse(ContactsPage.FindContact().WithFirstName("Panagiotis").Find(), "Contact was imported successfully though the import file, from which it was imported, contained no 'LastName' column");
            //            if (ContactsPage.FindContact().WithFirstName("Panagiotis").Find())
            //                ContactsPage.FindContact().WithFirstName("Panagiotis").Delete();
        }

        // 15. Import contacts - Template contains duplicate contacts

        // 16. Import contacts - Template contains columns in different order than that of the original template
        [TestMethod]
        public void Import_Contacts_Template_With_Columns_In_Random_Order()
        {
            ContactCreator.ImportTemplateWithColumnsInRandomOrder();
            //            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts16.xls").Submit();
            //            AssertThat.IsTrue(ImportContactsWindow.IsImportSuccessMessageShown, "There should be an message informing user that import was successful. The message did not show up.");
            //            ImportContactsWindow.CloseImportDialogBox();
            //
            //            AssertThat.IsTrue(ContactsPage.FindDummyContacts(), "Contacts were not imported successfully.");
            //            ContactsPage.DeleteDummyContacts();
        }

    }
}
