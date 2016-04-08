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
        public static List<ContactField> ContactFields;

        private static string firstName { get; set; }
        private static string lastName { get; set; }
        private static string middleName { get; set; }
        private static string suffix { get; set; }
        private static string organizationName { get; set; }
        private static string department { get; set; }
        private static string workPhone { get; set; }
        private static string workPhone2 { get; set; }
        private static string mobilePhone { get; set; }
        private static string mobilePhone2 { get; set; }
        private static string homePhone { get; set; }
        private static string homePhone2 { get; set; }
        private static string homeFax { get; set; }
        private static string workFax { get; set; }
        private static string otherPhone { get; set; }
        private static string email { get; set; }
        private static string personalEmail { get; set; }
        private static string otherEmail { get; set; }
        private static string workStreet { get; set; }
        private static string workCity { get; set; }
        private static string workState { get; set; }
        private static string workPostalCode { get; set; }
        private static string workCountry { get; set; }
        private static string homeStreet { get; set; }
        private static string homeCity { get; set; }
        private static string homeState { get; set; }
        private static string homePostalCode { get; set; }
        private static string homeCountry { get; set; }
        private static string otherStreet { get; set; }
        private static string otherCity { get; set; }
        private static string otherState { get; set; }
        private static string otherPostalCode { get; set; }
        private static string otherCountry { get; set; }
        private static string salutation { get; set; }
        private static string nickname { get; set; }
        private static string jobTitle { get; set; }
        private static string website { get; set; }
        private static string religion { get; set; }
        private static string birthdate { get; set; }
        private static string gender { get; set; }
        private static string comments { get; set; }
        private static bool? allowSms { get; set; }
        private static bool? allowPhones { get; set; }
        private static bool? allowEmails { get; set; }


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





                if ((firstName != null) && (firstName != ContactViewPage.FirstName)) notOk = NotOk("First Name", ContactViewPage.FirstName, firstName);

                if ((lastName != null) && (lastName != ContactViewPage.LastName)) notOk = NotOk("Last Name", ContactViewPage.LastName, lastName);

                

                    if ((ContactFields[0].Value != null) && (ContactFields[0].Value != ContactFields[0].ContactViewPageFieldValue))
                        notOk = NotOk(ContactFields[0].Label, ContactFields[0].ContactViewPageFieldValue, ContactFields[0].Value);
                    else if ((ContactFields[0].Value == null) && ContactFields[0].ContactViewPageIsFieldVisible)
                        FieldIsVisible(ContactFields[0].Label);
                

                //                if ((middleName != null) && (middleName != ContactViewPage.MiddleName)) notOk = NotOk("Middle Name", ContactViewPage.MiddleName, middleName);
                //
                //                if ((suffix != null) && (suffix != ContactViewPage.Suffix)) notOk = NotOk("Suffix", ContactViewPage.Suffix, suffix);
                //
                //                if ((organizationName != null) && (organizationName != ContactViewPage.OrganizationName)) notOk = NotOk("Organization Name", ContactViewPage.OrganizationName, organizationName);
                //
                //                if ((department != null) && (department != ContactViewPage.Department)) notOk = NotOk("Department", ContactViewPage.Department, department);
                //                else if (department == null && ContactViewPage.IsDepartmentFieldVisible) FieldIsVisible("Department");
                //
                //                if ((workPhone != null) && (workPhone != ContactViewPage.WorkPhone)) notOk = NotOk("Work Phone", ContactViewPage.WorkPhone, workPhone);
                //                else if (workPhone == null && ContactViewPage.IsWorkPhoneFieldVisible) FieldIsVisible("Work Phone");
                //
                //                if ((workPhone2 != null) && (workPhone2 != ContactViewPage.WorkPhone2)) notOk = NotOk("Work Phone 2", ContactViewPage.WorkPhone2, workPhone2);
                //                else if (workPhone2 == null && ContactViewPage.IsWorkPhone2FieldVisible) FieldIsVisible("Work Phone 2");
                //
                //                if ((mobilePhone != null) && (mobilePhone != ContactViewPage.MobilePhone)) notOk = NotOk("Mobile Phone", ContactViewPage.MobilePhone, mobilePhone);
                //
                //                if ((mobilePhone2 != null) && (mobilePhone2 != ContactViewPage.MobilePhone2)) notOk = NotOk("Mobile Phone 2", ContactViewPage.MobilePhone2, mobilePhone2);
                //                else if (mobilePhone2 == null && ContactViewPage.IsMobilePhone2FieldVisible) FieldIsVisible("Mobile Phone 2");
                //
                //                if ((homePhone != null) && (homePhone != ContactViewPage.HomePhone)) notOk = NotOk("Home Phone", ContactViewPage.HomePhone, homePhone);
                //                else if (homePhone == null && ContactViewPage.IsHomePhoneFieldVisible) FieldIsVisible("Home Phone");
                //
                //                if ((homePhone2 != null) && (homePhone2 != ContactViewPage.HomePhone2)) notOk = NotOk("Home Phone 2", ContactViewPage.HomePhone2, homePhone2);
                //                else if (homePhone2 == null && ContactViewPage.IsHomePhone2FieldVisible) FieldIsVisible("Home Phone 2");
                //
                //                if ((homeFax != null) && (homeFax != ContactViewPage.HomeFax)) notOk = NotOk("Home Fax", ContactViewPage.HomeFax, homeFax);
                //                else if (homeFax == null && ContactViewPage.IsHomeFaxFieldVisible) FieldIsVisible("Home Fax");
                //
                //                if ((workFax != null) && (workFax != ContactViewPage.WorkFax)) notOk = NotOk("Work Fax", ContactViewPage.WorkFax, workFax);
                //                else if (workFax == null && ContactViewPage.IsWorkFaxFieldVisible) FieldIsVisible("Work Fax");
                //
                //                if ((otherPhone != null) && (otherPhone != ContactViewPage.OtherPhone)) notOk = NotOk("Other Phone", ContactViewPage.OtherPhone, otherPhone);
                //                else if (otherPhone == null && ContactViewPage.IsOtherPhoneFieldVisible) FieldIsVisible("Other Phone");
                //
                //                if ((email != null) && (email != ContactViewPage.Email)) notOk = NotOk("Email", ContactViewPage.Email, email);
                //
                //                if ((personalEmail != null) && (personalEmail != ContactViewPage.PersonalEmail)) notOk = NotOk("Personal Email", ContactViewPage.PersonalEmail, personalEmail);
                //                else if (personalEmail == null && ContactViewPage.IsPersonalEmailFieldVisible) FieldIsVisible("Personal Email");
                //
                //                if ((otherEmail != null) && (otherEmail != ContactViewPage.OtherEmail)) notOk = NotOk("Other Email", ContactViewPage.OtherEmail, otherEmail);
                //                else if (otherEmail == null && ContactViewPage.IsOtherEmailFieldVisible) FieldIsVisible("Other Email");
                //
                //                if ((workStreet != null) && (workStreet != ContactViewPage.WorkStreet)) notOk = NotOk("Work Street", ContactViewPage.WorkStreet, workStreet);
                //                else if (workStreet != null && ContactViewPage.IsWorkStreetFieldVisible) FieldIsVisible("Work Street");
                //
                //                if ((workCity != null) && (workCity != ContactViewPage.WorkCity)) notOk = NotOk("Work City", ContactViewPage.WorkCity, workCity);
                //                else if (workCity == null && ContactViewPage.IsWorkStreetFieldVisible) FieldIsVisible("Work City");
                //
                //                if ((workState != null) && (workState != ContactViewPage.WorkState)) notOk = NotOk("Work State", ContactViewPage.WorkState, workState);
                //                else if (workState == null && ContactViewPage.IsWorkStateFieldVisible) FieldIsVisible("Work State");
                //
                //                if ((workPostalCode != null) && (workPostalCode != ContactViewPage.WorkPostalCode)) notOk = NotOk("Work Postal Code", ContactViewPage.WorkPostalCode, workPostalCode);
                //                else if (workPostalCode == null && ContactViewPage.IsWorkPostalCodeFieldVisible) FieldIsVisible("Work Postal Code");
                //
                //                if ((workCountry != null) && (workCountry != ContactViewPage.WorkCountry)) notOk = NotOk("Work Country", ContactViewPage.WorkCountry, workCountry);
                //                else if (workCountry == null && ContactViewPage.IsWorkCountryFieldVisible) FieldIsVisible("Work Country");
                //
                //                if ((homeStreet != null) && (homeStreet != ContactViewPage.HomeStreet)) notOk = NotOk("Home Street", ContactViewPage.HomeStreet, homeStreet);
                //                else if (homeStreet == null && ContactViewPage.IsHomeStreetFieldVisible) FieldIsVisible("Home Street");
                //
                //                if ((homeCity != null) && (homeCity != ContactViewPage.HomeCity)) notOk = NotOk("Home City", ContactViewPage.HomeCity, homeCity);
                //                else if (homeCity == null && ContactViewPage.IsHomeCityFieldVisible) FieldIsVisible("Home City");
                //
                //                if ((homeState != null) && (homeState != ContactViewPage.HomeState)) notOk = NotOk("Home State", ContactViewPage.HomeState, homeState);
                //                else if (homeState == null && ContactViewPage.IsHomeStateFieldVisible) FieldIsVisible("Home State");
                //
                //                if ((homePostalCode != null) && (homePostalCode != ContactViewPage.HomePostalCode)) notOk = NotOk("Home Postal Code", ContactViewPage.HomePostalCode, homePostalCode);
                //                else if (homePostalCode == null && ContactViewPage.IsHomePostalCodeFieldVisible) FieldIsVisible("Home Postal Code");
                //
                //                if ((homeCountry != null) && (homeCountry != ContactViewPage.HomeCountry)) notOk = NotOk("Home Country", ContactViewPage.HomeCountry, homeCountry);
                //                else if (homeCountry == null && ContactViewPage.IsHomeCountryFieldVisible) FieldIsVisible("Home Country");
                //
                //                if ((otherStreet != null) && (otherStreet != ContactViewPage.OtherStreet)) notOk = NotOk("Other Street", ContactViewPage.OtherStreet, otherStreet);
                //                else if (otherStreet == null && ContactViewPage.IsOtherStreetFieldVisible) FieldIsVisible("Other Street");
                //
                //                if ((otherCity != null) && (otherCity != ContactViewPage.OtherCity)) notOk = NotOk("Other City", ContactViewPage.OtherCity, otherCity);
                //                else if (otherCity == null && ContactViewPage.IsOtherCityFieldVisible) FieldIsVisible("Other City");
                //
                //                if ((otherState != null) && (otherState != ContactViewPage.OtherState)) notOk = NotOk("Other State", ContactViewPage.OtherState, otherState);
                //                else if (otherState == null && ContactViewPage.IsOtherStateFieldVisible) FieldIsVisible("Other State");
                //
                //                if ((otherPostalCode != null) && (otherPostalCode != ContactViewPage.OtherPostalCode)) notOk = NotOk("Other Postal Code", ContactViewPage.OtherPostalCode, otherPostalCode);
                //                else if (otherPostalCode == null && ContactViewPage.IsOtherPostalCodeFieldVisible) FieldIsVisible("Other Postal Code");
                //
                //                if ((otherCountry != null) && (otherCountry != ContactViewPage.OtherCountry)) notOk = NotOk("Other Country", ContactViewPage.OtherCountry, otherCountry);
                //                else if (otherCountry == null && ContactViewPage.IsOtherCountryFieldVisible) FieldIsVisible("Other Country");
                //
                //                if ((salutation != null) && (salutation != ContactViewPage.Salutation)) notOk = NotOk("Salutation", ContactViewPage.Salutation, salutation);
                //                else if (salutation == null && ContactViewPage.IsSalutationFieldVisible) FieldIsVisible("Salutation");
                //
                //                if ((nickname != null) && (nickname != ContactViewPage.Nickname)) notOk = NotOk("Nickname", ContactViewPage.Nickname, nickname);
                //                else if (nickname == null && ContactViewPage.IsNicknameFieldVisible) FieldIsVisible("Nickname");
                //
                //                if ((jobTitle != null) && (jobTitle != ContactViewPage.JobTitle)) notOk = NotOk("Job Title", ContactViewPage.JobTitle, jobTitle);
                //                else if (jobTitle == null && ContactViewPage.IsJobTitleFieldVisible) FieldIsVisible("Job Title");
                //
                //                if ((website != null) && (website != ContactViewPage.Website)) notOk = NotOk("Website", ContactViewPage.Website, website);
                //                else if (website == null && ContactViewPage.IsWebsiteFieldVisible) FieldIsVisible("Website");
                //
                //                if ((religion != null) && (religion != ContactViewPage.Religion)) notOk = NotOk("Religion", ContactViewPage.Religion, religion);
                //                else if (religion == null && ContactViewPage.IsReligionFieldVisible) FieldIsVisible("Religion");
                //
                //                if ((birthdate != null) && (birthdate != ContactViewPage.Birthdate)) notOk = NotOk("Birthdate", ContactViewPage.Birthdate, birthdate);
                //                else if (birthdate == null && ContactViewPage.IsBirthdateFieldVisible()) FieldIsVisible("Birthdate");
                //
                //                if ((gender != null) && (gender != ContactViewPage.Gender)) notOk = NotOk("Gender", ContactViewPage.Gender, gender);
                //                else if (gender == null && ContactViewPage.IsGenderFieldVisible()) FieldIsVisible("Gender");
                //
                //                if ((comments != null) && (comments != ContactViewPage.Comments)) notOk = NotOk("Comments", ContactViewPage.Comments, comments);
                //                else if (comments == null && ContactViewPage.IsCommentsFieldVisible) FieldIsVisible("Comments");
                //
                //                if ((allowSms != null) && (allowSms != ContactViewPage.AllowSMS)) notOk = NotOk("Allow SMS", ContactViewPage.AllowSMS, (bool)allowSms);
                //
                //                if ((allowPhones != null) && (allowPhones != ContactViewPage.AllowPhones)) notOk = NotOk("Allow Phones", ContactViewPage.AllowPhones, (bool)allowPhones);
                //
                //                if ((allowEmails != null) && (allowEmails != ContactViewPage.AllowEmails)) notOk = NotOk("Allow Emails", ContactViewPage.AllowEmails, (bool)allowEmails);

                return !notOk;
            }
        }

        /// <summary>
        /// Initialize Contact Creator properties
        /// </summary>
        public static void Initialize()
        {
            firstName = null;
            ContactFields = new List<ContactField>();

            ContactFields.Add(
                new ContactField(
                    "Work Phone",
                    null,
                    () => ContactViewPage.WorkPhone,
                    () => ContactViewPage.IsWorkPhoneFieldVisible
                ));

            lastName = null;
            middleName = null;
            suffix = null;
            organizationName = null;
            department = null;

            //            workPhone = null;
            workPhone2 = null;
            mobilePhone = null;
            mobilePhone2 = null;
            homePhone = null;
            homePhone2 = null;
            homeFax = null;
            workFax = null;
            otherPhone = null;

            email = null;
            personalEmail = null;
            otherEmail = null;

            workStreet = null;
            workCity = null;
            workState = null;
            workPostalCode = null;
            workCountry = null;

            homeStreet = null;
            homeCity = null;
            homeState = null;
            homePostalCode = null;
            homeCountry = null;

            otherStreet = null;
            otherCity = null;
            otherState = null;
            otherPostalCode = null;
            otherCountry = null;

            salutation = null;
            nickname = null;
            jobTitle = null;
            website = null;
            religion = null;
            birthdate = null;
            gender = null;
            comments = null;
            allowSms = null;
            allowPhones = null;
            allowEmails = null;
        }

        /// <summary>
        /// Delete every contact created by the test
        /// </summary>
        public static void CleanUp()
        {
            if (ContactWasCreated)
            {
                LeftSideMenu.GoToContacts();
                ContactsPage.FindContact().WithFirstName(firstName).AndLastName(lastName).Delete();
            }
        }

        /// <summary>
        /// For reporting purposes. Reports to file the given value and the expected dummy value of a record field
        /// </summary>
        /// <param name="fieldName">Contact field under chek</param>
        /// <param name="value">Given field value</param>
        /// <param name="dummyValue">Expected dummy value for the current filed</param>
        /// <returns>Always true</returns>
        private static bool NotOk(string fieldName, string value, string dummyValue)
        {
            Report.ToLogFile(MessageType.Message, $"Field: {fieldName} has value='{value}' but was expected to have value='{dummyValue}'", null);
            return true;
        }

        /// <summary>
        /// For reporting purposes. Reports to file when a field is shown with no value but it was expected to be hidden.
        /// </summary>
        /// <param name="fieldName"></param>
        private static void FieldIsVisible(string fieldName)
        {
            Report.ToLogFile(MessageType.Message,
                $"Field: {fieldName} has no value but its field is shown in contact's detail view page'", null);
        }

        /// <summary>
        /// For reporting purposes. Reports to file the given value and the expected dummy value of a record field
        /// </summary>
        /// <param name="fieldName">Contact field under chek</param>
        /// <param name="value">Given field value</param>
        /// <param name="dummyValue">Expected dummy value for the current filed</param>
        /// <returns>Always true</returns>
        private static bool NotOk(string fieldName, bool field, bool dummyField)
        {
            Report.ToLogFile(MessageType.Message, $"Field: {fieldName} has value='{field}' but was expected to have value='{dummyField}'", null);
            return true;
        }

        /// <summary>
        /// If a contact was created during test execution, returns true.
        /// </summary>
        public static bool ContactWasCreated
        {
            get
            {
                return
                  (
                      (
                          !string.IsNullOrEmpty(lastName)
                          ||
                          (string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(firstName))
                      )
                      &&
                      NewContactPage.IsContactSavedSuccessfully
                  );
            }
        }

        /// <summary>
        /// Create a simple contact with dummy first and last name
        /// </summary>
        public static void CreateSimpleContact()
        {
            firstName = DummyData.FirstName;
            lastName = DummyData.LastName;

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact with dummy values in every field
        /// </summary>
        public static void CreateContactWithAllValues()
        {

            firstName = DummyData.FirstName;
            lastName = DummyData.LastName;
            middleName = DummyData.MiddleName;
            suffix = DummyData.Suffix;
            organizationName = DummyData.OrganizationNameExisting;
            department = DummyData.Department;

            workPhone = DummyData.WorkPhone;
            workPhone2 = DummyData.WorkPhone2;
            mobilePhone = DummyData.MobilePhone;
            mobilePhone2 = DummyData.MobilePhone2;
            homePhone = DummyData.HomePhone;
            homePhone2 = DummyData.HomePhone2;
            homeFax = DummyData.HomeFax;
            workFax = DummyData.WorkFax;
            otherPhone = DummyData.OtherPhone;

            email = DummyData.Email;
            personalEmail = DummyData.PersonalEmail;
            otherEmail = DummyData.OtherEmail;

            workStreet = DummyData.WorkStreet;
            workCity = DummyData.WorkCity;
            workState = DummyData.WorkState;
            workPostalCode = DummyData.WorkPostalCode;
            workCountry = DummyData.WorkCountry;

            homeStreet = DummyData.HomeStreet;
            homeCity = DummyData.HomeCity;
            homeState = DummyData.HomeState;
            homePostalCode = DummyData.HomePostalCode;
            homeCountry = DummyData.HomeCountry;

            otherStreet = DummyData.OtherStreet;
            otherCity = DummyData.OtherCity;
            otherState = DummyData.OtherState;
            otherPostalCode = DummyData.OtherPostalCode;
            otherCountry = DummyData.OtherCountry;

            salutation = DummyData.Salutation;
            nickname = DummyData.Nickname;
            jobTitle = DummyData.JobTitle;
            website = DummyData.Website;
            religion = DummyData.Religion;
            birthdate = DummyData.Birthdate;
            gender = DummyData.Gender;
            comments = DummyData.Comments;
            allowSms = DummyData.AllowSMS;
            allowPhones = DummyData.AllowPhones;
            allowEmails = DummyData.AllowEmails;

            NewContactPage.CreateContact().WithDummyValues().Create();

        }

        /// <summary>
        /// Create a contact without value in last name field
        /// </summary>
        public static void CreateContactWithoutLastName()
        {
            firstName = DummyData.FirstName;

            NewContactPage.CreateContact().WithFirstName(firstName).Create();
        }

        /// <summary>
        /// Create a contact with values in first and last name that exceed the 50 character limit
        /// </summary>
        public static void CreateContactWithOverflowValues()
        {
            firstName = DummyData.OverflowValue;
            lastName = DummyData.OverflowValue;

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact with nonsense values in its fields
        /// </summary>
        public static void CreateContactWithNonsenseValues()
        {
            firstName = DummyData.NonsenseValue;
            lastName = DummyData.NonsenseValue;

            NewContactPage.CreateContact().WithFirstName(firstName).WithLastName(lastName).Create();
        }

        /// <summary>
        /// Create a contact which is linked to a non existent organization
        /// </summary>
        public static void CreateContactWithInvalidOrganization()
        {
            firstName = DummyData.FirstName;
            lastName = DummyData.LastName;
            organizationName = DummyData.OrganizationNameNotExisting;

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithOrganizationName(organizationName)
                .Create();
        }

        /// <summary>
        /// Create a contact with null string values in extra fields. Each extra field belongs to one of the extra fields categories
        /// </summary>
        public static void CreateContactWithNullValues()
        {
            firstName = DummyData.FirstName;
            lastName = DummyData.LastName;
            homePhone = string.Empty;
            personalEmail = string.Empty;
            workCity = string.Empty;
            nickname = string.Empty;

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithHomePhone(homePhone)
                .WithPersonalEmail(personalEmail)
                .WithWorkCity(workCity)
                .WithNickname(nickname)
                .Create();
        }

        public static void testcase()
        {
            firstName = DummyData.FirstName;
            lastName = DummyData.LastName;
            ContactFields[0].Value = DummyData.WorkPhone;

            NewContactPage.CreateContact()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithWorkPhone(ContactFields[0].Value)
                .Create();
        }
    }

    public class ContactField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string ContactViewPageFieldValue
        {
            get { return ContactViewPageFieldValueFunc(); }

        }
        public bool ContactViewPageIsFieldVisible
        {
            get { return ContactViewPageIsFieldVisibleFunc(); }

        }
        public Func<string> ContactViewPageFieldValueFunc { get; set; }
        public Func<bool> ContactViewPageIsFieldVisibleFunc { get; set; }

        public ContactField(string label, string value, Func<string> contactViewPageFieldValueFunc, Func<bool> contactViewPageIsFieldVisibleFunc)
        {
            Label = label;
            Value = value;
            ContactViewPageFieldValueFunc = contactViewPageFieldValueFunc;
            ContactViewPageIsFieldVisibleFunc = contactViewPageIsFieldVisibleFunc;
        }
    }
}
