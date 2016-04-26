using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Organizations_Tests
{
    [TestClass]
    public class OrganizationListTests : JpbBaseTest
    {

        // Check that organizations are sorted by default according to their name
        [TestMethod]
        public void Default_Sorting()
        {
            LeftSideMenu.GoToOrganizations();
            AssertThat.IsTrue(OrganizationsPage.IsOrganizationListSortedByNameAsceding, "Organizations are not sorted by default according to their names.");
        }

        // Check that Account Type filter options works correctly
        [TestMethod]
        public void Filter_Using_Account_Type()
        {
            LeftSideMenu.GoToOrganizations();
            OrganizationsPage.FilterBy().SelectingAccountType(AccountType.Consultant).Filter();
            int expectedResult1 = 22;
            VerifyThat.AreEqual(OrganizationsPage.OrganizationsBeingDisplayed, expectedResult1, $"The sum of organizations being displayed is different from the expected. OrganizationsDisplayed={OrganizationsPage.OrganizationsBeingDisplayed}, Expected={expectedResult1}");

        }

        // Check that "Search for Companies" search box in organizations page, works correctly

        // Check that Sort By options sort organizations according to the selected option

        // Check that selecting multiple organizations, updates regarding labels accordingly

        // Check that multiple selection works correctly no matter which sort by options are selected

        // Check that multiple selection works correctly no matter which filter options are selected

        // Check that alphabetical sorting works correctly

    }
}
