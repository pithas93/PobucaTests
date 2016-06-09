using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportTemplateContactsTests : ContactsBaseTest
    {

        /// <summary>
        /// Import a contact template that contains 1 contact that has value in every contact field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_All_Contact_Fields_Filled()
        {
            ContactCreator.ImportTemplateContactWithAllValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported successfully");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        /// <summary>
        /// Import a contact template that contains 1 contact which has first name value but no value in last name which is a mandatory field
        /// </summary>
        [TestMethod]
        public void Import_Contact_Without_Values_In_Mandatory_Field()
        {
            ContactCreator.ImportTemplateContactWithoutLastName();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully though last name field was left null. Defect spotted!");

        }


        /// <summary>
        /// Import a contact template that contains 1 contact that is linked with a non existent organization.
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Nonexistent_Organization()
        {
            ContactCreator.ImportTemplateContactWithInvalidOrganization();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully though organization name value contains a non existent organization. Defect spotted!"); 

        }

        /// <summary>
        /// Import a contact template that contains 1 contact which has every field filled with nonsense values.
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Nonsense_Values()
        {
            ContactCreator.ImportTemplateContactWithNonsenseValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact that contains 1 contact which has first and last name fields filled with values that exceed 50 character limit
        /// </summary>
        [TestMethod]
        public void Import_Contact_With_Overflow_Field_Values()
        {
            ContactCreator.ImportTemplateContactWithOverflowValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported successfully though first and last name values exceed character limit. Defect spotted!");

        }

        /// <summary>
        /// Import a contact template that contains 1 contact with invalid date format value in birthdate field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Invalid_Birthdate_Values()
        {
            ContactCreator.ImportTemplateContactWithInvalidBirthdate();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact were not imported but they should.");
            ContactsPage.FindContact().WithFirstName(ContactCreator.FirstContact.FirstName).AndLastName(ContactCreator.FirstContact.LastName).Open();
            VerifyThat.AreEqual(ContactViewPage.Birthdate,"", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.FirstContact.Birthdate} which is invalid.");

            ContactsPage.FindContact().WithFirstName(ContactCreator.SecondContact.FirstName).AndLastName(ContactCreator.SecondContact.LastName).Open();
            VerifyThat.AreEqual(ContactViewPage.Birthdate, "", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.SecondContact.Birthdate} which is invalid.");

            ContactsPage.FindContact().WithFirstName(ContactCreator.ThirdContact.FirstName).AndLastName(ContactCreator.ThirdContact.LastName).Open();
            VerifyThat.AreEqual(ContactViewPage.Birthdate, "", $"Contact birthdate should be empty because imported contact's birthdate value is {ContactCreator.ThirdContact.Birthdate} which is invalid.");

        }

        /// <summary>
        /// Import a contact template that contains less than normal columns
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Containing_Less_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithLessColumns();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact template that contains more than normal columns
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Containing_More_Columns_Than_Normal()
        {
            ContactCreator.ImportTemplateWithMoreColumns();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }

        /// <summary>
        /// Import a contact template that does not contain the mandatory field column
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_Without_Mandatory_Column()
        {
            ContactCreator.ImportTemplateWithoutMandatoryColumn();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact was imported but it should not because contact file did not contain the mandatory last name column.");

        }

        /// <summary>
        /// Import a contact template that contains columns in different order than that of the original template
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_With_Columns_In_Random_Order()
        {
            ContactCreator.ImportTemplateWithColumnsInRandomOrder();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contact was not imported but it should.");
            AssertThat.IsTrue(ContactCreator.FirstContact.AreContactFieldValuesSavedCorrectly, "Contact field values where not saved correctly");

        }


        /// <summary>
        /// Import a contact template that contains a contact twice (2 contacts with the same first and last name). Test checks whether duplicate filter works correctly
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_The_Same_Contact_Twice()
        {
            ContactCreator.ImportTemplateWithTwinContacts();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedWithDuplicates, "At least one of the 2 duplicate contacts should have been imported but neither did.");

            ContactsPage.FindContact()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Find();
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel,1,
                $"There should be only one contact with name '{ContactCreator.FirstContact.FullName}' being displayed. It seems that the second twin contact was imported successfully");
        }

        /// <summary>
        /// Import a contact template that contains a contact that already exists within contact list (the 2 contacts will have the same full name).Test checks whether duplicate filter works correctly
        /// </summary>
        [TestMethod]
        public void Import_Contact_That_Already_Exist_Within_Contacts()
        {
            ContactCreator.ImportTemplateWithAnExistingContact();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedWithDuplicates, "At least one of the 2 duplicate contacts should have been imported but neither did.");

            ContactsPage.FindContact()
                .WithFirstName(ContactCreator.FirstContact.FirstName)
                .AndLastName(ContactCreator.FirstContact.LastName)
                .Find();
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel, 1, $"There should be only one contact with name '{ContactCreator.FirstContact.FullName}' being displayed. It seems that the second twin contact was imported successfully");
        }

        /// <summary>
        /// Import a contact template that contains 2 contacts of whom, one has invalid value for a combo field
        /// </summary>
        [TestMethod]
        public void Import_Contacts_With_Invalid_Combo_Box_Values()
        {
            ContactCreator.ImportTemplateContactWithInvalidComboValues();
            AssertThat.IsTrue(ContactCreator.IsContactFileFailedToImport, "Contact with invalid combo box values was imported but it should not");
        }

        /// <summary>
        /// Import a contact template that contains 3 contacts that have void lines in between them
        /// </summary>
        [TestMethod]
        public void Import_Contacts_Template_With_Void_Lines_Between_Contacts()
        {
            ContactCreator.ImportTemplateWithVoidLinesBetweenContacts();
            AssertThat.IsTrue(ContactCreator.IsContactFileImportedSuccessfully, "Contacts were not imported but they should.");
        }
    }
}
