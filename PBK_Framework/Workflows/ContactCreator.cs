using System;
using System.Collections.Generic;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;


namespace JPB_Framework.Workflows
{
    public class ContactCreator
    {

        // Maybe create a mechanism to create/import more than 1 contacts at a test and then check all values
        //        public static int InitialContactsCount { get; set; }
        private static int InitialContactCount { get; set; }
        public static Contact FirstContact { get; set; }
        public static Contact SecondContact { get; set; }
        public static Contact ThirdContact { get; set; }

        private static Contact CurrentContact { get; set; }

        private const string ImportFilePath = "E:\\OneDrive\\Work\\Testing files - local temp\\Pobuca Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static void Initialize()
        {
            FirstContact = new Contact();
            SecondContact = new Contact();
            ThirdContact = new Contact();
            InitialContactCount = ContactsPage.TotalContactsCountByLabel;
        }

        public static void CleanUp()
        {
            FirstContact.CleanUp();
            SecondContact.CleanUp();
            ThirdContact.CleanUp();
            LeftSideMenu.GoToContacts();
            VerifyThat.AreEqual(InitialContactCount, ContactsPage.TotalContactsCountByLabel,
                $"Total contacts count is not the same as in the test initiation (Expected={InitialContactCount}, Actual={ContactsPage.TotalContactsCountByLabel}). Some contacts may have not been cleaned up at the end of test.");
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
        public static bool IsContactFileImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;

        /// <summary>
        /// Returns true if contacts were partially imported due to duplicate contact existance.
        /// </summary>
        public static bool IsContactFileImportedWithDuplicates => ImportPage.IsImportWithDuplicatesMessageShown;

        /// <summary>
        /// Returns true if contacts were not import due to some error
        /// </summary>
        public static bool IsContactFileFailedToImport => ImportPage.IsImportFailedMessageShown;

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
            if (string.IsNullOrEmpty(ThirdContact.FirstName) && string.IsNullOrEmpty(ThirdContact.LastName))
            {
                CurrentContact = ThirdContact;
                return;
            }
            Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
            Report.Report.AbruptFinalize();
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.OrganizationName, organizationName);
            CurrentContact.SetFieldValue(ContactFields.MobilePhone, mobilePhone);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.MobilePhone, mobilePhone);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.OrganizationName, OrganizationViewPage.OrganizationName);
            CurrentContact.SetFieldValue(ContactFields.Website, OrganizationViewPage.Website);
            CurrentContact.SetFieldValue(ContactFields.WorkStreet, OrganizationViewPage.BillingStreet);
            CurrentContact.SetFieldValue(ContactFields.WorkCity, OrganizationViewPage.BillingCity);
            CurrentContact.SetFieldValue(ContactFields.WorkState, OrganizationViewPage.BillingState);
            CurrentContact.SetFieldValue(ContactFields.WorkPostalCode, OrganizationViewPage.BillingPostalCode);
            CurrentContact.SetFieldValue(ContactFields.WorkCountry, OrganizationViewPage.BillingCountry);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {
            SetCurrentContact();

            var tmp = new Contact();

            tmp.SetFieldValue(ContactFields.FirstName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.LastName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.MiddleName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.JobTitle, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OrganizationName, DummyData.OrganizationValue);
            tmp.SetFieldValue(ContactFields.Department, DummyData.DepartmentValue);

            tmp.SetFieldValue(ContactFields.AllowSms, DummyData.BooleanValue);
            tmp.SetFieldValue(ContactFields.AllowPhones, DummyData.BooleanValue);
            tmp.SetFieldValue(ContactFields.AllowEmails, DummyData.BooleanValue);
            tmp.SetFieldValue(ContactFields.Favorite, true.ToString());

            tmp.SetFieldValue(ContactFields.WorkPhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkPhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.MobilePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.MobilePhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomePhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomeFax, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkFax, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.OtherPhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.PersonalEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.OtherEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.WorkStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.WorkCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.WorkState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.WorkPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.WorkCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ContactFields.HomeStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.HomeCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.HomeState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.HomePostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.HomeCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ContactFields.OtherStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.OtherCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OtherState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OtherPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.OtherCountry, DummyData.CountryValue);

            tmp.SetFieldValue(ContactFields.Salutation, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Nickname, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Suffix, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Website, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Religion, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Birthdate, DummyData.DateValue);
            tmp.SetFieldValue(ContactFields.Gender, DummyData.GenderValue);
            tmp.SetFieldValue(ContactFields.Comments, DummyData.SimpleText);

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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
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
            CurrentContact.SetFieldValue(ContactFields.FirstName, firstName);
            CurrentContact.SetFieldValue(ContactFields.LastName, lastName);
            CurrentContact.SetFieldValue(ContactFields.OrganizationName, organizationName);
            CurrentContact.SetFieldValue(ContactFields.HomePhone, homePhone);
            CurrentContact.SetFieldValue(ContactFields.Favorite, true.ToString());
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValuesInExtraFields()
        {
            SetCurrentContact();

            var tmp = new Contact();

            tmp.SetFieldValue(ContactFields.FirstName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.LastName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.HomePhone, string.Empty);
            tmp.SetFieldValue(ContactFields.PersonalEmail, string.Empty);
            tmp.SetFieldValue(ContactFields.WorkCity, string.Empty);
            tmp.SetFieldValue(ContactFields.Nickname, string.Empty);
            tmp.SetFieldValue(ContactFields.Favorite, true.ToString());

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

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.FirstName, firstName);
            editedContact.SetFieldValue(ContactFields.LastName, lastName);
        }

        /// <summary>
        /// Edit a previously created contact by changing its first, last and organization names
        /// </summary>
        public static void EditSimpleContactWithOrganization(Contact editedContact)
        {
            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
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
            editedContact.SetFieldValue(ContactFields.FirstName, firstName);
            editedContact.SetFieldValue(ContactFields.LastName, lastName);
            editedContact.SetFieldValue(ContactFields.OrganizationName, organizationName);
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from last name field
        /// </summary>
        public static void EditContactRemovingLastName(Contact editedContact)
        {
            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var lastName = string.Empty;

            EditContactPage.EditContact().WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.LastName, lastName);

        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that exceed the field character limit
        /// </summary>
        public static void EditContactAssigningOverflowValues(Contact editedContact)
        {

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.OverflowWordValue;
            var lastName = DummyData.OverflowWordValue;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.FirstName, firstName);
            editedContact.SetFieldValue(ContactFields.LastName, lastName);
        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that contain symbols, number and characters
        /// </summary>
        public static void EditContactAssigningNonsenseValues(Contact editedContact)
        {

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var firstName = DummyData.NonsenseValue;
            var lastName = DummyData.NonsenseValue;

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.FirstName, firstName);
            editedContact.SetFieldValue(ContactFields.LastName, lastName);
        }

        /// <summary>
        /// Edit a previously created contact by assigning an invalid organization value to the respective field
        /// </summary>
        public static void EditContactAssigningInvalidOrganization(Contact editedContact)
        {

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var organizationName = DummyData.SimpleWord;
            var homePhone = DummyData.PhoneValue;

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).WithNewMobilePhone(homePhone).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.OrganizationName, organizationName);
            editedContact.SetFieldValue(ContactFields.HomePhone, homePhone);
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from organization name field thus rendering it orphan
        /// </summary>
        public static void EditContactRemovingOrganization(Contact editedContact)
        {

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var organizationName = string.Empty;

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).Edit();

            editedContact.IsContactSavedAfterEdit = IsContactSavedAfterEdit;

            if (!editedContact.IsContactSavedAfterEdit) return;
            editedContact.SetFieldValue(ContactFields.OrganizationName, organizationName);
        }

        /// <summary>
        /// Edit a previously created contact changing every single field value.
        /// </summary>
        public static void EditContactAlteringAllValues(Contact editedContact)
        {

            var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

            if (!isAtContactViewPage || ((ContactViewPage.FirstName != editedContact.FirstName) || (ContactViewPage.LastName != editedContact.LastName)))
            {
                if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(editedContact.FirstName).AndLastName(editedContact.LastName).Open();
            }

            var tmp = new Contact();
            tmp.Clone(editedContact);

            tmp.SetFieldValue(ContactFields.FirstName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.LastName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.MiddleName, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Suffix, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OrganizationName, DummyData.OrganizationValue);
            tmp.SetFieldValue(ContactFields.MobilePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.AllowSms, DummyData.BooleanValue);
            tmp.SetFieldValue(ContactFields.AllowPhones, DummyData.BooleanValue);
            tmp.SetFieldValue(ContactFields.AllowEmails, DummyData.BooleanValue);

            tmp.SetFieldValue(ContactFields.Department, DummyData.DepartmentValue);
            tmp.SetFieldValue(ContactFields.WorkPhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkPhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.MobilePhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomePhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.HomeFax, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.WorkFax, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.OtherPhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ContactFields.PersonalEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.OtherEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ContactFields.WorkStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.WorkCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.WorkState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.WorkPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.WorkCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ContactFields.HomeStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.HomeCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.HomeState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.HomePostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.HomeCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ContactFields.OtherStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ContactFields.OtherCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OtherState, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.OtherPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ContactFields.OtherCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ContactFields.Salutation, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Nickname, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.JobTitle, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Website, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Religion, DummyData.SimpleWord);
            tmp.SetFieldValue(ContactFields.Birthdate, DummyData.DateValue);
            tmp.SetFieldValue(ContactFields.Gender, DummyData.GenderValue);
            tmp.SetFieldValue(ContactFields.Comments, DummyData.SimpleText);

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

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
        }

        /// <summary>
        /// Import template file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportTemplateContactWithAllValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            FirstContact.SetFieldValue(ContactFields.Suffix, "Mr");
            FirstContact.SetFieldValue(ContactFields.OrganizationName, "KONICA MINOLTA");
            FirstContact.SetFieldValue(ContactFields.MobilePhone, "6912345678");
            FirstContact.SetFieldValue(ContactFields.WorkEmail, "email@email.com");
            FirstContact.SetFieldValue(ContactFields.AllowSms, "False");
            FirstContact.SetFieldValue(ContactFields.AllowPhones, "False");
            FirstContact.SetFieldValue(ContactFields.AllowEmails, "True");

            FirstContact.SetFieldValue(ContactFields.Department, "Administration");
            FirstContact.SetFieldValue(ContactFields.WorkPhone, "2101234567");
            FirstContact.SetFieldValue(ContactFields.WorkPhone2, "2111234567");
            FirstContact.SetFieldValue(ContactFields.MobilePhone2, "69213456789");
            FirstContact.SetFieldValue(ContactFields.HomePhone, "1234567890");
            FirstContact.SetFieldValue(ContactFields.HomePhone2, "0987654321");
            FirstContact.SetFieldValue(ContactFields.HomeFax, "1234567890");
            FirstContact.SetFieldValue(ContactFields.WorkFax, "0987654322");
            FirstContact.SetFieldValue(ContactFields.OtherPhone, "2143658709");
            FirstContact.SetFieldValue(ContactFields.PersonalEmail, "myemail@email.com");
            FirstContact.SetFieldValue(ContactFields.OtherEmail, "otheremail@email.com");
            FirstContact.SetFieldValue(ContactFields.WorkStreet, "Παπαφλέσσα 10");
            FirstContact.SetFieldValue(ContactFields.WorkCity, "Τρίκαλα");
            FirstContact.SetFieldValue(ContactFields.WorkState, "ΝΥ");
            FirstContact.SetFieldValue(ContactFields.WorkPostalCode, "12345");
            FirstContact.SetFieldValue(ContactFields.WorkCountry, "Greece");
            FirstContact.SetFieldValue(ContactFields.HomeStreet, "Παπαφλέσσα 11");
            FirstContact.SetFieldValue(ContactFields.HomeCity, "Γιάννενα");
            FirstContact.SetFieldValue(ContactFields.HomeState, "CA");
            FirstContact.SetFieldValue(ContactFields.HomePostalCode, "12345");
            FirstContact.SetFieldValue(ContactFields.HomeCountry, "Greece");
            FirstContact.SetFieldValue(ContactFields.OtherStreet, "otherstreet");
            FirstContact.SetFieldValue(ContactFields.OtherCity, "othercity");
            FirstContact.SetFieldValue(ContactFields.OtherState, "OS");
            FirstContact.SetFieldValue(ContactFields.OtherPostalCode, "otherpostal");
            FirstContact.SetFieldValue(ContactFields.OtherCountry, "Wallis and Futuna");
            FirstContact.SetFieldValue(ContactFields.Salutation, "His majesty");
            FirstContact.SetFieldValue(ContactFields.Nickname, "Νοντας");
            FirstContact.SetFieldValue(ContactFields.JobTitle, "Ταξιτζής");
            FirstContact.SetFieldValue(ContactFields.Website, "http://www.facebook.com/TheNontas");
            FirstContact.SetFieldValue(ContactFields.Religion, "Αγνωστικιστής");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "28/02/1980");
            FirstContact.SetFieldValue(ContactFields.Gender, "Other");
            FirstContact.SetFieldValue(ContactFields.Comments, "No comments");

        }

        /// <summary>
        /// Import template file with 1 contact. Only first name field has value. Last name field is left null 
        /// </summary>
        public static void ImportTemplateContactWithoutLastName()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
        }

        /// <summary>
        /// Import template file with 1 contact. First and last name have assigned values. Organization name has an non-existent organization as value
        /// </summary>
        public static void ImportTemplateContactWithInvalidOrganization()
        {

            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.OrganizationName, "Rivendale Corp");
        }

        /// <summary>
        /// Import template file with 1 contact. Every contact field has non-sense value assigned except for the combo ones and the organization name.
        /// </summary>
        public static void ImportTemplateContactWithNonsenseValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts7.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.LastName, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Suffix, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OrganizationName, "KONICA MINOLTA");
            FirstContact.SetFieldValue(ContactFields.MobilePhone, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkEmail, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.AllowSms, "False");
            FirstContact.SetFieldValue(ContactFields.AllowPhones, "False");
            FirstContact.SetFieldValue(ContactFields.AllowEmails, "False");

            FirstContact.SetFieldValue(ContactFields.Department, "Administration");
            FirstContact.SetFieldValue(ContactFields.WorkPhone, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkPhone2, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.MobilePhone2, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomePhone, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomePhone2, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomeFax, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkFax, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherPhone, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.PersonalEmail, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherEmail, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkStreet, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkCity, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkState, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkPostalCode, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.WorkCountry, "Afghanistan");
            FirstContact.SetFieldValue(ContactFields.HomeStreet, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomeCity, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomeState, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomePostalCode, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.HomeCountry, "Afghanistan");
            FirstContact.SetFieldValue(ContactFields.OtherStreet, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherCity, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherState, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherPostalCode, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.OtherCountry, "Afghanistan");
            FirstContact.SetFieldValue(ContactFields.Salutation, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Nickname, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.JobTitle, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Website, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Religion, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "#$@#$");
            FirstContact.SetFieldValue(ContactFields.Gender, "Female");
            FirstContact.SetFieldValue(ContactFields.Comments, "#$@#$");
        }

        /// <summary>
        /// Import template file with 1 contact. First and last name fields have assigned values that exceed the 50 characters
        /// </summary>
        public static void ImportTemplateContactWithOverflowValues()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
            FirstContact.SetFieldValue(ContactFields.LastName, "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
        }

        /// <summary>
        /// Import template file with 3 contacts. Beside first and last name, birthdate fields contain invalid date values
        /// </summary>
        public static void ImportTemplateContactWithInvalidBirthdate()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts10.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis1");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "32/1/2000");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis2");
            SecondContact.SetFieldValue(ContactFields.Birthdate, "29/02/2001");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis3");
            ThirdContact.SetFieldValue(ContactFields.Birthdate, "12/13/2000");
        }

        /// <summary>
        /// Import template file with 1 contact. Template contains less columns than original
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.WorkEmail, "test");
            FirstContact.SetFieldValue(ContactFields.WorkCity, "test");
            FirstContact.SetFieldValue(ContactFields.HomeState, "test");

        }

        /// <summary>
        /// Import template file with 1 contact. Template contains 1 more column than original
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts12.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
        }

        /// <summary>
        /// Import template file with 1 contact. Template does not contain the mandatory field column
        /// </summary>
        public static void ImportTemplateWithoutMandatoryColumn()
        {

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
        }

        /// <summary>
        /// Import template file with 1 contact. Template columns are placed in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts16.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.AllowEmails, "True");
            FirstContact.SetFieldValue(ContactFields.AllowSms, "True");
            FirstContact.SetFieldValue(ContactFields.AllowPhones, "True");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "15/05/1987");
            FirstContact.SetFieldValue(ContactFields.MobilePhone, "6944833390");
            FirstContact.SetFieldValue(ContactFields.WorkPhone, "null");
            FirstContact.SetFieldValue(ContactFields.WorkEmail, ".");
            FirstContact.SetFieldValue(ContactFields.HomeStreet, "ΑΝΩΓΕΙΩΝ 60");
            FirstContact.SetFieldValue(ContactFields.HomeCity, "ΗΡΑΚΛΕΙΟ");
            FirstContact.SetFieldValue(ContactFields.HomePostalCode, "71304");

        }

        /// <summary>
        /// Imports a contact template that contains 3 contacts of which 1 contact is inserted twice (with the same full name).
        /// Check for duplicate full names is made during import
        /// </summary>
        public static void ImportTemplateWithTwinContacts()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Contacts)
                .FromPath(ImportFilePath)
                .WithFileName("Contacts17.xls")
                .CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Manos");
            SecondContact.SetFieldValue(ContactFields.LastName, "Spiridakis");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
        }

        /// <summary>
        /// First creates a contact and then imports a contact template that contains 2 contacts of which 1 has the same full name with the previously created contact.
        /// Check for duplicate full names is made during import
        /// </summary>
        public static void ImportTemplateWithAnExistingContact()
        {
            var firstName = "Panagiotis";
            var lastName = "Mavrogiannis";
            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            if (!NewContactPage.IsContactSavedSuccessfully) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, firstName);
            FirstContact.SetFieldValue(ContactFields.LastName, lastName);

            ImportPage.ImportFile()
                .Containing(ImportFileType.Contacts)
                .FromPath(ImportFilePath)
                .WithFileName("Contacts18.xls")
                .CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;
            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Manos");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Spiridakis");
        }

        /// <summary>
        /// Imports a contact template that contains 2 contacts of which, one has invalid value on a combo field 
        /// </summary>
        public static void ImportTemplateContactWithInvalidComboValues()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Contacts)
                .FromPath(ImportFilePath)
                .WithFileName("Contacts20.xls")
                .Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.Department, "Finance");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Giorgos");
            SecondContact.SetFieldValue(ContactFields.LastName, "Valsamakis");
            FirstContact.SetFieldValue(ContactFields.Department, "Testing");
        }

        /// <summary>
        /// Import a contact template that contains 3 contacts that have void lines in between them
        /// </summary>
        public static void ImportTemplateWithVoidLinesBetweenContacts()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Contacts)
                .FromPath(ImportFilePath)
                .WithFileName("Contacts19.xls")
                .CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Giorgos");
            SecondContact.SetFieldValue(ContactFields.LastName, "Valsamakis");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Manos");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Spiridakis");
        }

        /// <summary>
        /// Import gmail csv file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportGmailCsvContactWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts1.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.Salutation, "Mr.");
            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            FirstContact.SetFieldValue(ContactFields.Suffix, "Sir");


            FirstContact.SetFieldValue(ContactFields.MobilePhone, "6949585690");
            FirstContact.SetFieldValue(ContactFields.WorkPhone, "2130179000");
            FirstContact.SetFieldValue(ContactFields.WorkPhone2, "21012345000");
            FirstContact.SetFieldValue(ContactFields.HomePhone, "2294048550");
            FirstContact.SetFieldValue(ContactFields.HomePhone2, "6949585691");
            FirstContact.SetFieldValue(ContactFields.HomeFax, "2294048551");
            FirstContact.SetFieldValue(ContactFields.WorkFax, "2130179001");
            FirstContact.SetFieldValue(ContactFields.OtherPhone, "1234567890");


            FirstContact.SetFieldValue(ContactFields.WorkEmail, "p.mavrogiannis@sieben.gr");
            FirstContact.SetFieldValue(ContactFields.PersonalEmail, "p.mavrogiannis@outlook.com");
            FirstContact.SetFieldValue(ContactFields.OtherEmail, "apaixtoos@hotmail.com");

            FirstContact.SetFieldValue(ContactFields.WorkStreet, "Aristomenous 3");
            FirstContact.SetFieldValue(ContactFields.WorkCity, "Gerakas");
            FirstContact.SetFieldValue(ContactFields.WorkState, "Attica");
            FirstContact.SetFieldValue(ContactFields.WorkPostalCode, "10613");
            FirstContact.SetFieldValue(ContactFields.WorkCountry, "Greece");

            FirstContact.SetFieldValue(ContactFields.HomeStreet, "Papachristou 4");
            FirstContact.SetFieldValue(ContactFields.HomeCity, "Artemida");
            FirstContact.SetFieldValue(ContactFields.HomeState, "Attica");
            FirstContact.SetFieldValue(ContactFields.HomePostalCode, "19016");
            FirstContact.SetFieldValue(ContactFields.HomeCountry, "Greece");

            FirstContact.SetFieldValue(ContactFields.OtherStreet, "Karavan Serai 2");
            FirstContact.SetFieldValue(ContactFields.OtherCity, "Constantinople");
            FirstContact.SetFieldValue(ContactFields.OtherState, "Thrace");
            FirstContact.SetFieldValue(ContactFields.OtherPostalCode, "12345");
            FirstContact.SetFieldValue(ContactFields.OtherCountry, "Greece");

            FirstContact.SetFieldValue(ContactFields.OrganizationName, "KONICA MINOLTA");
            FirstContact.SetFieldValue(ContactFields.JobTitle, "Software Quality Tester");
            FirstContact.SetFieldValue(ContactFields.Nickname, "Panagof");
            FirstContact.SetFieldValue(ContactFields.Website, "www.sieben.gr");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "27/01/1990");
            FirstContact.SetFieldValue(ContactFields.Comments, "His majesty sir tester of the United Kingodom of Testingburg");
        }

        /// <summary>
        /// Imports a contact csv file exported from gmail contacts. The file contains a contact that has no value for last name field
        /// </summary>
        public static void ImportGmailCsvContactWithoutLastName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts2.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
        }

        /// <summary>
        /// Imports a gmail csv file that contains 1 contact. The contact has overflow values for first and last name fields
        /// </summary>
        public static void ImportGmailCsvContactWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts3.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "MRPanagiotisPanagiotisPanagiotisPanagiotisMRPanagiotisPanagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "MRPanagiotisPanagiotisPanagiotisPanagiotisMRPanagiotisPanagiotis");
        }

        /// <summary>
        /// Imports a gmail csv file that contains 2 contacts. The contacts have the same first, middle and last name fields same. During import, a check for duplicate full names is made
        /// </summary>
        public static void ImportGmailCsvWithTheSamesContactTwice()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts4.csv").CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            FirstContact.SetFieldValue(ContactFields.Salutation, "Sir");
            FirstContact.SetFieldValue(ContactFields.Suffix, "Jr");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            SecondContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            SecondContact.SetFieldValue(ContactFields.Salutation, "Sir");
            SecondContact.SetFieldValue(ContactFields.Suffix, "Jr");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Karagiannis");
            ThirdContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            ThirdContact.SetFieldValue(ContactFields.Salutation, "Sir");
            ThirdContact.SetFieldValue(ContactFields.Suffix, "Jr");
        }

        public static void ImportGmailCsvContactsThatAlreadyExists()
        {
            var firstName = "Panagiotis";
            var lastName = "Mavrogiannis";
            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            if (!NewContactPage.IsContactSavedSuccessfully) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, firstName);
            FirstContact.SetFieldValue(ContactFields.LastName, lastName);

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("GmailContacts5.csv").CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;

            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Karagiannis");

        }

        /// <summary>
        /// Import outlook csv file that contains 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportOutlookCsvContactWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("OutlookContacts1.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.Salutation, "His majesty");
            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            FirstContact.SetFieldValue(ContactFields.Suffix, "Mr");

            FirstContact.SetFieldValue(ContactFields.OrganizationName, "KONICA MINOLTA");

            FirstContact.SetFieldValue(ContactFields.WorkEmail, "email@email.com");
            FirstContact.SetFieldValue(ContactFields.PersonalEmail, "myemail@email.com");
            FirstContact.SetFieldValue(ContactFields.OtherEmail, "otheremail@email.com");



            FirstContact.SetFieldValue(ContactFields.MobilePhone, "6912345678");
            FirstContact.SetFieldValue(ContactFields.WorkPhone, "2101234567");
            FirstContact.SetFieldValue(ContactFields.WorkPhone2, "2111234567");
            FirstContact.SetFieldValue(ContactFields.HomePhone, "1234567890");
            FirstContact.SetFieldValue(ContactFields.HomePhone2, "0987654321");
            FirstContact.SetFieldValue(ContactFields.HomeFax, "1234567890");
            FirstContact.SetFieldValue(ContactFields.WorkFax, "0987654322");
            FirstContact.SetFieldValue(ContactFields.OtherPhone, "2143658709");

            FirstContact.SetFieldValue(ContactFields.WorkStreet, "Aristomenous 3");
            FirstContact.SetFieldValue(ContactFields.WorkCity, "Gerakas");
            FirstContact.SetFieldValue(ContactFields.WorkState, "Attica");
            FirstContact.SetFieldValue(ContactFields.WorkPostalCode, "153 44");
            FirstContact.SetFieldValue(ContactFields.WorkCountry, "Greece");

            FirstContact.SetFieldValue(ContactFields.HomeStreet, "Παπαχρήστου 4");
            FirstContact.SetFieldValue(ContactFields.HomeCity, "Αρτέμιδα");
            FirstContact.SetFieldValue(ContactFields.HomeState, "Αττική");
            FirstContact.SetFieldValue(ContactFields.HomePostalCode, "19016");
            FirstContact.SetFieldValue(ContactFields.HomeCountry, "Greece");

            FirstContact.SetFieldValue(ContactFields.OtherStreet, "64 Rue Ferrer");
            FirstContact.SetFieldValue(ContactFields.OtherCity, "Dunkirk");
            FirstContact.SetFieldValue(ContactFields.OtherState, "Nord-Pas-de-Calais-Picardie");
            FirstContact.SetFieldValue(ContactFields.OtherPostalCode, "59430");
            FirstContact.SetFieldValue(ContactFields.OtherCountry, "France");

            FirstContact.SetFieldValue(ContactFields.Department, "Administration");

            FirstContact.SetFieldValue(ContactFields.Nickname, "Panagof");
            FirstContact.SetFieldValue(ContactFields.JobTitle, "Software Quality Tester");
            FirstContact.SetFieldValue(ContactFields.Website, "http://www.facebook.com/TheNontas");
            FirstContact.SetFieldValue(ContactFields.Birthdate, "28/2/1980");
            FirstContact.SetFieldValue(ContactFields.Comments, "No comments");
        }

        /// <summary>
        /// Imports an outlook csv file that contains 1 contact. The contact has no value for last name field
        /// </summary>
        public static void ImportOutlookCsvContactWithoutLastName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("OutlookContacts2.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
        }

        /// <summary>
        /// Imports an outlook csv file that contains 1 contact. The contact has overflow values for first and last name fields
        /// </summary>
        public static void ImportOutlookCsvContactWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("OutlookContacts3.csv").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "PanagiotisPanagiotisPanagiotisPanagiotisPanagiotisPanagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "PanagiotisPanagiotisPanagiotisPanagiotisPanagiotisPanagiotis");
        }


        public static void ImportOutlookCsvWithTheSamesContactTwice()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("OutlookContacts4.csv").CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;

            FirstContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            FirstContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            FirstContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            FirstContact.SetFieldValue(ContactFields.Salutation, "Sir");
            FirstContact.SetFieldValue(ContactFields.Suffix, "Jr.");

            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");
            SecondContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            SecondContact.SetFieldValue(ContactFields.Salutation, "Sir");
            SecondContact.SetFieldValue(ContactFields.Suffix, "Jr.");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Karagiannis");
            ThirdContact.SetFieldValue(ContactFields.MiddleName, "Emmanouil");
            ThirdContact.SetFieldValue(ContactFields.Salutation, "Sir");
            ThirdContact.SetFieldValue(ContactFields.Suffix, "Jr.");
        }

        public static void ImportOutlookCsvContactsThatAlreadyExists()
        {
            var firstName = "Panagiotis";
            var lastName = "Mavrogiannis";
            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

            if (!NewContactPage.IsContactSavedSuccessfully) return;
            FirstContact.SetFieldValue(ContactFields.FirstName, firstName);
            FirstContact.SetFieldValue(ContactFields.LastName, lastName);

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("OutlookContacts5.csv").CheckingForDuplicate(ImportField.FullName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;

            SecondContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            SecondContact.SetFieldValue(ContactFields.LastName, "Mavrogiannis");

            ThirdContact.SetFieldValue(ContactFields.FirstName, "Panagiotis");
            ThirdContact.SetFieldValue(ContactFields.LastName, "Karagiannis");
        }
    }
}
