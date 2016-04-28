using System;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationViewPage
    {
        /// <summary>
        /// Check if browser is at the selected organization's detail view page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization View");

        /// <summary>
        /// Issue delete command from an organization's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteOrganization()
        {
            return new DeleteRecordCommand();
        }

        /// <summary>
        /// Issue a search command that instructs browser to search for a contact with an organization's contact list
        /// </summary>
        /// <returns></returns>
        public static SearchOrganizationContactListCommand FindContactFromOrganizationContactList()
        {
            return new SearchOrganizationContactListCommand();
        }

        /// <summary>
        /// Issue a create new contact command, from within organization view page, with given first name
        /// </summary>
        /// <returns></returns>
        public static CreateContactCommand CreateContact()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("a.dropdown-toggle.p-none"));
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
            var str = element.GetAttribute("aria-haspopup");
            if (str.Equals("true"))
            {
                var createNewContactBtn = Driver.Instance.FindElements(By.CssSelector("#related-contacts-section .dropdown-menu.animated.fadeInRight.m-t-xs a"))[1];
                createNewContactBtn.Click();
            }
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "After clicking to add contact to organization, within organization view page, the relative combo box should be expanded, nut it did not.", null);
                throw new Exception();
            }

            return new CreateContactCommand();
        }

        // REQUIRED FIELDS START ///////////////////////////////////////////////////////////

        public static string OrganizationName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Organization Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Phone
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Phone']"));
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

        public static string Fax
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Fax']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Website
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Website']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        // REQUIRED FIELDS END ///////////////////////////////////////////////////////////

        public static string Industry
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.industryID'] span"));
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string str = element.Text;
                    
                    return str.Split(':')[1].Trim();
                }
                return string.Empty;
            }
        }
        public static bool IsIndustryFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.industryID']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "m-b-xs");
            }
        }

        public static string AccountType
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.accountTypeID'] span"));
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string str = element.Text;
                    return str.Split(':')[1].Trim();
                }
                return string.Empty;
            }
        }
        public static bool IsAccountTypeFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.accountTypeID']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "m-b-xs");
            }
        }

        public static string Profession
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.profession'] span"));
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string str = element.Text;
                    return str.Split(':')[1].Trim();
                }
                return string.Empty;
            }
        }
        public static bool IsProfessionFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.profession']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "m-b-xs");
            }
        }


        public static string AllowSms
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.allowSMS']"));
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
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.allowPhones']"));
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
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.allowEmails']"));
                string text = element.GetAttribute("class");
                if (string.IsNullOrEmpty(text)) return true.ToString();
                if (string.Equals(text, "ng-hide")) return false.ToString();
                throw new Exception();
            }
        }

        public static string Comments
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.comments'] .feed-element.b-b-none.ng-binding"));
                if (!string.IsNullOrEmpty(element.Text))
                    return element.Text;
                return string.Empty;
            }
        }
        public static bool IsCommentsFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='group.comments']"));
                var attr = element.GetAttribute("class");
                return string.Equals(attr, "ibox float-e-margins");
            }
        }


        public static string BillingStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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
        public static bool IsBillingStreetFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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


        public static string BillingCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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
        public static bool IsBillingCityFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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


        public static string BillingState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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
        public static bool IsBillingStateFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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


        public static string BillingPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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
        public static bool IsBillingPostalCodeFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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


        public static string BillingCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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
        public static bool IsBillingCountryFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showBillingAddress(group);']")));
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


        public static string ShippingStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
        public static bool IsShippingStreetFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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


        public static string ShippingCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
        public static bool IsShippingCityFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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


        public static string ShippingState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
        public static bool IsShippingStateFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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


        public static string ShippingPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
        public static bool IsShippingPostalCodeFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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


        public static string ShippingCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
        public static bool IsShippingCountryFieldVisible
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    IWebElement element2 = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showShippingAddress(group);']")));
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
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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
                            element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(group);']")));
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

    }

}
