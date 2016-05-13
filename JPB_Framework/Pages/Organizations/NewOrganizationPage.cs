using System;
using System.Collections.Generic;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
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
    }

    public class CreateOrganizationCommand
    {
        private string organizationName;
        private string phone;
        private string email;
        private string fax;
        private string website;

        private string industry;
        private string accountType;
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

        public CreateOrganizationCommand WithPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        public CreateOrganizationCommand WithBillingStreet(string billingStreet)
        {
            this.billingStreet = billingStreet;
            return this;
        }

        public CreateOrganizationCommand WithProfession(string profession)
        {
            this.profession = profession;
            return this;
        }

        internal CreateOrganizationCommand WithMultipleValues(List<Workflows.RecordField> basicOrganizationFields, List<Workflows.RecordField> extraOrganizationFields, List<Workflows.RecordField> booleanOrganizationFields)
        {
            organizationName = basicOrganizationFields.Find(x => x.Label.Contains("Organization Name")).Value;
            phone = basicOrganizationFields.Find(x => x.Label.Contains("Phone")).Value;
            email = basicOrganizationFields.Find(x => x.Label.Contains("Email")).Value;
            fax = basicOrganizationFields.Find(x => x.Label.Contains("Fax")).Value;
            website = basicOrganizationFields.Find(x => x.Label.Contains("Website")).Value;

            industry = extraOrganizationFields.Find(x => x.Label.Contains("Industry")).Value;
            accountType = extraOrganizationFields.Find(x => x.Label.Contains("Account Type")).Value;
            profession = extraOrganizationFields.Find(x => x.Label.Contains("Profession")).Value;
            comments = extraOrganizationFields.Find(x => x.Label.Contains("Comments")).Value;
            billingStreet = extraOrganizationFields.Find(x => x.Label.Contains("Billing Street")).Value;
            billingCity = extraOrganizationFields.Find(x => x.Label.Contains("Billing City")).Value;
            billingState = extraOrganizationFields.Find(x => x.Label.Contains("Billing State")).Value;
            billingPostalCode = extraOrganizationFields.Find(x => x.Label.Contains("Billing Postal Code")).Value;
            billingCountry = extraOrganizationFields.Find(x => x.Label.Contains("Billing Country")).Value;
            shippingStreet = extraOrganizationFields.Find(x => x.Label.Contains("Shipping Street")).Value;
            shippingCity = extraOrganizationFields.Find(x => x.Label.Contains("Shipping City")).Value;
            shippingState = extraOrganizationFields.Find(x => x.Label.Contains("Shipping State")).Value;
            shippingPostalCode = extraOrganizationFields.Find(x => x.Label.Contains("Shipping Postal Code")).Value;
            shippingCountry = extraOrganizationFields.Find(x => x.Label.Contains("Shipping Country")).Value;
            otherStreet = extraOrganizationFields.Find(x => x.Label.Contains("Other Street")).Value;
            otherCity = extraOrganizationFields.Find(x => x.Label.Contains("Other City")).Value;
            otherState = extraOrganizationFields.Find(x => x.Label.Contains("Other State")).Value;
            otherPostalCode = extraOrganizationFields.Find(x => x.Label.Contains("Other Postal Code")).Value;
            otherCountry = extraOrganizationFields.Find(x => x.Label.Contains("Other Country")).Value;

            allowSms = booleanOrganizationFields.Find(x => x.Label.Contains("Allow SMS")).Value;
            allowPhones = booleanOrganizationFields.Find(x => x.Label.Contains("Allow Phones")).Value;
            allowEmails = booleanOrganizationFields.Find(x => x.Label.Contains("Allow Emails")).Value;

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
            if (accountType != null) EditOrganizationFields.AccountType= accountType;
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
