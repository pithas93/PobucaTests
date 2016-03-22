using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Selenium;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ContactListTests : JpbBaseTest
    {
        // Tests to be added
        /*
            Test that checks that filter by option are not checked And they are in the correct order
                1. Favorites
                2. Department
                3. Allow Email
                4. Allo SMS
                5. Allow Phones
                6. Orphans

            Test that departments from filterby are placed alphabetically And in the correct order

            Test that checks that the record list count is the same with the value displayed in the corresponding label

    */



        // 1. Check that default sorting works properly
        [TestMethod]
        public void Default_Sorting()
        {
            AssertThat.IsTrue(ContactsPage.IsContactListSortedByFirstNameAscending, "Contacts are not sorted by default according to their first/last name.");
        }

        // 2. Check that search filter 'Filter By' works properly
        [TestMethod]
        public void Filter_Using_Filter_By()
        {
            ContactsPage.FilterBy().AllowEmail().Filter();
            int expectedResult1 = 85;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult1, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult1}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().AllowSMS().Filter();
            int expectedResult2 = 75;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult2, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult2}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().AllowPhones().Filter();
            int expectedResult3 = 49;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult3, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult3}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().Orphans().Filter();
            int expectedResult4 = 17;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult4, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult4}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().DepartmentIs(Department.Logistics).Filter();
            int expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult5, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult5}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().DepartmentIs(Department.Consulting).Filter();
            int expectedResult6 = 57;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult6, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult6}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy().AllowEmail().And().DepartmentIs(Department.RnD).Filter();
            int expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult7, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult7}");
            ContactsPage.GoTo();

            ContactsPage.FilterBy()
                .AllowSMS()
                .Or()
                .AllowEmail()
                .And()
                .DepartmentIs(Department.Sales)
                .Or()
                .DepartmentIs(Department.Administration)
                .Filter();
            int expectedResult8 = 22;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult8, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult8}");

        }

        // 3. Check that search filter 'Search for Contact' works properly
        [TestMethod]
        public void Search_Using_Searchbox()
        {
            ContactsPage.FindContacts().ContainingKeyword("lavi").Find();
            int expectedResult1 = 1;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult1, $"Search using organization field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult1}");

            ContactsPage.FindContacts().ContainingKeyword("παπα").Find();
            int expectedResult2 = 9;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult2, $"Search using surname field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult2}");

            ContactsPage.FindContacts().ContainingKeyword("21066").Find();
            int expectedResult3 = 1;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult3, $"Search using phone field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult3}");

            ContactsPage.FindContacts().ContainingKeyword("αργυρουπ").Find();
            int expectedResult4 = 18;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult4, $"Search using city field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult4}");

            ContactsPage.FindContacts().ContainingKeyword("βουλιαγμ").Find();
            int expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult5, $"Search using street field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult5}");

            ContactsPage.FindContacts().ContainingKeyword("roma").Find();
            int expectedResult6 = 21;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult6, $"Search using country field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult6}");

            ContactsPage.FindContacts().ContainingKeyword("director").Find();
            int expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult7, $"Search using job title field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult7}");

            ContactsPage.FindContacts().ContainingKeyword("kosmocar.gr").Find();
            int expectedResult8 = 17;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult8, $"Search using website field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult8}");

        }

        // 4. Check that sorting works ok when using 'Sort By'
        [TestMethod]
        public void Sort_Contacts_Using_Sort_By()
        {
            ContactsPage.SortBy().FirstName().Ascending().Sort();
            VerifyThat.IsTrue(ContactsPage.IsContactListSortedByFirstNameAscending, "Contact list was expected to be sorted by first name ascending but, is not");

            ContactsPage.SortBy().FirstName().Descending().Sort();
            VerifyThat.IsTrue(ContactsPage.IsContactListSortedByFirstNameDescending, "Contact list was expected to be sorted by first name descending but, is not");

            ContactsPage.SortBy().LastName().Ascending().Sort();
            VerifyThat.IsTrue(ContactsPage.IsContactListSortedByLastNameAscending, "Contact list was expected to be sorted by last name ascending but, is not");

            ContactsPage.SortBy().LastName().Descending().Sort();
            VerifyThat.IsTrue(ContactsPage.IsContactListSortedByLastNameDescending, "Contact list was expected to be sorted by last name descending but, is not");
        }

        // 5. Check that multiple selection works properly
        [TestMethod]
        public void Select_Multiple_Contacts()
        {
            AssertThat.AreEqual(ContactsPage.SelectRandomNumberOfContacts(), ContactsPage.ContactsBeingSelected, "The count of selected contacts is not equal with the value of the corresponding label");
        }

        // 6. Check that multiple selection works properly in conjuction with sorting

        // 7. Check that multiple selection works properly in conjuction with search filters

        // 8. Check that search box works properly

    }
}
