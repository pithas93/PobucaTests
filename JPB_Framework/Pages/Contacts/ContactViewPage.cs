using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Contacts
{
    public class ContactViewPage
    {
        /// <summary>
        /// Check if browser is at the selected contact's detail view page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Contacts  /  Contact View");

        /// <summary>
        /// Check if browser is at the selected page when it is open for a contact from within an organization view page
        /// </summary>
        public static bool IsAtFromWithinOrganizationViewPage => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization  /  Contact View");

        /// <summary>
        /// Returns true if contact mobile number field value can be dialed. 
        /// System is supposed to ask user to execute the task with an app from a given list, or just executes the dialing command.
        /// </summary>
        public static bool IsMobileNumberCallable
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Mobile Phone'] a.ng-scope"));
                var href = element.GetAttribute("href");
                var expectedTelephoneLink = $"tel:{MobilePhone}";
                return (href == expectedTelephoneLink);
            }
        }

        public static bool IsWorkEmailEmailable => IsEmailLinkActive("Work Email", ()=>WorkEmail);
        public static bool IsPersonalEmailEmailable => IsEmailLinkActive("Personal Email", () => PersonalEmail);
        public static bool IsOtherEmailEmailable => IsEmailLinkActive("Other Email", () => OtherEmail);


        public static void ClickOrganizationName()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Organization Name'] div[ng-click='visitGroup()']"));
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Checks if the input for the share window email address field complies with the email format. 
        /// Returns true if the Share button is enabled.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsContactShareableTo(string email) => Commands.IsRecordShareableTo(email);

        /// <summary>
        /// Returns true if contact is set to favorite otherwise returns false
        /// </summary>
        public static bool IsContactFavorite
        {
            get
            {
                if (true.ToString() == Favorite) return true;
                if (false.ToString() == Favorite) return false;
                throw new Exception();
            }
        }

        /// <summary>
        /// Sets current contact to be favorite
        /// </summary>
        public static void SetContactFavorite(bool v)
        {
            if (v.ToString() != Favorite) Commands.ClickFavorite();
        }


        /// <summary>
        /// Issue delete command from a contact's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteContact()
        {
            return new DeleteRecordCommand();
        }

        private static bool IsEmailLinkActive(string fieldName, Func<string> contactViewPageField)
        {
            var element =
                  Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}'] a.ng-scope"));
            var href = element.GetAttribute("href");
            var expectedEmailLink = $"mailto:{contactViewPageField()}";
            return (href == expectedEmailLink);
        }

        private static string GetRequiredFieldValueFor(string fieldName)
        {
            var element = Driver.Instance.FindElement(By.CssSelector($"my-required-info[mytitle='{fieldName}']"));
            var text = element.GetAttribute("myitem");
            if (text != null)
                return text;
            return string.Empty;
        }

        private static string GetExtraFieldValueFor(string fieldName)
        {
            try
            {
                IWebElement element = null;
                Driver.NoWait(
                    () => element = Driver.Instance.FindElement(By.CssSelector($"my-extra-info[mytitle='{fieldName}']")));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        private static bool IsExtraFieldVisible(string fieldName)
        {
            try
            {
                Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector($"my-extra-info[mytitle='{fieldName}']")));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static string GetAddressFieldValueFor(string type, string field)
        {
            try
            {
                IWebElement element = null;
                Driver.NoWait(
                    () => element = Driver.Instance.FindElement(By.CssSelector($"div[ng-if='show{type}Address(contact);']")));
                var element2 = element.FindElement(By.CssSelector($"span[ng-show='myaddress{field}']"));
                string text = element2.Text;
                if (text != null)
                    return text;
                return string.Empty;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        private static bool IsAddressFieldVisible(string type, string field)
        {
            try
            {
                IWebElement element = null;
                IWebElement element2 = null;
                Driver.NoWait(
                    () =>
                        element = Driver.Instance.FindElement(By.CssSelector($"div[ng-if='show{type}Address(contact);']")));
                Driver.NoWait(
                    () =>
                        element2 = element.FindElement(By.CssSelector($"span[ng-show='myaddress{field}']")));
                var str = element2.GetAttribute("class");
                return !str.Contains("ng-hide");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static string FirstName => GetRequiredFieldValueFor("First Name");

        public static string LastName => GetRequiredFieldValueFor("Last Name");

        public static string MobilePhone => GetRequiredFieldValueFor("Mobile Phone");

        public static string WorkEmail => GetRequiredFieldValueFor("Work Email");

        public static string OrganizationName => GetRequiredFieldValueFor("Organization Name");

        public static string Department => GetRequiredFieldValueFor("Department");

        public static string WorkPhone => GetRequiredFieldValueFor("Work Phone");

        public static string JobTitle => GetRequiredFieldValueFor("Job Title");


        public static string WorkPhone2 => GetExtraFieldValueFor("Work Phone 2");
        public static bool IsWorkPhone2FieldVisible => IsExtraFieldVisible("Work Phone 2");


        public static string MobilePhone2 => GetExtraFieldValueFor("Mobile Phone 2");
        public static bool IsMobilePhone2FieldVisible => IsExtraFieldVisible("Mobile Phone 2");


        public static string HomePhone => GetExtraFieldValueFor("Home Phone");
        public static bool IsHomePhoneFieldVisible => IsExtraFieldVisible("Home Phone");


        public static string HomePhone2 => GetExtraFieldValueFor("Home Phone 2");
        public static bool IsHomePhone2FieldVisible => IsExtraFieldVisible("Home Phone 2");


        public static string WorkFax => GetExtraFieldValueFor("Work Fax");
        public static bool IsWorkFaxFieldVisible => IsExtraFieldVisible("Work Fax");


        public static string HomeFax => GetExtraFieldValueFor("Home Fax");
        public static bool IsHomeFaxFieldVisible => IsExtraFieldVisible("Home Fax");


        public static string OtherPhone => GetExtraFieldValueFor("Other Phone");
        public static bool IsOtherPhoneFieldVisible => IsExtraFieldVisible("Other Phone");


        public static string PersonalEmail => GetExtraFieldValueFor("Personal Email");
        public static bool IsPersonalEmailFieldVisible => IsExtraFieldVisible("Personal Email");


        public static string OtherEmail => GetExtraFieldValueFor("Other Email");
        public static bool IsOtherEmailFieldVisible => IsExtraFieldVisible("Other Email");


        public static string Salutation => GetExtraFieldValueFor("Title / Salutation");
        public static bool IsSalutationFieldVisible => IsExtraFieldVisible("Title / Salutation");

        public static string MiddleName => GetExtraFieldValueFor("Middle Name");
        public static bool IsMiddleNameFieldVisible => IsExtraFieldVisible("Middle Name");


        public static string Suffix => GetExtraFieldValueFor("Suffix");
        public static bool IsSuffixFieldVisible => IsExtraFieldVisible("Suffix");

        public static string Nickname => GetExtraFieldValueFor("Nickname");
        public static bool IsNicknameFieldVisible => IsExtraFieldVisible("Nickname");

        public static string Website => GetExtraFieldValueFor("Website");
        public static bool IsWebsiteFieldVisible => IsExtraFieldVisible("Website");


        public static string Religion => GetExtraFieldValueFor("Religion");
        public static bool IsReligionFieldVisible => IsExtraFieldVisible("Religion");


        public static string Birthdate => GetExtraFieldValueFor("Birthday");
        public static bool IsBirthdateFieldVisible => IsExtraFieldVisible("Birthday");


        public static string Gender => GetExtraFieldValueFor("Gender");
        public static bool IsGenderFieldVisible => IsExtraFieldVisible("Gender");


        public static string WorkStreet => GetAddressFieldValueFor("Work", "street");

        public static bool IsWorkStreetFieldVisible => IsAddressFieldVisible("Work", "street");


        public static string WorkCity => GetAddressFieldValueFor("Work", "city");
        public static bool IsWorkCityFieldVisible => IsAddressFieldVisible("Work", "city");


        public static string WorkState => GetAddressFieldValueFor("Work", "state");
        public static bool IsWorkStateFieldVisible => IsAddressFieldVisible("Work", "state");


        public static string WorkPostalCode => GetAddressFieldValueFor("Work", "postalcode");
        public static bool IsWorkPostalCodeFieldVisible => IsAddressFieldVisible("Work", "postalcode");


        public static string WorkCountry => GetAddressFieldValueFor("Work", "country");
        public static bool IsWorkCountryFieldVisible => IsAddressFieldVisible("Work", "country");


        public static string HomeStreet => GetAddressFieldValueFor("Home", "street");
        public static bool IsHomeStreetFieldVisible => IsAddressFieldVisible("Home", "street");


        public static string HomeCity => GetAddressFieldValueFor("Home", "city");
        public static bool IsHomeCityFieldVisible => IsAddressFieldVisible("Home", "city");


        public static string HomeState => GetAddressFieldValueFor("Home", "state");
        public static bool IsHomeStateFieldVisible => IsAddressFieldVisible("Home", "state");


        public static string HomePostalCode => GetAddressFieldValueFor("Home", "postalcode");
        public static bool IsHomePostalCodeFieldVisible => IsAddressFieldVisible("Home", "postalcode");


        public static string HomeCountry => GetAddressFieldValueFor("Home", "country");
        public static bool IsHomeCountryFieldVisible => IsAddressFieldVisible("Home", "country");


        public static string OtherStreet => GetAddressFieldValueFor("Other", "street");
        public static bool IsOtherStreetFieldVisible => IsAddressFieldVisible("Other", "street");


        public static string OtherCity => GetAddressFieldValueFor("Other", "city");
        public static bool IsOtherCityFieldVisible => IsAddressFieldVisible("Other", "city");


        public static string OtherState => GetAddressFieldValueFor("Other", "state");
        public static bool IsOtherStateFieldVisible => IsAddressFieldVisible("Other", "state");


        public static string OtherPostalCode => GetAddressFieldValueFor("Other", "postalcode");
        public static bool IsOtherPostalCodeFieldVisible => IsAddressFieldVisible("Other", "postalcode");


        public static string OtherCountry => GetAddressFieldValueFor("Other", "country");
        public static bool IsOtherCountryFieldVisible => IsAddressFieldVisible("Other", "country");



        public static string Comments
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.comments'] .feed-element.b-b-none.ng-binding"));
                if (!string.IsNullOrEmpty(element.Text))
                    return element.Text;
                return string.Empty;
            }
        }
        public static bool IsCommentsFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.comments']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "ibox float-e-margins");
            }
        }

        public static string AllowSms
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowSMS']"));
                string text = element.GetAttribute("class");
                if (string.IsNullOrEmpty(text)) return true.ToString();
                if (string.Equals(text, "ng-hide")) return false.ToString();
                throw new Exception();
            }
        }

        public static string AllowPhones
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowPhones']"));
                string text = element.GetAttribute("class");
                if (string.IsNullOrEmpty(text)) return true.ToString();
                if (string.Equals(text, "ng-hide")) return false.ToString();
                throw new Exception();
            }
        }

        public static string AllowEmails
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowEmails']"));
                string text = element.GetAttribute("class");
                if (string.IsNullOrEmpty(text)) return true.ToString();
                if (string.Equals(text, "ng-hide")) return false.ToString();
                throw new Exception();
            }
        }

        public static string Favorite
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("span#favorite-entity"));
                var text = element.GetAttribute("class");
                if (string.IsNullOrEmpty(text)) return true.ToString();
                if (string.Equals(text, "ng-hide")) return false.ToString();
                throw new Exception();
            }
        }


    }

}
