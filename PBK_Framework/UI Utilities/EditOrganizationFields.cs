using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    internal class EditOrganizationFields
    {

        public static string OrganizationName
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Organization Name"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string Email
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Email"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string Phone
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Phone"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string Website
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Website"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string Fax
        {
            set
            {
                var element = Driver.Instance.FindElement(By.Id("Fax"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string BillingStreet
        {
            set
            {
                var billingAddrElements = Driver.Instance.FindElement(By.Id("billingAddress"));
                if (!billingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Billing);
                }
                var element = billingAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string BillingCity
        {
            set
            {
                var billingAddrElements = Driver.Instance.FindElement(By.Id("billingAddress"));
                if (!billingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Billing);
                }
                var element = billingAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string BillingState
        {
            set
            {
                var billingAddrElements = Driver.Instance.FindElement(By.Id("billingAddress"));
                if (!billingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Billing);
                }
                var element = billingAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string BillingPostalCode
        {
            set
            {
                var billingAddrElements = Driver.Instance.FindElement(By.Id("billingAddress"));
                if (!billingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Billing);
                }
                var element = billingAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string BillingCountry
        {
            set
            {
                var billingAddrElements = Driver.Instance.FindElement(By.Id("billingAddress"));
                if (!billingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Billing);
                }

                var countryList = billingAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(countryList, value);
            }
        }

        public static string ShippingStreet
        {
            set
            {
                var shippingAddrElements = Driver.Instance.FindElement(By.Id("shippingAddress"));
                if (!shippingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Shipping);
                }
                var element = shippingAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string ShippingCity
        {
            set
            {
                var shippingAddrElements = Driver.Instance.FindElement(By.Id("shippingAddress"));
                if (!shippingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Shipping);
                }
                var element = shippingAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string ShippingState
        {
            set
            {
                var shippingAddrElements = Driver.Instance.FindElement(By.Id("shippingAddress"));
                if (!shippingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Shipping);
                }
                var element = shippingAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string ShippingPostalCode
        {
            set
            {
                var shippingAddrElements = Driver.Instance.FindElement(By.Id("shippingAddress"));
                if (!shippingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Shipping);
                }
                var element = shippingAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string ShippingCountry
        {
            set
            {
                var shippingAddrElements = Driver.Instance.FindElement(By.Id("shippingAddress"));
                if (!shippingAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Shipping);
                }

                var countryList = shippingAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
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
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string OtherCity
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string OtherState
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string OtherPostalCode
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Other);
                }
                var element = otherAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string OtherCountry
        {
            set
            {
                var otherAddrElements = Driver.Instance.FindElement(By.Id("otherAddress"));
                if (!otherAddrElements.Displayed)
                {
                    ExpandCategory(OrganizationFields.Address);
                    InsertExtraField(OrganizationFields.Address, OrganizationFields.AddressFields.Other);
                }

                var countryList = otherAddrElements.FindElements(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(countryList, value);
            }
        }

        public static string Industry
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-select[myname='Industry']"));
                if (!element.Displayed)
                {
                    Driver.Instance.FindElement(By.CssSelector("span[ng-hide='group.otherInfoVisible']")).Click();
                }
                var industryList = Driver.Instance.FindElements(By.CssSelector("my-select[myname='Industry'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(industryList, value);
            }
        }

        public static string OrganizationType
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-select[myname='Organization Type']"));
                if (!element.Displayed)
                {
                    Driver.Instance.FindElement(By.CssSelector("span[ng-hide='group.otherInfoVisible']")).Click();
                }
                var industryList = Driver.Instance.FindElements(By.CssSelector("my-select[myname='Organization Type'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(industryList, value);
            }
        }

        public static string Profession
        {
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-textbox[myname='Profession']"));
                if (!element.Displayed)
                {
                    Driver.Instance.FindElement(By.CssSelector("span[ng-hide='group.otherInfoVisible']")).Click();
                }
                var element2 = element.FindElement(By.Id("Profession"));
                element2.Clear();
                element2.SendKeys(value);
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
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='group.allowSMS']"));
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
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='group.allowPhones']"));
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
                var element = Driver.Instance.FindElement(By.CssSelector("input[ng-model='group.allowEmails']"));
                var checkbox = element.FindElement(By.XPath(".."));
                var val = checkbox.GetAttribute("class");
                var isChecked = "False";
                if (val.Equals("icheckbox-original") || val.Equals("icheckbox-original hover")) isChecked = "False";
                else if (val.Equals("icheckbox-original checked") || val.Equals("icheckbox-original checked hover")) isChecked = "True";

                if ((value.Equals("True") && isChecked.Equals("False")) || (value.Equals("False") && isChecked.Equals("True")))
                    checkbox.Click();
            }
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



        private static class OrganizationFields
        {
            public const string Address = "Address";

            public static class AddressFields
            {
                public const string Billing = "Billing";
                public const string Shipping = "Shipping";
                public const string Other = "Other";
            }

        }
    }
}
