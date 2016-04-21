using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Organizations
{
    public class EditOrganizationPage
    {
        /// <summary>
        /// Check if browser is at organization's edit page 
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Organizations  /  Organization  /  Edit Organization");

        /// <summary>
        ///  Navigates browser through the available edit button to the organization's edit page
        /// </summary>
        public static void GoTo() { Commands.ClickEdit(); }

    }
}
