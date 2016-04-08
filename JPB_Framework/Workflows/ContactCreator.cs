using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Workflows
{
    public class ContactCreator
    {
        public static List<ContactField> BasicContactFields;
        public static List<ContactField> ExtraContactFields;

        /// <summary>
        /// If a contact was created during test execution, returns true.
        /// </summary>
        public static bool ContactWasCreated
        {
            get
            {
                var firstName = BasicContactFields.Find(x => x.Label.Contains("First Name"));
                var lastName = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
                return
                  (
                      (
                          !string.IsNullOrEmpty(lastName.Value)
                          ||
                          (string.IsNullOrEmpty(lastName.Value) && !string.IsNullOrEmpty(firstName.Value))
                      )
                      &&
                      NewContactPage.IsContactSavedSuccessfully
                  );
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
                var notOk = false;

                foreach (var contactField in BasicContactFields)
                {
                    if ((contactField.Value != null) && (contactField.Value != contactField.ContactViewPageFieldValue))
                    {
                        Report.ToLogFile(MessageType.Message,
                            $"Field: {contactField.Label} has value='{contactField.ContactViewPageFieldValue}' but was expected to have value='{contactField.Value}'",
                            null);
                        notOk = true;
                    }
                }

                foreach (var contactField in ExtraContactFields)
                {
                    if ((contactField.Value != null) && (contactField.Value != contactField.ContactViewPageFieldValue))
                    {
                        Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has value='{contactField.ContactViewPageFieldValue}' but was expected to have value='{contactField.Value}'", null);
                        notOk = true;
                    }
                    else if ((contactField.Value == null) && (contactField.ContactViewPageIsFieldVisible))
                        Report.ToLogFile(MessageType.Message, $"Field: {BasicContactFields[0].Label} has no value but its field is shown in contact's detail view page'", null);
                }

                return !notOk;
            }
        }

        /// <summary>
        /// Initialize Contact Creator properties
        /// </summary>
        public static void Initialize()
        {
            BasicContactFields = new List<ContactField>();   
            ExtraContactFields = new List<ContactField>();            
//            Use line number 100 to identify its field position in the 

            BasicContactFields.Add(new ContactField("First Name", null, DummyData.FirstName, () => ContactViewPage.FirstName, null));
            BasicContactFields.Add(new ContactField("Last Name", null, DummyData.LastName, () => ContactViewPage.LastName, null));
            BasicContactFields.Add(new ContactField("Middle Name", null, DummyData.MiddleName, () => ContactViewPage.MiddleName, null));
            BasicContactFields.Add(new ContactField("Suffix", null, DummyData.Suffix, () => ContactViewPage.Suffix, null));
            BasicContactFields.Add(new ContactField("Organization Name", null, DummyData.OrganizationNameExisting, () => ContactViewPage.OrganizationName, null));
            BasicContactFields.Add(new ContactField("Mobile Phone", null, DummyData.MobilePhone, () => ContactViewPage.MobilePhone, null));
            BasicContactFields.Add(new ContactField("Email", null, DummyData.Email, () => ContactViewPage.Email, null));
            BasicContactFields.Add(new ContactField("Allow SMS", null, DummyData.AllowSMS, () => ContactViewPage.AllowSMS, null));
            BasicContactFields.Add(new ContactField("Allow Phones", null, DummyData.AllowPhones, () => ContactViewPage.AllowPhones, null));
            BasicContactFields.Add(new ContactField("Allow Emails", null, DummyData.AllowEmails, () => ContactViewPage.AllowEmails, null));

            ExtraContactFields.Add(new ContactField("Department", null, DummyData.Department, () => ContactViewPage.Department, () => ContactViewPage.IsDepartmentFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Phone", null, DummyData.WorkPhone, () => ContactViewPage.WorkPhone, () => ContactViewPage.IsWorkPhoneFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Phone 2", null, DummyData.WorkPhone2, () => ContactViewPage.WorkPhone2, () => ContactViewPage.IsWorkPhone2FieldVisible));
            ExtraContactFields.Add(new ContactField("Mobile Phone 2", null, DummyData.MobilePhone2, () => ContactViewPage.MobilePhone2, () => ContactViewPage.IsMobilePhone2FieldVisible));
            ExtraContactFields.Add(new ContactField("Home Phone", null, DummyData.HomePhone, () => ContactViewPage.HomePhone, () => ContactViewPage.IsHomePhoneFieldVisible));
            ExtraContactFields.Add(new ContactField("Home Phone 2", null, DummyData.HomePhone2, () => ContactViewPage.HomePhone2, () => ContactViewPage.IsHomePhone2FieldVisible));
            ExtraContactFields.Add(new ContactField("Home Fax", null, DummyData.HomeFax, () => ContactViewPage.HomeFax, () => ContactViewPage.IsHomeFaxFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Fax", null, DummyData.WorkFax, () => ContactViewPage.WorkFax, () => ContactViewPage.IsWorkFaxFieldVisible));
            ExtraContactFields.Add(new ContactField("Other Phone", null, DummyData.OtherPhone, () => ContactViewPage.OtherPhone, () => ContactViewPage.IsOtherPhoneFieldVisible));
            ExtraContactFields.Add(new ContactField("Personal Email", null, DummyData.PersonalEmail, () => ContactViewPage.PersonalEmail, () => ContactViewPage.IsPersonalEmailFieldVisible));
            ExtraContactFields.Add(new ContactField("Other Email", null, DummyData.OtherEmail, () => ContactViewPage.OtherEmail, () => ContactViewPage.IsOtherEmailFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Street", null, DummyData.WorkStreet, () => ContactViewPage.WorkStreet, () => ContactViewPage.IsWorkStreetFieldVisible));
            ExtraContactFields.Add(new ContactField("Work City", null, DummyData.WorkCity, () => ContactViewPage.WorkCity, () => ContactViewPage.IsWorkCityFieldVisible));
            ExtraContactFields.Add(new ContactField("Work State", null, DummyData.WorkState, () => ContactViewPage.WorkState, () => ContactViewPage.IsWorkStateFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Postal Code", null, DummyData.WorkPostalCode, () => ContactViewPage.WorkPostalCode, () => ContactViewPage.IsWorkPostalCodeFieldVisible));
            ExtraContactFields.Add(new ContactField("Work Country", null, DummyData.WorkCountry, () => ContactViewPage.WorkCountry, () => ContactViewPage.IsWorkCountryFieldVisible));
            ExtraContactFields.Add(new ContactField("Home Street", null, DummyData.HomeStreet, () => ContactViewPage.HomeStreet, () => ContactViewPage.IsHomeStreetFieldVisible));
            ExtraContactFields.Add(new ContactField("Home City", null, DummyData.HomeCity, () => ContactViewPage.HomeCity, () => ContactViewPage.IsHomeCityFieldVisible));
            ExtraContactFields.Add(new ContactField("Home State", null, DummyData.HomeState, () => ContactViewPage.HomeState, () => ContactViewPage.IsHomeStateFieldVisible));
            ExtraContactFields.Add(new ContactField("Home Postal Code", null, DummyData.HomePostalCode, () => ContactViewPage.HomePostalCode, () => ContactViewPage.IsHomePostalCodeFieldVisible));
            ExtraContactFields.Add(new ContactField("Home Country", null, DummyData.HomeCountry, () => ContactViewPage.HomeCountry, () => ContactViewPage.IsHomeCountryFieldVisible));
            ExtraContactFields.Add(new ContactField("Other Street", null, DummyData.OtherStreet, () => ContactViewPage.OtherStreet, () => ContactViewPage.IsOtherStreetFieldVisible));
            ExtraContactFields.Add(new ContactField("Other City", null, DummyData.OtherCity, () => ContactViewPage.OtherCity, () => ContactViewPage.IsOtherCityFieldVisible));
            ExtraContactFields.Add(new ContactField("Other State", null, DummyData.OtherState, () => ContactViewPage.OtherState, () => ContactViewPage.IsOtherStateFieldVisible));
            ExtraContactFields.Add(new ContactField("Other Postal Code", null, DummyData.OtherPostalCode, () => ContactViewPage.OtherPostalCode, () => ContactViewPage.IsOtherPostalCodeFieldVisible));
            ExtraContactFields.Add(new ContactField("Other Country", null, DummyData.OtherCountry, () => ContactViewPage.OtherCountry, () => ContactViewPage.IsOtherCountryFieldVisible));
            ExtraContactFields.Add(new ContactField("Salutation", null, DummyData.Salutation, () => ContactViewPage.Salutation, () => ContactViewPage.IsSalutationFieldVisible));
            ExtraContactFields.Add(new ContactField("Nickname", null, DummyData.Nickname, () => ContactViewPage.Nickname, () => ContactViewPage.IsNicknameFieldVisible));
            ExtraContactFields.Add(new ContactField("Job Title", null, DummyData.JobTitle, () => ContactViewPage.JobTitle, () => ContactViewPage.IsJobTitleFieldVisible));
            ExtraContactFields.Add(new ContactField("Website", null, DummyData.Website, () => ContactViewPage.Website, () => ContactViewPage.IsWebsiteFieldVisible));
            ExtraContactFields.Add(new ContactField("Religion", null, DummyData.Religion, () => ContactViewPage.Religion, () => ContactViewPage.IsReligionFieldVisible));
            ExtraContactFields.Add(new ContactField("Birthdate", null, DummyData.Birthdate, () => ContactViewPage.Birthdate, () => ContactViewPage.IsBirthdateFieldVisible));
            ExtraContactFields.Add(new ContactField("Gender", null, DummyData.Gender, () => ContactViewPage.Gender, () => ContactViewPage.IsGenderFieldVisible));
            ExtraContactFields.Add(new ContactField("Comments", null, DummyData.Comments, () => ContactViewPage.Comments, () => ContactViewPage.IsCommentsFieldVisible));


        }

        /// <summary>
        /// Delete every contact created by the test
        /// </summary>
        public static void CleanUp()
        {
            if (ContactWasCreated)
            {
                LeftSideMenu.GoToContacts();
                ContactsPage.FindContact()
                    .WithFirstName(BasicContactFields[0].Value)
                    .AndLastName(BasicContactFields[1].Value)
                    .Delete();
            }
        }      

        /// <summary>
        /// Create a simple contact with dummy first and last name
        /// </summary>
        public static void CreateSimpleContact()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            var lastNameField = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
            firstNameField.Value = firstNameField.DummyValue;
            lastNameField.Value = lastNameField.DummyValue;

            NewContactPage.CreateContact().WithFirstName(firstNameField.Value).WithLastName(lastNameField.Value).Create();
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {
            foreach (var contactField in BasicContactFields)
            {
                contactField.Value = contactField.DummyValue;
            }
            foreach (var contactField in ExtraContactFields)
            {
                contactField.Value = contactField.DummyValue;
            }

            NewContactPage.CreateContact().WithDummyValues().Create();

        }

        /// <summary>
        /// Create a contact without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            firstNameField.Value = firstNameField.DummyValue;

            NewContactPage.CreateContact().WithFirstName(firstNameField.Value).Create();
        }

        /// <summary>
        /// Create a contact with values in first and last name that exceed the 50 character limit
        /// </summary>
        public static void CreateContactWithOverflowValues()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            var lastNameField = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
            firstNameField.Value = DummyData.OverflowValue;
            lastNameField.Value = DummyData.OverflowValue;

            NewContactPage.CreateContact().WithFirstName(firstNameField.Value).WithLastName(lastNameField.Value).Create();
        }

        /// <summary>
        /// Create a contact with nonsense values in its fields
        /// </summary>
        public static void CreateContactWithNonsenseValues()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            var lastNameField = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
            firstNameField.Value = DummyData.NonsenseValue;
            lastNameField.Value = DummyData.NonsenseValue;

            NewContactPage.CreateContact().WithFirstName(firstNameField.Value).WithLastName(lastNameField.Value).Create();
        }

        /// <summary>
        /// Create a contact which is linked to a non existent organization
        /// </summary>
        public static void CreateContactWithInvalidOrganization()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            var lastNameField = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
            var organizationNameField = BasicContactFields.Find(x => x.Label.Contains("Organization Name"));
            firstNameField.Value = firstNameField.DummyValue;
            lastNameField.Value = lastNameField.DummyValue;
            organizationNameField.Value = DummyData.OrganizationNameNotExisting;

            NewContactPage.CreateContact()
                .WithFirstName(firstNameField.Value)
                .WithLastName(lastNameField.Value)
                .WithOrganizationName(organizationNameField.Value)
                .Create();
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValues()
        {
            var firstNameField = BasicContactFields.Find(x => x.Label.Contains("First Name"));
            var lastNameField = BasicContactFields.Find(x => x.Label.Contains("Last Name"));
            var homePhoneField = ExtraContactFields.Find(x => x.Label.Contains("Home Phone"));
            var personalEmailField = ExtraContactFields.Find(x => x.Label.Contains("Personal Email"));
            var workCityField = ExtraContactFields.Find(x => x.Label.Contains("Work City"));
            var nicknameField = ExtraContactFields.Find(x => x.Label.Contains("Nickname"));

            firstNameField.Value = firstNameField.DummyValue;
            lastNameField.Value = lastNameField.DummyValue;
            homePhoneField.Value = string.Empty;
            personalEmailField.Value = string.Empty;
            workCityField.Value = string.Empty;
            nicknameField.Value = string.Empty;

            NewContactPage.CreateContact()
                .WithFirstName(firstNameField.Value)
                .WithLastName(lastNameField.Value)
                .WithHomePhone(homePhoneField.Value)
                .WithPersonalEmail(personalEmailField.Value)
                .WithWorkCity(workCityField.Value)
                .WithNickname(nicknameField.Value)
                .Create();
        }

    }

    public class ContactField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string DummyValue { get; set; }
        public string ContactViewPageFieldValue { get { return ContactViewPageFieldValueFunc(); } }
        public bool ContactViewPageIsFieldVisible { get { return ContactViewPageIsFieldVisibleFunc(); } }
        private Func<string> ContactViewPageFieldValueFunc { get; set; }
        private Func<bool> ContactViewPageIsFieldVisibleFunc { get; set; }

        public ContactField(string label, string value, string dummyValue, Func<string> contactViewPageFieldValueFunc, Func<bool> contactViewPageIsFieldVisibleFunc)
        {
            Label = label;
            Value = value;
            DummyValue = dummyValue;
            ContactViewPageFieldValueFunc = contactViewPageFieldValueFunc;
            ContactViewPageIsFieldVisibleFunc = contactViewPageIsFieldVisibleFunc;
        }
    }
}
