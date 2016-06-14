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
        /// Returns true if the organization phone number is callable when it is clicked
        /// </summary>
        public static bool IsPhoneNumberCallable
        {
            get
            {
                var element =
                    Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Phone'] a.ng-scope"));
                var href = element.GetAttribute("href");
                var expectedTelephoneLink = $"tel:{Phone}";
                return (href == expectedTelephoneLink);
            }
        }

        /// <summary>
        /// Checks if the input for the share window email address field complies with the email format. 
        /// Returns true if the Share button is enabled.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsOrganizationShareableTo(string email) => Commands.IsRecordShareableTo(email);

        /// <summary>
        /// Get the full name for a contact specified by its position within contact list
        /// </summary>
        /// <param name="sequence">The position of the contact within contact list</param>
        /// <returns></returns>
        public static string GetContactFullNameBySequence(int sequence)
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            Driver.MoveToElement(records[sequence - 1]);
            return records[sequence - 1].FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
        }

        /// <summary>
        /// Issue delete command from an organization's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteOrganizationCommand DeleteOrganization()
        {
            return new DeleteOrganizationCommand();
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
        /// Issue a create new contact command, from within organization view page
        /// </summary>
        /// <returns></returns>
        public static CreateContactCommand CreateContact()
        {
            Commands.ClickCreateNewContactForOrganizationButton();
            return new CreateContactCommand();
        }


        public static bool IsAddressLinkActive(string addressType, Func<string> street, Func<string> state, Func<string> postalCode, Func<string> city, Func<string> country)
        {
            var element =
                    Driver.Instance.FindElement(By.CssSelector($"[ng-if='show{addressType}Address(group);'] [ng-show='myaddressstreet']"));
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

        /// <summary>
        /// Issue an add existing contact to organization contact list command, from within organization view page
        /// </summary>
        /// <returns></returns>
        public static AddContactsToContactListCommand AddContactsToContactList()
        {
            return new AddContactsToContactListCommand();
        }

        // REQUIRED FIELDS START ///////////////////////////////////////////////////////////

        public static string OrganizationName => GetRequiredFieldValueFor("Organization Name");

        public static string Phone => GetRequiredFieldValueFor("Phone");

        public static string Email => GetRequiredFieldValueFor("Email");
        public static bool IsEmailEmailable
        {
            get
            {
                var element =
                      Driver.Instance.FindElement(By.CssSelector($"[mytitle='Email'] a.ng-scope"));
                var href = element.GetAttribute("href");
                var expectedEmailLink = $"mailto:{Email}";
                return (href == expectedEmailLink);
            }
        }

        public static string Fax => GetRequiredFieldValueFor("Fax");

        public static string Website => GetRequiredFieldValueFor("Website");


        // REQUIRED FIELDS END ///////////////////////////////////////////////////////////

        public static string PrimaryContact
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div#primary-contact-box"));
                if (!element.Displayed) return string.Empty;
                return element.FindElement(By.CssSelector("font.name.font-regular.m-b-sm.ng-binding")).Text;
            }
        }
        public static bool IsPrimaryContactFieldVisible
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div#primary-contact-box"));
                return element.Displayed;
            }
        }

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


        public static string AllowSms => GetAllowFieldValue("allowSMS");
        public static string AllowPhones => GetAllowFieldValue("allowPhones");
        public static string AllowEmails => GetAllowFieldValue("allowEmails");


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


        public static string BillingStreet => GetAddressFieldValueFor("Billing", "street");
        public static bool IsBillingStreetFieldVisible => IsAddressFieldVisible("Billing", "street");

        public static string BillingCity => GetAddressFieldValueFor("Billing", "city");
        public static bool IsBillingCityFieldVisible => IsAddressFieldVisible("Billing", "city");

        public static string BillingState => GetAddressFieldValueFor("Billing", "state");
        public static bool IsBillingStateFieldVisible => IsAddressFieldVisible("Billing", "state");

        public static string BillingPostalCode => GetAddressFieldValueFor("Billing", "postalcode");
        public static bool IsBillingPostalCodeFieldVisible => IsAddressFieldVisible("Billing", "postalcode");

        public static string BillingCountry => GetAddressFieldValueFor("Billing", "country");
        public static bool IsBillingCountryFieldVisible => IsAddressFieldVisible("Billing", "country");



        public static string ShippingStreet => GetAddressFieldValueFor("Shipping", "street");
        public static bool IsShippingStreetFieldVisible => IsAddressFieldVisible("Shipping", "street");

        public static string ShippingCity => GetAddressFieldValueFor("Shipping", "city");
        public static bool IsShippingCityFieldVisible => IsAddressFieldVisible("Shipping", "city");

        public static string ShippingState => GetAddressFieldValueFor("Shipping", "state");
        public static bool IsShippingStateFieldVisible => IsAddressFieldVisible("Shipping", "state");

        public static string ShippingPostalCode => GetAddressFieldValueFor("Shipping", "postalcode");
        public static bool IsShippingPostalCodeFieldVisible => IsAddressFieldVisible("Shipping", "postalcode");

        public static string ShippingCountry => GetAddressFieldValueFor("Shipping", "country");
        public static bool IsShippingCountryFieldVisible => IsAddressFieldVisible("Shipping", "country");



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


        public static bool IsShippingAddressLinkActive => IsAddressLinkActive("Shipping", () => ShippingStreet, () => ShippingState, () => ShippingPostalCode, () => ShippingCity, () => ShippingCountry);
        public static bool IsBillingAddressLinkActive => IsAddressLinkActive("Billing", () => BillingStreet, () => BillingState, () => BillingPostalCode, () => BillingCity, () => BillingCountry);
        public static bool IsOtherAddressLinkActive => IsAddressLinkActive("Other", () => OtherStreet, () => OtherState, () => OtherPostalCode, () => OtherCity, () => OtherCountry);









        private static string GetRequiredFieldValueFor(string fieldName)
        {
            var element = Driver.Instance.FindElement(By.CssSelector($"my-required-info[mytitle='{fieldName}']"));
            var text = element.GetAttribute("myitem");
            if (text != null)
                return text;
            return string.Empty;
        }
        private static string GetAddressFieldValueFor(string type, string field)
        {
            try
            {
                IWebElement element = null;
                Driver.NoWait(
                    () => element = Driver.Instance.FindElement(By.CssSelector($"div[ng-if='show{type}Address(group);']")));
                var text = element.FindElement(By.CssSelector($"span[ng-show='myaddress{field}']")).Text;
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
            var element = Driver.Instance.FindElement(By.CssSelector($"div[ng-show='group.{field}']"));
            string text = element.GetAttribute("class");
            if (string.IsNullOrEmpty(text)) return true.ToString();
            if (string.Equals(text, "ng-hide")) return false.ToString();
            throw new Exception();
        }

    }


    public class AddContactsToContactListCommand
    {
        private int numberOfContactsToBeAdded;
        private bool random;
        private bool uncheckOnlyOrphan;
        private string firstName;
        private string lastName;

        /// <summary>
        /// Informs browser that during command execution, the "Orphan Contacts" check box needs to be unchecked
        /// </summary>
        /// <returns></returns>
        public AddContactsToContactListCommand UncheckingOrphanCheckbox()
        {
            uncheckOnlyOrphan = true;
            return this;
        }

        /// <summary>
        /// Defines that the existing contacts to be added, will be selected randomly
        /// </summary>
        /// <param name="i"> Defines the number of randomly selected contacts to be added</param>
        public AddContactsToContactListCommand Randomly(int i)
        {
            numberOfContactsToBeAdded = i;
            random = true;
            return this;
        }

        /// <summary>
        /// Defines the first name of the existing contact to be added
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public AddContactsToContactListCommand WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        /// <summary>
        /// Defines the last name of the existing contact to be added
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public AddContactsToContactListCommand AndLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        /// <summary>
        /// Executes the add existing contact command. Contacts to be added will be searched by the previously defined criteria or else the will be selected randomly
        /// </summary>
        public void Add()
        {
            Commands.ClickAddExistingContactsToOrganizationButton();

            if (uncheckOnlyOrphan)
            {
                // Unchecks the "Orphan Contacts" checkbox which at this point is checked by default
                Driver.Instance.FindElement(By.CssSelector("[ng-model='showOrphanContacts']")).FindElement(By.XPath("..")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }

            if (random)
               Commands.SelectRandomNumberOfRecords(numberOfContactsToBeAdded);           
            else
                new SearchContactCommand(null)
                    .WithFirstName(firstName)
                    .AndLastName(lastName)
                    .Select();

            Driver.Instance.FindElement(By.CssSelector("i[title='Αdd Contacts to Organization']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }



    }
}
