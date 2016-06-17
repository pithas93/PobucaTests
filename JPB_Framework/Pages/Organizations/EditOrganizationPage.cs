using System;
using System.Collections.Generic;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Framework.Workflows;

namespace JPB_Framework.Pages.Organizations
{
    public class EditOrganizationPage
    {
        /// <summary>
        /// Check if browser is at organization's edit page 
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization  /  Edit Organization");

        /// <summary>
        /// Returns true if an organization was saved successfully after editing 
        /// </summary>
        public static bool IsOrganizationSavedSuccessfully { get; set; }

        /// <summary>
        ///  Navigates browser through the available edit button to the organization's edit page
        /// </summary>
        public static void GoTo()
        {
            Commands.ClickEdit();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Issue an command that instructs browser to change the value for some or every single field within edit organization page
        /// </summary>
        /// <returns></returns>
        public static EditOrganizationCommand EditOrganization()
        {
            GoTo();
            return new EditOrganizationCommand();
        }

        /// <summary>
        /// Click the save button located in edit organization page
        /// </summary>
        public static void ClickSaveOrganizationButton() => Commands.ClickSave(); 

    }

    public class EditOrganizationCommand
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

        public EditOrganizationCommand WithNewOrganizationName(string organizationName)
        {
            this.organizationName = organizationName;
            return this;
        }

        public EditOrganizationCommand WithNewPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        internal EditOrganizationCommand WithMultipleNewValues(List<Workflows.RecordField> basicOrganizationFields, List<Workflows.RecordField> extraOrganizationFields, List<Workflows.RecordField> booleanOrganizationFields)
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

        public void Edit()
        {
            if (organizationName != null) EditOrganizationFields.OrganizationName = organizationName;
            if (phone != null) EditOrganizationFields.Phone = phone;
            if (email != null) EditOrganizationFields.Email = email;
            if (fax != null) EditOrganizationFields.Fax = fax;
            if (website != null) EditOrganizationFields.Website = website;
            if (industry != null) EditOrganizationFields.Industry = industry;
            if (organizationType != null) EditOrganizationFields.OrganizationType = organizationType;
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
            EditOrganizationPage.IsOrganizationSavedSuccessfully = Commands.ClickSave();
        }
    }
}
