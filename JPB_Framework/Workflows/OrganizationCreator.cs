using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Workflows
{
    public class OrganizationCreator
    {

        private static List<RecordField> BasicOrganizationFields;
        private static List<RecordField> ExtraOrganizationFields;
        private static List<RecordField> BooleanOrganizationFields; 
        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";

        public static string OrganizationName => GetFieldValue("Organization Name");

        /// <summary>
        /// If an organization was created during test execution, returns true. 
        /// </summary>
        public static bool OrganizationWasCreated
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
        public static bool AreOrganizationFieldValuesSavedCorrectly
        {
            get
            {
                if (!OrganizationViewPage.IsAt)
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
                    var valuesAreBothEmpty = (organizationField.Value == null) && string.IsNullOrEmpty(organizationField.RecordViewPageFieldValue);

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
                    string expectedValue;
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
        public static bool IsOrganizationCreatedSuccessfully => NewOrganizationPage.IsOrganizationSavedSuccessfully;

        /// <summary>
        /// Returns true if organization was saved successfully after edit.
        /// </summary>
        public static bool IsOrganizationSavedAfterEdit => EditOrganizationPage.IsOrganizationSavedSuccessfully;

        /// <summary>
        /// Returns true if organization was imported successfully.
        /// </summary>
        public static bool IsOrganizationImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;

        /// <summary>
        /// Initialize Organization Creator properties
        /// </summary>
        public static void Initialize()
        {
            BasicOrganizationFields = new List<RecordField>();
            ExtraOrganizationFields = new List<RecordField>();
            BooleanOrganizationFields = new List<RecordField>();

            BasicOrganizationFields.Add(new RecordField("Organization Name", null, () => OrganizationViewPage.OrganizationName, null));
            BasicOrganizationFields.Add(new RecordField("Phone", null, () => OrganizationViewPage.Phone, null));
            BasicOrganizationFields.Add(new RecordField("Email", null, () => OrganizationViewPage.Email, null));
            BasicOrganizationFields.Add(new RecordField("Fax", null, () => OrganizationViewPage.Fax, null));
            BasicOrganizationFields.Add(new RecordField("Website", null, () => OrganizationViewPage.Website, null));

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
        public static void CleanUp()
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
        private static string SetFieldValue(string fieldLabel, string newValue)
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
        private static string GetFieldValue(string fieldLabel)
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
        private static string SetFieldPreviousValue(string fieldLabel, string previousValue)
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
        private static string GetFieldPreviousValue(string fieldLabel)
        {
            if (BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BasicOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return ExtraOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return BooleanOrganizationFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;

            throw new Exception();
        }


        

        /// <summary>
        /// Create a simple organization with dummy organization name and phone values. 
        /// </summary>
        public static void CreateSimpleOrganization()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var phone = SetFieldValue("Phone", DummyData.PhoneValue);

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();
        }

        /// <summary>
        /// Create an organization that has values assigned in every single field.
        /// </summary>
        public static void CreateOrganizationWithAllValues()
        {
            SetFieldValue("Organization Name", DummyData.SimpleWord);
            SetFieldValue("Phone", DummyData.PhoneValue);
            SetFieldValue("Email", DummyData.EmailValue);
            SetFieldValue("Fax", DummyData.PhoneValue);
            SetFieldValue("Website", DummyData.SimpleWord);
            SetFieldValue("Allow SMS", DummyData.BooleanValue);
            SetFieldValue("Allow Phones", DummyData.BooleanValue);
            SetFieldValue("Allow Emails", DummyData.BooleanValue);

            SetFieldValue("Industry", DummyData.IndustryValue);
            SetFieldValue("Account Type", DummyData.AccountTypeValue);
            SetFieldValue("Profession", DummyData.SimpleWord);
            SetFieldValue("Comments", DummyData.SimpleText);
            SetFieldValue("Billing Street", DummyData.AddressValue);
            SetFieldValue("Billing City", DummyData.SimpleWord);
            SetFieldValue("Billing State", DummyData.SimpleWord);
            SetFieldValue("Billing Postal Code", DummyData.NumericValue);
            SetFieldValue("Billing Country", DummyData.CountryValue);
            SetFieldValue("Shipping Street", DummyData.AddressValue);
            SetFieldValue("Shipping City", DummyData.SimpleWord);
            SetFieldValue("Shipping State", DummyData.SimpleWord);
            SetFieldValue("Shipping Postal Code", DummyData.NumericValue);
            SetFieldValue("Shipping Country", DummyData.CountryValue);
            SetFieldValue("Other Street", DummyData.AddressValue);
            SetFieldValue("Other City", DummyData.SimpleWord);
            SetFieldValue("Other State", DummyData.SimpleWord);
            SetFieldValue("Other Postal Code", DummyData.NumericValue);
            SetFieldValue("Other Country", DummyData.CountryValue);

            NewOrganizationPage.CreateOrganization()
                .WithMultipleValues(BasicOrganizationFields, ExtraOrganizationFields, BooleanOrganizationFields)
                .Create();
        }

        /// <summary>
        /// Edit a simple organization changing its name and phone values to new dummy ones.
        /// </summary>
        public static void EditSimpleOrganization()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));
            SetFieldPreviousValue("Phone", GetFieldValue("Phone"));

            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var phone = SetFieldValue("Phone", DummyData.PhoneValue);

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
            SetFieldValue("Phone", GetFieldPreviousValue("Phone"));
        }

        /// <summary>
        /// Import a simple organization with dummy organization and phone values.
        /// </summary>
        public static void ImportSimpleContact()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations1.xls").Submit();

            if(!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Phone", "2130179000");
        }

       
    }
}
