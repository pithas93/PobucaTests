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
                    {
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {contactField.Label} has no value but its field is shown in contact's detail view page with value '{contactField.RecordViewPageFieldValue}'", null);
                        notOk = true;
                    }


                }

                // Section off because boolean fields are not visible in Contact View Page as of 6/6/16

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
            SetFieldValue(ContactFields.FirstName, tmp.GetFieldValue(ContactFields.FirstName));
            SetFieldValue(ContactFields.LastName, tmp.GetFieldValue(ContactFields.LastName));
            SetFieldValue(ContactFields.MiddleName, tmp.GetFieldValue(ContactFields.MiddleName));
            SetFieldValue(ContactFields.Suffix, tmp.GetFieldValue(ContactFields.Suffix));
            SetFieldValue(ContactFields.OrganizationName, tmp.GetFieldValue(ContactFields.OrganizationName));
            SetFieldValue(ContactFields.MobilePhone, tmp.GetFieldValue(ContactFields.MobilePhone));
            SetFieldValue(ContactFields.WorkEmail, tmp.GetFieldValue(ContactFields.WorkEmail));
            SetFieldValue(ContactFields.AllowSms, tmp.GetFieldValue(ContactFields.AllowSms));
            SetFieldValue(ContactFields.AllowPhones, tmp.GetFieldValue(ContactFields.AllowPhones));
            SetFieldValue(ContactFields.AllowEmails, tmp.GetFieldValue(ContactFields.AllowEmails));
            SetFieldValue(ContactFields.Favorite, tmp.GetFieldValue(ContactFields.Favorite));

            SetFieldValue(ContactFields.Department, tmp.GetFieldValue(ContactFields.Department));
            SetFieldValue(ContactFields.WorkPhone, tmp.GetFieldValue(ContactFields.WorkPhone));
            SetFieldValue(ContactFields.WorkPhone2, tmp.GetFieldValue(ContactFields.WorkPhone2));
            SetFieldValue(ContactFields.MobilePhone2, tmp.GetFieldValue(ContactFields.MobilePhone2));
            SetFieldValue(ContactFields.HomePhone, tmp.GetFieldValue(ContactFields.HomePhone));
            SetFieldValue(ContactFields.HomePhone2, tmp.GetFieldValue(ContactFields.HomePhone2));
            SetFieldValue(ContactFields.HomeFax, tmp.GetFieldValue(ContactFields.HomeFax));
            SetFieldValue(ContactFields.WorkFax, tmp.GetFieldValue(ContactFields.WorkFax));
            SetFieldValue(ContactFields.OtherPhone, tmp.GetFieldValue(ContactFields.OtherPhone));
            SetFieldValue(ContactFields.PersonalEmail, tmp.GetFieldValue(ContactFields.PersonalEmail));
            SetFieldValue(ContactFields.OtherEmail, tmp.GetFieldValue(ContactFields.OtherEmail));
            SetFieldValue(ContactFields.WorkStreet, tmp.GetFieldValue(ContactFields.WorkStreet));
            SetFieldValue(ContactFields.WorkCity, tmp.GetFieldValue(ContactFields.WorkCity));
            SetFieldValue(ContactFields.WorkState, tmp.GetFieldValue(ContactFields.WorkState));
            SetFieldValue(ContactFields.WorkPostalCode, tmp.GetFieldValue(ContactFields.WorkPostalCode));
            SetFieldValue(ContactFields.WorkCountry, tmp.GetFieldValue(ContactFields.WorkCountry));
            SetFieldValue(ContactFields.HomeStreet, tmp.GetFieldValue(ContactFields.HomeStreet));
            SetFieldValue(ContactFields.HomeCity, tmp.GetFieldValue(ContactFields.HomeCity));
            SetFieldValue(ContactFields.HomeState, tmp.GetFieldValue(ContactFields.HomeState));
            SetFieldValue(ContactFields.HomePostalCode, tmp.GetFieldValue(ContactFields.HomePostalCode));
            SetFieldValue(ContactFields.HomeCountry, tmp.GetFieldValue(ContactFields.HomeCountry));
            SetFieldValue(ContactFields.OtherStreet, tmp.GetFieldValue(ContactFields.OtherStreet));
            SetFieldValue(ContactFields.OtherCity, tmp.GetFieldValue(ContactFields.OtherCity));
            SetFieldValue(ContactFields.OtherState, tmp.GetFieldValue(ContactFields.OtherState));
            SetFieldValue(ContactFields.OtherPostalCode, tmp.GetFieldValue(ContactFields.OtherPostalCode));
            SetFieldValue(ContactFields.OtherCountry, tmp.GetFieldValue(ContactFields.OtherCountry));
            SetFieldValue(ContactFields.Salutation, tmp.GetFieldValue(ContactFields.Salutation));
            SetFieldValue(ContactFields.Nickname, tmp.GetFieldValue(ContactFields.Nickname));
            SetFieldValue(ContactFields.JobTitle, tmp.GetFieldValue(ContactFields.JobTitle));
            SetFieldValue(ContactFields.Website, tmp.GetFieldValue(ContactFields.Website));
            SetFieldValue(ContactFields.Religion, tmp.GetFieldValue(ContactFields.Religion));
            SetFieldValue(ContactFields.Birthdate, tmp.GetFieldValue(ContactFields.Birthdate));
            SetFieldValue(ContactFields.Gender, tmp.GetFieldValue(ContactFields.Gender));
            SetFieldValue(ContactFields.Comments, tmp.GetFieldValue(ContactFields.Comments));
        }

        /// <summary>
        /// Initializes contact properties
        /// </summary>
        public Contact()
        {
            BasicContactFields = new List<RecordField>();
            ExtraContactFields = new List<RecordField>();
            BooleanContactFields = new List<RecordField>();

            BasicContactFields.Add(new RecordField(ContactFields.FirstName, null, () => ContactViewPage.FirstName, null));
            BasicContactFields.Add(new RecordField(ContactFields.LastName, null, () => ContactViewPage.LastName, null));           
            BasicContactFields.Add(new RecordField(ContactFields.JobTitle, null, () => ContactViewPage.JobTitle, null));
            BasicContactFields.Add(new RecordField(ContactFields.OrganizationName, null, () => ContactViewPage.OrganizationName, null));                      
            BasicContactFields.Add(new RecordField(ContactFields.Department, null, () => ContactViewPage.Department, null));            
            BasicContactFields.Add(new RecordField(ContactFields.Favorite, false.ToString(), () => ContactViewPage.Favorite, null));
            BasicContactFields.Add(new RecordField(ContactFields.WorkPhone, null, () => ContactViewPage.WorkPhone, null));
            BasicContactFields.Add(new RecordField(ContactFields.MobilePhone, null, () => ContactViewPage.MobilePhone, null));
            BasicContactFields.Add(new RecordField(ContactFields.WorkEmail, null, () => ContactViewPage.WorkEmail, null));


            ExtraContactFields.Add(new RecordField(ContactFields.MiddleName, null, () => ContactViewPage.MiddleName, () => ContactViewPage.IsMiddleNameFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkPhone2, null, () => ContactViewPage.WorkPhone2, () => ContactViewPage.IsWorkPhone2FieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.MobilePhone2, null, () => ContactViewPage.MobilePhone2, () => ContactViewPage.IsMobilePhone2FieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomePhone, null, () => ContactViewPage.HomePhone, () => ContactViewPage.IsHomePhoneFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomePhone2, null, () => ContactViewPage.HomePhone2, () => ContactViewPage.IsHomePhone2FieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomeFax, null, () => ContactViewPage.HomeFax, () => ContactViewPage.IsHomeFaxFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkFax, null, () => ContactViewPage.WorkFax, () => ContactViewPage.IsWorkFaxFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherPhone, null, () => ContactViewPage.OtherPhone, () => ContactViewPage.IsOtherPhoneFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.PersonalEmail, null, () => ContactViewPage.PersonalEmail, () => ContactViewPage.IsPersonalEmailFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherEmail, null, () => ContactViewPage.OtherEmail, () => ContactViewPage.IsOtherEmailFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkStreet, null, () => ContactViewPage.WorkStreet, () => ContactViewPage.IsWorkStreetFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkCity, null, () => ContactViewPage.WorkCity, () => ContactViewPage.IsWorkCityFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkState, null, () => ContactViewPage.WorkState, () => ContactViewPage.IsWorkStateFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkPostalCode, null, () => ContactViewPage.WorkPostalCode, () => ContactViewPage.IsWorkPostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.WorkCountry, null, () => ContactViewPage.WorkCountry, () => ContactViewPage.IsWorkCountryFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomeStreet, null, () => ContactViewPage.HomeStreet, () => ContactViewPage.IsHomeStreetFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomeCity, null, () => ContactViewPage.HomeCity, () => ContactViewPage.IsHomeCityFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomeState, null, () => ContactViewPage.HomeState, () => ContactViewPage.IsHomeStateFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomePostalCode, null, () => ContactViewPage.HomePostalCode, () => ContactViewPage.IsHomePostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.HomeCountry, null, () => ContactViewPage.HomeCountry, () => ContactViewPage.IsHomeCountryFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherStreet, null, () => ContactViewPage.OtherStreet, () => ContactViewPage.IsOtherStreetFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherCity, null, () => ContactViewPage.OtherCity, () => ContactViewPage.IsOtherCityFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherState, null, () => ContactViewPage.OtherState, () => ContactViewPage.IsOtherStateFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherPostalCode, null, () => ContactViewPage.OtherPostalCode, () => ContactViewPage.IsOtherPostalCodeFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.OtherCountry, null, () => ContactViewPage.OtherCountry, () => ContactViewPage.IsOtherCountryFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Salutation, null, () => ContactViewPage.Salutation, () => ContactViewPage.IsSalutationFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Nickname, null, () => ContactViewPage.Nickname, () => ContactViewPage.IsNicknameFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Suffix, null, () => ContactViewPage.Suffix, () => ContactViewPage.IsSuffixFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Website, null, () => ContactViewPage.Website, () => ContactViewPage.IsWebsiteFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Religion, null, () => ContactViewPage.Religion, () => ContactViewPage.IsReligionFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Birthdate, null, () => ContactViewPage.Birthdate, () => ContactViewPage.IsBirthdateFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Gender, null, () => ContactViewPage.Gender, () => ContactViewPage.IsGenderFieldVisible));
            ExtraContactFields.Add(new RecordField(ContactFields.Comments, null, () => ContactViewPage.Comments, () => ContactViewPage.IsCommentsFieldVisible));

            BooleanContactFields.Add(new RecordField(ContactFields.AllowSms, null, () => ContactViewPage.AllowSms, null));
            BooleanContactFields.Add(new RecordField(ContactFields.AllowPhones, null, () => ContactViewPage.AllowPhones, null));
            BooleanContactFields.Add(new RecordField(ContactFields.AllowEmails, null, () => ContactViewPage.AllowEmails, null));

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
                    var firstName = GetFieldValue(ContactFields.FirstName);
                    var lastName = GetFieldValue(ContactFields.LastName);
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
            {
                Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
                Report.Report.AbruptFinalize();
                throw new Exception();
            }

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
            Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
            Report.Report.AbruptFinalize();
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
            {
                Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
                Report.Report.AbruptFinalize();
                throw new Exception();
            }
                

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
            Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
            Report.Report.AbruptFinalize();
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
