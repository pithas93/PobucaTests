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

        public static bool IsOrganizationListSortedByNameAsceding => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.OrganizationName,SortContactsCommand.SortOrder.Ascending);

        public static bool IsOrganizationListSortedByNameDesceding => Driver.CheckIfRecordListIsSortedBy(SortContactsCommand.SortField.OrganizationName, SortContactsCommand.SortOrder.Descending);

        public static int OrganizationsBeingDisplayed => Driver.GetRecordListCount();

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
    }

}
