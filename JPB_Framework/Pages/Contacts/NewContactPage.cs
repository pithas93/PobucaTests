using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Framework.Workflows;
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

        /// <summary>
        /// Returns whether the new contact Save button was pressed, and so the contact was saved, or not.
        /// </summary>
        public static bool IsContactSavedSuccessfully { get; set; }

        /// <summary>
        /// Sets the value for the First Name field
        /// </summary>
        public static string FirstName { set { Driver.Instance.FindElement(By.Id("First Name")).SendKeys(value); } }

        /// <summary>
        /// Sets the value for the Last Name field
        /// </summary>
        public static string LastName { set { Driver.Instance.FindElement(By.Id("Last Name")).SendKeys(value); } }

        /// <summary>
        /// Sets the value for the Middle Name field
        /// </summary>
        public static string MiddleName { set { Driver.Instance.FindElement(By.Id("Middle Name")).SendKeys(value); } }

        /// <summary>
        /// Sets the value for the Suffix field
        /// </summary>
        public static string Suffix { set { Driver.Instance.FindElement(By.Id("Suffix")).SendKeys(value); } }

        /// <summary>
        /// Sets the value for the Organization Name list field
        /// </summary>
        public static string OrganizationName
        {
            set
            {
                Driver.Instance.FindElement(By.CssSelector("my-auto-complete[myname='Organization'] div input")).SendKeys(value);
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
        }

        /// <summary>
        /// Sets the value for the Department combo box
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Work Phone field
        /// </summary>
        public static string WorkPhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkPhone);
                Driver.Instance.FindElement(By.Id("workphone")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Work Phone 2 field
        /// </summary>
        public static string WorkPhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkPhone2);
                Driver.Instance.FindElement(By.Id("workphone2")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Mobile Phone field
        /// </summary>
        public static string MobilePhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.MobilePhone);
                Driver.Instance.FindElement(By.Id("mobilephone")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Mobile Phone 2 field
        /// </summary>
        public static string MobilePhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.MobilePhone2);
                Driver.Instance.FindElement(By.Id("mobilephone2")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Phone field
        /// </summary>
        public static string HomePhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomePhone);
                Driver.Instance.FindElement(By.Id("homephone")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Phone 2 field
        /// </summary>
        public static string HomePhone2
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomePhone2);
                Driver.Instance.FindElement(By.Id("homephone2")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Fax field
        /// </summary>
        public static string HomeFax
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.HomeFax);
                Driver.Instance.FindElement(By.Id("homefax")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Work Fax field
        /// </summary>
        public static string WorkFax
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.WorkFax);
                Driver.Instance.FindElement(By.Id("workfax")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Other Phone field
        /// </summary>
        public static string OtherPhone
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Phone);
                InsertExtraField(ContactFieldCategories.Phone, ContactFieldCategories.PhoneFields.OtherPhone);
                Driver.Instance.FindElement(By.Id("otherphone")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Email field
        /// </summary>
        public static string Email
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.Email);
                Driver.Instance.FindElement(By.Id("email")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Personal Email field
        /// </summary>
        public static string PersonalEmail
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.PersonalEmail);
                Driver.Instance.FindElement(By.Id("personalemail")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Other Email field
        /// </summary>
        public static string OtherEmail
        {
            set
            {
                ExpandCategory(ContactFieldCategories.Email);
                InsertExtraField(ContactFieldCategories.Email, ContactFieldCategories.EmailFields.OtherEmail);
                Driver.Instance.FindElement(By.Id("otheremail")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Work Street textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Work City textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Work State textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Work Postal Code
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Work Country combo box
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Home Street textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Home City textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Home State textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Home Postal Code textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Home ountry combo box
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Other Street textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Other City textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Other State textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Other Postal Code textfield
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Other Country combo box
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Salutation / Title textfield
        /// </summary>
        public static string Salutation
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Salutation);
                Driver.Instance.FindElement(By.Id("titlesalutation")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Nickname textfield
        /// </summary>
        public static string Nickname
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Nickname);
                Driver.Instance.FindElement(By.Id("nickname")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Job Title textfield
        /// </summary>
        public static string JobTitle
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.JobTitle);
                Driver.Instance.FindElement(By.Id("jobtitle")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Website textfield
        /// </summary>
        public static string Website
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Website);
                Driver.Instance.FindElement(By.Id("website")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Religion textfield
        /// </summary>
        public static string Religion
        {
            set
            {
                ExpandCategory(ContactFieldCategories.OtherInfo);
                InsertExtraField(ContactFieldCategories.OtherInfo, ContactFieldCategories.OtherInfoFields.Religion);
                Driver.Instance.FindElement(By.Id("religion")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Birthdate date picklist field
        /// </summary>
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

        /// <summary>
        /// Sets the value for the Gender combo box
        /// </summary>
        public static string Gender
        {
            set
            {
                var genderList = Driver.Instance.FindElements(By.CssSelector("my-select#gender-select option"));
                SelectFromListByName(genderList, value);
            }
        }

        /// <summary>
        /// Sets the value for the Comments textbox
        /// </summary>
        public static string Comments
        {
            set
            {
                Driver.Instance.FindElement(By.Id("textboxid")).SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Allow SMS checkbox
        /// </summary>
        public static string AllowSMS
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowSMS']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = "False";
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = "True";

                if ((value.Equals("True") && isChecked.Equals("False")) || (value.Equals("False") && isChecked.Equals("True")))
                    checkbox.Click();
            }
        }

        /// <summary>
        /// Sets the value for the Allow Phones checkbox
        /// </summary>
        public static string AllowPhones
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowPhones']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = "False";
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = "True";

                if ((value.Equals("True") && isChecked.Equals("False")) || (value.Equals("False") && isChecked.Equals("True")))
                    checkbox.Click();
            }
        }

        /// <summary>
        /// Sets the value for the Allow Emails checkbox
        /// </summary>
        public static string AllowEmails
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='contact.allowEmails']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = "False";
                if (val.Equals("icheckbox-original") || (val.Equals("icheckbox-original hover"))) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || (val.Equals("icheckbox-original checked hover"))) isChecked = "True";

                if((value.Equals("True") && isChecked.Equals("False")) || (value.Equals("False") && isChecked.Equals("True")))
                    checkbox.Click();
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
        public static CreateContactCommand CreateContact()
        {
            GoTo();
            return new CreateContactCommand();
        }

        /// <summary>
        /// Expands the 4 field categories at new contact page so that browser can selects either of the hidden fields
        /// </summary>
        /// <param name="category">The field category name to expand</param>
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

        /// <summary>
        /// Inserts an extra, previously hidden field at the new contact page.
        /// </summary>
        /// <param name="category">The field category name that contains the field</param>
        /// <param name="field">The field to be added to the page</param>
        private static void InsertExtraField(string category, string field)
        {
            if (!category.Equals("Address"))
            {
                var comboList =
                    Driver.Instance.FindElements(By.CssSelector($"div[mytitle='{category}'] a.ng-scope.ng-binding"));
                foreach (var option in comboList)
                {
                    if (!string.Equals(option.Text, field)) continue;
                    option.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                    break;
                }
            }
            else
            {
                var comboList =
                    Driver.Instance.FindElements(By.CssSelector("a[onclick^='scrollToId']"));
                foreach (var option in comboList)
                {
                    if (!string.Equals(option.Text, field)) continue;
                    option.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                    break;
                }
            }

        }

        /// <summary>
        /// Selects an item from a combo field. The selected item is assigned as the new field value. If the item is not among the list, ESC key is pressed.
        /// </summary>
        /// <param name="elementList">The list of elements within the combo option list</param>
        /// <param name="value">The item we are looking for inside the combo option list</param>
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
        /// <param name="homePhone"></param>
        /// <returns></returns>
        public CreateContactCommand WithWorkPhone(string workPhone)
        {
            this.workPhone = workPhone;
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
        /// Sets dummy values for every field of the new contact
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the new contact with given contact field values
        /// </summary>
        public void Create()
        {

            if (firstName != null) NewContactPage.FirstName = firstName;
            if (lastName != null) NewContactPage.LastName = lastName;
            if (middleName != null) NewContactPage.MiddleName = middleName;
            if (suffix != null) NewContactPage.Suffix = suffix;
            if (organizationName != null) NewContactPage.OrganizationName = organizationName;
            if (department != null) NewContactPage.Department = department;
            if (workPhone != null) NewContactPage.WorkPhone = workPhone;
            if (workPhone2 != null) NewContactPage.WorkPhone2 = workPhone2;
            if (mobilePhone != null) NewContactPage.MobilePhone = mobilePhone;
            if (mobilePhone2 != null) NewContactPage.MobilePhone2 = mobilePhone2;
            if (homePhone != null) NewContactPage.HomePhone = homePhone;
            if (homePhone2 != null) NewContactPage.HomePhone2 = homePhone2;
            if (workFax != null) NewContactPage.WorkFax = workFax;
            if (homeFax != null) NewContactPage.HomeFax = homeFax;
            if (otherPhone != null) NewContactPage.OtherPhone = otherPhone;
            if (email != null) NewContactPage.Email = email;
            if (personalEmail != null) NewContactPage.PersonalEmail = personalEmail;
            if (otherEmail != null) NewContactPage.OtherEmail = otherEmail;

            if (workStreet != null) NewContactPage.WorkStreet = workStreet;
            if (workCity != null) NewContactPage.WorkCity = workCity;
            if (workState != null) NewContactPage.WorkState = workState;
            if (workPostalCode != null) NewContactPage.WorkPostalCode = workPostalCode;
            if (workCountry != null) NewContactPage.WorkCountry = workCountry;

            if (homeStreet != null) NewContactPage.HomeStreet = homeStreet;
            if (homeCity != null) NewContactPage.HomeCity = homeCity;
            if (homeState != null) NewContactPage.HomeState = homeState;
            if (homePostalCode != null) NewContactPage.HomePostalCode = homePostalCode;
            if (homeCountry != null) NewContactPage.HomeCountry = homeCountry;

            if (otherStreet != null) NewContactPage.OtherStreet = otherStreet;
            if (otherCity != null) NewContactPage.OtherCity = otherCity;
            if (otherState != null) NewContactPage.OtherState = otherState;
            if (otherPostalCode != null) NewContactPage.OtherPostalCode = otherPostalCode;
            if (otherCountry != null) NewContactPage.OtherCountry = otherCountry;

            if (salutation != null) NewContactPage.Salutation = salutation;
            if (nickname != null) NewContactPage.Nickname = nickname;
            if (jobTitle != null) NewContactPage.JobTitle = jobTitle;
            if (website != null) NewContactPage.Website = website;
            if (religion != null) NewContactPage.Religion = religion;
            if (birthdate != null) NewContactPage.Birthdate = birthdate;
            if (gender != null) NewContactPage.Gender = gender;
            if (comments != null) NewContactPage.Comments = comments;

            if (allowSms != null) NewContactPage.AllowSMS = allowSms;
            if (allowPhones != null) NewContactPage.AllowPhones = allowPhones;
            if (allowEmails != null) NewContactPage.AllowEmails = allowEmails;

            Driver.Wait(TimeSpan.FromSeconds(5));

            NewContactPage.IsContactSavedSuccessfully = Commands.ClickSave();
        }


        
    }

    /// <summary>
    /// Regards the extra contact fields and their respective categories inside new contact page.
    /// </summary>
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
