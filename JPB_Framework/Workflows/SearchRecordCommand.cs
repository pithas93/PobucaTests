using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPB_Framework.UI_Utilities
{
    public class SearchRecordCommand
    {
        private string keyword;

        public SearchRecordCommand()
        {
            keyword = "";
        }

        /// <summary>
        /// Append str to the existing value of keyword that will be used by the search command
        /// </summary>
        /// <param name="str"></param>
        public void AppendToKeyword(string str)
        {
            if (String.IsNullOrEmpty(keyword)) keyword = str;
            else keyword = keyword + ' ' + str;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public SearchRecordCommand WithFirstName(string firstName)
        {
            AppendToKeyword(firstName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for contacts with specific last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public SearchRecordCommand AndLastName(string lastName)
        {
            AppendToKeyword(lastName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for organizations with specific organization name
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        public SearchRecordCommand WithOrganizationName(string organizationName)
        {
            AppendToKeyword(organizationName);
            return this;
        }

        /// <summary>
        /// Direct the search command to search for records with specific keywords in their name
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SearchRecordCommand ContainingKeyword(string key)
        {
            AppendToKeyword(key);
            return this;
        }

        /// <summary>
        /// Direct the search command to execute itself
        /// </summary>
        /// <returns>Returns true if at least one record exists in the record list after the search execution</returns>
        public bool Find()
        {
            Commands.SearchFor(keyword);
            return Commands.FindIfRecordExists(keyword);
        }

        /// <summary>
        /// Applicable only for Contacts. Direct the search command to execute itself and then issue a delete command on the contacts returned by the search execution
        /// </summary>
        public void Delete()
        {
            Commands.SearchFor(keyword);
            Commands.SelectRecordsMatching(keyword);
            new DeleteRecordCommand().Delete();
        }

        /// <summary>
        /// Applicable only for Organizations. Direct the search command to execute itself and then issue a delete command on the organizations returned by the search execution
        /// </summary>
        /// <param name="option"> Defines if contacts linked to the organization will be also deleted or if the will become orphan</param>
        public void Delete(DeleteType option)
        {
            Commands.SearchFor(keyword);
            Commands.SelectRecordsMatching(keyword);
            var command = new DeleteRecordCommand();
            switch (option)
            {
                case DeleteType.OnlyOrganization:
                    {
                        command.OnlyOrganization();
                        break;
                    }
                case DeleteType.WithContacts:
                    {
                        command.WithContacts();
                        break;
                    }
                default:
                    {
                        command.OnlyOrganization();
                        break;
                    }
            }

        }

    }
}
