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
        private static List<RecordField> BasicContactFields;
        private static List<RecordField> ExtraContactFields;
        //        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static string FirstName => GetFieldValue("First Name");
        public static string LastName => GetFieldValue("Last Name");
        public static string OrganizationName => GetFieldValue("Organization Name");

        public static string Birthdate => GetFieldValue("Birthdate");

        /// <summary>
        /// If a contact was created during test execution, returns true.
        /// </summary>
        public static bool ContactWasCreated
        {
            get
            {
                var firstName = BasicContactFields.Find(x => x.Label.Contains("First Name")).Value;
                var lastName = BasicContactFields.Find(x => x.Label.Contains("Last Name")).Value;

                return
                    !string.IsNullOrEmpty(lastName)
                    ||
                    (string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(firstName));
            }
        }



        /// <summary>
        /// Determines whether the newly created contact has all its field values saved correctly.
        /// Checks only the fields that were given a value when the contact was created.
        /// </summary>
        /// <returns>True if all contact fields have the expected values. Returns false if at least one field does not have the expected value</returns>
        public static bool AreContactFieldValuesSavedCorrectly
        {
            get
            {
                if (!ContactViewPage.IsAt)
                {
                    LeftSideMenu.GoToContacts();
                    ContactsPage.FindContact().WithFirstName(FirstName).AndLastName(LastName).Open();
                }

                var notOk = false;

                foreach (var contactField in BasicContactFields)
                {
                    if ((contactField.Value != null) && (contactField.Value != contactField.RecordViewPageFieldValue))
                    {
                        Report.Report.ToLogFile(MessageType.Message,
                            $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'",
                            null);
                        notOk = true;
                    }
                }

                foreach (var contactField in ExtraContactFields)
                {
                    if ((contactField.Value != null) && (contactField.Value != contactField.RecordViewPageFieldValue))
                    {
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'", null);
                        notOk = true;
                    }
                    else if ((contactField.Value == null) && contactField.RecordViewPageIsFieldVisible)
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has no value but its field is shown in contact's detail view page'", null);
                }

                return !notOk;
            }
        }

        public static bool IsContactSavedAfterEdit => EditContactPage.IsContactSavedSuccessfully;

        public static bool IsContactImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;


        /// <summary>
        /// Initialize Contact Creator properties
        /// </summary>
        public static void Initialize()
        {
            BasicContactFields = new List<RecordField>();
            ExtraContactFields = new List<RecordField>();

            BasicContactFields.Add(new RecordField("First Name", null, () => ContactViewPage.FirstName, null));
            BasicContactFields.Add(new RecordField("Last Name", null, () => ContactViewPage.LastName, null));
            BasicContactFields.Add(new RecordField("Middle Name", null, () => ContactViewPage.MiddleName, null));
            BasicContactFields.Add(new RecordField("Suffix", null, () => ContactViewPage.Suffix, null));
            BasicContactFields.Add(new RecordField("Organization Name", null, () => ContactViewPage.OrganizationName, null));
            BasicContactFields.Add(new RecordField("Mobile Phone", null, () => ContactViewPage.MobilePhone, null));
            BasicContactFields.Add(new RecordField("Email", null, () => ContactViewPage.Email, null));
            BasicContactFields.Add(new RecordField("Allow SMS", null, () => ContactViewPage.AllowSMS, null));
            BasicContactFields.Add(new RecordField("Allow Phones", null, () => ContactViewPage.AllowPhones, null));
            BasicContactFields.Add(new RecordField("Allow Emails", null, () => ContactViewPage.AllowEmails, null));

            ExtraContactFields.Add(new RecordField("Department", null, () => ContactViewPage.Department, () => ContactViewPage.IsDepartmentFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Phone", null, () => ContactViewPage.WorkPhone, () => ContactViewPage.IsWorkPhoneFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Phone 2", null, () => ContactViewPage.WorkPhone2, () => ContactViewPage.IsWorkPhone2FieldVisible));
            ExtraContactFields.Add(new RecordField("Mobile Phone 2", null, () => ContactViewPage.MobilePhone2, () => ContactViewPage.IsMobilePhone2FieldVisible));
            ExtraContactFields.Add(new RecordField("Home Phone", null, () => ContactViewPage.HomePhone, () => ContactViewPage.IsHomePhoneFieldVisible));
            ExtraContactFields.Add(new RecordField("Home Phone 2", null, () => ContactViewPage.HomePhone2, () => ContactViewPage.IsHomePhone2FieldVisible));
            ExtraContactFields.Add(new RecordField("Home Fax", null, () => ContactViewPage.HomeFax, () => ContactViewPage.IsHomeFaxFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Fax", null, () => ContactViewPage.WorkFax, () => ContactViewPage.IsWorkFaxFieldVisible));
            ExtraContactFields.Add(new RecordField("Other Phone", null, () => ContactViewPage.OtherPhone, () => ContactViewPage.IsOtherPhoneFieldVisible));
            ExtraContactFields.Add(new RecordField("Personal Email", null, () => ContactViewPage.PersonalEmail, () => ContactViewPage.IsPersonalEmailFieldVisible));
            ExtraContactFields.Add(new RecordField("Other Email", null, () => ContactViewPage.OtherEmail, () => ContactViewPage.IsOtherEmailFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Street", null, () => ContactViewPage.WorkStreet, () => ContactViewPage.IsWorkStreetFieldVisible));
            ExtraContactFields.Add(new RecordField("Work City", null, () => ContactViewPage.WorkCity, () => ContactViewPage.IsWorkCityFieldVisible));
            ExtraContactFields.Add(new RecordField("Work State", null, () => ContactViewPage.WorkState, () => ContactViewPage.IsWorkStateFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Postal Code", null, () => ContactViewPage.WorkPostalCode, () => ContactViewPage.IsWorkPostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField("Work Country", null, () => ContactViewPage.WorkCountry, () => ContactViewPage.IsWorkCountryFieldVisible));
            ExtraContactFields.Add(new RecordField("Home Street", null, () => ContactViewPage.HomeStreet, () => ContactViewPage.IsHomeStreetFieldVisible));
            ExtraContactFields.Add(new RecordField("Home City", null, () => ContactViewPage.HomeCity, () => ContactViewPage.IsHomeCityFieldVisible));
            ExtraContactFields.Add(new RecordField("Home State", null, () => ContactViewPage.HomeState, () => ContactViewPage.IsHomeStateFieldVisible));
            ExtraContactFields.Add(new RecordField("Home Postal Code", null, () => ContactViewPage.HomePostalCode, () => ContactViewPage.IsHomePostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField("Home Country", null, () => ContactViewPage.HomeCountry, () => ContactViewPage.IsHomeCountryFieldVisible));
            ExtraContactFields.Add(new RecordField("Other Street", null, () => ContactViewPage.OtherStreet, () => ContactViewPage.IsOtherStreetFieldVisible));
            ExtraContactFields.Add(new RecordField("Other City", null, () => ContactViewPage.OtherCity, () => ContactViewPage.IsOtherCityFieldVisible));
            ExtraContactFields.Add(new RecordField("Other State", null, () => ContactViewPage.OtherState, () => ContactViewPage.IsOtherStateFieldVisible));
            ExtraContactFields.Add(new RecordField("Other Postal Code", null, () => ContactViewPage.OtherPostalCode, () => ContactViewPage.IsOtherPostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField("Other Country", null, () => ContactViewPage.OtherCountry, () => ContactViewPage.IsOtherCountryFieldVisible));
            ExtraContactFields.Add(new RecordField("Salutation", null, () => ContactViewPage.Salutation, () => ContactViewPage.IsSalutationFieldVisible));
            ExtraContactFields.Add(new RecordField("Nickname", null, () => ContactViewPage.Nickname, () => ContactViewPage.IsNicknameFieldVisible));
            ExtraContactFields.Add(new RecordField("Job Title", null, () => ContactViewPage.JobTitle, () => ContactViewPage.IsJobTitleFieldVisible));
            ExtraContactFields.Add(new RecordField("Website", null, () => ContactViewPage.Website, () => ContactViewPage.IsWebsiteFieldVisible));
            ExtraContactFields.Add(new RecordField("Religion", null, () => ContactViewPage.Religion, () => ContactViewPage.IsReligionFieldVisible));
            ExtraContactFields.Add(new RecordField("Birthdate", null, () => ContactViewPage.Birthdate, () => ContactViewPage.IsBirthdateFieldVisible));
            ExtraContactFields.Add(new RecordField("Gender", null, () => ContactViewPage.Gender, () => ContactViewPage.IsGenderFieldVisible));
            ExtraContactFields.Add(new RecordField("Comments", null, () => ContactViewPage.Comments, () => ContactViewPage.IsCommentsFieldVisible));


        }

        /// <summary>
        /// Delete every contact created by the test
        /// </summary>
        public static void CleanUp()
        {
            try
            {
                if (ContactWasCreated)
                {
                    var firstName = GetFieldValue("First Name");
                    var lastName = GetFieldValue("Last Name");
                    LeftSideMenu.GoToContacts();
                    ContactsPage.FindContact()
                        .WithFirstName(firstName)
                        .AndLastName(lastName)
                        .Delete();
                }
            }
            catch (NoSuchElementException)
            {

            }

        }



        /// <summary>
        /// Sets the new value for a field of ContactCreator and then returns that value.
        /// </summary>
        /// <param name="fieldLabel">The field label of field that will have its value changed</param>
        /// <param name="newValue">The new value that will be assigned to the fields value property</param>
        /// <returns>The new value that was assigned to the field</returns>
        private static string SetFieldValue(string fieldLabel, string newValue)
        {
            if (BasicContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BasicContactFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else if (ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else
                throw new Exception();

            return newValue;
        }

        /// <summary>
        /// Get the current value of the given field
        /// </summary>
        /// <param name="fieldLabel">Contact field label</param>
        /// <returns></returns>
        private static string GetFieldValue(string fieldLabel)
        {
            if (BasicContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicContactFields.Find(x => x.Label.Contains(fieldLabel)).Value;
            else if (ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)).Value;

            throw new Exception();
        }

        /// <summary>
        /// Sets the previous value for a field of ContactCreator and then returns that value.
        /// </summary>
        /// <param name="fieldLabel">The field label of field that will have its value changed</param>
        /// <param name="previousValue">The previous value that will be assigned to the fields previous value property</param>
        /// <returns>The new value that was assigned to the field</returns>
        private static string SetFieldPreviousValue(string fieldLabel, string previousValue)
        {
            if (BasicContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BasicContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else if (ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else
                throw new Exception();

            return previousValue;
        }

        /// <summary>
        /// Get the value that was assigned to the given field before it was changed to a new one.
        /// </summary>
        /// <param name="fieldLabel">Contact field label</param>
        /// <returns></returns>
        private static string GetFieldPreviousValue(string fieldLabel)
        {
            if (BasicContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;

            throw new Exception();
        }

        /// <summary>
        /// Create a simple contact with dummy first and last name
        /// </summary>
        public static void CreateSimpleContact()
        {
            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();

        }

        public static void CreateSimpleContactWithOrganization()
        {
            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationNameExisting);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).WithOrganizationName(organizationName).Create();
        }

        /// <summary>
        /// Create a simple contact with dummy first and last name and default organization the one where the browser is currently navigated to. 
        /// You have to be at OrganizationViewPage of an organization for this method to work properly
        /// </summary>
        public static void CreateSimpleContactFromWithinOrganization()
        {
            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);
            var organizationName = SetFieldValue("Organization Name", OrganizationViewPage.OrganizationName);

            if (organizationName.Equals(string.Empty))
            {
                Report.Report.ToLogFile(MessageType.Message, "Something has gone wrong with return current organization view page Organization Name. Organization Name value is empty!", null);
                throw new Exception();
            }

            OrganizationViewPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {

            SetFieldValue("First Name", DummyData.FirstName);
            SetFieldValue("Last Name", DummyData.LastName);
            SetFieldValue("Middle Name", DummyData.MiddleName);
            SetFieldValue("Suffix", DummyData.Suffix);
            SetFieldValue("Organization Name", DummyData.OrganizationNameExisting);
            SetFieldValue("Mobile Phone", DummyData.MobilePhone);
            SetFieldValue("Email", DummyData.Email);
            SetFieldValue("Allow SMS", DummyData.AllowSMS);
            SetFieldValue("Allow Phones", DummyData.AllowPhones);
            SetFieldValue("Allow Emails", DummyData.AllowEmails);

            SetFieldValue("Department", DummyData.Department);
            SetFieldValue("Work Phone", DummyData.WorkPhone);
            SetFieldValue("Work Phone 2", DummyData.WorkPhone2);
            SetFieldValue("Mobile Phone 2", DummyData.MobilePhone2);
            SetFieldValue("Home Phone", DummyData.HomePhone);
            SetFieldValue("Home Phone 2", DummyData.HomePhone2);
            SetFieldValue("Home Fax", DummyData.HomeFax);
            SetFieldValue("Work Fax", DummyData.WorkFax);
            SetFieldValue("Other Phone", DummyData.OtherPhone);
            SetFieldValue("Personal Email", DummyData.PersonalEmail);
            SetFieldValue("Other Email", DummyData.OtherEmail);
            SetFieldValue("Work Street", DummyData.WorkStreet);
            SetFieldValue("Work City", DummyData.WorkCity);
            SetFieldValue("Work State", DummyData.WorkState);
            SetFieldValue("Work Postal Code", DummyData.WorkPostalCode);
            SetFieldValue("Work Country", DummyData.WorkCountry);
            SetFieldValue("Home Street", DummyData.HomeStreet);
            SetFieldValue("Home City", DummyData.HomeCity);
            SetFieldValue("Home State", DummyData.HomeState);
            SetFieldValue("Home Postal Code", DummyData.HomePostalCode);
            SetFieldValue("Home Country", DummyData.HomeCountry);
            SetFieldValue("Other Street", DummyData.OtherStreet);
            SetFieldValue("Other City", DummyData.OtherCity);
            SetFieldValue("Other State", DummyData.OtherState);
            SetFieldValue("Other Postal Code", DummyData.OtherPostalCode);
            SetFieldValue("Other Country", DummyData.OtherCountry);
            SetFieldValue("Salutation", DummyData.Salutation);
            SetFieldValue("Nickname", DummyData.Nickname);
            SetFieldValue("Job Title", DummyData.JobTitle);
            SetFieldValue("Website", DummyData.Website);
            SetFieldValue("Religion", DummyData.Religion);
            SetFieldValue("Birthdate", DummyData.Birthdate);
            SetFieldValue("Gender", DummyData.Gender);
            SetFieldValue("Comments", DummyData.Comments);

            NewContactPage.CreateContact().WithMultipleValues(BasicContactFields, ExtraContactFields).Create();

        }

        /// <summary>
        /// Create a contact without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            var firstName = SetFieldValue("First Name", DummyData.FirstName);

            NewContactPage.CreateContact().WithFirstName(firstName).Create();
        }

        /// <summary>
        /// Create a contact with values in first and last name that exceed the 50 character limit
        /// </summary>
        public static void CreateContactWithOverflowValues()
        {
            var firstName = SetFieldValue("First Name", DummyData.OverflowValue);
            var lastName = SetFieldValue("Last Name", DummyData.OverflowValue);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact with nonsense values in its fields
        /// </summary>
        public static void CreateContactWithNonsenseValues()
        {
            var firstName = SetFieldValue("First Name", DummyData.NonsenseValue);
            var lastName = SetFieldValue("Last Name", DummyData.NonsenseValue);

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact which is linked to a non existent organization
        /// </summary>
        public static void CreateContactWithInvalidOrganization()
        {
            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationNameNotExisting);

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithHomePhone("123")
                .Create();
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValues()
        {
            SetFieldValue("First Name", DummyData.FirstName);
            SetFieldValue("Last Name", DummyData.LastName);
            SetFieldValue("Home Phone", string.Empty);
            SetFieldValue("Personal Email", string.Empty);
            SetFieldValue("Work City", string.Empty);
            SetFieldValue("Nickname", string.Empty);

            NewContactPage.CreateContact().WithMultipleValues(BasicContactFields, ExtraContactFields).Create();
        }

        /// <summary>
        /// Edit a previously created contact by changing its first and last names
        /// </summary>
        public static void EditSimpleContact()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);

            EditContactPage.EditContact().WithFirstName(firstName).WithLastName(lastName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by changing its first, last and organization names
        /// </summary>
        public static void EditSimpleContactWithOrganization()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

            var firstName = SetFieldValue("First Name", DummyData.FirstName);
            var lastName = SetFieldValue("Last Name", DummyData.LastName);
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationNameExisting);

            EditContactPage.EditContact().WithFirstName(firstName).WithLastName(lastName).WithOrganizationName(organizationName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from last name field
        /// </summary>
        public static void EditContactRemovingLastName()
        {
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            var lastName = SetFieldValue("Last Name", string.Empty);

            EditContactPage.EditContact().WithLastName(lastName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));

        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that exceed the field character limit
        /// </summary>
        public static void EditContactAssigningOverflowValues()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            var firstName = SetFieldValue("First Name", DummyData.OverflowValue);
            var lastName = SetFieldValue("Last Name", DummyData.OverflowValue);

            EditContactPage.EditContact().WithFirstName(firstName).WithLastName(lastName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by assigning values to its first and last name that contain symbols, number and characters
        /// </summary>
        public static void EditContactAssigningNonsenseValues()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            var firstName = SetFieldValue("First Name", DummyData.NonsenseValue);
            var lastName = SetFieldValue("Last Name", DummyData.NonsenseValue);

            EditContactPage.EditContact().WithFirstName(firstName).WithLastName(lastName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by assigning an invalid organization value to the respective field
        /// </summary>
        public static void EditContactAssigningInvalidOrganization()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationNameNotExisting);

            EditContactPage.EditContact().WithOrganizationName(organizationName).WithHomePhone("123").Edit();
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from organization name field thus rendering it orphan
        /// </summary>
        public static void EditContactRemovingOrganization()
        {
            var organizationName = SetFieldValue("Organization Name", string.Empty);

            EditContactPage.EditContact().WithOrganizationName(organizationName).Edit();
        }

        /// <summary>
        /// Edit a previously created contact changing every single field value.
        /// </summary>
        public static void EditContactAlteringAllValues()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            SetFieldValue("First Name", DummyData.FirstName);
            SetFieldValue("Last Name", DummyData.LastName);
            SetFieldValue("Middle Name", DummyData.MiddleName);
            SetFieldValue("Suffix", DummyData.Suffix);
            SetFieldValue("Organization Name", DummyData.OrganizationNameExisting);
            SetFieldValue("Mobile Phone", DummyData.MobilePhone);
            SetFieldValue("Email", DummyData.Email);
            SetFieldValue("Allow SMS", DummyData.AllowSMS);
            SetFieldValue("Allow Phones", DummyData.AllowPhones);
            SetFieldValue("Allow Emails", DummyData.AllowEmails);

            SetFieldValue("Department", DummyData.Department);
            SetFieldValue("Work Phone", DummyData.WorkPhone);
            SetFieldValue("Work Phone 2", DummyData.WorkPhone2);
            SetFieldValue("Mobile Phone 2", DummyData.MobilePhone2);
            SetFieldValue("Home Phone", DummyData.HomePhone);
            SetFieldValue("Home Phone 2", DummyData.HomePhone2);
            SetFieldValue("Home Fax", DummyData.HomeFax);
            SetFieldValue("Work Fax", DummyData.WorkFax);
            SetFieldValue("Other Phone", DummyData.OtherPhone);
            SetFieldValue("Personal Email", DummyData.PersonalEmail);
            SetFieldValue("Other Email", DummyData.OtherEmail);
            SetFieldValue("Work Street", DummyData.WorkStreet);
            SetFieldValue("Work City", DummyData.WorkCity);
            SetFieldValue("Work State", DummyData.WorkState);
            SetFieldValue("Work Postal Code", DummyData.WorkPostalCode);
            SetFieldValue("Work Country", DummyData.WorkCountry);
            SetFieldValue("Home Street", DummyData.HomeStreet);
            SetFieldValue("Home City", DummyData.HomeCity);
            SetFieldValue("Home State", DummyData.HomeState);
            SetFieldValue("Home Postal Code", DummyData.HomePostalCode);
            SetFieldValue("Home Country", DummyData.HomeCountry);
            SetFieldValue("Other Street", DummyData.OtherStreet);
            SetFieldValue("Other City", DummyData.OtherCity);
            SetFieldValue("Other State", DummyData.OtherState);
            SetFieldValue("Other Postal Code", DummyData.OtherPostalCode);
            SetFieldValue("Other Country", DummyData.OtherCountry);
            SetFieldValue("Salutation", DummyData.Salutation);
            SetFieldValue("Nickname", DummyData.Nickname);
            SetFieldValue("Job Title", DummyData.JobTitle);
            SetFieldValue("Website", DummyData.Website);
            SetFieldValue("Religion", DummyData.Religion);
            SetFieldValue("Birthdate", DummyData.Birthdate);
            SetFieldValue("Gender", DummyData.Gender);
            SetFieldValue("Comments", DummyData.Comments);

            EditContactPage.EditContact().WithMultipleValues(BasicContactFields, ExtraContactFields).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        public static void ImportSimpleContact()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts1.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
        }

        public static void ImportContactWithAllValues()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Middle Name", "Emmanouil");
            SetFieldValue("Suffix", "Mr");
            SetFieldValue("Organization Name", "KONICA MINOLTA");
            SetFieldValue("Mobile Phone", "6912345678");
            SetFieldValue("Email", "email@email.com");
            SetFieldValue("Allow SMS", "False");
            SetFieldValue("Allow Phones", "False");
            SetFieldValue("Allow Emails", "True");

            SetFieldValue("Department", "Administration");
            SetFieldValue("Work Phone", "2101234567");
            SetFieldValue("Work Phone 2", "2111234567");
            SetFieldValue("Mobile Phone 2", "69213456789");
            SetFieldValue("Home Phone", "1234567890");
            SetFieldValue("Home Phone 2", "0987654321");
            SetFieldValue("Home Fax", "1234567890");
            SetFieldValue("Work Fax", "0987654322");
            SetFieldValue("Other Phone", "2143658709");
            SetFieldValue("Personal Email", "myemail@email.com");
            SetFieldValue("Other Email", "otheremail@email.com");
            SetFieldValue("Work Street", "Παπαφλέσσα 10");
            SetFieldValue("Work City", "Τρίκαλα");
            SetFieldValue("Work State", "ΝΥ");
            SetFieldValue("Work Postal Code", "12345");
            SetFieldValue("Work Country", "Greece");
            SetFieldValue("Home Street", "Παπαφλέσσα 11");
            SetFieldValue("Home City", "Γιάννενα");
            SetFieldValue("Home State", "CA");
            SetFieldValue("Home Postal Code", "12345");
            SetFieldValue("Home Country", "Greece");
            SetFieldValue("Other Street", "otherstreet");
            SetFieldValue("Other City", "othercity");
            SetFieldValue("Other State", "OS");
            SetFieldValue("Other Postal Code", "otherpostal");
            SetFieldValue("Other Country", "Wallis and Futuna");
            SetFieldValue("Salutation", "His majesty");
            SetFieldValue("Nickname", "Νοντας");
            SetFieldValue("Job Title", "Ταξιτζής");
            SetFieldValue("Website", "http://www.facebook.com/TheNontas");
            SetFieldValue("Religion", "Αγνωστικιστής");
            SetFieldValue("Birthdate", "28/2/1980");
            SetFieldValue("Gender", "Other");
            SetFieldValue("Comments", "No comments");

        }

        public static void ImportContactWithoutLastName()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
        }

        public static void ImportContactWithInvalidOrganization()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Organization Name", "Rivendale Corp");
        }

        public static void ImportContactWithNonsenseValues()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts7.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "#$@#$");
            SetFieldValue("Last Name", "#$@#$");
            SetFieldValue("Middle Name", "#$@#$");
            SetFieldValue("Suffix", "#$@#$");
            SetFieldValue("Organization Name", "KONICA MINOLTA");
            SetFieldValue("Mobile Phone", "#$@#$");
            SetFieldValue("Email", "#$@#$");
            SetFieldValue("Allow SMS", "False");
            SetFieldValue("Allow Phones", "False");
            SetFieldValue("Allow Emails", "False");

            SetFieldValue("Department", "Administration");
            SetFieldValue("Work Phone", "#$@#$");
            SetFieldValue("Work Phone 2", "#$@#$");
            SetFieldValue("Mobile Phone 2", "#$@#$");
            SetFieldValue("Home Phone", "#$@#$");
            SetFieldValue("Home Phone 2", "#$@#$");
            SetFieldValue("Home Fax", "#$@#$");
            SetFieldValue("Work Fax", "#$@#$");
            SetFieldValue("Other Phone", "#$@#$");
            SetFieldValue("Personal Email", "#$@#$");
            SetFieldValue("Other Email", "#$@#$");
            SetFieldValue("Work Street", "#$@#$");
            SetFieldValue("Work City", "#$@#$");
            SetFieldValue("Work State", "#$@#$");
            SetFieldValue("Work Postal Code", "#$@#$");
            SetFieldValue("Work Country", "Afghanistan");
            SetFieldValue("Home Street", "#$@#$");
            SetFieldValue("Home City", "#$@#$");
            SetFieldValue("Home State", "#$@#$");
            SetFieldValue("Home Postal Code", "#$@#$");
            SetFieldValue("Home Country", "Afghanistan");
            SetFieldValue("Other Street", "#$@#$");
            SetFieldValue("Other City", "#$@#$");
            SetFieldValue("Other State", "#$@#$");
            SetFieldValue("Other Postal Code", "#$@#$");
            SetFieldValue("Other Country", "Afghanistan");
            SetFieldValue("Salutation", "#$@#$");
            SetFieldValue("Nickname", "#$@#$");
            SetFieldValue("Job Title", "#$@#$");
            SetFieldValue("Website", "#$@#$");
            SetFieldValue("Religion", "#$@#$");
            SetFieldValue("Birthdate", "#$@#$");
            SetFieldValue("Gender", "Female");
            SetFieldValue("Comments", "#$@#$");
        }

        public static void ImportContactWithOverflowValues()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
            SetFieldValue("Last Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
        }

        public static void ImportContactWithInvalidBirthdate1()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts100.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "32/1/2000");
        }

        public static void ImportContactWithInvalidBirthdate2()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts101.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "29/2/2001");
        }

        public static void ImportContactWithInvalidBirthdate3()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts102.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "12/13/2000");
        }

        public static void ImportTemplateWithLessColumns()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Email", "test");
            SetFieldValue("Work City", "test");
            SetFieldValue("Home State", "test");

        }

        public static void ImportTemplateWithMoreColumns()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts12.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
        }

        public static void ImportTemplateWithoutMandatoryColumn()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
        }

        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts16.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Allow Emails", "True");
            SetFieldValue("Allow SMS", "True");
            SetFieldValue("Allow Phones", "True");
            SetFieldValue("Birthdate", "15/5/1987");
            SetFieldValue("Mobile Phone", "6944833390");
            SetFieldValue("Work Phone", "null");
            SetFieldValue("Email", ".");
            SetFieldValue("Home Street", "ΑΝΩΓΕΙΩΝ 60");
            SetFieldValue("Home City", "ΗΡΑΚΛΕΙΟ");
            SetFieldValue("Home Postal Code", "71304");

        }
    }
}
