using System;
using System.Collections.Generic;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Framework.Workflows;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class NewOrganizationPage
    {
        /// <summary>
        /// Check if browser is at organization form page that allows to create a new organization
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations  /  Add Organization");

        /// <summary>
        /// Returns whether the new contact Save button was pressed, and so the contact was saved, or not.
        /// </summary>
        public static bool IsOrganizationSavedSuccessfully { get; set; }

        /// <summary>
        /// Returns the length of the string value from the comments field
        /// </summary>
        public static int CommentsTextLength {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("#textboxid"));
                string str = element.GetAttribute("value");
                return str.Length;
            }
        }

        /// <summary>
        /// Returns the value of the comment remaining characters indicator
        /// </summary>
        public static int CommentsLimitIndicator
        {
            get
            {
                var indicatorText = Driver.Instance.FindElement(By.CssSelector("span[ng-show='group.comments.length && group.comments.length <= 500']")).Text;
                return int.Parse(indicatorText.Split(' ')[2]);
            }
        }

        /// <summary>
        /// Returns true if both shipping, billing and other country combo field values are ordered alphabetically
        /// </summary>
        public static bool AreCountryComboListsSorted
        {
            get
            {
                SetBillingCountry("");
                var billingCountryIsSorted = Commands.CheckIfListIsSorted
                    (Driver.Instance.FindElements(By.CssSelector("#billingAddress my-select[myname='Country'] div select option.ng-binding.ng-scope")));

                SetShippingCountry("");
                var shippingCountryIsSorted = Commands.CheckIfListIsSorted(
                    Driver.Instance.FindElements(By.CssSelector("#shippingAddress my-select[myname='Country'] div select option.ng-binding.ng-scope"))
                    );

                SetOtherCountry("");
                var otherCountryIsSorted = Commands.CheckIfListIsSorted(
                    Driver.Instance.FindElements(By.CssSelector("#otherAddress my-select[myname='Country'] div select option.ng-binding.ng-scope"))
                    );

                if (billingCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Shipping country combo field values are not sorted correctly", null);
                if (shippingCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Billing country combo field values are not sorted correctly", null);
                if (otherCountryIsSorted == false) Report.Report.ToLogFile(MessageType.Message, "Other country combo field values are not sorted correctly", null);

                return (billingCountryIsSorted && shippingCountryIsSorted && otherCountryIsSorted);
            }
        }

        /// <summary>
        /// Returns true if industry combo field values are ordered alphabetically
        /// </summary>
        public static bool IsIndustryComboListSorted
        {
            get
            {
                SetIndustry("");
                return Commands.CheckIfListIsSorted(Driver.Instance.FindElements(By.CssSelector("[myname='Industry'] div select option.ng-binding.ng-scope")));
            }
        }

        /// <summary>
        /// Returns true if organization type combo field values are ordered alphabetically
        /// </summary>
        public static bool IsOrganizationTypeComboListSorted
        {
            get
            {
                SetOrganizationType("");
                return Commands.CheckIfListIsSorted(Driver.Instance.FindElements(By.CssSelector("[myname='Organization Type'] div select option.ng-binding.ng-scope")));
            }
        }

        

        /// <summary>
        /// Navigates browser to an organization form page that allows to create a new organization
        /// </summary>
        public static void GoTo()
        {
            var newOrganizationBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[4]/ul/li[1]/a/i"));
            newOrganizationBtn.Click();
        }

        /// <summary>
        /// Issue a create new organization command with given organization name
        /// </summary>
        /// <param name="organization_name"></param>
        /// <returns> A command upon which the parameters for the new organization are specified</returns>
        public static CreateOrganizationCommand CreateOrganization()
        {
            if (!OrganizationsPage.IsAt) LeftSideMenu.GoToOrganizations();
            GoTo();
            return new CreateOrganizationCommand();
        }

        /// <summary>
        /// Sets the given value to the Comments field
        /// </summary>
        /// <param name="v"></param>
        public static void SetComments(string v) => EditOrganizationFields.Comments = v;
        /// <summary>
        /// Sets the given value to the Shipping Country field
        /// </summary>
        /// <param name="v"></param>
        public static void SetShippingCountry(string v) => EditOrganizationFields.ShippingCountry = v;
        /// <summary>
        /// Sets the given value to the Billing Country field
        /// </summary>
        /// <param name="v"></param>
        public static void SetBillingCountry(string v) => EditOrganizationFields.BillingCountry = v;
        /// <summary>
        /// Sets the given value to the Other Country field
        /// </summary>
        /// <param name="v"></param>
        public static void SetOtherCountry(string v) => EditOrganizationFields.OtherCountry = v;
        /// <summary>
        /// Sets the given value to the Organization Type field
        /// </summary>
        /// <param name="v"></param>
        public static void SetOrganizationType(string v) => EditOrganizationFields.OrganizationType = v;
        /// <summary>
        /// Sets the given value to the Industry field
        /// </summary>
        /// <param name="v"></param>
        public static void SetIndustry(string v) => EditOrganizationFields.Industry = v;

    }

    public class CreateOrganizationCommand
    {
        private string organizationName;
        private string phone;
        private string email;
        private string fax;
        private string website;

        private string industry;
        private string organizationType;
        private string profession;

        private string billingStreet;
        private string billingCity;
        private string billingState;
        private string billingPostalCode;
        private string billingCountry;

        private string shippingStreet;
        private string shippingCity;
        private string shippingState;
        private string shippingPostalCode;
        private string shippingCountry;

        private string otherStreet;
        private string otherCity;
        private string otherState;
        private string otherPostalCode;
        private string otherCountry;

        private string comments;
        private string allowSms;
        private string allowPhones;
        private string allowEmails;

        /// <summary>
        /// Sets the organization name for the new organization
        /// </summary>
        /// <param name="organization_name"></param>
        public CreateOrganizationCommand WithOrganizationName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        /// <summary>
        /// Sets the phone for the new organization
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public CreateOrganizationCommand WithPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        /// <summary>
        /// Sets the billing street address for the new organization
        /// </summary>
        /// <param name="billingStreet"></param>
        /// <returns></returns>
        public CreateOrganizationCommand WithBillingStreet(string billingStreet)
        {
            this.billingStreet = billingStreet;
            return this;
        }

        /// <summary>
        /// Sets the profession for the new organization
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public CreateOrganizationCommand WithProfession(string profession)
        {
            this.profession = profession;
            return this;
        }

        /// <summary>
        /// Sets the values for every organization field value of the new organization
        /// </summary>
        /// <param name="basicOrganizationFields"></param>
        /// <param name="extraOrganizationFields"></param>
        /// <param name="booleanOrganizationFields"></param>
        /// <returns></returns>
        internal CreateOrganizationCommand WithMultipleValues(List<Workflows.RecordField> basicOrganizationFields, List<Workflows.RecordField> extraOrganizationFields, List<Workflows.RecordField> booleanOrganizationFields)
        {
            organizationName = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OrganizationName)).Value;
            phone = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Phone)).Value;
            email = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Email)).Value;
            fax = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Fax)).Value;
            website = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Website)).Value;
            organizationType = basicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OrganizationType)).Value;

            industry = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Industry)).Value;           
            profession = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Profession)).Value;
            comments = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.Comments)).Value;
            billingStreet = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.BillingStreet)).Value;
            billingCity = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.BillingCity)).Value;
            billingState = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.BillingState)).Value;
            billingPostalCode = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.BillingPostalCode)).Value;
            billingCountry = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.BillingCountry)).Value;
            shippingStreet = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.ShippingStreet)).Value;
            shippingCity = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.ShippingCity)).Value;
            shippingState = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.ShippingState)).Value;
            shippingPostalCode = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.ShippingPostalCode)).Value;
            shippingCountry = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.ShippingCountry)).Value;
            otherStreet = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OtherStreet)).Value;
            otherCity = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OtherCity)).Value;
            otherState = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OtherState)).Value;
            otherPostalCode = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OtherPostalCode)).Value;
            otherCountry = extraOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OtherCountry)).Value;

            allowSms = booleanOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.AllowSms)).Value;
            allowPhones = booleanOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.AllowPhones)).Value;
            allowEmails = booleanOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.AllowEmails)).Value;

            return this;
        }


        /// <summary>
        /// Creates the new organization with the given name
        /// </summary>
        public void Create()
        {

            if (organizationName != null) EditOrganizationFields.OrganizationName = organizationName;
            if (phone != null) EditOrganizationFields.Phone = phone;
            if (email != null) EditOrganizationFields.Email= email;
            if (fax != null) EditOrganizationFields.Fax= fax;
            if (website != null) EditOrganizationFields.Website= website;
            if (industry!= null) EditOrganizationFields.Industry= industry;
            if (organizationType != null) EditOrganizationFields.OrganizationType= organizationType;
            if (profession != null) EditOrganizationFields.Profession = profession;
            if (comments != null) EditOrganizationFields.Comments= comments;

            if (billingStreet != null) EditOrganizationFields.BillingStreet = billingStreet;
            if (billingCity != null) EditOrganizationFields.BillingCity = billingCity;
            if (billingState != null) EditOrganizationFields.BillingState = billingState;
            if (billingPostalCode != null) EditOrganizationFields.BillingPostalCode = billingPostalCode;
            if (billingCountry != null) EditOrganizationFields.BillingCountry = billingCountry;

            if (shippingStreet != null) EditOrganizationFields.ShippingStreet = shippingStreet;
            if (shippingCity != null) EditOrganizationFields.ShippingCity = shippingCity;
            if (shippingState != null) EditOrganizationFields.ShippingState = shippingState;
            if (shippingPostalCode != null) EditOrganizationFields.ShippingPostalCode = shippingPostalCode;
            if (shippingCountry != null) EditOrganizationFields.ShippingCountry = shippingCountry;

            if (otherStreet != null) EditOrganizationFields.OtherStreet = otherStreet;
            if (otherCity != null) EditOrganizationFields.OtherCity = otherCity;
            if (otherState != null) EditOrganizationFields.OtherState = otherState;
            if (otherPostalCode != null) EditOrganizationFields.OtherPostalCode = otherPostalCode;
            if (otherCountry != null) EditOrganizationFields.OtherCountry = otherCountry;

            if (allowSms != null) EditOrganizationFields.AllowSMS = allowSms;
            if (allowPhones != null) EditOrganizationFields.AllowPhones = allowPhones;
            if (allowEmails != null) EditOrganizationFields.AllowEmails = allowEmails;


            Driver.Wait(TimeSpan.FromSeconds(5));
            NewOrganizationPage.IsOrganizationSavedSuccessfully = Commands.ClickSave();
        }
    }
}
