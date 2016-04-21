using System;
using System.Collections.Generic;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Contacts
{
    public class EditContactPage
    {
        /// <summary>
        ///  Check if browser is at contact's edit page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Contacts  /  Contact  /  Edit Contact");

        /// <summary>
        /// Returns true if a contact was saved successfully after editing 
        /// </summary>
        public static bool IsContactSavedSuccessfully { get; set; }

        /// <summary>
        ///  Navigates browser through the available edit button to the contact's edit page
        /// </summary>
        public static void GoTo() { Commands.ClickEdit(); }

        /// <summary>
        /// Issue an command that instructs browser to change the value for some or every single field within edit contact page
        /// </summary>
        /// <returns></returns>
        public static EditContactCommand EditContact()
        {
            GoTo();
            return new EditContactCommand();
        }
    }

    public class EditContactCommand
    {
        private string firstName;
        private string lastName;
        private string middleName;
        private string suffix;
        private string organizationName;
        private string department;

        private string workPhone;
        private string workPhone2;
        private string mobilePhone;
        private string mobilePhone2;
        private string homePhone;
        private string homePhone2;
        private string homeFax;
        private string workFax;
        private string otherPhone;

        private string email;
        private string personalEmail;
        private string otherEmail;

        private string workStreet;
        private string workCity;
        private string workState;
        private string workPostalCode;
        private string workCountry;

        private string homeStreet;
        private string homeCity;
        private string homeState;
        private string homePostalCode;
        private string homeCountry;

        private string otherStreet;
        private string otherCity;
        private string otherState;
        private string otherPostalCode;
        private string otherCountry;

        private string salutation;
        private string nickname;
        private string jobTitle;
        private string website;
        private string religion;
        private string birthdate;
        private string gender;
        private string comments;
        private string allowSms;
        private string allowPhones;
        private string allowEmails;

        /// <summary>
        /// Sets the first name for the new contact
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public EditContactCommand WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        /// <summary>
        /// Sets the last name for the new contact
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public EditContactCommand WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        /// <summary>
        /// Sets the organization for the new contact
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public EditContactCommand WithOrganizationName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        /// <summary>
        /// Sets the home phone for the new contact
        /// </summary>
        /// <param name="homePhone"></param>
        /// <returns></returns>
        public EditContactCommand WithHomePhone(string homePhone)
        {
            this.homePhone = homePhone;
            return this;
        }

        /// <summary>
        /// Sets the work phone for the new contact
        /// </summary>
        /// <param name="workPhone"></param>
        /// <returns></returns>
        public EditContactCommand WithWorkPhone(string workPhone)
        {
            this.workPhone = workPhone;
            return this;
        }

        /// <summary>
        /// Sets the personal email for the new contact
        /// </summary>
        /// <param name="personalEmail"></param>
        /// <returns></returns>
        public EditContactCommand WithPersonalEmail(string personalEmail)
        {
            this.personalEmail = personalEmail;
            return this;
        }

        /// <summary>
        /// Sets the work city for the new contact
        /// </summary>
        /// <param name="workCity"></param>
        /// <returns></returns>
        public EditContactCommand WithWorkCity(string workCity)
        {
            this.workCity = workCity;
            return this;
        }

        /// <summary>
        /// Sets the nickname for the new contact
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public EditContactCommand WithNickname(string nickname)
        {
            this.nickname = nickname;
            return this;
        }

        /// <summary>
        /// Sets dummy values for every field of the new contact
        /// </summary>
        /// <returns></returns>
        internal EditContactCommand WithMultipleValues(List<Workflows.RecordField> basicContactFields, List<Workflows.RecordField> extraContactFields)
        {
            firstName = basicContactFields.Find(x => x.Label.Contains("First Name")).Value;
            lastName = basicContactFields.Find(x => x.Label.Contains("Last Name")).Value;
            middleName = basicContactFields.Find(x => x.Label.Contains("Middle Name")).Value;
            suffix = basicContactFields.Find(x => x.Label.Contains("Suffix")).Value;
            organizationName = basicContactFields.Find(x => x.Label.Contains("Organization Name")).Value;
            mobilePhone = basicContactFields.Find(x => x.Label.Contains("Mobile Phone")).Value;
            email = basicContactFields.Find(x => x.Label.Contains("Email")).Value;
            allowSms = basicContactFields.Find(x => x.Label.Contains("Allow SMS")).Value;
            allowPhones = basicContactFields.Find(x => x.Label.Contains("Allow Phones")).Value;
            allowEmails = basicContactFields.Find(x => x.Label.Contains("Allow Emails")).Value;

            department = extraContactFields.Find(x => x.Label.Contains("Department")).Value;
            workPhone = extraContactFields.Find(x => x.Label.Contains("Work Phone")).Value;
            workPhone2 = extraContactFields.Find(x => x.Label.Contains("Work Phone 2")).Value;
            mobilePhone2 = extraContactFields.Find(x => x.Label.Contains("Mobile Phone 2")).Value;
            homePhone = extraContactFields.Find(x => x.Label.Contains("Home Phone")).Value;
            homePhone2 = extraContactFields.Find(x => x.Label.Contains("Home Phone 2")).Value;
            homeFax = extraContactFields.Find(x => x.Label.Contains("Home Fax")).Value;
            workFax = extraContactFields.Find(x => x.Label.Contains("Work Fax")).Value;
            otherPhone = extraContactFields.Find(x => x.Label.Contains("Other Phone")).Value;
            personalEmail = extraContactFields.Find(x => x.Label.Contains("Personal Email")).Value;
            otherEmail = extraContactFields.Find(x => x.Label.Contains("Other Email")).Value;
            workStreet = extraContactFields.Find(x => x.Label.Contains("Work Street")).Value;
            workCity = extraContactFields.Find(x => x.Label.Contains("Work City")).Value;
            workState = extraContactFields.Find(x => x.Label.Contains("Work State")).Value;
            workPostalCode = extraContactFields.Find(x => x.Label.Contains("Work Postal Code")).Value;
            workCountry = extraContactFields.Find(x => x.Label.Contains("Work Country")).Value;
            homeStreet = extraContactFields.Find(x => x.Label.Contains("Home Street")).Value;
            homeCity = extraContactFields.Find(x => x.Label.Contains("Home City")).Value;
            homeState = extraContactFields.Find(x => x.Label.Contains("Home State")).Value;
            homePostalCode = extraContactFields.Find(x => x.Label.Contains("Home Postal Code")).Value;
            homeCountry = extraContactFields.Find(x => x.Label.Contains("Home Country")).Value;
            otherStreet = extraContactFields.Find(x => x.Label.Contains("Other Street")).Value;
            otherCity = extraContactFields.Find(x => x.Label.Contains("Other City")).Value;
            otherState = extraContactFields.Find(x => x.Label.Contains("Other State")).Value;
            otherPostalCode = extraContactFields.Find(x => x.Label.Contains("Other Postal Code")).Value;
            otherCountry = extraContactFields.Find(x => x.Label.Contains("Other Country")).Value;
            salutation = extraContactFields.Find(x => x.Label.Contains("Salutation")).Value;
            nickname = extraContactFields.Find(x => x.Label.Contains("Nickname")).Value;
            jobTitle = extraContactFields.Find(x => x.Label.Contains("Job Title")).Value;
            website = extraContactFields.Find(x => x.Label.Contains("Website")).Value;
            religion = extraContactFields.Find(x => x.Label.Contains("Religion")).Value;
            birthdate = extraContactFields.Find(x => x.Label.Contains("Birthdate")).Value;
            gender = extraContactFields.Find(x => x.Label.Contains("Gender")).Value;
            comments = extraContactFields.Find(x => x.Label.Contains("Comments")).Value;

            return this;
        }

        /// <summary>
        /// Creates the new contact with given contact field values
        /// </summary>
        public void Edit()
        {

            if (firstName != null) EditContactFields.FirstName = firstName;
            if (lastName != null) EditContactFields.LastName = lastName;
            if (middleName != null) EditContactFields.MiddleName = middleName;
            if (suffix != null) EditContactFields.Suffix = suffix;
            if (organizationName != null) EditContactFields.OrganizationName = organizationName;
            if (department != null) EditContactFields.Department = department;
            if (workPhone != null) EditContactFields.WorkPhone = workPhone;
            if (workPhone2 != null) EditContactFields.WorkPhone2 = workPhone2;
            if (mobilePhone != null) EditContactFields.MobilePhone = mobilePhone;
            if (mobilePhone2 != null) EditContactFields.MobilePhone2 = mobilePhone2;
            if (homePhone != null) EditContactFields.HomePhone = homePhone;
            if (homePhone2 != null) EditContactFields.HomePhone2 = homePhone2;
            if (workFax != null) EditContactFields.WorkFax = workFax;
            if (homeFax != null) EditContactFields.HomeFax = homeFax;
            if (otherPhone != null) EditContactFields.OtherPhone = otherPhone;
            if (email != null) EditContactFields.Email = email;
            if (personalEmail != null) EditContactFields.PersonalEmail = personalEmail;
            if (otherEmail != null) EditContactFields.OtherEmail = otherEmail;

            if (workStreet != null) EditContactFields.WorkStreet = workStreet;
            if (workCity != null) EditContactFields.WorkCity = workCity;
            if (workState != null) EditContactFields.WorkState = workState;
            if (workPostalCode != null) EditContactFields.WorkPostalCode = workPostalCode;
            if (workCountry != null) EditContactFields.WorkCountry = workCountry;

            if (homeStreet != null) EditContactFields.HomeStreet = homeStreet;
            if (homeCity != null) EditContactFields.HomeCity = homeCity;
            if (homeState != null) EditContactFields.HomeState = homeState;
            if (homePostalCode != null) EditContactFields.HomePostalCode = homePostalCode;
            if (homeCountry != null) EditContactFields.HomeCountry = homeCountry;

            if (otherStreet != null) EditContactFields.OtherStreet = otherStreet;
            if (otherCity != null) EditContactFields.OtherCity = otherCity;
            if (otherState != null) EditContactFields.OtherState = otherState;
            if (otherPostalCode != null) EditContactFields.OtherPostalCode = otherPostalCode;
            if (otherCountry != null) EditContactFields.OtherCountry = otherCountry;

            if (salutation != null) EditContactFields.Salutation = salutation;
            if (nickname != null) EditContactFields.Nickname = nickname;
            if (jobTitle != null) EditContactFields.JobTitle = jobTitle;
            if (website != null) EditContactFields.Website = website;
            if (religion != null) EditContactFields.Religion = religion;
            if (birthdate != null) EditContactFields.Birthdate = birthdate;
            if (gender != null) EditContactFields.Gender = gender;
            if (comments != null) EditContactFields.Comments = comments;

            if (allowSms != null) EditContactFields.AllowSMS = allowSms;
            if (allowPhones != null) EditContactFields.AllowPhones = allowPhones;
            if (allowEmails != null) EditContactFields.AllowEmails = allowEmails;

            Driver.Wait(TimeSpan.FromSeconds(5));

            EditContactPage.IsContactSavedSuccessfully = Commands.ClickSave();
        }
    }
}
