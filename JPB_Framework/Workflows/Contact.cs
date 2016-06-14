﻿using System;
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

        public string FirstName => GetFieldValue(ContactFields.FirstName);
        public string LastName => GetFieldValue(ContactFields.LastName);
        public string FullName => $"{GetFieldValue(ContactFields.FirstName)} {GetFieldValue(ContactFields.LastName)}";
        public string OrganizationName => GetFieldValue(ContactFields.OrganizationName);
        public string Favorite => GetFieldValue(ContactFields.Favorite);
        public string Birthdate => GetFieldValue(ContactFields.Birthdate);

        /// <summary>
        /// If a contact was created during test execution, returns true.
        /// </summary>
        public bool ContactWasCreated
        {
            get
            {
                var firstName = BasicContactFields.Find(x => x.Label.Contains(ContactFields.FirstName)).Value;
                var lastName = BasicContactFields.Find(x => x.Label.Contains(ContactFields.LastName)).Value;

                var firstNameHasValue = !string.IsNullOrEmpty(firstName);
                var lastNameHasValue = !string.IsNullOrEmpty(lastName);

                return (lastNameHasValue || firstNameHasValue);
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
                var isAtContactViewPage = ContactViewPage.IsAt || ContactViewPage.IsAtFromWithinOrganizationViewPage;

                if (!isAtContactViewPage || ((ContactViewPage.FirstName != FirstName) || (ContactViewPage.LastName != LastName)))
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

                // Section of because boolean fields are not visible in Contact View Page as of 6/6/16

//                foreach (var contactField in BooleanContactFields)
//                {
//                    if (contactField.Value == null && contactField.RecordViewPageFieldValue == "True") continue;
//                    if (contactField.Value == contactField.RecordViewPageFieldValue) continue;
//
//                    Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has value='{contactField.RecordViewPageFieldValue}' but was expected to have value='{contactField.Value}'", null);
//                    notOk = true;
//                }

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
        /// Copies field values from a given object to the object that calls the method
        /// </summary>
        /// <param name="tmp">The object to be copied</param>
        internal void Clone(Contact tmp)
        {
            SetFieldValue("First Name", tmp.GetFieldValue("First Name"));
            SetFieldValue("Last Name", tmp.GetFieldValue("Last Name"));
            SetFieldValue("Middle Name", tmp.GetFieldValue("Middle Name"));
            SetFieldValue("Suffix", tmp.GetFieldValue("Suffix"));
            SetFieldValue("Organization Name", tmp.GetFieldValue("Organization Name"));
            SetFieldValue("Mobile Phone", tmp.GetFieldValue("Mobile Phone"));
            SetFieldValue("Email", tmp.GetFieldValue("Email"));
            SetFieldValue("Allow SMS", tmp.GetFieldValue("Allow SMS"));
            SetFieldValue("Allow Phones", tmp.GetFieldValue("Allow Phones"));
            SetFieldValue("Allow Emails", tmp.GetFieldValue("Allow Emails"));
            SetFieldValue("Favorite", tmp.GetFieldValue("Favorite"));

            SetFieldValue("Department", tmp.GetFieldValue("Department"));
            SetFieldValue("Work Phone", tmp.GetFieldValue("Work Phone"));
            SetFieldValue("Work Phone 2", tmp.GetFieldValue("Work Phone 2"));
            SetFieldValue("Mobile Phone 2", tmp.GetFieldValue("Mobile Phone 2"));
            SetFieldValue("Home Phone", tmp.GetFieldValue("Home Phone"));
            SetFieldValue("Home Phone 2", tmp.GetFieldValue("Home Phone 2"));
            SetFieldValue("Home Fax", tmp.GetFieldValue("Home Fax"));
            SetFieldValue("Work Fax", tmp.GetFieldValue("Work Fax"));
            SetFieldValue("Other Phone", tmp.GetFieldValue("Other Phone"));
            SetFieldValue("Personal Email", tmp.GetFieldValue("Personal Email"));
            SetFieldValue("Other Email", tmp.GetFieldValue("Other Email"));
            SetFieldValue("Work Street", tmp.GetFieldValue("Work Street"));
            SetFieldValue("Work City", tmp.GetFieldValue("Work City"));
            SetFieldValue("Work State", tmp.GetFieldValue("Work State"));
            SetFieldValue("Work Postal Code", tmp.GetFieldValue("Work Postal Code"));
            SetFieldValue("Work Country", tmp.GetFieldValue("Work Country"));
            SetFieldValue("Home Street", tmp.GetFieldValue("Home Street"));
            SetFieldValue("Home City", tmp.GetFieldValue("Home City"));
            SetFieldValue("Home State", tmp.GetFieldValue("Home State"));
            SetFieldValue("Home Postal Code", tmp.GetFieldValue("Home Postal Code"));
            SetFieldValue("Home Country", tmp.GetFieldValue("Home Country"));
            SetFieldValue("Other Street", tmp.GetFieldValue("Other Street"));
            SetFieldValue("Other City", tmp.GetFieldValue("Other City"));
            SetFieldValue("Other State", tmp.GetFieldValue("Other State"));
            SetFieldValue("Other Postal Code", tmp.GetFieldValue("Other Postal Code"));
            SetFieldValue("Other Country", tmp.GetFieldValue("Other Country"));
            SetFieldValue("Salutation", tmp.GetFieldValue("Salutation"));
            SetFieldValue("Nickname", tmp.GetFieldValue("Nickname"));
            SetFieldValue("Job Title", tmp.GetFieldValue("Job Title"));
            SetFieldValue("Website", tmp.GetFieldValue("Website"));
            SetFieldValue("Religion", tmp.GetFieldValue("Religion"));
            SetFieldValue("Birthdate", tmp.GetFieldValue("Birthdate"));
            SetFieldValue("Gender", tmp.GetFieldValue("Gender"));
            SetFieldValue("Comments", tmp.GetFieldValue("Comments"));
        }

        /// <summary>
        /// Initializes contact properties
        /// </summary>
        public Contact()
        {
            BasicContactFields = new List<RecordField>();
            ExtraContactFields = new List<RecordField>();
            BooleanContactFields = new List<RecordField>();

            BasicContactFields.Add(new RecordField("First Name", null, () => ContactViewPage.FirstName, null));
            BasicContactFields.Add(new RecordField("Last Name", null, () => ContactViewPage.LastName, null));            
            BasicContactFields.Add(new RecordField("Organization Name", null, () => ContactViewPage.OrganizationName, null));
            BasicContactFields.Add(new RecordField("Mobile Phone", null, () => ContactViewPage.MobilePhone, null));
            BasicContactFields.Add(new RecordField("Email", null, () => ContactViewPage.WorkEmail, null));
            BasicContactFields.Add(new RecordField("Job Title", null, () => ContactViewPage.JobTitle, null));
            BasicContactFields.Add(new RecordField("Department", null, () => ContactViewPage.Department, null));
            BasicContactFields.Add(new RecordField("Work Phone", null, () => ContactViewPage.WorkPhone, null));
            BasicContactFields.Add(new RecordField("Favorite", false.ToString(), () => ContactViewPage.Favorite, null));

            ExtraContactFields.Add(new RecordField("Middle Name", null, () => ContactViewPage.MiddleName, () => ContactViewPage.IsMiddleNameFieldVisible));
            ExtraContactFields.Add(new RecordField("Suffix", null, () => ContactViewPage.Suffix, () => ContactViewPage.IsSuffixFieldVisible));
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

    internal class ContactFields
    {
        internal const string FirstName = "First Name";
        internal const string LastName = "Last Name";
        internal const string MiddleName = "Middle Name";
        internal const string Suffix = "Suffix";
        internal const string OrganizationName = "Organization Name";
        internal const string MobilePhone = "Mobile Phone";
        internal const string WorkEmail = "Email";
        internal const string AllowSms = "Allow SMS";
        internal const string AllowPhones = "Allow Phones";
        internal const string AllowEmails = "Allow Emails";
        internal const string Favorite = "Favorite";

        internal const string Department = "Department";
        internal const string WorkPhone = "Work Phone";
        internal const string WorkPhone2 = "Work Phone 2";
        internal const string MobilePhone2 = "Mobile Phone 2";
        internal const string HomePhone = "Home Phone";
        internal const string HomePhone2 = "Home Phone 2";
        internal const string HomeFax = "Home Fax";
        internal const string WorkFax = "Work Fax";
        internal const string OtherPhone = "Other Phone";
        internal const string PersonalEmail = "Personal Email";
        internal const string OtherEmail = "Other Email";
        internal const string WorkStreet = "Work Street";
        internal const string WorkCity = "Work City";
        internal const string WorkState = "Work State";
        internal const string WorkPostalCode = "Work Postal Code";
        internal const string WorkCountry = "Work Country";
        internal const string HomeStreet = "Home Street";
        internal const string HomeCity = "Home City";
        internal const string HomeState = "Home State";
        internal const string HomePostalCode = "Home Postal Code";
        internal const string HomeCountry = "Home Country";
        internal const string OtherStreet = "Other Street";
        internal const string OtherCity = "Other City";
        internal const string OtherState = "Other State";
        internal const string OtherPostalCode = "Other Postal Code";
        internal const string OtherCountry = "Other Country";
        internal const string Salutation = "Salutation";
        internal const string Nickname = "Nickname";
        internal const string JobTitle = "Job Title";
        internal const string Website = "Website";
        internal const string Religion = "Religion";
        internal const string Birthdate = "Birthdate";
        internal const string Gender = "Gender";
        internal const string Comments = "Comments";
    }
}
