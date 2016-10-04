using System;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class DeleteRecordCommand
    {
        /// <summary>
        /// Clicks the delete button
        /// </summary>
        public DeleteRecordCommand()
        {
            Commands.ClickDelete();
        }

    }

    public class DeleteOrganizationCommand : DeleteRecordCommand
    {


        /// <summary>
        /// Delete organization and its assigned contacts through organization view page or selected organizations through organizations list page
        /// </summary>
        public void WithContacts()
        {
            var deleteAllBtn =
                Driver.Instance.FindElement(
                    By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[1]"));
            deleteAllBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Delete only the organization through organization view page or selected organizations through organizations list page. Its/Their assigned contacts will become orphan.
        /// </summary>
        public void OnlyOrganization()
        {
            var deleteOnlyOrganizationBtn =
                Driver.Instance.FindElement(
                    By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[2]"));
            deleteOnlyOrganizationBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }
    }

    public class DeleteContactCommand : DeleteRecordCommand
    {
        /// <summary>
        /// Delete contact through contact view page or selected contacts through contacts list page
        /// </summary>
        public void Delete()
        {
            var deleteBtn =
                Driver.Instance.FindElement(
                    By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[1]/div[2]/div/div[2]/div[2]/button[1]"));
            deleteBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }
    }

public enum DeleteType
    {
        WithContacts,
        OnlyOrganization
    }
}
