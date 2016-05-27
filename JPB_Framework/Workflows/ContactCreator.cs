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

        private static Contact CurrentContact { get; set; }

        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static void Initialize()
        {
//            InitialContactsCount = ContactsPage.ContactsBeingDisplayed;
            FirstContact = new Contact();
            SecondContact = new Contact();

            FirstContact.Initialize();
            SecondContact.Initialize();
        }

        public static void CleanUp()
        {
            FirstContact.CleanUp();
            SecondContact.CleanUp();
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

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = CurrentContact.SetFieldValue("Organization Name", DummyData.OrganizationValue);
            var mobilePhone = CurrentContact.SetFieldValue("Mobile Phone", DummyData.PhoneValue);


            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithMobilePhone(mobilePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }



        /// <summary>
        /// Create a simple contact with dummy first, last name and phone values.
        /// </summary>
        public static void CreateSimpleOrphanContact()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            var mobilePhone = CurrentContact.SetFieldValue("Mobile Phone", DummyData.PhoneValue);

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithMobilePhone(mobilePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a simple contact with dummy first and last name and default organization the one where the browser is currently navigated to. 
        /// You have to be at OrganizationViewPage of an organization for this method to work properly
        /// </summary>
        public static void CreateSimpleContactFromWithinOrganization()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = CurrentContact.SetFieldValue("Organization Name", OrganizationViewPage.OrganizationName);
            var website = CurrentContact.SetFieldValue("Website", OrganizationViewPage.Website);
            var workStreet = CurrentContact.SetFieldValue("Work Street", OrganizationViewPage.BillingStreet);
            var workCity = CurrentContact.SetFieldValue("Work City", OrganizationViewPage.BillingCity);
            var workState = CurrentContact.SetFieldValue("Work State", OrganizationViewPage.BillingState);
            var workPostalCode = CurrentContact.SetFieldValue("Work Postal Code", OrganizationViewPage.BillingPostalCode);
            var workCountry = CurrentContact.SetFieldValue("Work Country", OrganizationViewPage.BillingCountry);

            OrganizationViewPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {
            SetCurrentContact();

            CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Middle Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Suffix", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Organization Name", DummyData.OrganizationValue);
            CurrentContact.SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            CurrentContact.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            CurrentContact.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            CurrentContact.SetFieldValue("Department", DummyData.DepartmentValue);
            CurrentContact.SetFieldValue("Work Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Fax", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Work Fax", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Other Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Personal Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Other Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Work Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Work City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Work State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Work Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Work Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Home Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Home City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Home State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Home Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Home Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Other Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Other City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Other State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Other Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Salutation", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Nickname", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Job Title", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Website", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Religion", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Birthdate", DummyData.DateValue);
            CurrentContact.SetFieldValue("Gender", DummyData.GenderValue);
            CurrentContact.SetFieldValue("Comments", DummyData.SimpleText);

            NewContactPage.CreateContact().WithMultipleValues(CurrentContact.BasicContactFields, CurrentContact.ExtraContactFields, CurrentContact.BooleanContactFields).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;

        }

        /// <summary>
        /// Create a contact with first name value but without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);

            NewContactPage.CreateContact().WithFirstName(firstName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a contact with values in first and last name that exceed the 50 character limit
        /// </summary>
        public static void CreateContactWithOverflowValues()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.OverflowWordValue);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.OverflowWordValue);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a contact with nonsense values in its fields
        /// </summary>
        public static void CreateContactWithNonsenseValues()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.NonsenseValue);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.NonsenseValue);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a contact which is linked to a non existent organization
        /// </summary>
        public static void CreateContactWithInvalidOrganization()
        {
            SetCurrentContact();

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = CurrentContact.SetFieldValue("Organization Name", DummyData.SimpleWord);
            var homePhone = CurrentContact.SetFieldValue("Home Phone", DummyData.PhoneValue);

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithHomePhone(homePhone)
                .Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValuesInExtraFields()
        {
            SetCurrentContact();

            CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Home Phone", string.Empty);
            CurrentContact.SetFieldValue("Personal Email", string.Empty);
            CurrentContact.SetFieldValue("Work City", string.Empty);
            CurrentContact.SetFieldValue("Nickname", string.Empty);

            NewContactPage.CreateContact().WithMultipleValues(CurrentContact.BasicContactFields, CurrentContact.ExtraContactFields, CurrentContact.BooleanContactFields).Create();

            CurrentContact.IsContactCreatedSuccessfully = IsContactCreatedSuccessfully;
        }

        /// <summary>
        /// Edit a previously created contact by changing its first and last names
        /// </summary>
        public static void EditSimpleContact()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("First Name", CurrentContact.GetFieldValue("First Name"));
            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", CurrentContact.GetFieldPreviousValue("First Name"));
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by changing its first, last and organization names
        /// </summary>
        public static void EditSimpleContactWithOrganization()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("First Name", CurrentContact.GetFieldValue("First Name"));
            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));
            CurrentContact.SetFieldPreviousValue("Organization Name", CurrentContact.GetFieldValue("Organization Name"));

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = CurrentContact.SetFieldValue("Organization Name", DummyData.OrganizationValue);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).WithNewOrganizationName(organizationName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", CurrentContact.GetFieldPreviousValue("First Name"));
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from last name field
        /// </summary>
        public static void EditContactRemovingLastName()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));

            var lastName = CurrentContact.SetFieldValue("Last Name", string.Empty);

            EditContactPage.EditContact().WithNewLastName(lastName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));

        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that exceed the field character limit
        /// </summary>
        public static void EditContactAssigningOverflowValues()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("First Name", CurrentContact.GetFieldValue("First Name"));
            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.OverflowWordValue);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.OverflowWordValue);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", CurrentContact.GetFieldPreviousValue("First Name"));
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that contain symbols, number and characters
        /// </summary>
        public static void EditContactAssigningNonsenseValues()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("First Name", CurrentContact.GetFieldValue("First Name"));
            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));

            var firstName = CurrentContact.SetFieldValue("First Name", DummyData.NonsenseValue);
            var lastName = CurrentContact.SetFieldValue("Last Name", DummyData.NonsenseValue);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", CurrentContact.GetFieldPreviousValue("First Name"));
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by assigning an invalid organization value to the respective field
        /// </summary>
        public static void EditContactAssigningInvalidOrganization()
        {
            SetCurrentContact();

            var organizationName = CurrentContact.SetFieldValue("Organization Name", DummyData.SimpleWord);
            var homePhone = CurrentContact.SetFieldValue("Home Phone", DummyData.PhoneValue);

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).WithNewHomePhone(homePhone).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from organization name field thus rendering it orphan
        /// </summary>
        public static void EditContactRemovingOrganization()
        {
            SetCurrentContact();

            var organizationName = CurrentContact.SetFieldValue("Organization Name", string.Empty);

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;
        }

        /// <summary>
        /// Edit a previously created contact changing every single field value.
        /// </summary>
        public static void EditContactAlteringAllValues()
        {
            SetCurrentContact();

            CurrentContact.SetFieldPreviousValue("First Name", CurrentContact.GetFieldValue("First Name"));
            CurrentContact.SetFieldPreviousValue("Last Name", CurrentContact.GetFieldValue("Last Name"));

            CurrentContact.SetFieldValue("First Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Last Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Middle Name", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Suffix", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Organization Name", DummyData.OrganizationValue);
            CurrentContact.SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            CurrentContact.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            CurrentContact.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            CurrentContact.SetFieldValue("Department", DummyData.DepartmentValue);
            CurrentContact.SetFieldValue("Work Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Home Fax", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Work Fax", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Other Phone", DummyData.PhoneValue);
            CurrentContact.SetFieldValue("Personal Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Other Email", DummyData.EmailValue);
            CurrentContact.SetFieldValue("Work Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Work City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Work State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Work Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Work Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Home Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Home City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Home State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Home Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Home Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Other Street", DummyData.AddressValue);
            CurrentContact.SetFieldValue("Other City", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Other State", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            CurrentContact.SetFieldValue("Other Country", DummyData.CountryValue);
            CurrentContact.SetFieldValue("Salutation", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Nickname", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Job Title", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Website", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Religion", DummyData.SimpleWord);
            CurrentContact.SetFieldValue("Birthdate", DummyData.DateValue);
            CurrentContact.SetFieldValue("Gender", DummyData.GenderValue);
            CurrentContact.SetFieldValue("Comments", DummyData.SimpleText);

            EditContactPage.EditContact().WithMultipleNewValues(CurrentContact.BasicContactFields, CurrentContact.ExtraContactFields, CurrentContact.BooleanContactFields).Edit();

            CurrentContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (EditContactPage.IsContactSavedSuccessfully) return;
            CurrentContact.SetFieldValue("First Name", CurrentContact.GetFieldPreviousValue("First Name"));
            CurrentContact.SetFieldValue("Last Name", CurrentContact.GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Import file with 1 contact. Only first and last name fields have values
        /// </summary>
        public static void ImportSimpleContact()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts1.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportContactWithAllValues()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Middle Name", "Emmanouil");
            CurrentContact.SetFieldValue("Suffix", "Mr");
            CurrentContact.SetFieldValue("Organization Name", "KONICA MINOLTA");
            CurrentContact.SetFieldValue("Mobile Phone", "6912345678");
            CurrentContact.SetFieldValue("Email", "email@email.com");
            CurrentContact.SetFieldValue("Allow SMS", "False");
            CurrentContact.SetFieldValue("Allow Phones", "False");
            CurrentContact.SetFieldValue("Allow Emails", "True");

            CurrentContact.SetFieldValue("Department", "Administration");
            CurrentContact.SetFieldValue("Work Phone", "2101234567");
            CurrentContact.SetFieldValue("Work Phone 2", "2111234567");
            CurrentContact.SetFieldValue("Mobile Phone 2", "69213456789");
            CurrentContact.SetFieldValue("Home Phone", "1234567890");
            CurrentContact.SetFieldValue("Home Phone 2", "0987654321");
            CurrentContact.SetFieldValue("Home Fax", "1234567890");
            CurrentContact.SetFieldValue("Work Fax", "0987654322");
            CurrentContact.SetFieldValue("Other Phone", "2143658709");
            CurrentContact.SetFieldValue("Personal Email", "myemail@email.com");
            CurrentContact.SetFieldValue("Other Email", "otheremail@email.com");
            CurrentContact.SetFieldValue("Work Street", "Παπαφλέσσα 10");
            CurrentContact.SetFieldValue("Work City", "Τρίκαλα");
            CurrentContact.SetFieldValue("Work State", "ΝΥ");
            CurrentContact.SetFieldValue("Work Postal Code", "12345");
            CurrentContact.SetFieldValue("Work Country", "Greece");
            CurrentContact.SetFieldValue("Home Street", "Παπαφλέσσα 11");
            CurrentContact.SetFieldValue("Home City", "Γιάννενα");
            CurrentContact.SetFieldValue("Home State", "CA");
            CurrentContact.SetFieldValue("Home Postal Code", "12345");
            CurrentContact.SetFieldValue("Home Country", "Greece");
            CurrentContact.SetFieldValue("Other Street", "otherstreet");
            CurrentContact.SetFieldValue("Other City", "othercity");
            CurrentContact.SetFieldValue("Other State", "OS");
            CurrentContact.SetFieldValue("Other Postal Code", "otherpostal");
            CurrentContact.SetFieldValue("Other Country", "Wallis and Futuna");
            CurrentContact.SetFieldValue("Salutation", "His majesty");
            CurrentContact.SetFieldValue("Nickname", "Νοντας");
            CurrentContact.SetFieldValue("Job Title", "Ταξιτζής");
            CurrentContact.SetFieldValue("Website", "http://www.facebook.com/TheNontas");
            CurrentContact.SetFieldValue("Religion", "Αγνωστικιστής");
            CurrentContact.SetFieldValue("Birthdate", "28/2/1980");
            CurrentContact.SetFieldValue("Gender", "Other");
            CurrentContact.SetFieldValue("Comments", "No comments");

        }

        /// <summary>
        /// Import file with 1 contact. Only first name field has value. Last name field is left null 
        /// </summary>
        public static void ImportContactWithoutLastName()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import file with 1 contact. First and last name have assigned values. Organization name has an non-existent organization as value
        /// </summary>
        public static void ImportContactWithInvalidOrganization()
        {
            SetCurrentContact();

            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Organization Name", "Rivendale Corp");
        }

        /// <summary>
        /// Import file with 1 contact. Every contact field has non-sense value assigned except for the combo ones and the organization name.
        /// </summary>
        public static void ImportContactWithNonsenseValues()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts7.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "#$@#$");
            CurrentContact.SetFieldValue("Last Name", "#$@#$");
            CurrentContact.SetFieldValue("Middle Name", "#$@#$");
            CurrentContact.SetFieldValue("Suffix", "#$@#$");
            CurrentContact.SetFieldValue("Organization Name", "KONICA MINOLTA");
            CurrentContact.SetFieldValue("Mobile Phone", "#$@#$");
            CurrentContact.SetFieldValue("Email", "#$@#$");
            CurrentContact.SetFieldValue("Allow SMS", "False");
            CurrentContact.SetFieldValue("Allow Phones", "False");
            CurrentContact.SetFieldValue("Allow Emails", "False");

            CurrentContact.SetFieldValue("Department", "Administration");
            CurrentContact.SetFieldValue("Work Phone", "#$@#$");
            CurrentContact.SetFieldValue("Work Phone 2", "#$@#$");
            CurrentContact.SetFieldValue("Mobile Phone 2", "#$@#$");
            CurrentContact.SetFieldValue("Home Phone", "#$@#$");
            CurrentContact.SetFieldValue("Home Phone 2", "#$@#$");
            CurrentContact.SetFieldValue("Home Fax", "#$@#$");
            CurrentContact.SetFieldValue("Work Fax", "#$@#$");
            CurrentContact.SetFieldValue("Other Phone", "#$@#$");
            CurrentContact.SetFieldValue("Personal Email", "#$@#$");
            CurrentContact.SetFieldValue("Other Email", "#$@#$");
            CurrentContact.SetFieldValue("Work Street", "#$@#$");
            CurrentContact.SetFieldValue("Work City", "#$@#$");
            CurrentContact.SetFieldValue("Work State", "#$@#$");
            CurrentContact.SetFieldValue("Work Postal Code", "#$@#$");
            CurrentContact.SetFieldValue("Work Country", "Afghanistan");
            CurrentContact.SetFieldValue("Home Street", "#$@#$");
            CurrentContact.SetFieldValue("Home City", "#$@#$");
            CurrentContact.SetFieldValue("Home State", "#$@#$");
            CurrentContact.SetFieldValue("Home Postal Code", "#$@#$");
            CurrentContact.SetFieldValue("Home Country", "Afghanistan");
            CurrentContact.SetFieldValue("Other Street", "#$@#$");
            CurrentContact.SetFieldValue("Other City", "#$@#$");
            CurrentContact.SetFieldValue("Other State", "#$@#$");
            CurrentContact.SetFieldValue("Other Postal Code", "#$@#$");
            CurrentContact.SetFieldValue("Other Country", "Afghanistan");
            CurrentContact.SetFieldValue("Salutation", "#$@#$");
            CurrentContact.SetFieldValue("Nickname", "#$@#$");
            CurrentContact.SetFieldValue("Job Title", "#$@#$");
            CurrentContact.SetFieldValue("Website", "#$@#$");
            CurrentContact.SetFieldValue("Religion", "#$@#$");
            CurrentContact.SetFieldValue("Birthdate", "#$@#$");
            CurrentContact.SetFieldValue("Gender", "Female");
            CurrentContact.SetFieldValue("Comments", "#$@#$");
        }

        /// <summary>
        /// Import file with 1 contact. First and last name fields have assigned values that exceed the 50 characters
        /// </summary>
        public static void ImportContactWithOverflowValues()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
            CurrentContact.SetFieldValue("Last Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 32/1/2000
        /// </summary>
        public static void ImportContactWithInvalidBirthdate1()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts100.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Birthdate", "32/1/2000");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 29/2/2001
        /// </summary>
        public static void ImportContactWithInvalidBirthdate2()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts101.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Birthdate", "29/2/2001");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 12/13/2000
        /// </summary>
        public static void ImportContactWithInvalidBirthdate3()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts102.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Birthdate", "12/13/2000");
        }

        /// <summary>
        /// Import file with 1 contact. Template contains less columns than original
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Email", "test");
            CurrentContact.SetFieldValue("Work City", "test");
            CurrentContact.SetFieldValue("Home State", "test");

        }

        /// <summary>
        /// Import file with 1 contact. Template contains 1 more column than original
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts12.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import file with 1 contact. Template does not contain the mandatory field column
        /// </summary>
        public static void ImportTemplateWithoutMandatoryColumn()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import file with 1 contact. Template columns are placed in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            SetCurrentContact();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts16.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            CurrentContact.SetFieldValue("First Name", "Panagiotis");
            CurrentContact.SetFieldValue("Last Name", "Mavrogiannis");
            CurrentContact.SetFieldValue("Allow Emails", "True");
            CurrentContact.SetFieldValue("Allow SMS", "True");
            CurrentContact.SetFieldValue("Allow Phones", "True");
            CurrentContact.SetFieldValue("Birthdate", "15/5/1987");
            CurrentContact.SetFieldValue("Mobile Phone", "6944833390");
            CurrentContact.SetFieldValue("Work Phone", "null");
            CurrentContact.SetFieldValue("Email", ".");
            CurrentContact.SetFieldValue("Home Street", "ΑΝΩΓΕΙΩΝ 60");
            CurrentContact.SetFieldValue("Home City", "ΗΡΑΚΛΕΙΟ");
            CurrentContact.SetFieldValue("Home Postal Code", "71304");

        }
    }
}
