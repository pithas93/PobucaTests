using System;
using System.Collections.Generic;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Workflows
{
    public class ContactCreator
    {

        // Maybe create a mechanism to create/import more than 1 contacts at a test and then check all values
        //        public static int InitialContactsCount { get; set; }

        public static Contact FirstContact { get; set; }
        public static Contact SecondContact { get; set; }

        public static Contact ThirdContact { get; set; }

        private static Contact CurrentContact { get; set; }

        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static void Initialize()
        {
            FirstContact = new Contact();
            SecondContact = new Contact();
            ThirdContact = new Contact();
        }

        public static void CleanUp()
        {
            FirstContact.CleanUp();
            SecondContact.CleanUp();
            ThirdContact.CleanUp();
        }

        /// <summary>
        /// Returns true if contact was saved successfully on its creation.
        /// </summary>
        private static bool IsContactCreatedSuccessfully => NewContactPage.IsContactSavedSuccessfully;

        /// <summary>
        /// Returns true if contact was saved successfully after edit.
        /// </summary>
        private static bool IsContactSavedAfterEdit => EditContactPage.IsContactSavedSuccessfully;


        /// <summary>
        /// Returns true if contact was imported successfully.
        /// </summary>
        public static bool IsContactImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;

        /// <summary>
        /// Determines which contact will hold the data for the new contact that will be created
        /// </summary>
        private static void SetCurrentContact()
        {
            if (string.IsNullOrEmpty(FirstContact.FirstName) && string.IsNullOrEmpty(FirstContact.LastName))
            {
                CurrentContact = FirstContact;
                return;
            }
            if (string.IsNullOrEmpty(SecondContact.FirstName) && string.IsNullOrEmpty(SecondContact.LastName))
            {
                CurrentContact = SecondContact;
                return;
            }
            throw new Exception();
        }


        /// <summary>
        /// Create a simple contact with dummy first, last and organization name and phone values.
        /// </summary>
        public static void CreateSimpleContact()
        {
            SetCurrentContact();

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;
            var organizationName = DummyData.OrganizationValue;
            var mobilePhone = DummyData.PhoneValue;


            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithMobilePhone(mobilePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
            CurrentContact.SetFieldValue("Organization Name", organizationName);
            CurrentContact.SetFieldValue("Mobile Phone", mobilePhone);
        }



        /// <summary>
        /// Create a simple contact with dummy first, last name and phone values.
        /// </summary>
        public static void CreateSimpleOrphanContact()
        {
            SetCurrentContact();

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;
            var mobilePhone = DummyData.PhoneValue;

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithMobilePhone(mobilePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
            CurrentContact.SetFieldValue("Mobile Phone", mobilePhone);
        }

        /// <summary>
        /// Create a simple contact with dummy first and last name and default organization the one where the browser is currently navigated to. 
        /// You have to be at OrganizationViewPage of an organization for this method to work properly
        /// </summary>
        public static void CreateSimpleContactFromWithinOrganization()
        {
            SetCurrentContact();

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;

            OrganizationViewPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
            CurrentContact.SetFieldValue("Organization Name", OrganizationViewPage.OrganizationName);
            CurrentContact.SetFieldValue("Website", OrganizationViewPage.Website);
            CurrentContact.SetFieldValue("Work Street", OrganizationViewPage.BillingStreet);
            CurrentContact.SetFieldValue("Work City", OrganizationViewPage.BillingCity);
            CurrentContact.SetFieldValue("Work State", OrganizationViewPage.BillingState);
            CurrentContact.SetFieldValue("Work Postal Code", OrganizationViewPage.BillingPostalCode);
            CurrentContact.SetFieldValue("Work Country", OrganizationViewPage.BillingCountry);
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {
            SetCurrentContact();

            var tmp = new Contact();

            tmp.SetFieldValue("First Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Last Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Middle Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Suffix", DummyData.SimpleWord);
            tmp.SetFieldValue("Organization Name", DummyData.OrganizationValue);
            tmp.SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Email", DummyData.EmailValue);
            tmp.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            tmp.SetFieldValue("Department", DummyData.DepartmentValue);
            tmp.SetFieldValue("Work Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Work Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Other Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Personal Email", DummyData.EmailValue);
            tmp.SetFieldValue("Other Email", DummyData.EmailValue);
            tmp.SetFieldValue("Work Street", DummyData.AddressValue);
            tmp.SetFieldValue("Work City", DummyData.SimpleWord);
            tmp.SetFieldValue("Work State", DummyData.SimpleWord);
            tmp.SetFieldValue("Work Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Work Country", DummyData.CountryValue);
            tmp.SetFieldValue("Home Street", DummyData.AddressValue);
            tmp.SetFieldValue("Home City", DummyData.SimpleWord);
            tmp.SetFieldValue("Home State", DummyData.SimpleWord);
            tmp.SetFieldValue("Home Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Home Country", DummyData.CountryValue);
            tmp.SetFieldValue("Other Street", DummyData.AddressValue);
            tmp.SetFieldValue("Other City", DummyData.SimpleWord);
            tmp.SetFieldValue("Other State", DummyData.SimpleWord);
            tmp.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Other Country", DummyData.CountryValue);
            tmp.SetFieldValue("Salutation", DummyData.SimpleWord);
            tmp.SetFieldValue("Nickname", DummyData.SimpleWord);
            tmp.SetFieldValue("Job Title", DummyData.SimpleWord);
            tmp.SetFieldValue("Website", DummyData.SimpleWord);
            tmp.SetFieldValue("Religion", DummyData.SimpleWord);
            tmp.SetFieldValue("Birthdate", DummyData.DateValue);
            tmp.SetFieldValue("Gender", DummyData.GenderValue);
            tmp.SetFieldValue("Comments", DummyData.SimpleText);

            NewContactPage.CreateContact().WithMultipleValues(tmp.BasicContactFields, tmp.ExtraContactFields, tmp.BooleanContactFields).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.Clone(tmp);
        }

        /// <summary>
        /// Create a contact with first name value but without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            SetCurrentContact();

            var firstName = DummyData.SimpleWord;

            NewContactPage.CreateContact().WithFirstName(firstName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
        }

        /// <summary>
        /// Create a contact with values in first and last name that exceed the 50 character limit
        /// </summary>
        public static void CreateContactWithOverflowValues()
        {
            SetCurrentContact();

            var firstName = DummyData.OverflowWordValue;
            var lastName = DummyData.OverflowWordValue;

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
        }

        /// <summary>
        /// Create a contact with nonsense values in its fields
        /// </summary>
        public static void CreateContactWithNonsenseValues()
        {
            SetCurrentContact();

            var firstName = DummyData.NonsenseValue;
            var lastName = DummyData.NonsenseValue;

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
        }

        /// <summary>
        /// Create a contact which is linked to a non existent organization
        /// </summary>
        public static void CreateContactWithInvalidOrganization()
        {
            SetCurrentContact();

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;
            var organizationName = DummyData.SimpleWord;
            var homePhone = DummyData.PhoneValue;

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithHomePhone(homePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", firstName);
            CurrentContact.SetFieldValue("Last Name", lastName);
            CurrentContact.SetFieldValue("Organization Name", organizationName);
            CurrentContact.SetFieldValue("Home Phone", homePhone);
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValuesInExtraFields()
        {
            SetCurrentContact();

            var tmp = new Contact();

            tmp.SetFieldValue("First Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Last Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Home Phone", string.Empty);
            tmp.SetFieldValue("Personal Email", string.Empty);
            tmp.SetFieldValue("Work City", string.Empty);
            tmp.SetFieldValue("Nickname", string.Empty);

            NewContactPage.CreateContact().WithMultipleValues(tmp.BasicContactFields, tmp.ExtraContactFields, tmp.BooleanContactFields).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

            if (!CurrentContact.IsContactCreatedSuccessfully) return;
            CurrentContact.Clone(tmp);
        }

        /// <summary>
        /// Edit a previously created contact by changing its first and last names
        /// </summary>
        public static void EditSimpleContact(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("First Name", firstName);
            editedContact.SetFieldValue("Last Name", lastName);
        }

        /// <summary>
        /// Edit a previously created contact by changing its first, last and organization names
        /// </summary>
        public static void EditSimpleContactWithOrganization(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;
            var organizationName = DummyData.OrganizationValue;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).WithNewOrganizationName(organizationName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("First Name", firstName);
            editedContact.SetFieldValue("Last Name", lastName);
            editedContact.SetFieldValue("Organization Name", organizationName);
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from last name field
        /// </summary>
        public static void EditContactRemovingLastName(Contact editedContact)
        {
            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var lastName = string.Empty;

            EditContactPage.EditContact().WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("Last Name", lastName);

        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that exceed the field character limit
        /// </summary>
        public static void EditContactAssigningOverflowValues(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.OverflowWordValue;
            var lastName = DummyData.OverflowWordValue;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("First Name", firstName);
            editedContact.SetFieldValue("Last Name", lastName);
        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that contain symbols, number and characters
        /// </summary>
        public static void EditContactAssigningNonsenseValues(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.NonsenseValue;
            var lastName = DummyData.NonsenseValue;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("First Name", firstName);
            editedContact.SetFieldValue("Last Name", lastName);
        }

        /// <summary>
        /// Edit a previously created contact by assigning an invalid organization value to the respective field
        /// </summary>
        public static void EditContactAssigningInvalidOrganization(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var organizationName = DummyData.SimpleWord;
            var homePhone = DummyData.PhoneValue;

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).WithNewMobilePhone(homePhone).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("Organization Name", organizationName);
            editedContact.SetFieldValue("Home Phone", homePhone);
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from organization name field thus rendering it orphan
        /// </summary>
        public static void EditContactRemovingOrganization(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var organizationName = string.Empty;

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue("Organization Name", organizationName);
        }

        /// <summary>
        /// Edit a previously created contact changing every single field value.
        /// </summary>
        public static void EditContactAlteringAllValues(Contact editedContact)
        {

            if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName))))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var tmp = new Contact();

            tmp.SetFieldValue("First Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Last Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Middle Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Suffix", DummyData.SimpleWord);
            tmp.SetFieldValue("Organization Name", DummyData.OrganizationValue);
            tmp.SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Email", DummyData.EmailValue);
            tmp.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            tmp.SetFieldValue("Department", DummyData.DepartmentValue);
            tmp.SetFieldValue("Work Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            tmp.SetFieldValue("Home Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Work Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Other Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Personal Email", DummyData.EmailValue);
            tmp.SetFieldValue("Other Email", DummyData.EmailValue);
            tmp.SetFieldValue("Work Street", DummyData.AddressValue);
            tmp.SetFieldValue("Work City", DummyData.SimpleWord);
            tmp.SetFieldValue("Work State", DummyData.SimpleWord);
            tmp.SetFieldValue("Work Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Work Country", DummyData.CountryValue);
            tmp.SetFieldValue("Home Street", DummyData.AddressValue);
            tmp.SetFieldValue("Home City", DummyData.SimpleWord);
            tmp.SetFieldValue("Home State", DummyData.SimpleWord);
            tmp.SetFieldValue("Home Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Home Country", DummyData.CountryValue);
            tmp.SetFieldValue("Other Street", DummyData.AddressValue);
            tmp.SetFieldValue("Other City", DummyData.SimpleWord);
            tmp.SetFieldValue("Other State", DummyData.SimpleWord);
            tmp.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Other Country", DummyData.CountryValue);
            tmp.SetFieldValue("Salutation", DummyData.SimpleWord);
            tmp.SetFieldValue("Nickname", DummyData.SimpleWord);
            tmp.SetFieldValue("Job Title", DummyData.SimpleWord);
            tmp.SetFieldValue("Website", DummyData.SimpleWord);
            tmp.SetFieldValue("Religion", DummyData.SimpleWord);
            tmp.SetFieldValue("Birthdate", DummyData.DateValue);
            tmp.SetFieldValue("Gender", DummyData.GenderValue);
            tmp.SetFieldValue("Comments", DummyData.SimpleText);

            EditContactPage.EditContact().WithMultipleNewValues(tmp.BasicContactFields, tmp.ExtraContactFields, tmp.BooleanContactFields).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.Clone(tmp);
        }

        /// <summary>
        /// Import template file with 1 contact. Only first and last name fields have values
        /// </summary>
        public static void ImportTemplateSimpleContact()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts1.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import template file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportTemplateContactWithAllValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
            FirstContact.SetFieldValue("Middle Name", "Emmanouil");
            FirstContact.SetFieldValue("Suffix", "Mr");
            FirstContact.SetFieldValue("Organization Name", "KONICA MINOLTA");
            FirstContact.SetFieldValue("Mobile Phone", "6912345678");
            FirstContact.SetFieldValue("Email", "email@email.com");
            FirstContact.SetFieldValue("Allow SMS", "False");
            FirstContact.SetFieldValue("Allow Phones", "False");
            FirstContact.SetFieldValue("Allow Emails", "True");

            FirstContact.SetFieldValue("Department", "Administration");
            FirstContact.SetFieldValue("Work Phone", "2101234567");
            FirstContact.SetFieldValue("Work Phone 2", "2111234567");
            FirstContact.SetFieldValue("Mobile Phone 2", "69213456789");
            FirstContact.SetFieldValue("Home Phone", "1234567890");
            FirstContact.SetFieldValue("Home Phone 2", "0987654321");
            FirstContact.SetFieldValue("Home Fax", "1234567890");
            FirstContact.SetFieldValue("Work Fax", "0987654322");
            FirstContact.SetFieldValue("Other Phone", "2143658709");
            FirstContact.SetFieldValue("Personal Email", "myemail@email.com");
            FirstContact.SetFieldValue("Other Email", "otheremail@email.com");
            FirstContact.SetFieldValue("Work Street", "Παπαφλέσσα 10");
            FirstContact.SetFieldValue("Work City", "Τρίκαλα");
            FirstContact.SetFieldValue("Work State", "ΝΥ");
            FirstContact.SetFieldValue("Work Postal Code", "12345");
            FirstContact.SetFieldValue("Work Country", "Greece");
            FirstContact.SetFieldValue("Home Street", "Παπαφλέσσα 11");
            FirstContact.SetFieldValue("Home City", "Γιάννενα");
            FirstContact.SetFieldValue("Home State", "CA");
            FirstContact.SetFieldValue("Home Postal Code", "12345");
            FirstContact.SetFieldValue("Home Country", "Greece");
            FirstContact.SetFieldValue("Other Street", "otherstreet");
            FirstContact.SetFieldValue("Other City", "othercity");
            FirstContact.SetFieldValue("Other State", "OS");
            FirstContact.SetFieldValue("Other Postal Code", "otherpostal");
            FirstContact.SetFieldValue("Other Country", "Wallis and Futuna");
            FirstContact.SetFieldValue("Salutation", "His majesty");
            FirstContact.SetFieldValue("Nickname", "Νοντας");
            FirstContact.SetFieldValue("Job Title", "Ταξιτζής");
            FirstContact.SetFieldValue("Website", "http://www.facebook.com/TheNontas");
            FirstContact.SetFieldValue("Religion", "Αγνωστικιστής");
            FirstContact.SetFieldValue("Birthdate", "28/2/1980");
            FirstContact.SetFieldValue("Gender", "Other");
            FirstContact.SetFieldValue("Comments", "No comments");

        }

        /// <summary>
        /// Import template file with 1 contact. Only first name field has value. Last name field is left null 
        /// </summary>
        public static void ImportTemplateContactWithoutLastName()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import template file with 1 contact. First and last name have assigned values. Organization name has an non-existent organization as value
        /// </summary>
        public static void ImportTemplateContactWithInvalidOrganization()
        {

            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
            FirstContact.SetFieldValue("Organization Name", "Rivendale Corp");
        }

        /// <summary>
        /// Import template file with 1 contact. Every contact field has non-sense value assigned except for the combo ones and the organization name.
        /// </summary>
        public static void ImportTemplateContactWithNonsenseValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts7.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "#$@#$");
            FirstContact.SetFieldValue("Last Name", "#$@#$");
            FirstContact.SetFieldValue("Middle Name", "#$@#$");
            FirstContact.SetFieldValue("Suffix", "#$@#$");
            FirstContact.SetFieldValue("Organization Name", "KONICA MINOLTA");
            FirstContact.SetFieldValue("Mobile Phone", "#$@#$");
            FirstContact.SetFieldValue("Email", "#$@#$");
            FirstContact.SetFieldValue("Allow SMS", "False");
            FirstContact.SetFieldValue("Allow Phones", "False");
            FirstContact.SetFieldValue("Allow Emails", "False");

            FirstContact.SetFieldValue("Department", "Administration");
            FirstContact.SetFieldValue("Work Phone", "#$@#$");
            FirstContact.SetFieldValue("Work Phone 2", "#$@#$");
            FirstContact.SetFieldValue("Mobile Phone 2", "#$@#$");
            FirstContact.SetFieldValue("Home Phone", "#$@#$");
            FirstContact.SetFieldValue("Home Phone 2", "#$@#$");
            FirstContact.SetFieldValue("Home Fax", "#$@#$");
            FirstContact.SetFieldValue("Work Fax", "#$@#$");
            FirstContact.SetFieldValue("Other Phone", "#$@#$");
            FirstContact.SetFieldValue("Personal Email", "#$@#$");
            FirstContact.SetFieldValue("Other Email", "#$@#$");
            FirstContact.SetFieldValue("Work Street", "#$@#$");
            FirstContact.SetFieldValue("Work City", "#$@#$");
            FirstContact.SetFieldValue("Work State", "#$@#$");
            FirstContact.SetFieldValue("Work Postal Code", "#$@#$");
            FirstContact.SetFieldValue("Work Country", "Afghanistan");
            FirstContact.SetFieldValue("Home Street", "#$@#$");
            FirstContact.SetFieldValue("Home City", "#$@#$");
            FirstContact.SetFieldValue("Home State", "#$@#$");
            FirstContact.SetFieldValue("Home Postal Code", "#$@#$");
            FirstContact.SetFieldValue("Home Country", "Afghanistan");
            FirstContact.SetFieldValue("Other Street", "#$@#$");
            FirstContact.SetFieldValue("Other City", "#$@#$");
            FirstContact.SetFieldValue("Other State", "#$@#$");
            FirstContact.SetFieldValue("Other Postal Code", "#$@#$");
            FirstContact.SetFieldValue("Other Country", "Afghanistan");
            FirstContact.SetFieldValue("Salutation", "#$@#$");
            FirstContact.SetFieldValue("Nickname", "#$@#$");
            FirstContact.SetFieldValue("Job Title", "#$@#$");
            FirstContact.SetFieldValue("Website", "#$@#$");
            FirstContact.SetFieldValue("Religion", "#$@#$");
            FirstContact.SetFieldValue("Birthdate", "#$@#$");
            FirstContact.SetFieldValue("Gender", "Female");
            FirstContact.SetFieldValue("Comments", "#$@#$");
        }

        /// <summary>
        /// Import template file with 1 contact. First and last name fields have assigned values that exceed the 50 characters
        /// </summary>
        public static void ImportTemplateContactWithOverflowValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
            FirstContact.SetFieldValue("Last Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
        }

        /// <summary>
        /// Import template file with 3 contacts. Beside first and last name, birthdate fields contain invalid date values
        /// </summary>
        public static void ImportTemplateContactWithInvalidBirthdate()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts10.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis1");
            FirstContact.SetFieldValue("Birthdate", "32/1/2000");

            SecondContact.SetFieldValue("First Name", "Panagiotis");
            SecondContact.SetFieldValue("Last Name", "Mavrogiannis2");
            SecondContact.SetFieldValue("Birthdate", "29/2/2001");

            ThirdContact.SetFieldValue("First Name", "Panagiotis");
            ThirdContact.SetFieldValue("Last Name", "Mavrogiannis3");
            ThirdContact.SetFieldValue("Birthdate", "12/13/2000");
        }

        /// <summary>
        /// Import template file with 1 contact. Template contains less columns than original
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
            FirstContact.SetFieldValue("Email", "test");
            FirstContact.SetFieldValue("Work City", "test");
            FirstContact.SetFieldValue("Home State", "test");

        }

        /// <summary>
        /// Import template file with 1 contact. Template contains 1 more column than original
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts12.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import template file with 1 contact. Template does not contain the mandatory field column
        /// </summary>
        public static void ImportTemplateWithoutMandatoryColumn()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import template file with 1 contact. Template columns are placed in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts16.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
            FirstContact.SetFieldValue("Allow Emails", "True");
            FirstContact.SetFieldValue("Allow SMS", "True");
            FirstContact.SetFieldValue("Allow Phones", "True");
            FirstContact.SetFieldValue("Birthdate", "15/5/1987");
            FirstContact.SetFieldValue("Mobile Phone", "6944833390");
            FirstContact.SetFieldValue("Work Phone", "null");
            FirstContact.SetFieldValue("Email", ".");
            FirstContact.SetFieldValue("Home Street", "ΑΝΩΓΕΙΩΝ 60");
            FirstContact.SetFieldValue("Home City", "ΗΡΑΚΛΕΙΟ");
            FirstContact.SetFieldValue("Home Postal Code", "71304");

        }

        /// <summary>
        /// Import gmail csv file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportGmailCsvContactWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts1.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
            FirstContact.SetFieldValue("Last Name", "Mavrogiannis");
            FirstContact.SetFieldValue("Middle Name", "Emmanouil");
            FirstContact.SetFieldValue("Suffix", "Sir");
            FirstContact.SetFieldValue("Mobile Phone", "6949585690");

            FirstContact.SetFieldValue("Email", "p.mavrogiannis@sieben.gr");

            FirstContact.SetFieldValue("Work Phone", "2130179000");


            FirstContact.SetFieldValue("Home Phone", "2294048550");

            FirstContact.SetFieldValue("Home Fax", "2294048551");
            FirstContact.SetFieldValue("Work Fax", "2130179001");

            FirstContact.SetFieldValue("Personal Email", "p.mavrogiannis@outlook.com");
            FirstContact.SetFieldValue("Other Email", "apaixtoos@hotmail.com");

            FirstContact.SetFieldValue("Work Street", "Aristomenous 3");
            FirstContact.SetFieldValue("Work City", "Gerakas");
            FirstContact.SetFieldValue("Work State", "Attica");
            FirstContact.SetFieldValue("Work Postal Code", "10613");
            FirstContact.SetFieldValue("Work Country", "Greece");

            FirstContact.SetFieldValue("Home Street", "Papachristou 4");
            FirstContact.SetFieldValue("Home City", "Artemida");
            FirstContact.SetFieldValue("Home State", "Attica");
            FirstContact.SetFieldValue("Home Postal Code", "19016");
            FirstContact.SetFieldValue("Home Country", "Greece");

            FirstContact.SetFieldValue("Other Street", "Karavan Serai 2");
            FirstContact.SetFieldValue("Other City", "Constantinople");
            FirstContact.SetFieldValue("Other State", "Thrace");
            FirstContact.SetFieldValue("Other Postal Code", "12345");
            FirstContact.SetFieldValue("Other Country", "Greece");
            FirstContact.SetFieldValue("Salutation", "Mr.");
            FirstContact.SetFieldValue("Nickname", "Panagof");

            FirstContact.SetFieldValue("Website", "www.sieben.gr");

            FirstContact.SetFieldValue("Birthdate", "27/1/1990");
            FirstContact.SetFieldValue("Comments", "His majesty sir tester of the United Kingodom of Testingburg");
        }

        public static void ImportGmailCsvContactWithoutLastName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts2.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "Panagiotis");
        }

        public static void ImportGmailCsvContactWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts3.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue("First Name", "MRPanagiotisPanagiotisPanagiotisPanagiotisMRPanagiotisPanagiotis");
            FirstContact.SetFieldValue("Last Name", "MRPanagiotisPanagiotisPanagiotisPanagiotisMRPanagiotisPanagiotis");
        }
    }
}
