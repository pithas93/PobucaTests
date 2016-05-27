using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Utilities
{
    public class ContactsBaseTest : JpbBaseTest
    {

        [TestInitialize]
        public void SetUp()
        {
            ContactCreator.Initialize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ContactCreator.CleanUp();
//            LeftSideMenu.GoToContacts();
//            AssertThat.AreEqual(ContactsPage.ContactsBeingDisplayed, ContactCreator.InitialContactsCount, $"Contact created by the test, was not deleted or was not found to be deleted! Initially, contact count was {ContactCreator.InitialContactsCount} but, current contact count is {ContactsPage.ContactsBeingDisplayed}");
        }

    }
}
