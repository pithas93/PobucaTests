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

        private static Organization CurrentOrganization { get; set; }

        private const string ImportFilePath = "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";
        //        private const string ImportFilePath = "C:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\";


        public static void Initialize()
        {
            FirstOrganization = new Organization();
            SecondOrganization = new Organization();
            InitialOrganizationCount = OrganizationsPage.TotalOrganizationsCountByLabel;
        }

        public static void CleanUp()
        {
            FirstOrganization.CleanUp();
            SecondOrganization.CleanUp();
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
        public static bool IsOrganizationImportedSuccessfully => ImportPage.IsImportSuccessMessageShown;

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
            CurrentOrganization.SetFieldValue("Organization Name", organizationName);
            CurrentOrganization.SetFieldValue("Phone", phone);
        }

        /// <summary>
        /// Create an organization that has values assigned in every single field.
        /// </summary>
        public static void CreateOrganizationWithAllValues()
        {
            SetCurrentOrganization();

            var tmp = new Organization();

            tmp.SetFieldValue("Organization Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Email", DummyData.EmailValue);
            tmp.SetFieldValue("Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Website", DummyData.SimpleWord);
            tmp.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            tmp.SetFieldValue("Industry", DummyData.IndustryValue);
            tmp.SetFieldValue("Account Type", DummyData.AccountTypeValue);
            tmp.SetFieldValue("Profession", DummyData.SimpleWord);
            tmp.SetFieldValue("Comments", DummyData.SimpleText);
            tmp.SetFieldValue("Billing Street", DummyData.AddressValue);
            tmp.SetFieldValue("Billing City", DummyData.SimpleWord);
            tmp.SetFieldValue("Billing State", DummyData.SimpleWord);
            tmp.SetFieldValue("Billing Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Billing Country", DummyData.CountryValue);
            tmp.SetFieldValue("Shipping Street", DummyData.AddressValue);
            tmp.SetFieldValue("Shipping City", DummyData.SimpleWord);
            tmp.SetFieldValue("Shipping State", DummyData.SimpleWord);
            tmp.SetFieldValue("Shipping Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Shipping Country", DummyData.CountryValue);
            tmp.SetFieldValue("Other Street", DummyData.AddressValue);
            tmp.SetFieldValue("Other City", DummyData.SimpleWord);
            tmp.SetFieldValue("Other State", DummyData.SimpleWord);
            tmp.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Other Country", DummyData.CountryValue);

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
            CurrentOrganization.SetFieldValue("Phone", phone);

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
            CurrentOrganization.SetFieldValue("Organization Name", organizationName);
            CurrentOrganization.SetFieldValue("Phone", phone);
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
            CurrentOrganization.SetFieldValue("Organization Name", organizationName);
            CurrentOrganization.SetFieldValue("Phone", phone);
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
            CurrentOrganization.SetFieldValue("Organization Name", organizationName);
            CurrentOrganization.SetFieldValue("Phone", phone);
            CurrentOrganization.SetFieldValue("Billing Street", billingStreet);
            CurrentOrganization.SetFieldValue("Profession", profession);

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
            editedOrganization.SetFieldValue("Organization Name", organizationName);
            editedOrganization.SetFieldValue("Phone", phone);
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
            editedOrganization.SetFieldValue("Organization Name", organizationName);
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
             editedOrganization.SetFieldValue("Organization Name",organizationName);
             editedOrganization.SetFieldValue("Phone",phone);
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
            editedOrganization.SetFieldValue("Organization Name", organizationName);
            editedOrganization.SetFieldValue("Phone", phone);
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

            tmp.SetFieldValue("Organization Name", DummyData.SimpleWord);
            tmp.SetFieldValue("Phone", DummyData.PhoneValue);
            tmp.SetFieldValue("Email", DummyData.EmailValue);
            tmp.SetFieldValue("Fax", DummyData.PhoneValue);
            tmp.SetFieldValue("Website", DummyData.SimpleWord);
            tmp.SetFieldValue("Allow SMS", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Phones", DummyData.BooleanValue);
            tmp.SetFieldValue("Allow Emails", DummyData.BooleanValue);

            tmp.SetFieldValue("Industry", DummyData.IndustryValue);
            tmp.SetFieldValue("Account Type", DummyData.AccountTypeValue);
            tmp.SetFieldValue("Profession", DummyData.SimpleWord);
            tmp.SetFieldValue("Comments", DummyData.SimpleText);
            tmp.SetFieldValue("Billing Street", DummyData.AddressValue);
            tmp.SetFieldValue("Billing City", DummyData.SimpleWord);
            tmp.SetFieldValue("Billing State", DummyData.SimpleWord);
            tmp.SetFieldValue("Billing Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Billing Country", DummyData.CountryValue);
            tmp.SetFieldValue("Shipping Street", DummyData.AddressValue);
            tmp.SetFieldValue("Shipping City", DummyData.SimpleWord);
            tmp.SetFieldValue("Shipping State", DummyData.SimpleWord);
            tmp.SetFieldValue("Shipping Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Shipping Country", DummyData.CountryValue);
            tmp.SetFieldValue("Other Street", DummyData.AddressValue);
            tmp.SetFieldValue("Other City", DummyData.SimpleWord);
            tmp.SetFieldValue("Other State", DummyData.SimpleWord);
            tmp.SetFieldValue("Other Postal Code", DummyData.NumericValue);
            tmp.SetFieldValue("Other Country", DummyData.CountryValue);

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

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Phone", "2130179000");
        }

        /// <summary>
        /// Import a file that contains 1 organization that has values for all its fields
        /// </summary>
        public static void ImportOrganizationWithAllValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations2.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Phone", "2130179000");
            FirstOrganization.SetFieldValue("Email", "sieben@sieben.gr");
            FirstOrganization.SetFieldValue("Fax", "2130179001");
            FirstOrganization.SetFieldValue("Website", "http://www.sieben.gr");
            FirstOrganization.SetFieldValue("Allow SMS", "False");
            FirstOrganization.SetFieldValue("Allow Phones", "False");
            FirstOrganization.SetFieldValue("Allow Emails", "True");

            FirstOrganization.SetFieldValue("Industry", "Consulting");
            FirstOrganization.SetFieldValue("Account Type", "Consultant");
            FirstOrganization.SetFieldValue("Profession", "Informatics");
            FirstOrganization.SetFieldValue("Comments", "Sieben on the rocks");
            FirstOrganization.SetFieldValue("Billing Street", "Aristomenous 3");
            FirstOrganization.SetFieldValue("Billing City", "Gerakas");
            FirstOrganization.SetFieldValue("Billing State", "Attica");
            FirstOrganization.SetFieldValue("Billing Postal Code", "10442");
            FirstOrganization.SetFieldValue("Billing Country", "Greece");
            FirstOrganization.SetFieldValue("Shipping Street", "Aristomenous 2");
            FirstOrganization.SetFieldValue("Shipping City", "Pallini");
            FirstOrganization.SetFieldValue("Shipping State", "Thessalonica");
            FirstOrganization.SetFieldValue("Shipping Postal Code", "10443");
            FirstOrganization.SetFieldValue("Shipping Country", "Greece");
            FirstOrganization.SetFieldValue("Other Street", "Armigado 2");
            FirstOrganization.SetFieldValue("Other City", "Valencia");
            FirstOrganization.SetFieldValue("Other State", "Deportivo");
            FirstOrganization.SetFieldValue("Other Postal Code", "15016");
            FirstOrganization.SetFieldValue("Other Country", "Spain");
        }

        /// <summary>
        /// Import a file that contains 1 organization that has value for phone field but no value for organization name
        /// </summary>
        public static void ImportOrganizationWithoutOrganizationName()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations4.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Phone", "1234567890");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that are nonsense
        /// </summary>
        public static void ImportOrganizationWithNonsenseValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations5.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "!@#qweQWE123");
            FirstOrganization.SetFieldValue("Phone", "!@#qweQWE123");

        }

        /// <summary>
        /// Import a file that contains 1 organization with organization name and phone values that exceed 50 characters
        /// </summary>
        public static void ImportOrganizationWithOverflowValues()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations6.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");
            FirstOrganization.SetFieldValue("Phone", "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");


        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that is linked to another organization, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatBelongsToAnotherOrganization()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations14.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Primary Contact", "Carja Ramona");
        }

        /// <summary>
        /// Import a file that contains 1 organization which has a contact that does not exist within contact list, as its primary contact
        /// </summary>
        public static void ImportOrganizationWithPrimaryContactThatDoesNotExist()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations15.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains less field columns than normal
        /// </summary>
        public static void ImportTemplateWithLessColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations8.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Phone", "2130179000");
        }

        /// <summary>
        /// Import a file that contains 1 organization but the file contains more field columns than normal
        /// </summary>
        public static void ImportTemplateWithMoreColumns()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations9.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Phone", "2130179000");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file has its field columns in random order
        /// </summary>
        public static void ImportTemplateWithColumnsInRandomOrder()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations13.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Organization Name", "SiEBEN");
            FirstOrganization.SetFieldValue("Phone", "2130179000");
            FirstOrganization.SetFieldValue("Email", "sieben@sieben.gr");
            FirstOrganization.SetFieldValue("Website", "http://www.sieben.gr");

        }

        /// <summary>
        /// Import a file that contains 1 organization but the file misses its organization name field column
        /// </summary>
        public static void ImportTemplateWithoutOrganizationNameColumn()
        {
            ImportPage.ImportFile().Containing(ImportFileType.Organizations).FromPath(ImportFilePath).WithFileName("Organizations11.xls").Submit();

            if (!ImportPage.IsImportSuccessMessageShown) return;

            FirstOrganization.SetFieldValue("Email", "sieben@sieben.gr");
            FirstOrganization.SetFieldValue("Website", "http://www.sieben.gr");

        }
    }
}

