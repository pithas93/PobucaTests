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

        public string OrganizationName => GetFieldValue("Organization Name");

        public string PrimaryContact => GetFieldValue("Primary Contact");

        /// <summary>
        /// If an organization was created during test execution, returns true. 
        /// </summary>
        public bool OrganizationWasCreated
        {
            get
            {
                var organizationName = BasicOrganizationFields.Find(x => x.Label.Contains("Organization Name")).Value;

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

                foreach (var organizationField in BooleanOrganizationFields)
                {
                    if (organizationField.Value == null && organizationField.RecordViewPageFieldValue == "True") continue;
                    if (organizationField.Value == organizationField.RecordViewPageFieldValue) continue;

                    Report.Report.ToLogFile(MessageType.Message, $"Field: {organizationField.Label} has value='{organizationField.RecordViewPageFieldValue}' but was expected to have value='{organizationField.Value}'", null);
                    notOk = true;
                }

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
            SetFieldValue("Organization Name", tmp.GetFieldValue("Organization Name"));
            SetFieldValue("Phone", tmp.GetFieldValue("Phone"));
            SetFieldValue("Email", tmp.GetFieldValue("Email"));
            SetFieldValue("Fax", tmp.GetFieldValue("Fax"));
            SetFieldValue("Website", tmp.GetFieldValue("Website"));
            SetFieldValue("Allow SMS", tmp.GetFieldValue("Allow SMS"));
            SetFieldValue("Allow Phones", tmp.GetFieldValue("Allow Phones"));
            SetFieldValue("Allow Emails", tmp.GetFieldValue("Allow Emails"));

            SetFieldValue("Industry", tmp.GetFieldValue("Industry"));
            SetFieldValue("Account Type", tmp.GetFieldValue("Account Type"));
            SetFieldValue("Profession", tmp.GetFieldValue("Profession"));
            SetFieldValue("Comments", tmp.GetFieldValue("Comments"));
            SetFieldValue("Billing Street", tmp.GetFieldValue("Billing Street"));
            SetFieldValue("Billing City", tmp.GetFieldValue("Billing City"));
            SetFieldValue("Billing State", tmp.GetFieldValue("Billing State"));
            SetFieldValue("Billing Postal Code", tmp.GetFieldValue("Billing Postal Code"));
            SetFieldValue("Billing Country", tmp.GetFieldValue("Billing Country"));
            SetFieldValue("Shipping Street", tmp.GetFieldValue("Shipping Street"));
            SetFieldValue("Shipping City", tmp.GetFieldValue("Shipping City"));
            SetFieldValue("Shipping State", tmp.GetFieldValue("Shipping State"));
            SetFieldValue("Shipping Postal Code", tmp.GetFieldValue("Shipping Postal Code"));
            SetFieldValue("Shipping Country", tmp.GetFieldValue("Shipping Country"));
            SetFieldValue("Other Street", tmp.GetFieldValue("Other Street"));
            SetFieldValue("Other City", tmp.GetFieldValue("Other City"));
            SetFieldValue("Other State", tmp.GetFieldValue("Other State"));
            SetFieldValue("Other Postal Code", tmp.GetFieldValue("Other Postal Code"));
            SetFieldValue("Other Country", tmp.GetFieldValue("Other Country"));
        }

        /// <summary>
        /// Initialize Organization Creator properties
        /// </summary>
        public Organization()
        {
            BasicOrganizationFields = new List<RecordField>();
            ExtraOrganizationFields = new List<RecordField>();
            BooleanOrganizationFields = new List<RecordField>();

            BasicOrganizationFields.Add(new RecordField("Organization Name", null, () => OrganizationViewPage.OrganizationName, null));
            BasicOrganizationFields.Add(new RecordField("Phone", null, () => OrganizationViewPage.Phone, null));
            BasicOrganizationFields.Add(new RecordField("Email", null, () => OrganizationViewPage.Email, null));
            BasicOrganizationFields.Add(new RecordField("Fax", null, () => OrganizationViewPage.Fax, null));
            BasicOrganizationFields.Add(new RecordField("Website", null, () => OrganizationViewPage.Website, null));

            ExtraOrganizationFields.Add(new RecordField("Primary Contact", null, () => OrganizationViewPage.PrimaryContact, () => OrganizationViewPage.IsPrimaryContactFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Industry", null, () => OrganizationViewPage.Industry, () => OrganizationViewPage.IsIndustryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Account Type", null, () => OrganizationViewPage.AccountType, () => OrganizationViewPage.IsAccountTypeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Profession", null, () => OrganizationViewPage.Profession, () => OrganizationViewPage.IsProfessionFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Comments", null, () => OrganizationViewPage.Comments, () => OrganizationViewPage.IsCommentsFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Billing Street", null, () => OrganizationViewPage.BillingStreet, () => OrganizationViewPage.IsBillingStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Billing City", null, () => OrganizationViewPage.BillingCity, () => OrganizationViewPage.IsBillingCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Billing State", null, () => OrganizationViewPage.BillingState, () => OrganizationViewPage.IsBillingStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Billing Postal Code", null, () => OrganizationViewPage.BillingPostalCode, () => OrganizationViewPage.IsBillingPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Billing Country", null, () => OrganizationViewPage.BillingCountry, () => OrganizationViewPage.IsBillingCountryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Shipping Street", null, () => OrganizationViewPage.ShippingStreet, () => OrganizationViewPage.IsShippingStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Shipping City", null, () => OrganizationViewPage.ShippingCity, () => OrganizationViewPage.IsShippingCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Shipping State", null, () => OrganizationViewPage.ShippingState, () => OrganizationViewPage.IsShippingStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Shipping Postal Code", null, () => OrganizationViewPage.ShippingPostalCode, () => OrganizationViewPage.IsShippingPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Shipping Country", null, () => OrganizationViewPage.ShippingCountry, () => OrganizationViewPage.IsShippingCountryFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Other Street", null, () => OrganizationViewPage.OtherStreet, () => OrganizationViewPage.IsOtherStreetFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Other City", null, () => OrganizationViewPage.OtherCity, () => OrganizationViewPage.IsOtherCityFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Other State", null, () => OrganizationViewPage.OtherState, () => OrganizationViewPage.IsOtherStateFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Other Postal Code", null, () => OrganizationViewPage.OtherPostalCode, () => OrganizationViewPage.IsOtherPostalCodeFieldVisible));
            ExtraOrganizationFields.Add(new RecordField("Other Country", null, () => OrganizationViewPage.OtherCountry, () => OrganizationViewPage.IsOtherCountryFieldVisible));

            BooleanOrganizationFields.Add(new RecordField("Allow SMS", null, () => OrganizationViewPage.AllowSms, null));
            BooleanOrganizationFields.Add(new RecordField("Allow Phones", null, () => OrganizationViewPage.AllowPhones, null));
            BooleanOrganizationFields.Add(new RecordField("Allow Emails", null, () => OrganizationViewPage.AllowEmails, null));
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
                    var organizationName = GetFieldValue("Organization Name");
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
}
