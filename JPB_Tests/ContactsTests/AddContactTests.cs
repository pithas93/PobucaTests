using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class AddContactTests : JpbBaseTest
    {


        // 1. Create contact with all fields filled
        [TestMethod]
        public void Create_Contact_With_All_Fields_Filled()
        {
            NewContactPage.CreateDummyContact();
            VerifyThat.IsTrue(ContactViewPage.AreContactFieldValuesCorrect, "Contact field values are not the ones expected.");

            ContactViewPage.DeleteContact().Delete();
            VerifyThat.IsFalse(ContactsPage.FindDummyContacts(), "Previously created dummy contact was not deleted successfully");
        }

        // 2. Create contact from within existing organization

        // 3. Create contact without assigning values in mandatory field

        // 4. Create contact assinging field values that exceed character overflow limit

        // 5. Create contact with nonsense field values

        // 6. Create contact with invalid birthdate value

        // 7. Create contact with random field values in combo fields

        // 8. Create contact linked with non-existent organization

        // 9. Create an orphan contact

        // 10. Add an extra field and save contact without assigning value to the added field
    }
}
