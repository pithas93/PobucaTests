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
    public class ContactListTests : PbkBaseTest
    {
           
        /// <summary>
        /// Check that default sorting works properly
        /// </summary>
        [TestMethod]
        public void Default_Sorting()
        {
            
            AssertThat.IsTrue(ContactsPage.IsContactListSortedByFirstNameAscending, "Contacts are not sorted by default according to their first/last name.");
        }

        /// <summary>
        /// Check that search filter 'Filter By' works properly
        /// </summary>
        [TestMethod]
        public void Filter_Using_Filter_By()
        {
            ContactsPage.FilterBy().SelectingAllowEmail().Filter();
            var expectedResult1 = 85;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult1, $"The sum of contacts being displayed, with Allow Emails = True, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult1}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingAllowSMS().Filter();
            var expectedResult2 = 75;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult2, $"The sum of contacts being displayed, with Allow SMS = True, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult2}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingAllowPhones().Filter();
            var expectedResult3 = 49;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult3, $"The sum of contacts being displayed, with Allow Phones = True, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult3}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingOrphans().Filter();
            var expectedResult4 = 17;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult4, $"The sum of orphan contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult4}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingDepartment(Department.Logistics).Filter();
            var expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult5, $"The sum of contacts being displayed and belong to Logistics department, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult5}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingDepartment(Department.Consulting).Filter();
            var expectedResult6 = 57;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult6, $"The sum of contacts being displayed and belong to Consulting department, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult6}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy().SelectingAllowEmail().SelectingDepartment(Department.RnD).Filter();
            var expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult7, $"The sum of contacts being displayed, with Allow Email = True and belong to RnD department, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult7}");
            ContactsPage.ResetFilters();

            ContactsPage.FilterBy()
                .SelectingAllowSMS()
                .SelectingAllowEmail()
                .SelectingDepartment(Department.Sales)
                .SelectingDepartment(Department.Administration)

                .Filter();
            var expectedResult8 = 6;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult8, $"The sum of contacts being displayed, with Allow SMS and Emails and belong either to the Sales or Administration departments, is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult8}");

        }

        /// <summary>
        /// Check that search filter 'Search for Contact' works properly
        /// </summary>
        [TestMethod]
        public void Search_Using_Searchbox()
        {
            ContactsPage.FindContact().ContainingKeyword("lavi").Find();
            int expectedResult1 = 1;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult1, $"Search using organization field, with keyword = 'lavi', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult1}");

            ContactsPage.FindContact().ContainingKeyword("παπα").Find();
            int expectedResult2 = 9;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult2, $"Search using surname field, with keyword = 'παπα', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult2}");

            ContactsPage.FindContact().ContainingKeyword("21066").Find();
            int expectedResult3 = 1;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult3, $"Search using phone field, with keyword = '21066', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult3}");

            ContactsPage.FindContact().ContainingKeyword("αργυρουπ").Find();
            int expectedResult4 = 18;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult4, $"Search using city field, with keyword = 'αργυρουπ', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult4}");

            ContactsPage.FindContact().ContainingKeyword("βουλιαγμ").Find();
            int expectedResult5 = 13;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult5, $"Search using street field, with keyword = 'βουλιαγμ', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult5}");

            ContactsPage.FindContact().ContainingKeyword("roma").Find();
            int expectedResult6 = 21;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult6, $"Search using country field, with keyword = 'roma', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult6}");

            ContactsPage.FindContact().ContainingKeyword("director").Find();
            int expectedResult7 = 7;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult7, $"Search using job title field, with keyword = 'director', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult7}");

            ContactsPage.FindContact().ContainingKeyword("kosmocar.gr").Find();
            int expectedResult8 = 17;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCount, expectedResult8, $"Search using website field, with keyword = 'kosmocar.gr', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult8}");

        }

        /// <summary>
        /// Check that sorting works ok when using 'Sort By'
        /// </summary>
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


        /// <summary>
        /// Check that multiple selection works properly
        /// </summary>
        [TestMethod]
        public void Select_Multiple_Contacts()
        {
            ContactsPage.SelectRandomNumberOfContacts();
            AssertThat.AreEqual(ContactsPage.SelectedContactsCount, ContactsPage.SelectedContactsCountByLabel, "The count of selected contacts is not equal with the value of the corresponding label");
        }
 
        /// <summary>
        /// Check that search using side alphabet bar works properly
        /// </summary>
        [TestMethod]
        public void Search_Using_Side_Alphabet_Bar()
        {

            // Alphabet choice '#' was removed for Latin Alphabet

            //            AlphabetSideBar.SelectLetter(LatinAlphabet.RestChars);
            //            int expectedResult1 = 168;
            //            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult1, $"Alphabet side bar doesn't work using character '#' for latin alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult1}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.Z);
            int expectedResult2 = 0;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult2, $"Alphabet side bar doesn't work using character 'Z' of latin alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult2}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.P);
            int expectedResult3 = 2;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult3, $"Alphabet side bar doesn't work using character 'P' of latin alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult3}");

            // Alphabet choice '#' was removed for Greek Alphabet

            //            AlphabetSideBar.SelectLetter(GreekAlphabet.RestChars);
            //            int expectedResult4 = 35;
            //            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult4, $"Alphabet side bar doesn't work using character '#' for greek alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult4}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Α);
            int expectedResult5 = 4;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult5, $"Alphabet side bar doesn't work using character 'A' for greek alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult5}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Β);
            int expectedResult6 = 2;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult6, $"Alphabet side bar doesn't work using character 'B' for greek alphabet. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult6}");

        }
 
        /// <summary>
        /// Check that "filter by" options are not checked and they are in the correct order
        /// </summary>
        [TestMethod]
        public void Filter_By_Options_Are_In_Correct_Initial_State()
        {
            AssertThat.IsTrue(ContactsPage.AreFilterByOptionsInCorrectState, "Filter By filters are not in the correct initial state");
        }
 
        /// <summary>
        /// Check that departments from filterby are placed alphabetically And in the correct order
        /// </summary>
        [TestMethod]
        public void Filter_By_Departments_In_Correct_Initial_State()
        {
            AssertThat.IsTrue(ContactsPage.AreFilterByDepartmentsInCorrectState, "Filter By departments list is not in the correct initial state");
        }

        /// <summary>
        /// Check that the contacts displayed count is the same with the value displayed in the corresponding label
        /// </summary>
        [TestMethod]
        public void Check_Contacts_Displayed_Is_Equal_To_Label()
        {
            AssertThat.AreEqual(ContactsPage.TotalContactsCountByLabel, ContactsPage.TotalContactsCount, "The count of contacts being displayed is not equal with the value of the corresponding label");
        }
    }
}
