using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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

                Report.Report.ToLogFile(MessageType.Message, "Something went wrong with getting if contact is Favorite.", null);
                Report.Report.AbruptFinalize();
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
        public static DeleteContactCommand DeleteContact()
        {
            return new DeleteContactCommand();
        }


        public static bool IsAddressLinkActive(string addressType, Func<string> street, Func<string> state, Func<string> postalCode, Func<string> city, Func<string> country)
        {
            var element =
                    Driver.Instance.FindElement(By.CssSelector($"[ng-if='show{addressType}Address(contact);'] [ng-show='myaddressstreet']"));
            Driver.MoveToElement(element);
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            var mainWindow = Driver.Instance.WindowHandles[0];
            var googleMapsWindow = Driver.Instance.WindowHandles[1];

            // Navigate to Google Maps page
            Driver.Instance.SwitchTo().Window(googleMapsWindow);
            var googleMapsSearchbox = Driver.Instance.FindElement(By.CssSelector("input#searchboxinput"));
            var googleAddressBar = googleMapsSearchbox.GetAttribute("value");
            Driver.Instance.SwitchTo().Window(googleMapsWindow).Close();
            Driver.Wait(TimeSpan.FromSeconds(2));

            // Return to jpb page
            Driver.Instance.SwitchTo().Window(mainWindow);
            var address = $"{street()}, {state()}, {postalCode()}, {city()}, {country()}";

            return (googleAddressBar.Equals(address));
        }

        public static string FirstName => GetFieldValueFor("First Name");

        public static string LastName => GetFieldValueFor("Last Name");

        public static string MiddleName => GetFieldValueFor("Middle Name");
        public static bool IsMiddleNameFieldVisible => IsFieldVisible("Middle Name");

        public static string JobTitle => GetFieldValueFor("Job Title");

        public static string OrganizationName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("[mytitle='Organization Name']"));
                var text = element.GetAttribute("myitem");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Department => GetFieldValueFor("Department");



        public static string WorkPhone => GetFieldValueFor("Work Phone");
        public static bool IsWorkPhoneFieldVisible => IsFieldVisible("Work Phone");
        public static bool IsWorkPhoneCallable => IsTelephoneLinkActive("Work Phone", () => WorkPhone);


        public static string WorkPhone2 => GetFieldValueFor("Work Phone 2");
        public static bool IsWorkPhone2FieldVisible => IsFieldVisible("Work Phone 2");
        public static bool IsWorkPhone2Callable => IsTelephoneLinkActive("Work Phone 2", () => WorkPhone2);


        public static string MobilePhone => GetFieldValueFor("Mobile Phone");
        public static bool IsMobilePhoneFieldVisible => IsFieldVisible("Mobile Phone");
        public static bool IsMobilePhoneCallable => IsTelephoneLinkActive("Mobile Phone", () => MobilePhone);


        public static string MobilePhone2 => GetFieldValueFor("Mobile Phone 2");
        public static bool IsMobilePhone2FieldVisible => IsFieldVisible("Mobile Phone 2");
        public static bool IsMobilePhone2Callable => IsTelephoneLinkActive("Mobile Phone 2", () => MobilePhone2);


        public static string HomePhone => GetFieldValueFor("Home Phone");
        public static bool IsHomePhoneFieldVisible => IsFieldVisible("Home Phone");
        public static bool IsHomePhoneCallable => IsTelephoneLinkActive("Home Phone", () => HomePhone);


        public static string HomePhone2 => GetFieldValueFor("Home Phone 2");
        public static bool IsHomePhone2FieldVisible => IsFieldVisible("Home Phone 2");
        public static bool IsHomePhone2Callable => IsTelephoneLinkActive("Home Phone 2", () => HomePhone2);


        public static string WorkFax => GetFieldValueFor("Work Fax");
        public static bool IsWorkFaxFieldVisible => IsFieldVisible("Work Fax");


        public static string HomeFax => GetFieldValueFor("Home Fax");
        public static bool IsHomeFaxFieldVisible => IsFieldVisible("Home Fax");


        public static string OtherPhone => GetFieldValueFor("Other Phone");
        public static bool IsOtherPhoneFieldVisible => IsFieldVisible("Other Phone");
        public static bool IsOtherPhoneCallable => IsTelephoneLinkActive("Other Phone", () => OtherPhone);


        public static string WorkEmail => GetFieldValueFor("Work Email");
        public static bool IsWorkEmailFieldVisible => IsFieldVisible("Work Email");
        public static bool IsWorkEmailEmailable => IsEmailLinkActive("Work Email", () => WorkEmail);

        public static string PersonalEmail => GetFieldValueFor("Personal Email");
        public static bool IsPersonalEmailFieldVisible => IsFieldVisible("Personal Email");
        public static bool IsPersonalEmailEmailable => IsEmailLinkActive("Personal Email", () => PersonalEmail);

        public static string OtherEmail => GetFieldValueFor("Other Email");
        public static bool IsOtherEmailFieldVisible => IsFieldVisible("Other Email");
        public static bool IsOtherEmailEmailable => IsEmailLinkActive("Other Email", () => OtherEmail);

        public static string Salutation => GetFieldValueFor("Title / Salutation");
        public static bool IsSalutationFieldVisible => IsFieldVisible("Title / Salutation");

        


        public static string Suffix => GetFieldValueFor("Suffix");
        public static bool IsSuffixFieldVisible => IsFieldVisible("Suffix");

        public static string Nickname => GetFieldValueFor("Nickname");
        public static bool IsNicknameFieldVisible => IsFieldVisible("Nickname");

        public static string Website => GetFieldValueFor("Website");
        public static bool IsWebsiteFieldVisible => IsFieldVisible("Website");


        public static string Religion => GetFieldValueFor("Religion");
        public static bool IsReligionFieldVisible => IsFieldVisible("Religion");


        public static string Birthdate => GetFieldValueFor("Birthday");
        public static bool IsBirthdateFieldVisible => IsFieldVisible("Birthday");


        public static string Gender => GetFieldValueFor("Gender");
        public static bool IsGenderFieldVisible => IsFieldVisible("Gender");


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

        public static bool IsWorkAddressLinkActive => IsAddressLinkActive("Work", ()=>WorkStreet, () => WorkState, () => WorkPostalCode, () => WorkCity, () => WorkCountry);
        public static bool IsHomeAddressLinkActive => IsAddressLinkActive("Home", () => HomeStreet, () => HomeState, () => HomePostalCode, () => HomeCity, () => HomeCountry);
        public static bool IsOtherAddressLinkActive => IsAddressLinkActive("Other", () => OtherStreet, () => OtherState, () => OtherPostalCode, () => OtherCity, () => OtherCountry);



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

        public static string AllowSms => GetAllowFieldValue("allowSMS");
        public static string AllowPhones => GetAllowFieldValue("allowPhones");
        public static string AllowEmails => GetAllowFieldValue("allowEmails");

        public static string Favorite
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("span[id*='favorite-entity']"));
                var text = element.GetAttribute("id");
                if (text.Equals("favorite-entity")) return true.ToString();
                if (text.Equals("unfavorite-entity")) return false.ToString();

                Report.Report.ToLogFile(MessageType.Message, "Something went wrong when getting Favorite value in contact view page.", null);
                Report.Report.AbruptFinalize();
                throw new Exception();
            }
        }





        private static bool IsEmailLinkActive(string fieldName, Func<string> contactViewPageField)
        {
            var element =
                  Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}'] a.ng-scope"));
            var href = element.GetAttribute("href");
            var expectedEmailLink = $"mailto:{contactViewPageField()}";
            return (href == expectedEmailLink);
        }
        private static bool IsTelephoneLinkActive(string fieldName, Func<string> contactViewPageField)
        {
            var element =
                  Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}'] a.ng-scope"));
            var href = element.GetAttribute("href");
            var expectedEmailLink = $"tel:{contactViewPageField()}";
            return (href == expectedEmailLink);
        }
        private static string GetFieldValueFor(string fieldName)
        {
            if (!IsFieldVisible(fieldName)) return string.Empty;
            var element = Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}']"));
            var text = element.GetAttribute("myattr");
            if (text != null)
                return text;
            return string.Empty;
        }
       
        private static bool IsFieldVisible(string fieldName)
        {
            try
            {
                Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}']")));
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
        private static string GetAllowFieldValue(string field)
        {
            var element = Driver.Instance.FindElement(By.CssSelector($"div[ng-show='contact.{field}']"));
            string text = element.GetAttribute("class");
            if (string.IsNullOrEmpty(text)) return true.ToString();
            if (string.Equals(text, "ng-hide")) return false.ToString();

            Report.Report.ToLogFile(MessageType.Message, $"Something went wrong when getting allow{field} value in contact view page.", null);
            Report.Report.AbruptFinalize();
            throw new Exception();
        }


    }

}
