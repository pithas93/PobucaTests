using System;
using System.Collections.ObjectModel;
using System.Globalization;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    internal class EditContactFields
    {

        /// <summary>
        /// Sets the value for the First Name field
        /// </summary>
        public static string FirstName
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("First Name"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Last Name field
        /// </summary>
        public static string LastName
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Last Name"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Middle Name field
        /// </summary>
        public static string MiddleName
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Middle Name"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Suffix field
        /// </summary>
        public static string Suffix {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Suffix"));
                element.Clear();
                element.SendKeys(value);
            } }

        /// <summary>
        /// Sets the value for the Organization Name list field
        /// </summary>
        public static string OrganizationName
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-auto-complete[myname='Organization'] div input"));
                element.Clear();
                element.SendKeys(value);
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
                var element = Driver.Instance.FindElement(By.Id("workphone"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.WorkPhone);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Work Phone 2 field
        /// </summary>
        public static string WorkPhone2
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("workphone2"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.WorkPhone2);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Mobile Phone field
        /// </summary>
        public static string MobilePhone
        {
            set
            {
                ExpandCategory(ContactFields.Phone);
                InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.MobilePhone);
                var element = Driver.Instance.FindElement(By.Id("mobilephone"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Mobile Phone 2 field
        /// </summary>
        public static string MobilePhone2
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("mobilephone2"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.MobilePhone2);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Phone field
        /// </summary>
        public static string HomePhone
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("homephone"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.HomePhone);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Phone 2 field
        /// </summary>
        public static string HomePhone2
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("homephone2"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.HomePhone2);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Home Fax field
        /// </summary>
        public static string HomeFax
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("homefax"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.HomeFax);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Work Fax field
        /// </summary>
        public static string WorkFax
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("workfax"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.WorkFax);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Other Phone field
        /// </summary>
        public static string OtherPhone
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("otherphone"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Phone);
                    InsertExtraField(ContactFields.Phone, ContactFields.PhoneFields.OtherPhone);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Email field
        /// </summary>
        public static string Email
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("email"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Email);
                    InsertExtraField(ContactFields.Email, ContactFields.EmailFields.Email);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Personal Email field
        /// </summary>
        public static string PersonalEmail
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("personalemail"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Email);
                    InsertExtraField(ContactFields.Email, ContactFields.EmailFields.PersonalEmail);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Other Email field
        /// </summary>
        public static string OtherEmail
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("otheremail"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.Email);
                    InsertExtraField(ContactFields.Email, ContactFields.EmailFields.OtherEmail);
                }
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Work);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Home);
                }
                var element = homeAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Home);
                }
                var element = homeAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Home);
                }
                var element = homeAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Home);
                }
                var element = homeAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Home);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
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
                    ExpandCategory(ContactFields.Address);
                    InsertExtraField(ContactFields.Address, ContactFields.AddressFields.Other);
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
                var element = Driver.Instance.FindElement(By.Id("titlesalutation"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.OtherInfo);
                    InsertExtraField(ContactFields.OtherInfo, ContactFields.OtherInfoFields.Salutation);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Nickname textfield
        /// </summary>
        public static string Nickname
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("nickname"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.OtherInfo);
                    InsertExtraField(ContactFields.OtherInfo, ContactFields.OtherInfoFields.Nickname);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Job Title textfield
        /// </summary>
        public static string JobTitle
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("jobtitle"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.OtherInfo);
                    InsertExtraField(ContactFields.OtherInfo, ContactFields.OtherInfoFields.JobTitle);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Website textfield
        /// </summary>
        public static string Website
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("website"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.OtherInfo);
                    InsertExtraField(ContactFields.OtherInfo, ContactFields.OtherInfoFields.Website);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Religion textfield
        /// </summary>
        public static string Religion
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("religion"));
                if (!element.Displayed)
                {
                    ExpandCategory(ContactFields.OtherInfo);
                    InsertExtraField(ContactFields.OtherInfo, ContactFields.OtherInfoFields.Religion);
                }
                element.Clear();
                element.SendKeys(value);
            }
        }

        /// <summary>
        /// Sets the value for the Birthdate date picklist field
        /// </summary>
        public static string Birthdate
        {
            set
            {
                IWebElement dateCleaner = null;
                try
                {
                    Driver.NoWait(
                        () =>
                            dateCleaner =
                                Driver.Instance.FindElement(By.CssSelector("span.birthday-field-remove-icon.ng-scope"))
                        );
                    dateCleaner.Click();
                }
                catch (NoSuchElementException)
                {
                }

                var birthDayField = Driver.Instance.FindElement(By.CssSelector("#birthday"));
                birthDayField.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));

                var birthdate = Convert.ToDateTime(value);

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
                var element = Driver.Instance.FindElement(By.Id("textboxid"));
                element.Clear();
                element.SendKeys(value);
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
                if (val.Equals("icheckbox-original") || val.Equals("icheckbox-original hover")) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || val.Equals("icheckbox-original checked hover")) isChecked = "True";

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
                if (val.Equals("icheckbox-original") || val.Equals("icheckbox-original hover")) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || val.Equals("icheckbox-original checked hover")) isChecked = "True";

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
                if (val.Equals("icheckbox-original") || val.Equals("icheckbox-original hover")) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || val.Equals("icheckbox-original checked hover")) isChecked = "True";

                if ((value.Equals("True") && isChecked.Equals("False")) || (value.Equals("False") && isChecked.Equals("True")))
                    checkbox.Click();
            }
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
            Commands.PressEscapeKey();
            Report.Report.ToLogFile(MessageType.Message, $"The option {value} does not exist within the list.", null);
        }


        private static class ContactFields
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

    
}
