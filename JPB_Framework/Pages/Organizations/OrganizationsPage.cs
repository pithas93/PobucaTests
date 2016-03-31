using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationsPage
    {
        /// <summary>
        /// Check if browser is at contacts list page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organizations"); } }

        public static bool IsOrganizationListLoaded { get { return Driver.CheckIfRecordListIsLoaded(); } }

       

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
        /// <param name="organization_name"></param>
        /// <returns>True if there is at least one such organization</returns>
        public static SearchOrganizationCommand FindOrganization()
        {
            return new SearchOrganizationCommand();

        }
    }

    public class SearchOrganizationCommand
    {
        private SearchRecordCommand command;

        public SearchOrganizationCommand()
        {
            command = new SearchRecordCommand();
        }

        /// <summary>
        /// Direct the search command to search for organizations with specific organization name
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public SearchOrganizationCommand WithOrganizationName(string organizationName)
        {
            command.WithOrganizationName(organizationName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for organizations with specific keywords in their name
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SearchOrganizationCommand ContainingKeyword(string key)
        {
            command.ContainingKeyword(key);
            return this;

        }

        /// <summary>
        /// Direct the search command to execute itself
        /// </summary>
        /// <returns>Returns true if at least one record exists in the record list after the search execution</returns>
        public bool Find()
        {
            return command.Find();
        }

        /// <summary>
        /// Direct the search command to execute itself and then issue a delete command on the organizations returned by the search execution
        /// </summary>
        /// <param name="deleteType">Defines whether the contacts linked with organization, will be also deleted or not</param>
        public void Delete(DeleteType deleteType)
        {
            command.Delete(deleteType);
        }
    }
}
