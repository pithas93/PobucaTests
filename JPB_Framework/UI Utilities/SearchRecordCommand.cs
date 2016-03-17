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

        private void AppendToKeyword(string str)
        {
            if (String.IsNullOrEmpty(keyword)) keyword = str;
            else keyword = keyword + ' ' + str;
        }

        public SearchRecordCommand FirstName(string firstName)
        {
            AppendToKeyword(firstName);
            return this;
        }

        public SearchRecordCommand LastName(string lastName)
        {
            AppendToKeyword(lastName);
            return this;
        }

        public SearchRecordCommand OrganizationName(string organizationName)
        {
            AppendToKeyword(organizationName);
            return this;
        }

        public SearchRecordCommand ContainingKeyword(string key)
        {
            AppendToKeyword(key);
            return this;
        }

        public bool Find()
        {
            Commands.SearchFor(keyword);
            return Commands.FindIfRecordExists(keyword);
        }

        public void Delete()
        {
            Commands.SearchFor(keyword);
            Commands.SelectRecordsMatching(keyword);
            new DeleteRecordCommand().Delete();
        }

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

        public SearchRecordCommand With() { return this; }

        public SearchRecordCommand And() { return this; }
    }
}
