using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace JPB_Framework
{
    public class NewContactPage
    {
        /// <summary>
        /// Check if browser is at contact form page that allows to create a new contact
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Add Contact"); } }

        public static string FirstName { set { Driver.Instance.FindElement(By.Id("First Name")).SendKeys(value); } }
        public static string LastName { set { Driver.Instance.FindElement(By.Id("Last Name")).SendKeys(value); } }
        public static string MiddleName { set { Driver.Instance.FindElement(By.Id("Middle Name")).SendKeys(value); } }
        public static string Suffix { set { Driver.Instance.FindElement(By.Id("Suffix")).SendKeys(value); } }
        public static string OrganizationName
        {
            set
            {
                Driver.Instance.FindElement(By.CssSelector("my-auto-complete[myname='Organization'] div input")).SendKeys(value);
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
        }

        public static string Department
        {
            set
            {
                var departmentList =
                    Driver.Instance.FindElements(
                        By.CssSelector("my-select[myname='Department'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(departmentList, value);
            }
        }

        public static string WorkPhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkPhone);
                Driver.Instance.FindElement(By.Id("workphone")).SendKeys(value);
            }
        }

        public static string WorkPhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkPhone2);
                Driver.Instance.FindElement(By.Id("workphone2")).SendKeys(value);
            }
        }

        public static string MobilePhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.MobilePhone);
                Driver.Instance.FindElement(By.Id("mobilephone")).SendKeys(value);
            }
        }

        public static string MobilePhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.MobilePhone2);
                Driver.Instance.FindElement(By.Id("mobilephone2")).SendKeys(value);
            }
        }

        public static string HomePhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomePhone);
                Driver.Instance.FindElement(By.Id("homephone")).SendKeys(value);
            }
        }

        public static string HomePhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomePhone2);
                Driver.Instance.FindElement(By.Id("homephone2")).SendKeys(value);
            }
        }

        public static string HomeFax
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomeFax);
                Driver.Instance.FindElement(By.Id("homefax")).SendKeys(value);
            }
        }

        public static string WorkFax
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkFax);
                Driver.Instance.FindElement(By.Id("workfax")).SendKeys(value);
            }
        }

        public static string OtherPhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.OtherPhone);
                Driver.Instance.FindElement(By.Id("otherphone")).SendKeys(value);
            }
        }

        public static string Email
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.Email);
                Driver.Instance.FindElement(By.Id("email")).SendKeys(value);
            }
        }

        public static string PersonalEmail
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.PersonalEmail);
                Driver.Instance.FindElement(By.Id("personalemail")).SendKeys(value);
            }
        }

        public static string OtherEmail
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.OtherEmail);
                Driver.Instance.FindElement(By.Id("otheremail")).SendKeys(value);
            }
        }

        public static string WorkStreet
        {
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Work);
                }
                workAddrElements.FindElement(By.CssSelector("input#Street")).SendKeys(value);
            }
        }

        public static string WorkCity
        {
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Work);
                }
                workAddrElements.FindElement(By.CssSelector("input#City")).SendKeys(value);
            }
        }

        public static string WorkState
        {
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Work);
                }
                workAddrElements.FindElement(By.CssSelector("input[id='State / Province']")).SendKeys(value);
            }
        }

        public static string WorkPostalCode
        {
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Work);
                }
                workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']")).SendKeys(value);
            }
        }

        public static string WorkCountry
        {
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Work);
                }

                var countryList = workAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(countryList, value);
            }
        }

        public static string HomeStreet
        {
            set
            {
                var homeAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!homeAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Home);
                }
                homeAddrElements.FindElement(By.CssSelector("input#Street")).SendKeys(value);
            }
        }

        public static string HomeCity
        {
            set
            {
                var homeAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!homeAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Home);
                }
                homeAddrElements.FindElement(By.CssSelector("input#City")).SendKeys(value);
            }
        }

        public static string HomeState
        {
            set
            {
                var homeAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!homeAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Home);
                }
                homeAddrElements.FindElement(By.CssSelector("input[id='State / Province']")).SendKeys(value);
            }
        }

        public static string HomePostalCode
        {
            set
            {
                var homeAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!homeAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Home);
                }
                homeAddrElements.FindElement(By.CssSelector("input[id='Postal Code']")).SendKeys(value);
            }
        }

        public static string HomeCountry
        {
            set
            {
                var homeAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!homeAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Home);
                }

                var countryList = homeAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(countryList, value);
            }
        }

        public static string OtherStreet
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Other);
                }
                otherAddrElements.FindElement(By.CssSelector("input#Street")).SendKeys(value);
            }
        }

        public static string OtherCity
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Other);
                }
                otherAddrElements.FindElement(By.CssSelector("input#City")).SendKeys(value);
            }
        }

        public static string OtherState
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Other);
                }
                otherAddrElements.FindElement(By.CssSelector("input[id='State / Province']")).SendKeys(value);
            }
        }

        public static string OtherPostalCode
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Other);
                }
                otherAddrElements.FindElement(By.CssSelector("input[id='Postal Code']")).SendKeys(value);
            }
        }

        public static string OtherCountry
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(ContactFieldCategories.Address);
                    InsertExtraField(ContactFieldCategories.Address, ContactFieldCategories.AddressFields.Other);
                }

                var countryList = otherAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(countryList, value);
            }
        }

        public static string Salutation
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Salutation);
                Driver.Instance.FindElement(By.Id("titlesalutation")).SendKeys(value);
            }
        }

        public static string Nickname
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Nickname);
                Driver.Instance.FindElement(By.Id("nickname")).SendKeys(value);
            }
        }
        public static string JobTitle
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.JobTitle);
                Driver.Instance.FindElement(By.Id("jobtitle")).SendKeys(value);
            }
        }
        public static string Website
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Website);
                Driver.Instance.FindElement(By.Id("website")).SendKeys(value);
            }
        }
        public static string Religion
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Religion);
                Driver.Instance.FindElement(By.Id("religion")).SendKeys(value);
            }
        }

        public static string Birthdate
        {
            set
            {



                Driver.Instance.FindElement(By.Id("birthday")).Click();




                DateTime birthdate = Convert.ToDateTime(value);

                Driver.Instance.FindElement(By.CssSelector("select.ui-datepicker-year")).Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                var yearList = Driver.Instance.FindElements(By.CssSelector("select.ui-datepicker-year option"));
                SelectFromListByName(yearList, birthdate.ToString("yyyy"));

                Driver.Instance.FindElement(By.CssSelector("select.ui-datepicker-month")).Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                var monthList = Driver.Instance.FindElements(By.CssSelector("select.ui-datepicker-month option"));
                SelectFromListByName(monthList, birthdate.ToString("MMM", new CultureInfo("en-US")));

                var dateList = Driver.Instance.FindElements(By.CssSelector("table.ui-datepicker-calendar a.ui-state-default"));
                SelectFromListByName(dateList, birthdate.Day.ToString());
            }
        }

        public static string Gender
        {
            set
            {
                var genderList = Driver.Instance.FindElements(By.CssSelector("my-select#gender-select option"));
                SelectFromListByName(genderList, value);
            }
        }

        public static string Comments
        {
            set
            {
                Driver.Instance.FindElement(By.Id("textboxid")).SendKeys(value);
            }
        }

        public static bool AllowSMS
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowSMS']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = false;
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = false;
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = true;

                if (value ^ isChecked) checkbox.Click();
            }
        }
        public static bool AllowPhones
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowPhones']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = false;
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = false;
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = true;

                if (value ^ isChecked) checkbox.Click();
            }
        }
        public static bool AllowEmails
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowEmails']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = false;
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = false;
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = true;

                if (value ^ isChecked) checkbox.Click();
            }
        }

        /// <summary>
        /// Navigates browser, through the available button, to a contact form page that allows to create a new contact
        /// </summary>
        public static void GoTo()
        {
            var newContactBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[5]/ul/li[2]/a/i"));
            newContactBtn.Click();
        }

        /// <summary>
        /// Issue a create new contact command with given first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns> A command upon which the parameters for the new contact are specified</returns>
        public static CreateContactCommand CreateContact(string firstName)
        {
            GoTo();
            return new CreateContactCommand(firstName);
        }

        public static void CreateDummyContact()
        {
            GoTo();
            new CreateContactCommand().WithDummyValues().Create();
        }

        private static void ExpandCategory(string category)
        {

            var categoryFields = Driver.Instance.FindElements(By.CssSelector("span[class^='jp-light-blue']"));
            foreach (var option in categoryFields)
            {
                if (!string.Equals(option.Text, category)) continue;
                option.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                break;
            }
        }

        private static void InsertExtraField(string category, string field)
        {
            switch (category)
            {
                case "Phone":
                    {
                        var phoneComboList =
                                        Driver.Instance.FindElements(By.CssSelector("div[mytitle='Phone'] a.ng-scope.ng-binding"));
                        foreach (var option in phoneComboList)
                        {
                            if (!string.Equals(option.Text, field)) continue;
                            option.Click();
                            Driver.Wait(TimeSpan.FromSeconds(1));
                            break;
                        }
                        break;
                    }
                case "Email":
                    {
                        var emailComboList =
                                        Driver.Instance.FindElements(By.CssSelector("div[mytitle='Email'] a.ng-scope.ng-binding"));
                        foreach (var option in emailComboList)
                        {
                            if (!string.Equals(option.Text, field)) continue;
                            option.Click();
                            Driver.Wait(TimeSpan.FromSeconds(1));
                            break;
                        }
                        break;
                    }
                case "Address":
                    {
                        var addressComboList =
                                        Driver.Instance.FindElements(By.CssSelector("a[onclick^='scrollToId']"));
                        foreach (var option in addressComboList)
                        {
                            if (!string.Equals(option.Text, field)) continue;
                            option.Click();
                            Driver.Wait(TimeSpan.FromSeconds(1));
                            break;
                        }
                        break;
                    }
                case "Other Info":
                    {
                        var otherInfoComboList =
                                        Driver.Instance.FindElements(By.CssSelector("div[mytitle='Other Info'] a.ng-scope.ng-binding"));
                        foreach (var option in otherInfoComboList)
                        {
                            if (!string.Equals(option.Text, field)) continue;
                            option.Click();
                            Driver.Wait(TimeSpan.FromSeconds(1));
                            break;
                        }
                        break;
                    }
            }


        }

        private static void SelectFromListByName(ReadOnlyCollection<IWebElement> elementList, string value)
        {
            var optionExists = false;
            foreach (var option in elementList)
            {
                if (string.Equals(option.Text, value))
                {
                    option.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                    optionExists = true;
                    break;
                }
            }
            if (optionExists)
            {
                return;
            }
            Actions action = new Actions(Driver.Instance);
            action.SendKeys(OpenQA.Selenium.Keys.Escape);
            Driver.Wait(TimeSpan.FromSeconds(1));
            Report.ToLogFile(MessageType.Message, $"The option {value} does not exist within the list.", null);
        }
    }

    public class CreateContactCommand
    {

        private string firstName = "";
        private string lastName = "";
        private string middleName = "";
        private string suffix = "";
        private string organizationName = "";
        private string department = "";

        private string workPhone = "";
        private string workPhone2 = "";
        private string mobilePhone = "";
        private string mobilePhone2 = "";
        private string homePhone = "";
        private string homePhone2 = "";
        private string homeFax = "";
        private string workFax = "";
        private string otherPhone = "";

        private string email = "";
        private string personalEmail = "";
        private string otherEmail = "";

        private string workStreet = "";
        private string workCity = "";
        private string workState = "";
        private string workPostalCode = "";
        private string workCountry;

        private string homeStreet = "";
        private string homeCity = "";
        private string homeState = "";
        private string homePostalCode = "";
        private string homeCountry = "";

        private string otherStreet = "";
        private string otherCity = "";
        private string otherState = "";
        private string otherPostalCode = "";
        private string otherCountry = "";

        private string salutation = "";
        private string nickname = "";
        private string jobTitle = "";
        private string website = "";
        private string religion = "";
        private string birthdate = "";
        private string gender = "";
        private string comments = "";
        private bool allowSms = false;
        private bool allowPhones = false;
        private bool allowEmails = false;

        public CreateContactCommand() { }

        public CreateContactCommand(string firstName)
        {
            this.firstName = firstName;
        }

        /// <summary>
        /// Sets the last name for the new contact
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public CreateContactCommand withLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        /// <summary>
        /// Creates the new contact with given first and last name
        /// </summary>
        public void Create()
        {

            if (!string.IsNullOrEmpty(firstName)) NewContactPage.FirstName = firstName;
            if (!string.IsNullOrEmpty(lastName)) NewContactPage.LastName = lastName;
            if (!string.IsNullOrEmpty(middleName)) NewContactPage.MiddleName = middleName;
            if (!string.IsNullOrEmpty(suffix)) NewContactPage.Suffix = suffix;
            if (!string.IsNullOrEmpty(organizationName)) NewContactPage.OrganizationName = organizationName;
            if (!string.IsNullOrEmpty(department)) NewContactPage.Department = department;
            if (!string.IsNullOrEmpty(workPhone)) NewContactPage.WorkPhone = workPhone;
            if (!string.IsNullOrEmpty(workPhone2)) NewContactPage.WorkPhone2 = workPhone2;
            if (!string.IsNullOrEmpty(mobilePhone)) NewContactPage.MobilePhone = mobilePhone;
            if (!string.IsNullOrEmpty(mobilePhone2)) NewContactPage.MobilePhone2 = mobilePhone2;
            if (!string.IsNullOrEmpty(homePhone)) NewContactPage.HomePhone = homePhone;
            if (!string.IsNullOrEmpty(homePhone2)) NewContactPage.HomePhone2 = homePhone2;
            if (!string.IsNullOrEmpty(workFax)) NewContactPage.WorkFax = workFax;
            if (!string.IsNullOrEmpty(homeFax)) NewContactPage.HomeFax = homeFax;
            if (!string.IsNullOrEmpty(otherPhone)) NewContactPage.OtherPhone = otherPhone;
            if (!string.IsNullOrEmpty(email)) NewContactPage.Email = email;
            if (!string.IsNullOrEmpty(personalEmail)) NewContactPage.PersonalEmail = personalEmail;
            if (!string.IsNullOrEmpty(otherEmail)) NewContactPage.OtherEmail = otherEmail;

            if (!string.IsNullOrEmpty(workStreet)) NewContactPage.WorkStreet = workStreet;
            if (!string.IsNullOrEmpty(workCity)) NewContactPage.WorkCity = workCity;
            if (!string.IsNullOrEmpty(workState)) NewContactPage.WorkState = workState;
            if (!string.IsNullOrEmpty(workPostalCode)) NewContactPage.WorkPostalCode = workPostalCode;
            if (!string.IsNullOrEmpty(workCountry)) NewContactPage.WorkCountry = workCountry;

            if (!string.IsNullOrEmpty(homeStreet)) NewContactPage.HomeStreet = homeStreet;
            if (!string.IsNullOrEmpty(homeCity)) NewContactPage.HomeCity = homeCity;
            if (!string.IsNullOrEmpty(homeState)) NewContactPage.HomeState = homeState;
            if (!string.IsNullOrEmpty(homePostalCode)) NewContactPage.HomePostalCode = homePostalCode;
            if (!string.IsNullOrEmpty(homeCountry)) NewContactPage.HomeCountry = homeCountry;

            if (!string.IsNullOrEmpty(otherStreet)) NewContactPage.OtherStreet = otherStreet;
            if (!string.IsNullOrEmpty(otherCity)) NewContactPage.OtherCity = otherCity;
            if (!string.IsNullOrEmpty(otherState)) NewContactPage.OtherState = otherState;
            if (!string.IsNullOrEmpty(otherPostalCode)) NewContactPage.OtherPostalCode = otherPostalCode;
            if (!string.IsNullOrEmpty(otherCountry)) NewContactPage.OtherCountry = otherCountry;

            if (!string.IsNullOrEmpty(salutation)) NewContactPage.Salutation = salutation;
            if (!string.IsNullOrEmpty(nickname)) NewContactPage.Nickname = nickname;
            if (!string.IsNullOrEmpty(jobTitle)) NewContactPage.JobTitle = jobTitle;
            if (!string.IsNullOrEmpty(website)) NewContactPage.Website = website;
            if (!string.IsNullOrEmpty(religion)) NewContactPage.Religion = religion;
            if (!string.IsNullOrEmpty(birthdate)) NewContactPage.Birthdate = birthdate;
            if (!string.IsNullOrEmpty(gender)) NewContactPage.Gender = gender;
            if (!string.IsNullOrEmpty(comments)) NewContactPage.Comments = comments;

            NewContactPage.AllowSMS = allowSms;
            NewContactPage.AllowPhones = allowPhones;
            NewContactPage.AllowEmails = allowEmails;

            Driver.Wait(TimeSpan.FromSeconds(5));

            Commands.ClickSave();

        }

        public CreateContactCommand WithDummyValues()
        {
            this.firstName = DummyData.FirstName;
            this.lastName = DummyData.LastName;
            this.middleName = DummyData.MiddleName;
            this.suffix = DummyData.Suffix;
            this.organizationName = DummyData.OrganizationNameExisting;
            this.department = DummyData.Department;

            this.workPhone = DummyData.WorkPhone;
            this.workPhone2 = DummyData.WorkPhone2;
            this.mobilePhone = DummyData.MobilePhone;
            this.mobilePhone2 = DummyData.MobilePhone2;
            this.homePhone = DummyData.HomePhone;
            this.homePhone2 = DummyData.HomePhone2;
            this.homeFax = DummyData.HomeFax;
            this.workFax = DummyData.WorkFax;
            this.otherPhone = DummyData.OtherPhone;

            this.email = DummyData.Email;
            this.personalEmail = DummyData.PersonalEmail;
            this.otherEmail = DummyData.OtherEmail;

            this.workStreet = DummyData.WorkStreet;
            this.workCity = DummyData.WorkCity;
            this.workState = DummyData.WorkState;
            this.workPostalCode = DummyData.WorkPostalCode;
            this.workCountry = DummyData.WorkCountry;

            this.homeStreet = DummyData.HomeStreet;
            this.homeCity = DummyData.HomeCity;
            this.homeState = DummyData.HomeState;
            this.homePostalCode = DummyData.HomePostalCode;
            this.homeCountry = DummyData.HomeCountry;

            this.otherStreet = DummyData.OtherStreet;
            this.otherCity = DummyData.OtherCity;
            this.otherState = DummyData.OtherState;
            this.otherPostalCode = DummyData.OtherPostalCode;
            this.otherCountry = DummyData.OtherCountry;

            this.salutation = DummyData.Salutation;
            this.nickname = DummyData.Nickname;
            this.jobTitle = DummyData.JobTitle;
            this.website = DummyData.Website;
            this.religion = DummyData.Religion;
            this.birthdate = DummyData.Birthdate;
            this.gender = DummyData.Gender;
            this.comments = DummyData.Comments;
            this.allowSms = DummyData.AllowSMS;
            this.allowPhones = DummyData.AllowPhones;
            this.allowEmails = DummyData.AllowEmails;
            return this;
        }
    }

    public static class ContactFieldCategories
    {
        public const string Phone = "Phone";
        public const string Email = "Email";
        public const string Address = "Address";
        public const string OtherInfo = "Other Info";

        public static class PhoneFields
        {
            public const string WorkPhone = "Work Phone";
            public const string WorkPhone2 = "Work Phone 2";
            public const string MobilePhone = "Mobile Phone";
            public const string MobilePhone2 = "Mobile Phone 2";
            public const string HomePhone = "Home Phone";
            public const string HomePhone2 = "Home Phone 2";
            public const string HomeFax = "Home Fax";
            public const string WorkFax = "Work Fax";
            public const string OtherPhone = "Other Phone";

        }

        public static class EmailFields
        {
            public const string Email = "Email";
            public const string PersonalEmail = "Personal Email";
            public const string OtherEmail = "Other Email";
        }

        public static class AddressFields
        {
            public const string Work = "Work";
            public const string Home = "Home";
            public const string Other = "Other";
        }

        public static class OtherInfoFields
        {
            public const string Salutation = "Title / Salutation";
            public const string Nickname = "Nickname";
            public const string JobTitle = "Job Title";
            public const string Website = "Website";
            public const string Religion = "Religion";
        }
    }


}
