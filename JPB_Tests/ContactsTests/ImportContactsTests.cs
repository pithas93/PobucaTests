using JPB_Framework.Report;
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
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though last name field was left null. Defect spotted!");

        }

        // 5. Import contacts - only mandatory fields are filled. Organization field takes existent value

        // 6. Import contacts - only mandatory fields are filled. Organization field takes nonexistent value
        [TestMethod]
        public void Import_Contacts_With_Nonexistent_Organization()
        {
            ContactCreator.ImportContactWithInvalidOrganization();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though organization name value contains a non existent organization. Defect spotted!"); 

        }

        // 7. Import contacts - Fields are filled with nonsense values
        [TestMethod]
        public void Import_Contacts_With_Nonsense_Values()
        {
            ContactCreator.ImportContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 8. Import contacts - Fields are filled with values so that it cause field character overflow
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though first and last name values exceed character limit. Defect spotted!");

        }

        // 9. Import contacts - Test the max imported contact threshold

        // 10. Import contacts - Birthdate field contains invalid for date
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_1()
        {
            ContactCreator.ImportContactWithInvalidBirthdate1();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.AreEqual(ContactCreator.Birthdate,"", $"Contact birthdate value is {ContactCreator.Birthdate} which is invalid.");

        }

        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_2()
        {
            ContactCreator.ImportContactWithInvalidBirthdate2();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.AreEqual(ContactCreator.Birthdate, "", $"Contact birthdate value is {ContactCreator.Birthdate} which is invalid.");

        }

        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_3()
        {
            ContactCreator.ImportContactWithInvalidBirthdate3();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.AreEqual(ContactCreator.Birthdate, "", $"Contact birthdate value is {ContactCreator.Birthdate} which is invalid.");

        }

        // 11. Import contacts - Template contains less columns than the original template
        [TestMethod]
        public void Import_Contacts_Template_Containing_Less_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithLessColumns();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 12. Import contacts - Template contains more columns that the original template
        [TestMethod]
        public void Import_Contacts_Template_Containing_More_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithMoreColumns();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 14. Immport contacts - Template does not contain the mandatory field column
        [TestMethod]
        public void Import_Contacts_Template_Without_Mandatory_Column()
        {
            ContactCreator.ImportTemplateWithoutMandatoryColumn();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported but it should not because contact file did not contain the mandatory last name column.");

        }

        // 15. Import contacts - Template contains duplicate contacts

        // 16. Import contacts - Template contains columns in different order than that of the original template
        [TestMethod]
        public void Import_Contacts_Template_With_Columns_In_Random_Order()
        {
            ContactCreator.ImportTemplateWithColumnsInRandomOrder();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

    }
}
