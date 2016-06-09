using System;
using JPB_Framework.Navigation;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations");

        /// <summary>
        /// Returns the number of organizations contained in the organization list currently displayed
        /// </summary>
        public static int TotalOrganizationsCount => Commands.TotalRecordsCount();

        /// <summary>
        /// Returns the number of selected organizations contained in the organization list currently displayed
        /// </summary>
        public static int SelectedOrganizationsCount => Commands.SelectedRecordsCount();

        /// <summary>
        /// Returns the value of label showing the total number of organizations currently displayed
        /// </summary>
        public static int TotalOrganizationsCountByLabel => Commands.TotalRecordsCountByLabel();

        /// <summary>
        /// Returns the value of label showing the number of selected organizations
        /// </summary>
        public static int SelectedOrganizationsCountByLabel => Commands.SelectedRecordsCountByLabel();

        /// <summary>
        /// Returns true if organizations list is loaded properly
        /// </summary>
        public static bool IsOrganizationListLoaded => Driver.CheckIfRecordListIsLoaded();

        /// <summary>
        /// Returns true if organization list is sorted by organization name ascendingly
        /// </summary>
        public static bool IsOrganizationListSortedByNameAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.OrganizationName, SortOrganizationsCommand.SortOrder.Ascending);

        /// <summary>
        /// Returns true if organization list is sorted by organization name descendingly
        /// </summary>
        public static bool IsOrganizationListSortedByNameDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.OrganizationName, SortOrganizationsCommand.SortOrder.Descending);

        /// <summary>
        /// Returns true if organization list is sorted by city name ascendingly
        /// </summary>
        public static bool IsOrganizationListSortedByCityAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.City, SortOrganizationsCommand.SortOrder.Ascending);

        /// <summary>
        /// Returns true if organization list is sorted by city name descendingly
        /// </summary>
        public static bool IsOrganizationListSortedByCityDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.City, SortOrganizationsCommand.SortOrder.Descending);

        /// <summary>
        /// Returns true if organization list is sorted by profession ascendingly
        /// </summary>
        public static bool IsOrganizationListSortedByProfessionAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.Profession, SortOrganizationsCommand.SortOrder.Ascending);

        /// <summary>
        /// Returns true if organization list is sorted by profession descendingly
        /// </summary>
        public static bool IsOrganizationListSortedByProfessionDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.Profession, SortOrganizationsCommand.SortOrder.Descending);

        

        /// <summary>
        /// Opens the first organization from the list to view its details
        /// </summary>
        public static void OpenFirstOrganization()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if organizations exists matching given organization name
        /// </summary>
        /// <returns>True if there is at least one such organization</returns>
        public static SearchOrganizationCommand FindOrganization()
        {
            return new SearchOrganizationCommand(LeftSideMenu.GoToOrganizations);
        }

        /// <summary>
        /// Issue a FilterBy command. Selects the filterby button from organization list page to reveal the filterby options
        /// </summary>
        /// <returns>A command upon which the filterby criteria are being build</returns>
        public static FilterOrganizationsCommand FilterBy()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            Commands.ClickAccountTypeFilter();
            return new FilterOrganizationsCommand();
        }

        /// <summary>
        /// Issue a SortBy command. Selects the sortby button from organization list page to reveal the sortby options
        /// </summary>
        /// <returns>A command upon which the sortby criteria are being build</returns>
        public static SortOrganizationsCommand SortBy()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            Commands.ClickSortBy();
            return new SortOrganizationsCommand();
        }

        /// <summary>
        /// Selects a random number of up to 20 organizations from a list of no more than 40 organizations. 
        /// </summary>
        /// <returns>The count of contacts that where selected</returns>
        public static void SelectRandomNumberOfOrganizations()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            Commands.SelectRandomNumberOfRecords(0);
        }

        /// <summary>
        /// If browser is at Organization List Page and there are filters set, it clears those filters
        /// </summary>
        public static void ResetFilters()
        {
            if (IsAt)
                Commands.ResetFilters();
            else
            {
                throw new Exception();
            }
        }
    }

}
