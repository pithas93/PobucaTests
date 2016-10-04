using System;
using System.Collections.Generic;
using JPB_Framework.Navigation;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Contacts
{
    public class NewContactPage
    {
        /// <summary>
        /// Check if browser is at contact form page that allows to create a new contact
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Contacts  /  Add Contact");

        /// <summary>
        ///  Check if browser is at contact form page that allows to create a new contact from within an organization view page
        /// </summary>
        public static bool IsAtFromWithinOrganizationViewPage => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization  /  Add Contact");

        /// <summary>
        /// Returns whether the new contact Save button was pressed, and so the contact was saved, or not.
        /// </summary>
        public static bool IsContactSavedSuccessfully { get; set; }

        /// <summary>
        /// Returns true if the organization name field is editable
        /// </summary>
        public static bool IsOrganizationNameEditable
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-auto-complete[myname='Organization'] div input"));
                var tmp = element.GetAttribute("disabled");
                return (tmp == null);
            }

        }

        /// <summary>
        /// Returns true if department combo field values are ordered alphabetically
        /// </summary>
        public static bool IsDepartmentComboListSorted
        {
            get
            {
                var departmentList =
                    Driver.Instance.FindElements(
                        By.CssSelector("my-select[myname='Department'] div select option.ng-binding.ng-scope"));

                return Commands.CheckIfListIsSorted(departmentList);
            }
        }

        /// <summary>
        /// Returns true if both work, home and other country combo field values are ordered alphabetically
        /// </summary>
        public static bool AreCountryComboListsSorted
        {
            get
            {
                SetWorkCountry("");
                var workCountryIsSorted = Commands.CheckIfListIsSorted(
                    Driver.Instance.FindElements(By.CssSelector("#workAddress my-select[myname='Country'] div select option.ng-binding.ng-scope"))
                    );

                SetHomeCountry("");
                var homeCountryIsSorted = Commands.CheckIfListIsSorted(
                    Driver.Instance.FindElements(By.CssSelector("#homeAddress my-select[myname='Country'] div select option.ng-binding.ng-scope"))
                    );

                SetOtherCountry("");
                var otherCountryIsSorted = Commands.CheckIfListIsSorted(
                    Driver.Instance.FindElements(By.CssSelector("#otherAddress my-select[myname='Country'] div select option.ng-binding.ng-scope"))
                    );

                if (workCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Work country combo field values are not sorted correctly", null);
                if (homeCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Home country combo field values are not sorted correctly", null);
                if (otherCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Other country combo field values are not sorted correctly", null);

                return (workCountryIsSorted && homeCountryIsSorted && otherCountryIsSorted);
            }
        }

        /// <summary>
        /// Returns the length of string value from the comment field
        /// </summary>
        public static int CommentsTextLength
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("#textboxid"));
                string str = element.GetAttribute("value");
                return str.Length;
            }
        }

        /// <summary>
        /// Returns the value of comments remaining characters indicator
        /// </summary>
        public static int CommentsLimitIndicator
        {
            get
            {
                var indicatorText = Driver.Instance.FindElement(By.CssSelector("span[ng-show='contact.comments.length && contact.comments.length <= 500']")).Text;
                return int.Parse(indicatorText.Split(' ')[2]);
            }
        }

        /// <summary>
        /// Returns true if the message that alerts user of possible duplicate contact is being shown
        /// </summary>
        public static bool IsPossibleDuplicateAlertShown {
            get
            {

                try
                {
                    Driver.WaitForElementToBeVisible(TimeSpan.FromSeconds(5),
                        "[ng-if='searchingExistingContacts && showDuplicateContactCheck']");
                    Driver.Instance.FindElement(
                        By.CssSelector("[ng-if='searchingExistingContacts && showDuplicateContactCheck']"));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
            }
        }



        /// <summary>
        /// Navigates browser, through the available button, to a contact form page that allows to create a new contact
        /// </summary>
        public static void GoTo()
        {
            var newContactBtn = Driver.Instance.FindElement(By.CssSelector("i#newContactButton"));
            newContactBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Issue a create new contact command with given first name
        /// </summary>
        /// <returns> A command upon which the parameters for the new contact are specified</returns>
        public static CreateContactCommand CreateContact()
        {
            if (!ContactsPage.IsAt) LeftSideMenu.GoToContacts();
            GoTo();
            return new CreateContactCommand();
        }

        public static void SetComments(string v) => EditContactFields.Comments = v;
        public static void SetFirstName(string v) => EditContactFields.FirstName = v;
        public static void SetLastName(string v) => EditContactFields.LastName = v;
        public static void SetWorkCountry(string v) => EditContactFields.WorkCountry = v;
        public static void SetHomeCountry(string v) => EditContactFields.HomeCountry = v;
        public static void SetOtherCountry(string v) => EditContactFields.OtherCountry = v;

    }

    public class CreateContactCommand
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
        public CreateContactCommand WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        /// <summary>
        /// Sets the last name for the new contact
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public CreateContactCommand WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        /// <summary>
        /// Sets the organization for the new contact
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public CreateContactCommand WithOrganizationName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        /// <summary>
        /// Sets the home phone for the new contact
        /// </summary>
        /// <param name="homePhone"></param>
        /// <returns></returns>
        public CreateContactCommand WithHomePhone(string homePhone)
        {
            this.homePhone = homePhone;
            return this;
        }

        /// <summary>
        /// Sets the work phone for the new contact
        /// </summary>
        /// <param name="workPhone"></param>
        /// <returns></returns>
        public CreateContactCommand WithMobilePhone(string mobilePhone)
        {
            this.mobilePhone = mobilePhone;
            return this;
        }

        /// <summary>
        /// Sets the personal email for the new contact
        /// </summary>
        /// <param name="personalEmail"></param>
        /// <returns></returns>
        public CreateContactCommand WithPersonalEmail(string personalEmail)
        {
            this.personalEmail = personalEmail;
            return this;
        }

        /// <summary>
        /// Sets the work city for the new contact
        /// </summary>
        /// <param name="workCity"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkCity(string workCity)
        {
            this.workCity = workCity;
            return this;
        }

        /// <summary>
        /// Sets the nickname for the new contact
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public CreateContactCommand WithNickname(string nickname)
        {
            this.nickname = nickname;
            return this;
        }

        /// <summary>
        /// Sets the birthdate for the new contact
        /// </summary>
        /// <param name="birthdate"></param>
        /// <returns></returns>
        public CreateContactCommand WithBirthdate(string birthdate)
        {
            this.birthdate = birthdate;
            return this;
        }

        /// <summary>
        /// Sets the workStreet for the new contact
        /// </summary>
        /// <param name="workStreet"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkStreet(string workStreet)
        {
            this.workStreet = workStreet;
            return this;
        }

        /// <summary>
        /// Sets the workState for the new contact
        /// </summary>
        /// <param name="workState"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkState(string workState)
        {
            this.workState = workState;
            return this;
        }

        /// <summary>
        /// Sets the workPostalCode for the new contact
        /// </summary>
        /// <param name="workPostalCode"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkPostalCode(string workPostalCode)
        {
            this.workPostalCode = workPostalCode;
            return this;
        }

        /// <summary>
        /// Sets the workCountry for the new contact
        /// </summary>
        /// <param name="workCountry"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkCountry(string workCountry)
        {
            this.workCountry = workCountry;
            return this;
        }

        /// <summary>
        /// Sets the website for the new contact
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public CreateContactCommand WithWebsite(string website)
        {
            this.website = website;
            return this;
        }

        /// <summary>
        /// Sets dummy values for every field of the new contact
        /// </summary>
        /// <returns></returns>
        internal CreateContactCommand WithMultipleValues(List<Workflows.RecordField> basicContactFields, List<Workflows.RecordField> extraContactFields, List<Workflows.RecordField> booleanContactFields)
        {
            firstName = basicContactFields.Find(x => x.Label.Contains("First Name")).Value;
            lastName = basicContactFields.Find(x => x.Label.Contains("Last Name")).Value;
            department = basicContactFields.Find(x => x.Label.Contains("Department")).Value;
            workPhone = basicContactFields.Find(x => x.Label.Contains("Work Phone")).Value;
            organizationName = basicContactFields.Find(x => x.Label.Contains("Organization Name")).Value;
            mobilePhone = basicContactFields.Find(x => x.Label.Contains("Mobile Phone")).Value;
            email = basicContactFields.Find(x => x.Label.Contains("Email")).Value;
            jobTitle = basicContactFields.Find(x => x.Label.Contains("Job Title")).Value;


            middleName = extraContactFields.Find(x => x.Label.Contains("Middle Name")).Value;
            suffix = extraContactFields.Find(x => x.Label.Contains("Suffix")).Value;
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
            website = extraContactFields.Find(x => x.Label.Contains("Website")).Value;
            religion = extraContactFields.Find(x => x.Label.Contains("Religion")).Value;
            birthdate = extraContactFields.Find(x => x.Label.Contains("Birthdate")).Value;
            gender = extraContactFields.Find(x => x.Label.Contains("Gender")).Value;
            comments = extraContactFields.Find(x => x.Label.Contains("Comments")).Value;

            allowSms = booleanContactFields.Find(x => x.Label.Contains("Allow SMS")).Value;
            allowPhones = booleanContactFields.Find(x => x.Label.Contains("Allow Phones")).Value;
            allowEmails = booleanContactFields.Find(x => x.Label.Contains("Allow Emails")).Value;

            return this;
        }

        /// <summary>
        /// Creates the new contact with given contact field values
        /// </summary>
        public void Create()
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

            NewContactPage.IsContactSavedSuccessfully = Commands.ClickSave();
        }



    }



}
