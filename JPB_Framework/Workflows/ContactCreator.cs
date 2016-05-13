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

        private static List<RecordField> BasicContactFields;
        private static List<RecordField> ExtraContactFields;
        private static List<RecordField> BooleanContactFields;
        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


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
                    var valuesAreEqual = contactField.Value == contactField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = string.IsNullOrEmpty(contactField.Value) && string.IsNullOrEmpty(contactField.RecordViewPageFieldValue);

                    if (valuesAreEqual || valuesAreBothEmpty) continue;

                    Report.Report.ToLogFile(MessageType.Message,
                        $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'",
                        null);
                    notOk = true;

                }

                foreach (var contactField in ExtraContactFields)
                {
                    var valuesAreEqual = contactField.Value == contactField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = (contactField.Value == null) && string.IsNullOrEmpty(contactField.RecordViewPageFieldValue);

                    if (!valuesAreEqual && !valuesAreBothEmpty)
                    {
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'", null);
                        notOk = true;
                    }
                    else if (valuesAreBothEmpty && contactField.RecordViewPageIsFieldVisible)
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has no value but its field is shown in contact's detail view page with value '{contactField.RecordViewPageFieldValue}'", null);

                }

                foreach (var contactField in BooleanContactFields)
                {
                    if (contactField.Value == null && contactField.RecordViewPageFieldValue == "True") continue;
                    if (contactField.Value == contactField.RecordViewPageFieldValue) continue;

                    Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'", null);
                    notOk = true;
                }

                return !notOk;
            }
        }

        /// <summary>
        /// Returns true if contact was saved successfully on its creation.
        /// </summary>
        public static bool IsContactCreatedSuccessfully => NewContactPage.IsContactSavedSuccessfully;

        /// <summary>
        /// Returns true if contact was saved successfully after edit.
        /// </summary>
        public static bool IsContactSavedAfterEdit => EditContactPage.IsContactSavedSuccessfully;

        /// <summary>
        /// Returns true if contact was imported successfully.
        /// </summary>
        public static bool IsContactImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;



        /// <summary>
        /// Initialize Contact Creator properties
        /// </summary>
        public static void Initialize()
        {
            BasicContactFields = new List<RecordField>();
            ExtraContactFields = new List<RecordField>();
            BooleanContactFields = new List<RecordField>();

            BasicContactFields.Add(new RecordField("First Name", null, () => ContactViewPage.FirstName, null));
            BasicContactFields.Add(new RecordField("Last Name", null, () => ContactViewPage.LastName, null));
            BasicContactFields.Add(new RecordField("Middle Name", null, () => ContactViewPage.MiddleName, null));
            BasicContactFields.Add(new RecordField("Suffix", null, () => ContactViewPage.Suffix, null));
            BasicContactFields.Add(new RecordField("Organization Name", null, () => ContactViewPage.OrganizationName, null));
            BasicContactFields.Add(new RecordField("Mobile Phone", null, () => ContactViewPage.MobilePhone, null));
            BasicContactFields.Add(new RecordField("Email", null, () => ContactViewPage.Email, null));
            
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

            BooleanContactFields.Add(new RecordField("Allow SMS", null, () => ContactViewPage.AllowSms, null));
            BooleanContactFields.Add(new RecordField("Allow Phones", null, () => ContactViewPage.AllowPhones, null));
            BooleanContactFields.Add(new RecordField("Allow Emails", null, () => ContactViewPage.AllowEmails, null));

        }

        /// <summary>
        /// If a contact was created by ContactCreator, it is deleted if it hasn't been already
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
            else if (BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
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
            else if (BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)).Value;

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
            else if (BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
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
            else if (BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            throw new Exception();
        }




        /// <summary>
        /// Create a simple contact with dummy first and last name values.
        /// </summary>
        public static void CreateSimpleContact()
        {
            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationValue);
            var mobilePhone = SetFieldValue("Mobile Phone", DummyData.PhoneValue);


            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithMobilePhone(mobilePhone)
                .Create();

        }

        /// <summary>
        /// Create a simple contact with dummy first and last name and default organization the one where the browser is currently navigated to. 
        /// You have to be at OrganizationViewPage of an organization for this method to work properly
        /// </summary>
        public static void CreateSimpleContactFromWithinOrganization()
        {
            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = SetFieldValue("Last Name", DummyData.SimpleWord);
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

            SetFieldValue("First Name", DummyData.SimpleWord);
            SetFieldValue("Last Name", DummyData.SimpleWord);
            SetFieldValue("Middle Name", DummyData.SimpleWord);
            SetFieldValue("Suffix", DummyData.SimpleWord);
            SetFieldValue("Organization Name", DummyData.OrganizationValue);
            SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            SetFieldValue("Email", DummyData.EmailValue);
            SetFieldValue("Allow SMS", DummyData.BooleanValue);
            SetFieldValue("Allow Phones", DummyData.BooleanValue);
            SetFieldValue("Allow Emails", DummyData.BooleanValue);

            SetFieldValue("Department", DummyData.DepartmentValue);
            SetFieldValue("Work Phone", DummyData.PhoneValue);
            SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            SetFieldValue("Home Phone", DummyData.PhoneValue);
            SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            SetFieldValue("Home Fax", DummyData.PhoneValue);
            SetFieldValue("Work Fax", DummyData.PhoneValue);
            SetFieldValue("Other Phone", DummyData.PhoneValue);
            SetFieldValue("Personal Email", DummyData.EmailValue);
            SetFieldValue("Other Email", DummyData.EmailValue);
            SetFieldValue("Work Street", DummyData.AddressValue);
            SetFieldValue("Work City", DummyData.SimpleWord);
            SetFieldValue("Work State", DummyData.SimpleWord);
            SetFieldValue("Work Postal Code", DummyData.NumericValue);
            SetFieldValue("Work Country", DummyData.CountryValue);
            SetFieldValue("Home Street", DummyData.AddressValue);
            SetFieldValue("Home City", DummyData.SimpleWord);
            SetFieldValue("Home State", DummyData.SimpleWord);
            SetFieldValue("Home Postal Code", DummyData.NumericValue);
            SetFieldValue("Home Country", DummyData.CountryValue);
            SetFieldValue("Other Street", DummyData.AddressValue);
            SetFieldValue("Other City", DummyData.SimpleWord);
            SetFieldValue("Other State", DummyData.SimpleWord);
            SetFieldValue("Other Postal Code", DummyData.NumericValue);
            SetFieldValue("Other Country", DummyData.CountryValue);
            SetFieldValue("Salutation", DummyData.SimpleWord);
            SetFieldValue("Nickname", DummyData.SimpleWord);
            SetFieldValue("Job Title", DummyData.SimpleWord);
            SetFieldValue("Website", DummyData.SimpleWord);
            SetFieldValue("Religion", DummyData.SimpleWord);
            SetFieldValue("Birthdate", DummyData.DateValue);
            SetFieldValue("Gender", DummyData.GenderValue);
            SetFieldValue("Comments", DummyData.SimpleText);

            NewContactPage.CreateContact().WithMultipleValues(BasicContactFields, ExtraContactFields, BooleanContactFields).Create();

        }

        /// <summary>
        /// Create a contact with first name value but without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);

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
            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var homePhone = SetFieldValue("Home Phone", DummyData.PhoneValue);

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .WithHomePhone(homePhone)
                .Create();
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValuesInExtraFields()
        {
            SetFieldValue("First Name", DummyData.SimpleWord);
            SetFieldValue("Last Name", DummyData.SimpleWord);
            SetFieldValue("Home Phone", string.Empty);
            SetFieldValue("Personal Email", string.Empty);
            SetFieldValue("Work City", string.Empty);
            SetFieldValue("Nickname", string.Empty);

            NewContactPage.CreateContact().WithMultipleValues(BasicContactFields, ExtraContactFields, BooleanContactFields).Create();
        }

        /// <summary>
        /// Edit a previously created contact by changing its first and last names
        /// </summary>
        public static void EditSimpleContact()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = SetFieldValue("Last Name", DummyData.SimpleWord);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

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

            var firstName = SetFieldValue("First Name", DummyData.SimpleWord);
            var lastName = SetFieldValue("Last Name", DummyData.SimpleWord);
            var organizationName = SetFieldValue("Organization Name", DummyData.OrganizationValue);

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).WithNewOrganizationName(organizationName).Edit();

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

            EditContactPage.EditContact().WithNewLastName(lastName).Edit();

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

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

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

            EditContactPage.EditContact().WithNewFirstName(firstName).WithNewLastName(lastName).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Edit a previously created contact by assigning an invalid organization value to the respective field
        /// </summary>
        public static void EditContactAssigningInvalidOrganization()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var homePhone = SetFieldValue("Home Phone", DummyData.PhoneValue);

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).WithNewHomePhone(homePhone).Edit();
        }

        /// <summary>
        /// Edit a previously created contact by removing the value from organization name field thus rendering it orphan
        /// </summary>
        public static void EditContactRemovingOrganization()
        {
            var organizationName = SetFieldValue("Organization Name", string.Empty);

            EditContactPage.EditContact().WithNewOrganizationName(organizationName).Edit();
        }

        /// <summary>
        /// Edit a previously created contact changing every single field value.
        /// </summary>
        public static void EditContactAlteringAllValues()
        {
            SetFieldPreviousValue("First Name", GetFieldValue("First Name"));
            SetFieldPreviousValue("Last Name", GetFieldValue("Last Name"));

            SetFieldValue("First Name", DummyData.SimpleWord);
            SetFieldValue("Last Name", DummyData.SimpleWord);
            SetFieldValue("Middle Name", DummyData.SimpleWord);
            SetFieldValue("Suffix", DummyData.SimpleWord);
            SetFieldValue("Organization Name", DummyData.OrganizationValue);
            SetFieldValue("Mobile Phone", DummyData.PhoneValue);
            SetFieldValue("Email", DummyData.EmailValue);
            SetFieldValue("Allow SMS", DummyData.BooleanValue);
            SetFieldValue("Allow Phones", DummyData.BooleanValue);
            SetFieldValue("Allow Emails", DummyData.BooleanValue);

            SetFieldValue("Department", DummyData.DepartmentValue);
            SetFieldValue("Work Phone", DummyData.PhoneValue);
            SetFieldValue("Work Phone 2", DummyData.PhoneValue);
            SetFieldValue("Mobile Phone 2", DummyData.PhoneValue);
            SetFieldValue("Home Phone", DummyData.PhoneValue);
            SetFieldValue("Home Phone 2", DummyData.PhoneValue);
            SetFieldValue("Home Fax", DummyData.PhoneValue);
            SetFieldValue("Work Fax", DummyData.PhoneValue);
            SetFieldValue("Other Phone", DummyData.PhoneValue);
            SetFieldValue("Personal Email", DummyData.EmailValue);
            SetFieldValue("Other Email", DummyData.EmailValue);
            SetFieldValue("Work Street", DummyData.AddressValue);
            SetFieldValue("Work City", DummyData.SimpleWord);
            SetFieldValue("Work State", DummyData.SimpleWord);
            SetFieldValue("Work Postal Code", DummyData.NumericValue);
            SetFieldValue("Work Country", DummyData.CountryValue);
            SetFieldValue("Home Street", DummyData.AddressValue);
            SetFieldValue("Home City", DummyData.SimpleWord);
            SetFieldValue("Home State", DummyData.SimpleWord);
            SetFieldValue("Home Postal Code", DummyData.NumericValue);
            SetFieldValue("Home Country", DummyData.CountryValue);
            SetFieldValue("Other Street", DummyData.AddressValue);
            SetFieldValue("Other City", DummyData.SimpleWord);
            SetFieldValue("Other State", DummyData.SimpleWord);
            SetFieldValue("Other Postal Code", DummyData.NumericValue);
            SetFieldValue("Other Country", DummyData.CountryValue);
            SetFieldValue("Salutation", DummyData.SimpleWord);
            SetFieldValue("Nickname", DummyData.SimpleWord);
            SetFieldValue("Job Title", DummyData.SimpleWord);
            SetFieldValue("Website", DummyData.SimpleWord);
            SetFieldValue("Religion", DummyData.SimpleWord);
            SetFieldValue("Birthdate", DummyData.DateValue);
            SetFieldValue("Gender", DummyData.GenderValue);
            SetFieldValue("Comments", DummyData.SimpleText);

            EditContactPage.EditContact().WithMultipleNewValues(BasicContactFields, ExtraContactFields, BooleanContactFields).Edit();

            if (EditContactPage.IsContactSavedSuccessfully) return;
            SetFieldValue("First Name", GetFieldPreviousValue("First Name"));
            SetFieldValue("Last Name", GetFieldPreviousValue("Last Name"));
        }

        /// <summary>
        /// Import file with 1 contact. Only first and last name fields have values
        /// </summary>
        public static void ImportSimpleContact()
        {          
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts1.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import file with 1 contact. Every contact field has assigned value.
        /// </summary>
        public static void ImportContactWithAllValues()
        {
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

        /// <summary>
        /// Import file with 1 contact. Only first name field has value. Last name field is left null 
        /// </summary>
        public static void ImportContactWithoutLastName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import file with 1 contact. First and last name have assigned values. Organization name has an non-existent organization as value
        /// </summary>
        public static void ImportContactWithInvalidOrganization()
        {
            if (!ImportPage.IsAt) LeftSideMenu.GoToImports();

            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Organization Name", "Rivendale Corp");
        }

        /// <summary>
        /// Import file with 1 contact. Every contact field has non-sense value assigned except for the combo ones and the organization name.
        /// </summary>
        public static void ImportContactWithNonsenseValues()
        {
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

        /// <summary>
        /// Import file with 1 contact. First and last name fields have assigned values that exceed the 50 characters
        /// </summary>
        public static void ImportContactWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
            SetFieldValue("Last Name", "qwertyuiopasdfghjklzxcvbnmςερτυθιοπασδφγηξκλζχψωβνμ1234567890");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 32/1/2000
        /// </summary>
        public static void ImportContactWithInvalidBirthdate1()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts100.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "32/1/2000");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 29/2/2001
        /// </summary>
        public static void ImportContactWithInvalidBirthdate2()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts101.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "29/2/2001");
        }

        /// <summary>
        /// Import file with 1 contact. Beside first and last name, birthdate field has assigned value 12/13/2000
        /// </summary>
        public static void ImportContactWithInvalidBirthdate3()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts102.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Birthdate", "12/13/2000");
        }

        /// <summary>
        /// Import file with 1 contact. Template contains less columns than original
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
            SetFieldValue("Email", "test");
            SetFieldValue("Work City", "test");
            SetFieldValue("Home State", "test");

        }

        /// <summary>
        /// Import file with 1 contact. Template contains 1 more column than original
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts12.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
            SetFieldValue("Last Name", "Mavrogiannis");
        }

        /// <summary>
        /// Import file with 1 contact. Template does not contain the mandatory field column
        /// </summary>
        public static void ImportTemplateWithoutMandatoryColumn()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Contacts).FromPath(ImportFilePath).WithFileName("Contacts14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("First Name", "Panagiotis");
        }

        /// <summary>
        /// Import file with 1 contact. Template columns are placed in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
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
