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
    }

}
