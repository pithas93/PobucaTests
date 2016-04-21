using JPB_Framework;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ContactListTests : JpbBaseTest
    {
        // Tests to be added
        /*

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
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().AllowSMS().Filter();
            int expectedResult2 = 75;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult2, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult2}");
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().AllowPhones().Filter();
            int expectedResult3 = 49;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult3, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult3}");
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().Orphans().Filter();
            int expectedResult4 = 17;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult4, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult4}");
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().DepartmentIs(Department.Logistics).Filter();
            int expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult5, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult5}");
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().DepartmentIs(Department.Consulting).Filter();
            int expectedResult6 = 57;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult6, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult6}");
            LeftSideMenu.GoToContacts();

            ContactsPage.FilterBy().AllowEmail().And().DepartmentIs(Department.RnD).Filter();
            int expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult7, $"The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult7}");
            LeftSideMenu.GoToContacts();

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
            ContactsPage.FindContact().ContainingKeyword("lavi").Find();
            int expectedResult1 = 1;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult1, $"Search using organization field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult1}");

            ContactsPage.FindContact().ContainingKeyword("παπα").Find();
            int expectedResult2 = 9;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult2, $"Search using surname field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult2}");

            ContactsPage.FindContact().ContainingKeyword("21066").Find();
            int expectedResult3 = 1;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult3, $"Search using phone field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult3}");

            ContactsPage.FindContact().ContainingKeyword("αργυρουπ").Find();
            int expectedResult4 = 18;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult4, $"Search using city field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult4}");

            ContactsPage.FindContact().ContainingKeyword("βουλιαγμ").Find();
            int expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult5, $"Search using street field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult5}");

            ContactsPage.FindContact().ContainingKeyword("roma").Find();
            int expectedResult6 = 21;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult6, $"Search using country field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult6}");

            ContactsPage.FindContact().ContainingKeyword("director").Find();
            int expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult7, $"Search using job title field, doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult7}");

            ContactsPage.FindContact().ContainingKeyword("kosmocar.gr").Find();
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

        // 8. Check that search using side alphabet bar works properly
        [TestMethod]
        public void Search_Using_Side_Alphabet_Bar()
        {
            AlphabetSideBar.SelectLetter(LatinAlphabet.RestChars);
            int expectedResult1 = 168;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult1, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult1}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.Z);
            int expectedResult2 = 0;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult2, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult2}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.P);
            int expectedResult3 = 2;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult3, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult3}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.RestChars);
            int expectedResult4 = 35;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult4, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult4}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Α);
            int expectedResult5 = 4;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult5, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult5}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Β);
            int expectedResult6 = 2;
            VerifyThat.AreEqual(ContactsPage.ContactsBeingDisplayed, expectedResult6, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.ContactsBeingDisplayed}, Expected={expectedResult6}");

        }

        // 9. Check that "filter by" options are not checked and they are in the correct order
        [TestMethod]
        public void Filter_By_Options_Are_In_Correct_Initial_State()
        {
            AssertThat.IsTrue(ContactsPage.AreFilterByOptionsInCorrectState, "Filter By filters are not in the correct initial state");
        }

        // 10. Check that departments from filterby are placed alphabetically And in the correct order
        [TestMethod]
        public void Filter_By_Departments_In_Correct_Initial_State()
        {
            AssertThat.IsTrue(ContactsPage.AreFilterByDepartmentsInCorrectState, "Filter By departments list is not in the correct initial state");
        }

        // 11. Check that the contacts displayed count is the same with the value displayed in the corresponding label
        [TestMethod]
        public void Check_Contacts_Displayed_Is_Equal_To_Label()
        {
            AssertThat.AreEqual(ContactsPage.TotalContactsCount, ContactsPage.ContactsBeingDisplayed, "The count of contacts being displayed is not equal with the value of the corresponding label");
        }
    }
}
