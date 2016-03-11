using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class ImportRecordsTest : JpbBaseTest
    {

        [TestMethod]
        public void Can_Import_Contact_Template()
        {
            ImportContactsWindow
                 .FromPath("D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\")
                 .ImportFile("Contacts1.xls").Submit();

            ContactsPage.FindContact().WithFirstName("Panagiotis").AndLastName("Mavrogiannis").Delete();

            ContactsPage.GoTo();
            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to show contacts list page");
            VerifyThat.IsFalse(ContactsPage.FindContact().WithFirstName("Panagiotis").AndLastName("Mavrogiannis").Find(), "Previously imported contact failed to be deleted");
        }


        [TestMethod]
        public void Can_Import_Organization_Template()
        {
            OrganizationsPage.GoTo();
            ImportOrganizationsWindow.FromPath(
                "D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\")
                .ImportFile("Organizations1.xls").Submit();

            OrganizationsPage.FindOrganization().WithName("SiEBEN").Delete();

            OrganizationsPage.GoTo();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().WithName("SiEBEN").Find(), "Previously imported organization failed to be deleted");
        }
    }
}
