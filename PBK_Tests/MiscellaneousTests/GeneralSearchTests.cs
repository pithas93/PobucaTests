using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Coworkers;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.MiscellaneousTests
{
    [TestClass]
    public class GeneralSearchTests : PbkBaseTest
    {
        /// <summary>
        /// Assert that when selecting an individual record, browser navigates to its view page
        /// </summary>
        [TestMethod]
        public void Individual_Record_Selection_From_General_Search_Navigates_To_Record_View()
        {

            UpperToolBar.UseGeneralSearch().WithKeyword("Mattia Pelloni").Select();
            VerifyThat.IsTrue(ContactViewPage.IsAt,"Contact view page should be displayed but is not.");

            UpperToolBar.UseGeneralSearch().WithKeyword("Kolonaki Center").Select();
            VerifyThat.IsTrue(OrganizationViewPage.IsAt, "Organization view page should be displayed but is not.");

            UpperToolBar.UseGeneralSearch().WithKeyword("Γεράσιμος Λυμπεράτος").Select();
            VerifyThat.IsTrue(CoworkerViewPage.IsAt, "Coworker view page should be displayed but is not.");
        }
        
        /// <summary>
        /// Assert that general search retrieves results from all three entities and with correct number of results
        /// </summary>
        [TestMethod]
        public void General_Search_Returns_Correct_Results_From_All_Entities()
        {

            var expectedResult1 = 5;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("Kor").GetContactCount(), expectedResult1, "Number of contacts returned from search is incorrect.");

            var expectedResult2 = 2;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("2310").GetContactCount(), expectedResult2, "Number of contacts returned from search is incorrect.");

            var expectedResult3 = 2;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("leroymerlin").GetContactCount(), expectedResult3, "Number of contacts returned from search is incorrect.");

            var expectedResult4 = 3;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("Zurich").GetContactCount(), expectedResult4, "Number of contacts returned from search is incorrect.");

            var expectedResult5 = 2;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("Project Manager").GetContactCount(), expectedResult5, "Number of contacts returned from search is incorrect.");


            var expectedResult6 = 2;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("www.mas").GetOrganizationCount(), expectedResult6, "Number of contacts returned from search is incorrect.");

            var expectedResult7 = 2;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("Παπανδρέου").GetOrganizationCount(), expectedResult7, "Number of contacts returned from search is incorrect.");


            var expectedResult8 = 4;
            VerifyThat.AreEqual(UpperToolBar.UseGeneralSearch().WithKeyword("Research").GetCoworkerCount(), expectedResult8, $"Number of contacts returned from search is incorrect.");


        }

        /// <summary>
        /// Assert that where clicking on See All Results button in general search results, navigates browser to the respective record list page
        /// </summary>
        [TestMethod]
        public void Navigate_To_Record_Lists_Through_See_All_Results()
        {

            UpperToolBar.UseGeneralSearch().WithKeyword("K").AndSeeResultsForContacts();
            VerifyThat.IsTrue(ContactsPage.IsAt, "Browser should have navigate at contact list page");

            UpperToolBar.UseGeneralSearch().WithKeyword("K").AndSeeResultsForOrganizations();
            VerifyThat.IsTrue(OrganizationsPage.IsAt, "Browser should have navigate at organization list page");

            UpperToolBar.UseGeneralSearch().WithKeyword("K").AndSeeResultsForCoworkers();
            VerifyThat.IsTrue(CoworkersPage.IsAt, "Browser should have navigate at coworker list page");
        }
    }
}
