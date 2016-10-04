using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Report;
using JPB_Framework.Pages.Coworkers;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.CoworkersTests
{
    [TestClass]
    public class CoworkerListTests : PbkBaseTest
    {

        /// <summary>
        /// Check that default sorting works properly
        /// </summary>
        [TestMethod]
        public void Default_Sorting()
        {
            LeftSideMenu.GoToCoworkers();
            AssertThat.IsTrue(CoworkersPage.IsCoworkerListSortedByFirstNameAscending, "Coworkers are not sorted by default according to their first/last name.");
        }


        /// <summary>
        /// Check that search filter 'Departments' works properly
        /// </summary>
        [TestMethod]
        public void Filter_Using_Department_Filter()
        {
            CoworkersPage.FilterBy().SelectingDepartment(Department.RnD).Filter();
            var expectedResult1 = 4;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult1,
                $"The sum of contacts being displayed, with Allow Emails = True, is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult1}");
            CoworkersPage.ResetFilters();

            CoworkersPage.FilterBy().SelectingDepartment(Department.Administration).SelectingDepartment(Department.Consulting).Filter();
            var expectedResult2 = 5;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult2,
                $"The sum of contacts being displayed, with Allow Emails = True, is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult2}");
            CoworkersPage.ResetFilters();
        }

        /// <summary>
        /// Check that search filter 'Search for Coworkers' works properly
        /// </summary>
        [TestMethod]
        public void Search_Using_Searchbox()
        {
            CoworkersPage.FindCoworker().ContainingKeyword("γιάννης").Find();
            int expectedResult1 = 3;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult1,
                $"Search using first name field, with keyword = 'γιάννης', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult1}");

            CoworkersPage.FindCoworker().ContainingKeyword("Γιωργος").Find();
            int expectedResult2 = 3;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult2,
                $"Search using first name field, with keyword = 'Γιωργος', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult2}");

            CoworkersPage.FindCoworker().ContainingKeyword("research").Find();
            int expectedResult3 = 4;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult3,
                $"Search using department field, with keyword = 'research', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult3}");

            CoworkersPage.FindCoworker().ContainingKeyword("13332423").Find();
            int expectedResult4 = 1;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult4,
                $"Search using mobile phone field, with keyword = '13332423', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult4}");

            CoworkersPage.FindCoworker().ContainingKeyword("koko").Find();
            int expectedResult5 = 1;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCount, expectedResult5,
                $"Search using email field, with keyword = 'koko', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCount}, Expected={expectedResult5}");

        }

        /// <summary>
        /// Check that sorting works ok when using 'Sort By'
        /// </summary>
        [TestMethod]
        public void Sort_Coworkers_Using_Sort_By()
        {
            CoworkersPage.SortBy().LastName().Ascending().Sort();
            VerifyThat.IsTrue(CoworkersPage.IsCoworkerListSortedByLastNameAscending, "Coworker list was expected to be sorted by last name ascending but, is not");

            CoworkersPage.SortBy().FirstName().Descending().Sort();
            VerifyThat.IsTrue(CoworkersPage.IsCoworkerListSortedByFirstNameDescending, "Coworker list was expected to be sorted by first name descending but, is not");

            CoworkersPage.SortBy().FirstName().Ascending().Sort();
            VerifyThat.IsTrue(CoworkersPage.IsCoworkerListSortedByFirstNameAscending, "Coworker list was expected to be sorted by first name ascending but, is not");

            CoworkersPage.SortBy().LastName().Descending().Sort();
            VerifyThat.IsTrue(CoworkersPage.IsCoworkerListSortedByLastNameDescending, "Coworker list was expected to be sorted by last name descending but, is not");
            
        }


        /// <summary>
        /// Check that search using side alphabet bar works properly
        /// </summary>
        [TestMethod]
        public void Search_Using_Side_Alphabet_Bar()
        {
            LeftSideMenu.GoToCoworkers();

            AlphabetSideBar.SelectLetter(LatinAlphabet.R);
            int expectedResult1 = 1;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, expectedResult1, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCountByLabel}, Expected={expectedResult1}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.RestChars);
            int expectedResult2 = 10;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, expectedResult2, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCountByLabel}, Expected={expectedResult2}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.RestChars);
            int expectedResult4 = 1;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, expectedResult4, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCountByLabel}, Expected={expectedResult4}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Κ);
            int expectedResult5 = 2;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, expectedResult5, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCountByLabel}, Expected={expectedResult5}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Φ);
            int expectedResult6 = 1;
            VerifyThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, expectedResult6, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={CoworkersPage.TotalCoworkersCountByLabel}, Expected={expectedResult6}");

        }

        /// <summary>
        /// Check that departments filter are placed alphabetically And in the correct order
        /// </summary>
        [TestMethod]
        public void Departments_Filter_Is_In_Correct_Initial_State()
        {
            AssertThat.IsTrue(CoworkersPage.AreFilterByDepartmentsInCorrectState, "Departments filter list is not in the correct initial state");
        }

        /// <summary>
        /// Check that the coworkers displayed count is the same with the value displayed in the corresponding label
        /// </summary>
        [TestMethod]
        public void Check_Coworkers_Displayed_Is_Equal_To_Label()
        {
            LeftSideMenu.GoToCoworkers();
            AssertThat.AreEqual(CoworkersPage.TotalCoworkersCountByLabel, CoworkersPage.TotalCoworkersCount, "The count of coworkers being displayed is not equal with the value of the corresponding label");
        }
    }
}
