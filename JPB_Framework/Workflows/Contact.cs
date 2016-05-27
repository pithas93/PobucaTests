using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using OpenQA.Selenium;

namespace JPB_Framework.Workflows
{
    public class Contact
    {
        internal List<RecordField> BasicContactFields;
        internal List<RecordField> ExtraContactFields;
        internal List<RecordField> BooleanContactFields;

        public string FirstName => GetFieldValue("First Name");
        public string LastName => GetFieldValue("Last Name");
        public string FullName => $"{GetFieldValue("First Name")} {GetFieldValue("Last Name")}";
        public string OrganizationName => GetFieldValue("Organization Name");

        public string Birthdate => GetFieldValue("Birthdate");

        /// <summary>
        /// If a contact was created during test execution, returns true.
        /// </summary>
        public bool ContactWasCreated
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
        public bool AreContactFieldValuesSavedCorrectly
        {
            get
            {

                if (!ContactViewPage.IsAt || (ContactViewPage.IsAt && ((ContactViewPage.FirstName != FirstName) || (ContactViewPage.LastName != LastName))))
                {
                    if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
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
        public bool IsContactCreatedSuccessfully { get; set; }

        /// <summary>
        /// Returns true if contact was saved successfully after edit.
        /// </summary>
        public bool IsContactSavedAfterEdit { get; set; }


        /// <summary>
        /// Initialize Contact properties
        /// </summary>
        public void Initialize()
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
        public void CleanUp()
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
        internal string SetFieldValue(string fieldLabel, string newValue)
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
        internal string GetFieldValue(string fieldLabel)
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
        internal string SetFieldPreviousValue(string fieldLabel, string previousValue)
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
        internal string GetFieldPreviousValue(string fieldLabel)
        {
            if (BasicContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanContactFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            throw new Exception();
        }
    }
}
