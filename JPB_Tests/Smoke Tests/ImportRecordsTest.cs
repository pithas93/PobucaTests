using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
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
//           ImportContactsWindow
//                .FromPath("D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\")
//                .ImportFile("Contacts1.xls").Submit();
            
            ContactsPage.FindContactWithFirstName("Panagiotis").AndLastName("Mavrogiannis").Delete();
                        //ContactViewPage.DeleteContact().Delete();
            //
            //Assert.IsTrue(ContactsPage.IsAt, "Failed to show organizations page");
            //Assert.IsFalse(ContactsPage.FindContactWithFirstName("Panagiotis").AndLastName("Mavrogiannis").Find());
        }


        [TestMethod]
        public void Can_Import_Organization_Template()
        {
            OrganizationsPage.GoTo();
            ImportOrganizationsWindow.GoTo();
            ImportOrganizationsWindow
                .FromPath("D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\")
                .ImportFile("Organizations1.xls").Submit();
        }
    }
}
