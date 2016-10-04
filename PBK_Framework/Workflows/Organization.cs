using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Workflows
{
    public class Organization
    {
        internal List<RecordField> BasicOrganizationFields;
        internal List<RecordField> ExtraOrganizationFields;
        internal List<RecordField> BooleanOrganizationFields;

        public string OrganizationName => GetFieldValue(OrganizationFields.OrganizationName);

        public string PrimaryContact => GetFieldValue(OrganizationFields.PrimaryContact);

        /// <summary>
        /// If an organization was created during test execution, returns true. 
        /// </summary>
        public bool OrganizationWasCreated
        {
            get
            {
                var organizationName = BasicOrganizationFields.Find(x => x.Label.Contains(OrganizationFields.OrganizationName)).Value;

                return !string.IsNullOrEmpty(organizationName);
            }
        }

        /// <summary>
        /// Determines whether the newly created organization has all its field values saved correctly.
        /// Checks every field that was given a value when the contact was created and asserts that fields that were not given values, are null or have the default values
        /// </summary>
        /// <returns>True if all contact fields have the expected values. Returns false otherwise</returns>
        public bool AreOrganizationFieldValuesSavedCorrectly
        {
            get
            {
                if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != OrganizationName)))
                {
                    LeftSideMenu.GoToOrganizations();
                    OrganizationsPage.FindOrganization().WithOrganizationName(OrganizationName).Open();
                }

                var notOk = false;

                foreach (var organizationField in BasicOrganizationFields)
                {
                    var valuesAreEqual = organizationField.Value == organizationField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = (organizationField.Value == null) && string.IsNullOrEmpty(organizationField.RecordViewPageFieldValue);

                    if (valuesAreEqual || valuesAreBothEmpty) continue;

                    Report.Report.ToLogFile(MessageType.Message,
                        $"Field: {organizationField.Label} has value='{organizationField.RecordViewPageFieldValue}' but was expected to have value='{organizationField.Value}'",
                        null);
                    notOk = true;

                }

                foreach (var organizationField in ExtraOrganizationFields)
                {
                    var valuesAreEqual = organizationField.Value == organizationField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = string.IsNullOrEmpty(organizationField.Value) && string.IsNullOrEmpty(organizationField.RecordViewPageFieldValue);

                    if (!valuesAreEqual && !valuesAreBothEmpty)
                    {
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {organizationField.Label} has value='{organizationField.RecordViewPageFieldValue}' but was expected to have value='{organizationField.Value}'", null);
                        notOk = true;
                    }
                    else if (valuesAreBothEmpty && organizationField.RecordViewPageIsFieldVisible)
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {organizationField.Label} has no value but its field is shown in contact's detail view page with value '{organizationField.RecordViewPageFieldValue}'", null);
                }

//                foreach (var organizationField in BooleanOrganizationFields)
//                {
//                    if (organizationField.Value == null && organizationField.RecordViewPageFieldValue == "True") continue;
//                    if (organizationField.Value == organizationField.RecordViewPageFieldValue) continue;
//
//                    Report.Report.ToLogFile(MessageType.Message, $"Field: {organizationField.Label} has value='{organizationField.RecordViewPageFieldValue}' but was expected to have value='{organizationField.Value}'", null);
//                    notOk = true;
//                }

                return !notOk;
            }
        }

        /// <summary>
        /// Returns true if organization was saved successfully on its creation.
        /// </summary>
        public bool IsOrganizationCreatedSuccessfully { get; set; }

        /// <summary>
        /// Returns true if organization was saved successfully after edit.
        /// </summary>
        public bool IsOrganizationSavedAfterEdit { get; set; }

        /// <summary>
        /// Copies field values from a given object to the object that calls the method
        /// </summary>
        /// <param name="tmp">The object to be copied</param>
        public void Clone(Organization tmp)
        {
            SetFieldValue(OrganizationFields.OrganizationName, tmp.GetFieldValue(OrganizationFields.OrganizationName));
            SetFieldValue(OrganizationFields.Phone, tmp.GetFieldValue(OrganizationFields.Phone));
            SetFieldValue(OrganizationFields.Email, tmp.GetFieldValue(OrganizationFields.Email));
            SetFieldValue(OrganizationFields.Fax, tmp.GetFieldValue(OrganizationFields.Fax));
            SetFieldValue(OrganizationFields.Website, tmp.GetFieldValue(OrganizationFields.Website));
            SetFieldValue(OrganizationFields.AllowSms, tmp.GetFieldValue(OrganizationFields.AllowSms));
            SetFieldValue(OrganizationFields.AllowPhones, tmp.GetFieldValue(OrganizationFields.AllowPhones));
            SetFieldValue(OrganizationFields.AllowEmails, tmp.GetFieldValue(OrganizationFields.AllowEmails));
            SetFieldValue(OrganizationFields.PrimaryContact, tmp.GetFieldValue(OrganizationFields.PrimaryContact));

            SetFieldValue(OrganizationFields.Industry, tmp.GetFieldValue(OrganizationFields.Industry));
            SetFieldValue(OrganizationFields.OrganizationType, tmp.GetFieldValue(OrganizationFields.OrganizationType));
            SetFieldValue(OrganizationFields.Profession, tmp.GetFieldValue(OrganizationFields.Profession));
            SetFieldValue(OrganizationFields.Comments, tmp.GetFieldValue(OrganizationFields.Comments));
            SetFieldValue(OrganizationFields.BillingStreet, tmp.GetFieldValue(OrganizationFields.BillingStreet));
            SetFieldValue(OrganizationFields.BillingCity, tmp.GetFieldValue(OrganizationFields.BillingCity));
            SetFieldValue(OrganizationFields.BillingState, tmp.GetFieldValue(OrganizationFields.BillingState));
            SetFieldValue(OrganizationFields.BillingPostalCode, tmp.GetFieldValue(OrganizationFields.BillingPostalCode));
            SetFieldValue(OrganizationFields.BillingCountry, tmp.GetFieldValue(OrganizationFields.BillingCountry));
            SetFieldValue(OrganizationFields.ShippingStreet, tmp.GetFieldValue(OrganizationFields.ShippingStreet));
            SetFieldValue(OrganizationFields.ShippingCity, tmp.GetFieldValue(OrganizationFields.ShippingCity));
            SetFieldValue(OrganizationFields.ShippingState, tmp.GetFieldValue(OrganizationFields.ShippingState));
            SetFieldValue(OrganizationFields.ShippingPostalCode, tmp.GetFieldValue(OrganizationFields.ShippingPostalCode));
            SetFieldValue(OrganizationFields.ShippingCountry, tmp.GetFieldValue(OrganizationFields.ShippingCountry));
            SetFieldValue(OrganizationFields.OtherStreet, tmp.GetFieldValue(OrganizationFields.OtherStreet));
            SetFieldValue(OrganizationFields.OtherCity, tmp.GetFieldValue(OrganizationFields.OtherCity));
            SetFieldValue(OrganizationFields.OtherState, tmp.GetFieldValue(OrganizationFields.OtherState));
            SetFieldValue(OrganizationFields.OtherPostalCode, tmp.GetFieldValue(OrganizationFields.OtherPostalCode));
            SetFieldValue(OrganizationFields.OtherCountry, tmp.GetFieldValue(OrganizationFields.OtherCountry));
        }

        /// <summary>
        /// Initialize Organization Creator properties
        /// </summary>
        public Organization()
        {
            BasicOrganizationFields = new List<RecordField>();
            ExtraOrganizationFields = new List<RecordField>();
            BooleanOrganizationFields = new List<RecordField>();

            BasicOrganizationFields.Add(new RecordField(OrganizationFields.OrganizationName, null, () => OrganizationViewPage.OrganizationName, null));
            BasicOrganizationFields.Add(new RecordField(OrganizationFields.Phone, null, () => OrganizationViewPage.Phone, null));
            BasicOrganizationFields.Add(new RecordField(OrganizationFields.Email, null, () => OrganizationViewPage.Email, null));
            BasicOrganizationFields.Add(new RecordField(OrganizationFields.Fax, null, () => OrganizationViewPage.Fax, null));
            BasicOrganizationFields.Add(new RecordField(OrganizationFields.Website, null, () => OrganizationViewPage.Website, null));
            BasicOrganizationFields.Add(new RecordField(OrganizationFields.OrganizationType, null, () => OrganizationViewPage.OrganizationType, null));

            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.PrimaryContact, null, () => OrganizationViewPage.PrimaryContact, () => OrganizationViewPage.IsPrimaryContactFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.Industry, null, () => OrganizationViewPage.Industry, () => OrganizationViewPage.IsIndustryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.Profession, null, () => OrganizationViewPage.Profession, () => OrganizationViewPage.IsProfessionFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.Comments, null, () => OrganizationViewPage.Comments, () => OrganizationViewPage.IsCommentsFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.BillingStreet, null, () => OrganizationViewPage.BillingStreet, () => OrganizationViewPage.IsBillingStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.BillingCity, null, () => OrganizationViewPage.BillingCity, () => OrganizationViewPage.IsBillingCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.BillingState, null, () => OrganizationViewPage.BillingState, () => OrganizationViewPage.IsBillingStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.BillingPostalCode, null, () => OrganizationViewPage.BillingPostalCode, () => OrganizationViewPage.IsBillingPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.BillingCountry, null, () => OrganizationViewPage.BillingCountry, () => OrganizationViewPage.IsBillingCountryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.ShippingStreet, null, () => OrganizationViewPage.ShippingStreet, () => OrganizationViewPage.IsShippingStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.ShippingCity, null, () => OrganizationViewPage.ShippingCity, () => OrganizationViewPage.IsShippingCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.ShippingState, null, () => OrganizationViewPage.ShippingState, () => OrganizationViewPage.IsShippingStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.ShippingPostalCode, null, () => OrganizationViewPage.ShippingPostalCode, () => OrganizationViewPage.IsShippingPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.ShippingCountry, null, () => OrganizationViewPage.ShippingCountry, () => OrganizationViewPage.IsShippingCountryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.OtherStreet, null, () => OrganizationViewPage.OtherStreet, () => OrganizationViewPage.IsOtherStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.OtherCity, null, () => OrganizationViewPage.OtherCity, () => OrganizationViewPage.IsOtherCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.OtherState, null, () => OrganizationViewPage.OtherState, () => OrganizationViewPage.IsOtherStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.OtherPostalCode, null, () => OrganizationViewPage.OtherPostalCode, () => OrganizationViewPage.IsOtherPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField(OrganizationFields.OtherCountry, null, () => OrganizationViewPage.OtherCountry, () => OrganizationViewPage.IsOtherCountryFieldVisible));

            BooleanOrganizationFields.Add(new RecordField(OrganizationFields.AllowSms, null, () => OrganizationViewPage.AllowSms, null));
            BooleanOrganizationFields.Add(new RecordField(OrganizationFields.AllowPhones, null, () => OrganizationViewPage.AllowPhones, null));
            BooleanOrganizationFields.Add(new RecordField(OrganizationFields.AllowEmails, null, () => OrganizationViewPage.AllowEmails, null));
        }

        /// <summary>
        /// If an organization was created by OrganizationCreator, it is deleted if it hasn't been already.
        /// </summary>
        public void CleanUp()
        {
            try
            {
                if (OrganizationWasCreated)
                {
                    var organizationName = GetFieldValue(OrganizationFields.OrganizationName);
                    LeftSideMenu.GoToOrganizations();
                    OrganizationsPage.FindOrganization()
                        .WithOrganizationName(organizationName)
                        .Delete(DeleteType.OnlyOrganization);
                }
            }
            catch (NoSuchElementException)
            {

            }

        }

        /// <summary>
        /// Sets the new value for a field of OrganizationCreator and then returns that value.
        /// </summary>
        /// <param name="fieldLabel">The field label of field that will have its value changed</param>
        /// <param name="newValue">The new value that will be assigned to the fields value property</param>
        /// <returns>The new value that was assigned to the field</returns>
        internal string SetFieldValue(string fieldLabel, string newValue)
        {
            if (BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else if (ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else if (BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else
                throw new Exception();

            return newValue;
        }

        /// <summary>
        /// Get the current value of the given field
        /// </summary>
        /// <param name="fieldLabel">Contact field label</param>
        /// <returns></returns>
        internal string GetFieldValue(string fieldLabel)
        {
            if (BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value;
            else if (ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value;
            else if (BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).Value;

            throw new Exception();
        }

        /// <summary>
        /// Sets the previous value for a field of OrganizationCreator and then returns that value.
        /// </summary>
        /// <param name="fieldLabel">The field label of field that will have its value changed</param>
        /// <param name="previousValue">The previous value that will be assigned to the fields previous value property</param>
        /// <returns>The new value that was assigned to the field</returns>
        internal string SetFieldPreviousValue(string fieldLabel, string previousValue)
        {
            if (BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else if (ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else if (BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else
                throw new Exception();

            return previousValue;
        }

        /// <summary>
        /// Get the value that was assigned to the given field before it was changed to a new one.
        /// </summary>
        /// <param name="fieldLabel">Contact field label</param>
        /// <returns></returns>
        internal string GetFieldPreviousValue(string fieldLabel)
        {
            if (BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;

            throw new Exception();
        }
    }

    internal class OrganizationFields
    {
        internal const string OrganizationName = "Organization Name";
        internal const string Website = "Website";
        internal const string Fax = "Fax";
        internal const string Email = "Email";
        internal const string Phone = "Phone";
        internal const string Industry = "Industry";
        internal const string OrganizationType = "Organization Type";
        internal const string Profession = "Profession";
        internal const string Comments = "Comments";
        internal const string PrimaryContact = "Primary Contact";

        internal const string AllowSms = "Allow SMS";
        internal const string AllowPhones = "Allow Phones";
        internal const string AllowEmails = "Allow Emails";

        internal const string BillingStreet = "Billing Street";
        internal const string BillingCity = "Billing City";
        internal const string BillingState = "Billing State";
        internal const string BillingPostalCode = "Billing Postal Code";
        internal const string BillingCountry = "Billing Country";
        internal const string ShippingStreet = "Shipping Street";
        internal const string ShippingCity = "Shipping City";
        internal const string ShippingState = "Shipping State";
        internal const string ShippingPostalCode = "Shipping Postal Code";
        internal const string ShippingCountry = "Shipping Country";
        internal const string OtherStreet = "Other Street";
        internal const string OtherCity = "Other City";
        internal const string OtherState = "Other State";
        internal const string OtherPostalCode = "Other Postal Code";
        internal const string OtherCountry = "Other Country";
    }
}
