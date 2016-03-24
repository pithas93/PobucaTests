﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class ImportRecordsTests : JpbBaseTest
    {

        [TestMethod]
        public void Can_Import_Contact_Template()
        {
            ImportContactsWindow.FromPath(ImportFilePath).ImportFile("Contacts1.xls").Submit();

            ContactsPage.FindContacts().With().FirstName("Panagiotis").And().LastName("Mavrogiannis").Delete();

            LeftSideMenu.GoToContacts();
            AssertThat.IsTrue(ContactsPage.IsAt, "Failed to show contacts list page");
            VerifyThat.IsFalse(ContactsPage.FindContacts().With().FirstName("Panagiotis").And().LastName("Mavrogiannis").Find(), "Previously imported contact failed to be deleted");
        }


        [TestMethod]
        public void Can_Import_Organization_Template()
        {
            LeftSideMenu.GoToOrganizations();
            ImportOrganizationsWindow.FromPath(ImportFilePath).ImportFile("Organizations1.xls").Submit();

            OrganizationsPage.FindOrganization().With().OrganizationName("SiEBEN").Delete();

            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsAt, "Failed to show organizations list page");
            VerifyThat.IsFalse(OrganizationsPage.FindOrganization().With().OrganizationName("SiEBEN").Find(), "Previously imported organization failed to be deleted");
        }
    }
}
