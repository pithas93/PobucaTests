using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class DeleteRecordCommand
    {

        public DeleteRecordCommand()
        {
            Commands.ClickDelete();
        }
        
        /// <summary>
        /// Delete contact through contact view page or selected contacts through contacts list page
        /// </summary>
        public void Delete()
        {
            var deleteBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[1]"));
            deleteBtn.Click();
        }

        /// <summary>
        /// Delete organization and its assigned contacts through organization view page or selected organizations through organizations list page
        /// </summary>
        public void WithContacts()
        {
            var deleteAllBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[1]"));
            deleteAllBtn.Click();
        }

        /// <summary>
        /// Delete only the organization through organization view page or selected organizations through organizations list page. Its/Their assigned contacts will become orphan.
        /// </summary>
        public void OnlyOrganization()
        {
            var deleteOnlyOrganizationBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[2]"));
            deleteOnlyOrganizationBtn.Click();
        }

    }
}
