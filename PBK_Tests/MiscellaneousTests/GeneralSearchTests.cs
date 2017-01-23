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

                        UpperToolBar.UseGeneralSearch().ToFindContact("Mattia Pelloni").Open();
                        VerifyThat.IsTrue(ContactViewPage.IsAt,"Contact view page should be displayed but is not.");
            
                        UpperToolBar.UseGeneralSearch().ToFindOrganization("Kolonaki Center").Open();
                        VerifyThat.IsTrue(OrganizationViewPage.IsAt, "Organization view page should be displayed but is not.");
            
                        UpperToolBar.UseGeneralSearch().ToFindCoworker("Γεράσιμος Λυμπεράτος").Open();
                        VerifyThat.IsTrue(CoworkerViewPage.IsAt, "Coworker view page should be displayed but is not.");
        }

        /// <summary>
        /// Assert that general search retrieves results from all three entities and with correct number of results
        /// </summary>
        [TestMethod]
        public void General_Search_Returns_Correct_Results_From_All_Entities()
        {


            UpperToolBar.UseGeneralSearch().WithKeyword("Kor").Search();
            var expectedResult1 = 5;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchContactsDisplayed, expectedResult1, $"Number of contacts returned from search of is incorrect for keyword 'kor'. Expected={expectedResult1}, Actual={UpperToolBar.GeneralSearchContactsDisplayed}");

            UpperToolBar.UseGeneralSearch().WithKeyword("2310").Search();
            var expectedResult2 = 2;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchContactsDisplayed, expectedResult2, $"Number of contacts returned from search is incorrect for keyword '2310'. Expected={expectedResult2}, Actual={UpperToolBar.GeneralSearchContactsDisplayed}");
            
            UpperToolBar.UseGeneralSearch().WithKeyword("leroymerlin").Search();
            var expectedResult3 = 2;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchContactsDisplayed, expectedResult3, $"Number of contacts returned from search is incorrect for keyword 'leroymerlin'. Expected={expectedResult3}, Actual={UpperToolBar.GeneralSearchContactsDisplayed}");
            
            UpperToolBar.UseGeneralSearch().WithKeyword("Zurich").Search();
            var expectedResult4 = 3;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchContactsDisplayed, expectedResult4, $"Number of contacts returned from search is incorrect for keyword 'Zurich'. Expected={expectedResult4}, Actual={UpperToolBar.GeneralSearchContactsDisplayed}");
            
            UpperToolBar.UseGeneralSearch().WithKeyword("Project Manager").Search();
            var expectedResult5 = 2;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchContactsDisplayed, expectedResult5, $"Number of contacts returned from search is incorrect for keyword 'Project Manager'. Expected={expectedResult5}, Actual={UpperToolBar.GeneralSearchContactsDisplayed}");
                        
            UpperToolBar.UseGeneralSearch().WithKeyword("www.mas").Search();
            var expectedResult6 = 2;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchOrganizationsDisplayed, expectedResult6, $"Number of contacts returned from search is incorrect for keyword 'www.mas'. Expected={expectedResult6}, Actual={UpperToolBar.GeneralSearchOrganizationsDisplayed}");
            
            UpperToolBar.UseGeneralSearch().WithKeyword("Παπανδρέου").Search();
            var expectedResult7 = 2;
            VerifyThat.AreEqual(UpperToolBar.GeneralSearchOrganizationsDisplayed, expectedResult7, $"Number of contacts returned from search is incorrect for keyword 'Παπανδρέου'. Expected={expectedResult7}, Actual={UpperToolBar.GeneralSearchOrganizationsDisplayed}");

        }

        /// <summary>
        /// Assert that where clicking on See All Results button in general search results, navigates browser to the respective record list page
        /// </summary>
        [TestMethod]
        public void Navigate_To_Record_Lists_Through_See_All_Results()
        {

                        UpperToolBar.UseGeneralSearch().WithKeyword("Παπ").AndSeeResultsForContacts();
                        VerifyThat.IsTrue(ContactsPage.IsAt, "Browser should have navigate at contact list page");
            
                        UpperToolBar.UseGeneralSearch().WithKeyword("Kno").AndSeeResultsForOrganizations();
                        VerifyThat.IsTrue(OrganizationsPage.IsAt, "Browser should have navigate at organization list page");
            
                        UpperToolBar.UseGeneralSearch().WithKeyword("Κοκ").AndSeeResultsForCoworkers();
                        VerifyThat.IsTrue(CoworkersPage.IsAt, "Browser should have navigate at coworker list page");
        }
    }
}
