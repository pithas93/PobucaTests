using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportContactsTests : ContactsBaseTest
    {

        // 1. Check that contacts template is successfully downloaded. Test not implemented because downloading files through automation is not suggested
        // 9. Import contacts - Test the max imported contact threshold
        
        // oti mporw na kanw export apo to outlook kai gmail ta contacts mou kai na ta kanw import me thn epilogh csv.file


        /// <summary>
        /// Import a contact template that contains 1 contact that has value in every contact field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        /// <summary>
        /// Import a contact template that contains 1 contact which has first name value but no value in last name which is a mandatory field
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.ImportContactWithoutLastName();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though last name field was left null. Defect spotted!");

        }


        /// <summary>
        /// Import a contact template that contains 1 contact that is linked with a non existent organization.
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Nonexistent_Organization()
        {
            ContactCreator.ImportContactWithInvalidOrganization();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though organization name value contains a non existent organization. Defect spotted!"); 

        }

        /// <summary>
        /// Import a contact template that contains 1 contact which has every field filled with nonsense values.
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Nonsense_Values()
        {
            ContactCreator.ImportContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact that contains 1 contact which has first and last name fields filled with values that exceed 50 character limit
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportContactWithOverflowValues();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported successfully though first and last name values exceed character limit. Defect spotted!");

        }


        /// <summary>
        /// Import a contact template that contains 1 contact with invalid date format value in birthdate field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_1()
        {
            ContactCreator.ImportContactWithInvalidBirthdate1();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            AssertThat.AreEqual(ContactViewPage.Birthdate,"", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.FirstContact.Birthdate} which is invalid.");

        }

        /// <summary>
        /// Import a contact template that contains 1 contact with invalid date format value in birthdate field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_2()
        {
            ContactCreator.ImportContactWithInvalidBirthdate2();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            AssertThat.AreEqual(ContactViewPage.Birthdate, "", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.FirstContact.Birthdate} which is invalid.");

        }

        /// <summary>
        /// Import a contact template that contains 1 contact with invalid date format value in birthdate field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values_3()
        {
            ContactCreator.ImportContactWithInvalidBirthdate3();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            AssertThat.AreEqual(ContactViewPage.Birthdate, "", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.FirstContact.Birthdate} which is invalid.");

        }

        /// <summary>
        /// Import a contact template that contains less than normal columns
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Containing_Less_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithLessColumns();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact template that contains more than normal columns
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Containing_More_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithMoreColumns();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact template that does not contain the mandatory field column
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Without_Mandatory_Column()
        {
            ContactCreator.ImportTemplateWithoutMandatoryColumn();
            AssertThat.IsFalse(ContactCreator.IsContactImportedSuccessfully, "Contact was imported but it should not because contact file did not contain the mandatory last name column.");

        }

        /// <summary>
        /// Import a contact template that contains columns in different order than that of the original template
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_With_Columns_In_Random_Order()
        {
            ContactCreator.ImportTemplateWithColumnsInRandomOrder();
            AssertThat.IsTrue(ContactCreator.IsContactImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        // 15. Import contacts - Template contains duplicate contacts
        //          -an h timh einai idia gia dyo epafes tote 8a eisagetai h mia apo tis 2 kai oles oi ypoloipes epityxws
        [TestMethod]
        public void Import_Contacts_With_Duplicate_First_And_Last_Name()
        {
//            ContactCreator.ImportContacts
        }
    }
}
