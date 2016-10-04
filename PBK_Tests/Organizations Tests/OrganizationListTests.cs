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
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class OrganizationListTests : PbkBaseTest
    {

        /// <summary>
        /// Check that organizations are sorted by default according to their name
        /// </summary>
        [TestMethod]
        public void Default_Sorting()
        {
            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByNameAscending, "Organizations are not sorted by default according to their names.");

        }

        /// <summary>
        /// Check that Account Type filter options works correctly
        /// </summary>
        [TestMethod]
        public void Filter_Using_Organization_Type()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Consultant).Filter();
            int expectedResult1 = 0;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult1, $"The sum of organizations being displayed with Account Type = {AccountType.Consultant} is different from the expected. OrganizationsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult1}");

            OrganizationsPage.ResetFilters();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Reseller).Filter();
            int expectedResult2 = 10;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult2, $"The sum of organizations being displayed with Account Type = {AccountType.Reseller} is different from the expected. OrganizationsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult2}");

            OrganizationsPage.ResetFilters();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Customer).Filter();
            int expectedResult3 = 82;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult3, $"The sum of organizations being displayed with Account Type = {AccountType.Customer} is different from the expected. OrganizationsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult3}");

            OrganizationsPage.ResetFilters();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Supplier).Filter();
            int expectedResult4 = 7;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult4, $"The sum of organizations being displayed with Account Type = {AccountType.Supplier} is different from the expected. OrganizationsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult4}");

            OrganizationsPage.ResetFilters();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Partner).Filter();
            int expectedResult5 = 1;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult5, $"The sum of organizations being displayed with Account Type = {AccountType.Partner} is different from the expected. OrganizationsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult5}");

        }

        /// <summary>
        /// Check that "Search for Companies" search box in organizations page, works correctly
        /// </summary>
        [TestMethod]
        public void Filter_Organizations_With_SearchBox()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.FindOrganization().ContainingKeyword("koro").Find();
            int expectedResult1 = 2;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult1, $"Search using organization name field, with keyword = 'koro', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult1}");

            OrganizationsPage.FindOrganization().ContainingKeyword("21066").Find();
            int expectedResult2 = 3;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult2, $"Search using phone field, with keyword = '21066', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult2}");

            OrganizationsPage.FindOrganization().ContainingKeyword("@la").Find();
            int expectedResult3 = 6;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult3, $"Search using website name field, with keyword = '@la', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult3}");

            OrganizationsPage.FindOrganization().ContainingKeyword("γο").Find();
            int expectedResult4 = 3;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult4, $"Search using street name field, with keyword = 'γο', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult4}");

            OrganizationsPage.FindOrganization().ContainingKeyword("Καλλιθε").Find();
            int expectedResult5 = 4;
            VerifyThat.AreEqual(OrganizationsPage.TotalOrganizationsCountByLabel, expectedResult5, $"Search using city name field, with keyword = 'Καλλιθε', doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={OrganizationsPage.TotalOrganizationsCountByLabel}, Expected={expectedResult5}");

        }

        /// <summary>
        /// Check that Sort By options sort organizations according to the selected option
        /// </summary>
        [TestMethod]
        public void Sort_Organizations_By_Sort_Options()
        {
            LeftSideMenu.GoToOrganizations();

            OrganizationsPage.SortBy().OrganizationName().Ascending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByNameAscending, "Organization list was expected to be sorted by organization name ascending but, is not");

            OrganizationsPage.SortBy().OrganizationName().Descending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByNameDescending, "Organization list was expected to be sorted by organization name descending but, is not");

            OrganizationsPage.SortBy().City().Ascending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByCityAscending, "Organization list was expected to be sorted by city ascending but, is not");

            OrganizationsPage.SortBy().City().Descending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByCityDescending, "Organization list was expected to be sorted by city descending but, is not");

            OrganizationsPage.SortBy().Profession().Ascending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByProfessionAscending, "Organization list was expected to be sorted by profession ascending but, is not");

            OrganizationsPage.SortBy().Profession().Descending().Sort();
            VerifyThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByProfessionDescending, "Organization list was expected to be sorted by profession descending but, is not");



        }

        /// <summary>
        /// Check that selecting multiple organizations, updates regarding labels accordingly
        /// </summary>
        [TestMethod]
        public void Select_Multiple_Organizations()
        {
            OrganizationsPage.SelectRandomNumberOfOrganizations();
            AssertThat.AreEqual(OrganizationsPage.SelectedOrganizationsCountByLabel, OrganizationsPage.SelectedOrganizationsCount, "The count of selected organizations is not equal with the value of the corresponding label");
        }

        // Check that multiple selection works correctly no matter which sort by options are selected

        // Check that multiple selection works correctly no matter which filter options are selected

        /// <summary>
        /// Check that alphabetical sorting works correctly
        /// </summary>
        [TestMethod]
        public void Search_Using_Side_Alphabet_Bar()
        {
            LeftSideMenu.GoToOrganizations();

            AlphabetSideBar.SelectLetter(LatinAlphabet.RestChars);
            int expectedResult1 = 0;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult1, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult1}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.Z);
            int expectedResult2 = 0;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult2, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult2}");

            AlphabetSideBar.SelectLetter(LatinAlphabet.L);
            int expectedResult3 = 93;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult3, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult3}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.RestChars);
            int expectedResult4 = 200;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult4, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult4}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Α);
            int expectedResult5 = 0;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult5, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult5}");

            AlphabetSideBar.SelectLetter(GreekAlphabet.Β);
            int expectedResult6 = 0;
            VerifyThat.AreEqual(ContactsPage.TotalContactsCountByLabel, expectedResult6, $"Alphabet side bar doesn't work. The sum of contacts being displayed is different from the expected. ContactsDisplayed={ContactsPage.TotalContactsCount}, Expected={expectedResult6}");

        }

    }
}
