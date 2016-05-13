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

        public static string PrimaryContact => GetFieldValue("Primary Contact");

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
        /// Create an organization with phone value but without value in organization name field
        /// </summary>
        public static void CreateOrganizationWithoutOrganizationName()
        {
            var phone = SetFieldValue("Phone", DummyData.PhoneValue);

            NewOrganizationPage.CreateOrganization()
                .WithPhone(phone)
                .Create();
        }

        /// <summary>
        /// Create an organization with organization name and phone values that exceed the 50 characters
        /// </summary>
        public static void CreateOrganizationWithOverflowValues()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.OverflowValue);
            var phone = SetFieldValue("Phone", DummyData.OverflowValue);

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();
        }

        /// <summary>
        /// Create an organization with organization name and phone values that are nonsense
        /// </summary>
        public static void CreateOrganizationWithNonsenseValues()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.NonsenseValue);
            var phone = SetFieldValue("Phone", DummyData.NonsenseValue);

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();
        }

        /// <summary>
        /// Create an organization with normal organization name and phone values. During creation, billing street and profession extra fields are added but are left empty upon saving.
        /// </summary>
        public static void CreateOrganizationWithNullValuesInExtraFields()
        {
            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var phone = SetFieldValue("Phone", DummyData.PhoneValue);
            var billingStreet = SetFieldValue("Billing Street", string.Empty);
            var profession = SetFieldValue("Profession", string.Empty);

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .WithBillingStreet(billingStreet)
                .WithProfession(profession)
                .Create();

        }

        /// <summary>
        /// Edit a simple organization changing its name and phone values to new dummy ones.
        /// </summary>
        public static void EditSimpleOrganization()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

            var organizationName = SetFieldValue("Organization Name", DummyData.SimpleWord);
            var phone = SetFieldValue("Phone", DummyData.PhoneValue);

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
        }

        /// <summary>
        /// Edit an existing organization, deleting organization name field value before saving.
        /// </summary>
        public static void EditOrganizationRemovingOrganizationName()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

            var organizationName = SetFieldValue("Organization Name", string.Empty);

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
        }

        /// <summary>
        /// Edit an existing organization, assigning new organization name and phone values that exceed 50 characters
        /// </summary>
        public static void EditOrganizationAssigningOverflowValues()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

            var organizationName = SetFieldValue("Organization Name", DummyData.OverflowValue);
            var phone = SetFieldValue("Phone", DummyData.OverflowValue);

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
        }

        /// <summary>
        /// Edit an existing organization, assigning new organization name and phone values that are nonsense
        /// </summary>
        public static void EditOrganizationAssigningNonsenseValues()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

            var organizationName = SetFieldValue("Organization Name", DummyData.NonsenseValue);
            var phone = SetFieldValue("Phone", DummyData.NonsenseValue);

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
        }

        /// <summary>
        /// Edit an existing organization, assigning new values to every field
        /// </summary>
        public static void EditOrganizationAlteringAllValues()
        {
            SetFieldPreviousValue("Organization Name", GetFieldValue("Organization Name"));

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

            EditOrganizationPage.EditOrganization()
                .WithMultipleNewValues(BasicOrganizationFields,ExtraOrganizationFields,BooleanOrganizationFields)
                .Edit();

            if (EditOrganizationPage.IsOrganizationSavedSuccessfully) return;
            SetFieldValue("Organization Name", GetFieldPreviousValue("Organization Name"));
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

        /// <summary>
        /// Import a file that contains 1 organization that has values for all its fields
        /// </summary>
        public static void ImportOrganizationWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Phone", "2130179000");
            SetFieldValue("Email", "sieben@sieben.gr");
            SetFieldValue("Fax", "2130179001");
            SetFieldValue("Website", "http://www.sieben.gr");
            SetFieldValue("Allow SMS", "False");
            SetFieldValue("Allow Phones", "False");
            SetFieldValue("Allow Emails", "True");

            SetFieldValue("Primary Contact", "Massimiliano Amato");
            SetFieldValue("Industry", "Consulting");
            SetFieldValue("Account Type", "Consultant");
            SetFieldValue("Profession", "Informatics");
            SetFieldValue("Comments", "Sieben on the rocks");
            SetFieldValue("Billing Street", "Aristomenous 3");
            SetFieldValue("Billing City", "Gerakas");
            SetFieldValue("Billing State", "Attica");
            SetFieldValue("Billing Postal Code", "10442");
            SetFieldValue("Billing Country", "Greece");
            SetFieldValue("Shipping Street", "Aristomenous 2");
            SetFieldValue("Shipping City", "Pallini");
            SetFieldValue("Shipping State", "Thessalonica");
            SetFieldValue("Shipping Postal Code", "10443");
            SetFieldValue("Shipping Country", "Greece");
            SetFieldValue("Other Street", "Armigado 2");
            SetFieldValue("Other City", "Valencia");
            SetFieldValue("Other State", "Deportivo");
            SetFieldValue("Other Postal Code", "15016");
            SetFieldValue("Other Country", "Spain");
        }

        /// <summary>
        /// Import a file that contains 1 organization that has value for phone field but no value for organization name
        /// </summary>
        public static void ImportOrganizationWithoutOrganizationName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Phone", "1234567890");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that are nonsense
        /// </summary>
        public static void ImportOrganizationWithNonsenseValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations5.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "!@#qweQWE123");
            SetFieldValue("Phone", "!@#qweQWE123");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that exceed 50 characters
        /// </summary>
        public static void ImportOrganizationWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");
            SetFieldValue("Phone", "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");


        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that is linked to another organization, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatBelongsToAnotherOrganization()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Primary Contact", "Carja Ramona");
        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that does not exist within contact list, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatDoesNotExist()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations15.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains less field columns than normal
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Phone", "2130179000");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains more field columns than normal
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations9.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Phone", "2130179000");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file has its field columns in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations13.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Organization Name", "SiEBEN");
            SetFieldValue("Phone", "2130179000");
            SetFieldValue("Email", "sieben@sieben.gr");
            SetFieldValue("Website", "http://www.sieben.gr");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file misses its organization name field column
        /// </summary>
        public static void ImportTemplateWithoutOrganizationNameColumn()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            SetFieldValue("Email", "sieben@sieben.gr");
            SetFieldValue("Website", "http://www.sieben.gr");

        }

    }
}
