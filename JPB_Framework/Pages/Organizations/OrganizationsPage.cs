using JPB_Framework.Navigation;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations");

        /// <summary>
        /// Returns true if organizations list is loaded properly
        /// </summary>
        public static bool IsOrganizationListLoaded => Driver.CheckIfRecordListIsLoaded();

        public static bool IsOrganizationListSortedByNameAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.OrganizationName, SortOrganizationsCommand.SortOrder.Ascending);

        public static bool IsOrganizationListSortedByNameDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.OrganizationName, SortOrganizationsCommand.SortOrder.Descending);

        public static bool IsOrganizationListSortedByCityAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.City, SortOrganizationsCommand.SortOrder.Ascending);

        public static bool IsOrganizationListSortedByCityDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.City, SortOrganizationsCommand.SortOrder.Descending);

        public static bool IsOrganizationListSortedByProfessionAscending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.Profession, SortOrganizationsCommand.SortOrder.Ascending);

        public static bool IsOrganizationListSortedByProfessionDescending => Driver.CheckIfRecordListIsSortedBy(SortOrganizationsCommand.SortField.Profession, SortOrganizationsCommand.SortOrder.Descending);

        /// <summary>
        /// The total number of organizations being displayed by the organization list according to the corresponding label on the page
        /// </summary>
        public static int OrganizationsBeingDisplayed => Driver.GetRecordListCount();

        /// <summary>
        /// The total number of organizations being selected in the organization list according to the corresponding label on the page
        /// </summary>
        public static int OrganizationsBeingSelected => Driver.GetSelectedRecordsCount();

        /// <summary>
        /// Selects an organization from the list. By default selects the first one
        /// </summary>
        public static void OpenOrganization()
        {
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        /// Check if organizations exists matching given organization name
        /// </summary>
        /// <returns>True if there is at least one such organization</returns>
        public static SearchOrganizationCommand FindOrganization()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            return new SearchOrganizationCommand();

        }

        /// <summary>
        /// Issue a FilterBy command. Selects the filterby button from contact list page to reveal the filterby options
        /// </summary>
        /// <returns>A command upon which the filterby criteria are being build</returns>
        public static FilterOrganizationsCommand FilterBy()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            Commands.ClickAccountTypeFilter();
            return new FilterOrganizationsCommand();
        }

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
        public static int SelectRandomNumberOfOrganizations()
        {
            if (!IsAt) LeftSideMenu.GoToOrganizations();
            return Commands.SelectRandomNumberOfRecords();
        }
    }

}
