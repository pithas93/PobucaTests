using System;
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
        public static bool IsAt => Driver.CheckIfIsAt("Contact View");

        /// <summary>
        /// Issue delete command from a contact's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteContact()
        {
            return new DeleteRecordCommand();
        }


        // REQUIRED FIELDS START ////////////////////////////////////////////////

        public static string FirstName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='First Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string LastName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Last Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string MiddleName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Middle Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Suffix
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Suffix']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string MobilePhone
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Mobile Phone']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Email
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Email']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string OrganizationName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Organization Name']"));
                string text = element.GetAttribute("myitem");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        // REQUIRED FIELDS END ////////////////////////////////////////////////

        public static string Department
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.departmentID'] span"));
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string str = element.Text;
                    return str.Substring(12);
                }
                return string.Empty;
            }
        }
        public static bool IsDepartmentFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.departmentID']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "m-b-xs");
            }
        }

        // EXTRA FIELDS START ////////////////////////////////////////////////

        public static string WorkPhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone']")));
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
        }
        public static bool IsWorkPhoneFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkPhone2
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone 2']")));
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
        }
        public static bool IsWorkPhone2FieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone 2']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string MobilePhone2
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Mobile Phone 2']")));
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
        }
        public static bool IsMobilePhone2FieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Mobile Phone 2']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomePhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone']")));
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
        }
        public static bool IsHomePhoneFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomePhone2
        {
            get
            {

                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone 2']")));
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
        }
        public static bool IsHomePhone2FieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone 2']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkFax
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Fax']")));
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
        }
        public static bool IsWorkFaxFieldVisible
        {
            get
            {
                try
                {

                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Fax']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomeFax
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Fax']")));
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
        }
        public static bool IsHomeFaxFieldVisible
        {
            get
            {
                try
                {

                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Fax']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherPhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other']")));
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
        }
        public static bool IsOtherPhoneFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string PersonalEmail
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Personal Email']")));
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
        }
        public static bool IsPersonalEmailFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Personal Email']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherEmail
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other Email']")));
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
        }
        public static bool IsOtherEmailFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other Email']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
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
        }
        public static bool IsWorkStreetFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
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
        }
        public static bool IsWorkCityFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
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
        }
        public static bool IsWorkStateFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
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
        }
        public static bool IsWorkPostalCodeFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string WorkCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
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
        }
        public static bool IsWorkCountryFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscountry']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }


        public static string HomeStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
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
        }
        public static bool IsHomeStreetFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomeCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
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
        }
        public static bool IsHomeCityFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomeState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
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
        }
        public static bool IsHomeStateFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomePostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
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
        }
        public static bool IsHomePostalCodeFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string HomeCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
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
        }
        public static bool IsHomeCountryFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscountry']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }


        public static string OtherStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
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
        }
        public static bool IsOtherStreetFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
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
        }
        public static bool IsOtherCityFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
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
        }
        public static bool IsOtherStateFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
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
        }
        public static bool IsOtherPostalCodeFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string OtherCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
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
        }
        public static bool IsOtherCountryFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    Driver.NoWait(
                        () =>
                            element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscountry']")));
                    var str = element2.GetAttribute("class");
                    return !str.Contains("ng-hide");
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }


        public static string Salutation
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Tile / Salutation']")));
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
        }
        public static bool IsSalutationFieldVisible
        {
            get
            {
                try
                {

                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Salutation']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string Nickname
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Nickname']")));
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
        }
        public static bool IsNicknameFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Nickname']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string JobTitle
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Job Title']")));
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
        }
        public static bool IsJobTitleFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Job Title']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string Website
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Website']")));
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
        }
        public static bool IsWebsiteFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Website']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string Religion
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Religion']")));
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
        }
        public static bool IsReligionFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Religion']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string Birthdate
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Birthday']")));
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
        }
        public static bool IsBirthdateFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Birthday']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

        public static string Gender
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Gender']")));
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
        }
        public static bool IsGenderFieldVisible
        {
            get
            {
                try
                {
                    Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Gender']")));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
        }

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

        public static string AllowSMS
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

        // EXTRA FIELDS END ////////////////////////////////////////////////


    }

}
