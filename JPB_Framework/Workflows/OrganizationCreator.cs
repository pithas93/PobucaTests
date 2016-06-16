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
        private static int InitialOrganizationCount { get; set; }
        public static Organization FirstOrganization { get; set; }
        public static Organization SecondOrganization { get; set; }
        public static Organization ThirdOrganization { get; set; }

        private static Organization CurrentOrganization { get; set; }

        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static void Initialize()
        {
            FirstOrganization = new Organization();
            SecondOrganization = new Organization();
            ThirdOrganization = new Organization();
            InitialOrganizationCount = OrganizationsPage.TotalOrganizationsCountByLabel;
        }

        public static void CleanUp()
        {
            FirstOrganization.CleanUp();
            SecondOrganization.CleanUp();
            ThirdOrganization.CleanUp();
            LeftSideMenu.GoToContacts();
            VerifyThat.AreEqual(InitialOrganizationCount, OrganizationsPage.TotalOrganizationsCountByLabel,
                $"Total organizations count is not the same as in the test initiation (Expected={InitialOrganizationCount}, Actual={OrganizationsPage.TotalOrganizationsCountByLabel}). Some organizations may have not been cleaned up at the end of test.");
        }

        /// <summary>
        /// Returns true if contact was saved successfully on its creation.
        /// </summary>
        private static bool IsOrganizationCreatedSuccessfully => NewOrganizationPage.IsOrganizationSavedSuccessfully;

        /// <summary>
        /// Returns true if contact was saved successfully after edit.
        /// </summary>
        private static bool IsOrganizationSavedAfterEdit => EditOrganizationPage.IsOrganizationSavedSuccessfully;

        /// <summary>
        /// Returns true if organization was imported successfully.
        /// </summary>
        public static bool IsOrganizationFileImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;

        /// <summary>
        /// Returns true if organizations were partially imported due to duplicate organization existance.
        /// </summary>
        public static bool IsOrganizationFileImportedWithDuplicates => ImportPage.IsImportWithDuplicatesMessageShown;

        /// <summary>
        /// Returns true if organizations were not import due to some error
        /// </summary>
        public static bool IsOrganizationFileFailedToImport => ImportPage.IsImportFailedMessageShown;

        /// <summary>
        /// Determines which contact will hold the data for the new contact that will be created
        /// </summary>
        private static void SetCurrentOrganization()
        {
            if (string.IsNullOrEmpty(FirstOrganization.OrganizationName))
            {
                CurrentOrganization = FirstOrganization;
                return;
            }
            if (string.IsNullOrEmpty(SecondOrganization.OrganizationName))
            {
                CurrentOrganization = SecondOrganization;
                return;
            }
            if (string.IsNullOrEmpty(ThirdOrganization.OrganizationName))
            {
                CurrentOrganization = ThirdOrganization;
                return;
            }
            throw new Exception();
        }

        /// <summary>
        /// Create a simple organization with dummy organization name and phone values. 
        /// </summary>
        public static void CreateSimpleOrganization()
        {
            SetCurrentOrganization();

            var organizationName = DummyData.SimpleWord;
            var phone = DummyData.PhoneValue;

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            CurrentOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Create an organization that has values assigned in every single field.
        /// </summary>
        public static void CreateOrganizationWithAllValues()
        {
            SetCurrentOrganization();

            var tmp = new Organization();

            tmp.SetFieldValue(OrganizationFields.OrganizationName, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.Phone, DummyData.PhoneValue);
            tmp.SetFieldValue(OrganizationFields.Email, DummyData.EmailValue);
            tmp.SetFieldValue(OrganizationFields.Fax, DummyData.PhoneValue);
            tmp.SetFieldValue(OrganizationFields.Website, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.AllowSms, DummyData.BooleanValue);
            tmp.SetFieldValue(OrganizationFields.AllowPhones, DummyData.BooleanValue);
            tmp.SetFieldValue(OrganizationFields.AllowEmails, DummyData.BooleanValue);

            tmp.SetFieldValue(OrganizationFields.Industry, DummyData.IndustryValue);
            tmp.SetFieldValue(OrganizationFields.OrganizationType, DummyData.OrganizationTypeValue);
            tmp.SetFieldValue(OrganizationFields.Profession, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.Comments, DummyData.SimpleText);
            tmp.SetFieldValue(OrganizationFields.BillingStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.BillingCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.BillingState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.BillingPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.BillingCountry, DummyData.CountryValue);
            tmp.SetFieldValue(OrganizationFields.ShippingStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.ShippingCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.ShippingState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.ShippingPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.ShippingCountry, DummyData.CountryValue);
            tmp.SetFieldValue(OrganizationFields.OtherStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.OtherCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.OtherState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.OtherPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.OtherCountry, DummyData.CountryValue);

            NewOrganizationPage.CreateOrganization()
                .WithMultipleValues(tmp.BasicOrganizationFields, tmp.ExtraOrganizationFields, tmp.BooleanOrganizationFields)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.Clone(tmp);
        }

        /// <summary>
        /// Create an organization with phone value but without value in organization name field
        /// </summary>
        public static void CreateOrganizationWithoutOrganizationName()
        {
            SetCurrentOrganization();

            var phone = DummyData.PhoneValue;

            NewOrganizationPage.CreateOrganization()
                .WithPhone(phone)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.SetFieldValue(OrganizationFields.Phone, phone);

        }

        /// <summary>
        /// Create an organization with organization name and phone values that exceed the 50 characters
        /// </summary>
        public static void CreateOrganizationWithOverflowValues()
        {
            SetCurrentOrganization();

            var organizationName = DummyData.OverflowWordValue;
            var phone = DummyData.OverflowWordValue;

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            CurrentOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Create an organization with organization name and phone values that are nonsense
        /// </summary>
        public static void CreateOrganizationWithNonsenseValues()
        {
            SetCurrentOrganization();

            var organizationName = DummyData.NonsenseValue;
            var phone = DummyData.NonsenseValue;

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            CurrentOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Create an organization with normal organization name and phone values. During creation, billing street and profession extra fields are added but are left empty upon saving.
        /// </summary>
        public static void CreateOrganizationWithNullValuesInExtraFields()
        {
            SetCurrentOrganization();

            var organizationName = DummyData.SimpleWord;
            var phone = DummyData.PhoneValue;
            var billingStreet = string.Empty;
            var profession = string.Empty;

            NewOrganizationPage.CreateOrganization()
                .WithOrganizationName(organizationName)
                .WithPhone(phone)
                .WithBillingStreet(billingStreet)
                .WithProfession(profession)
                .Create();

            CurrentOrganization.IsOrganizationCreatedSuccessfully = IsOrganizationCreatedSuccessfully;

            if (!CurrentOrganization.IsOrganizationCreatedSuccessfully) return;
            CurrentOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            CurrentOrganization.SetFieldValue(OrganizationFields.Phone, phone);
            CurrentOrganization.SetFieldValue(OrganizationFields.BillingStreet, billingStreet);
            CurrentOrganization.SetFieldValue(OrganizationFields.Profession, profession);

        }

        /// <summary>
        /// Edit a simple organization changing its name and phone values to new dummy ones.
        /// </summary>
        public static void EditSimpleOrganization(Organization editedOrganization)
        {
            if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != editedOrganization.OrganizationName)))
            {
                LeftSideMenu.GoToOrganizations();
                OrganizationsPage.FindOrganization().WithOrganizationName(editedOrganization.OrganizationName).Open();
            }

            var organizationName = DummyData.SimpleWord;
            var phone = DummyData.PhoneValue;

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            editedOrganization.IsOrganizationSavedAfterEdit = IsOrganizationSavedAfterEdit;

            if (!editedOrganization.IsOrganizationSavedAfterEdit) return;
            editedOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            editedOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Edit an existing organization, deleting organization name field value before saving.
        /// </summary>
        public static void EditOrganizationRemovingOrganizationName(Organization editedOrganization)
        {
            if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != editedOrganization.OrganizationName)))
            {
                LeftSideMenu.GoToOrganizations();
                OrganizationsPage.FindOrganization().WithOrganizationName(editedOrganization.OrganizationName).Open();
            }

            var organizationName = string.Empty;

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .Edit();

            editedOrganization.IsOrganizationSavedAfterEdit = IsOrganizationSavedAfterEdit;

            if (!editedOrganization.IsOrganizationSavedAfterEdit) return;
            editedOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
        }

        /// <summary>
        /// Edit an existing organization, assigning new organization name and phone values that exceed 50 characters
        /// </summary>
        public static void EditOrganizationAssigningOverflowValues(Organization editedOrganization)
        {
            if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != editedOrganization.OrganizationName)))
            {
                LeftSideMenu.GoToOrganizations();
                OrganizationsPage.FindOrganization().WithOrganizationName(editedOrganization.OrganizationName).Open();
            }

            var organizationName = DummyData.OverflowWordValue;
            var phone = DummyData.OverflowWordValue;

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            editedOrganization.IsOrganizationSavedAfterEdit = IsOrganizationSavedAfterEdit;

            if (!editedOrganization.IsOrganizationSavedAfterEdit) return;
            editedOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            editedOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Edit an existing organization, assigning new organization name and phone values that are nonsense
        /// </summary>
        public static void EditOrganizationAssigningNonsenseValues(Organization editedOrganization)
        {
            if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != editedOrganization.OrganizationName)))
            {
                LeftSideMenu.GoToOrganizations();
                OrganizationsPage.FindOrganization().WithOrganizationName(editedOrganization.OrganizationName).Open();
            }

            var organizationName = DummyData.NonsenseValue;
            var phone = DummyData.NonsenseValue;

            EditOrganizationPage.EditOrganization()
                .WithNewOrganizationName(organizationName)
                .WithNewPhone(phone)
                .Edit();

            editedOrganization.IsOrganizationSavedAfterEdit = IsOrganizationSavedAfterEdit;

            if (!editedOrganization.IsOrganizationSavedAfterEdit) return;
            editedOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);
            editedOrganization.SetFieldValue(OrganizationFields.Phone, phone);
        }

        /// <summary>
        /// Edit an existing organization, assigning new values to every field
        /// </summary>
        public static void EditOrganizationAlteringAllValues(Organization editedOrganization)
        {
            if (!OrganizationViewPage.IsAt || (OrganizationViewPage.IsAt && (OrganizationViewPage.OrganizationName != editedOrganization.OrganizationName)))
            {
                LeftSideMenu.GoToOrganizations();
                OrganizationsPage.FindOrganization().WithOrganizationName(editedOrganization.OrganizationName).Open();
            }

            var tmp = new Organization();

            tmp.SetFieldValue(OrganizationFields.OrganizationName, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.Phone, DummyData.PhoneValue);
            tmp.SetFieldValue(OrganizationFields.Email, DummyData.EmailValue);
            tmp.SetFieldValue(OrganizationFields.Fax, DummyData.PhoneValue);
            tmp.SetFieldValue(OrganizationFields.Website, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.AllowSms, DummyData.BooleanValue);
            tmp.SetFieldValue(OrganizationFields.AllowPhones, DummyData.BooleanValue);
            tmp.SetFieldValue(OrganizationFields.AllowEmails, DummyData.BooleanValue);

            tmp.SetFieldValue(OrganizationFields.Industry, DummyData.IndustryValue);
            tmp.SetFieldValue(OrganizationFields.OrganizationType, DummyData.OrganizationTypeValue);
            tmp.SetFieldValue(OrganizationFields.Profession, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.Comments, DummyData.SimpleText);
            tmp.SetFieldValue(OrganizationFields.BillingStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.BillingCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.BillingState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.BillingPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.BillingCountry, DummyData.CountryValue);
            tmp.SetFieldValue(OrganizationFields.ShippingStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.ShippingCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.ShippingState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.ShippingPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.ShippingCountry, DummyData.CountryValue);
            tmp.SetFieldValue(OrganizationFields.OtherStreet, DummyData.AddressValue);
            tmp.SetFieldValue(OrganizationFields.OtherCity, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.OtherState, DummyData.SimpleWord);
            tmp.SetFieldValue(OrganizationFields.OtherPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(OrganizationFields.OtherCountry, DummyData.CountryValue);

            EditOrganizationPage.EditOrganization()
                .WithMultipleNewValues(tmp.BasicOrganizationFields, tmp.ExtraOrganizationFields, tmp.BooleanOrganizationFields)
                .Edit();

            editedOrganization.IsOrganizationSavedAfterEdit = IsOrganizationSavedAfterEdit;

            if (!editedOrganization.IsOrganizationSavedAfterEdit) return;
            editedOrganization.Clone(tmp);
        }

        /// <summary>
        /// Import a simple organization with dummy organization and phone values.
        /// </summary>
        public static void ImportSimpleContact()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations1.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "2130179000");
        }

        /// <summary>
        /// Import a file that contains 1 organization that has values for all its fields
        /// </summary>
        public static void ImportOrganizationWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "2130179000");
            FirstOrganization.SetFieldValue(OrganizationFields.Email, "sieben@sieben.gr");
            FirstOrganization.SetFieldValue(OrganizationFields.Fax, "2130179001");
            FirstOrganization.SetFieldValue(OrganizationFields.Website, "http://www.sieben.gr");
            FirstOrganization.SetFieldValue(OrganizationFields.AllowSms, "False");
            FirstOrganization.SetFieldValue(OrganizationFields.AllowPhones, "False");
            FirstOrganization.SetFieldValue(OrganizationFields.AllowEmails, "True");

            FirstOrganization.SetFieldValue(OrganizationFields.Industry, "Consulting");
            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationType, "Consultant");
            FirstOrganization.SetFieldValue(OrganizationFields.Profession, "Informatics");
            FirstOrganization.SetFieldValue(OrganizationFields.Comments, "Sieben on the rocks");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingStreet, "Aristomenous 3");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingCity, "Gerakas");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingState, "Attica");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingPostalCode, "10442");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingCountry, "Greece");
            FirstOrganization.SetFieldValue(OrganizationFields.ShippingStreet, "Aristomenous 2");
            FirstOrganization.SetFieldValue(OrganizationFields.ShippingCity, "Pallini");
            FirstOrganization.SetFieldValue(OrganizationFields.ShippingState, "Thessalonica");
            FirstOrganization.SetFieldValue(OrganizationFields.ShippingPostalCode, "10443");
            FirstOrganization.SetFieldValue(OrganizationFields.ShippingCountry, "Greece");
            FirstOrganization.SetFieldValue(OrganizationFields.OtherStreet, "Armigado 2");
            FirstOrganization.SetFieldValue(OrganizationFields.OtherCity, "Valencia");
            FirstOrganization.SetFieldValue(OrganizationFields.OtherState, "Deportivo");
            FirstOrganization.SetFieldValue(OrganizationFields.OtherPostalCode, "15016");
            FirstOrganization.SetFieldValue(OrganizationFields.OtherCountry, "Spain");
        }

        /// <summary>
        /// Import a file that contains 1 organization that has value for phone field but no value for organization name
        /// </summary>
        public static void ImportOrganizationWithoutOrganizationName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "1234567890");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that are nonsense
        /// </summary>
        public static void ImportOrganizationWithNonsenseValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations5.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "!@#qweQWE123");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "!@#qweQWE123");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that exceed 50 characters
        /// </summary>
        public static void ImportOrganizationWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");


        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that is linked to another organization, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatBelongsToAnotherOrganization()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.PrimaryContact, "Carja Ramona");
        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that does not exist within contact list, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatDoesNotExist()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations15.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains less field columns than normal
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "2130179000");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains more field columns than normal
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations9.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "2130179000");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file has its field columns in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations13.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.Phone, "2130179000");
            FirstOrganization.SetFieldValue(OrganizationFields.Email, "sieben@sieben.gr");
            FirstOrganization.SetFieldValue(OrganizationFields.Website, "http://www.sieben.gr");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file misses its organization name field column
        /// </summary>
        public static void ImportTemplateWithoutOrganizationNameColumn()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue(OrganizationFields.Email, "sieben@sieben.gr");
            FirstOrganization.SetFieldValue(OrganizationFields.Website, "http://www.sieben.gr");

        }

        /// <summary>
        /// First creates an organization and then imports an organization template that contains 2 organizations of which 1 has the same organization name with the previously created organization.
        /// Check for duplicate organziation names is made during import
        /// </summary>
        public static void ImportTemplateWithAnExistingOrganization()
        {
            var organizationName = "SiEBEN";
            NewOrganizationPage.CreateOrganization().WithOrganizationName(organizationName).Create();

            if (!NewOrganizationPage.IsOrganizationSavedSuccessfully) return;
            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, organizationName);


            ImportPage.ImportFile()
                .Containing(ImportFileType.Organizations)
                .FromPath(ImportFilePath)
                .WithFileName("Organizations16.xls")
                .CheckingForDuplicate(ImportField.OrganizationName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;
            SecondOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            ThirdOrganization.SetFieldValue(OrganizationFields.OrganizationName, "InEdu");
        }

        /// <summary>
        /// Import an organization template that contains 2 organizations with the same organization name. During import, duplicate organization name checkbox is checked
        /// </summary>
        public static void ImportTemplateWithTwinOrganizations()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Organizations)
                .FromPath(ImportFilePath)
                .WithFileName("Organizations17.xls")
                .CheckingForDuplicate(ImportField.OrganizationName).Submit();

            if (!ImportPage.IsImportWithDuplicatesMessageShown) return;
            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            SecondOrganization.SetFieldValue(OrganizationFields.OrganizationName, "InEdu");
            ThirdOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");

        }

        /// <summary>
        /// Import an organization template that contains 3 organization that have void lines in between them
        /// </summary>
        public static void ImportTemplateWithVoidLinesBetweenOrganizations()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Organizations)
                .FromPath(ImportFilePath)
                .WithFileName("Organizations18.xls")
                .Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;
            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            SecondOrganization.SetFieldValue(OrganizationFields.OrganizationName, "InEdu");
            ThirdOrganization.SetFieldValue(OrganizationFields.OrganizationName, "Microsoft");
        }

        /// <summary>
        /// Import an organization template that contains 2 organization of whom, one has invlaid value for a combo field
        /// </summary>
        public static void ImportTemplateOrganizationWithInvalidComboValues()
        {
            ImportPage.ImportFile()
                .Containing(ImportFileType.Organizations)
                .FromPath(ImportFilePath)
                .WithFileName("Organizations19.xls")
                .Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;
            FirstOrganization.SetFieldValue(OrganizationFields.OrganizationName, "SiEBEN");
            FirstOrganization.SetFieldValue(OrganizationFields.BillingCountry, "Ελλάδα");

            SecondOrganization.SetFieldValue(OrganizationFields.OrganizationName, "InEdu");
            SecondOrganization.SetFieldValue(OrganizationFields.BillingCountry, "Greece");
        }
    }
}

