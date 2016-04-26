using System;
using System.Collections.Generic;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Organizations
{
    public class EditOrganizationPage
    {
        /// <summary>
        /// Check if browser is at organization's edit page 
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization  /  Edit Organization");

        /// <summary>
        /// Returns true if a contact was saved successfully after editing 
        /// </summary>
        public static bool IsOrganizationSavedSuccessfully { get; set; }

        /// <summary>
        ///  Navigates browser through the available edit button to the organization's edit page
        /// </summary>
        public static void GoTo() { Commands.ClickEdit(); }

        /// <summary>
        /// Issue an command that instructs browser to change the value for some or every single field within edit contact page
        /// </summary>
        /// <returns></returns>
        public static EditOrganizationCommand EditContact()
        {
            GoTo();
            return new EditOrganizationCommand();
        }
    }

    public class EditOrganizationCommand
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

        public EditOrganizationCommand WithOrganizationName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        public EditOrganizationCommand WithPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        internal EditOrganizationCommand WithMultipleValues(List<Workflows.RecordField> basicOrganizationFields, List<Workflows.RecordField> extraOrganizationFields)
        {
            organizationName = basicOrganizationFields.Find(x => x.Label.Contains("Organization Name")).Value;
            phone = basicOrganizationFields.Find(x => x.Label.Contains("Phone")).Value;
            email = basicOrganizationFields.Find(x => x.Label.Contains("Email")).Value;
            fax = basicOrganizationFields.Find(x => x.Label.Contains("Fax")).Value;
            website = basicOrganizationFields.Find(x => x.Label.Contains("Website")).Value;
            allowSms = basicOrganizationFields.Find(x => x.Label.Contains("Allow SMS")).Value;
            allowPhones = basicOrganizationFields.Find(x => x.Label.Contains("Allow Phones")).Value;
            allowEmails = basicOrganizationFields.Find(x => x.Label.Contains("Allow Emails")).Value;

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

            return this;
        }

        public void Edit()
        {
            if (organizationName != null) EditOrganizationFields.OrganizationName = organizationName;
            if (phone != null) EditOrganizationFields.Phone = phone;
            if (email != null) EditOrganizationFields.Email = email;
            if (fax != null) EditOrganizationFields.Fax = fax;
            if (website != null) EditOrganizationFields.Website = website;
            if (industry != null) EditOrganizationFields.Industry = industry;
            if (accountType != null) EditOrganizationFields.AccountType = accountType;
            if (profession != null) EditOrganizationFields.Profession = profession;
            if (comments != null) EditOrganizationFields.Comments = comments;

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
