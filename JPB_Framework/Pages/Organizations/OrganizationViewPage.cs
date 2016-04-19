using System;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationViewPage
    {
        /// <summary>
        /// Check if browser is at the selected organization's detail view page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Organization View");


        public static string OrganizationName
        {
            get
            {
                var organizationName =
                    Driver.Instance.FindElement(
                        By.XPath(
                            "/html/body/div[4]/div/div[2]/div[2]/div[3]/div[2]/div[2]/div[2]/div[1]/div[2]/div[1]/div[1]/my-required-info/div/div/div"));
                if (organizationName != null)
                    return organizationName.Text;
                return string.Empty;
            }
        }

        /// <summary>
        /// Issue delete command from an organization's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteOrganization()
        {
            return new DeleteRecordCommand();
        }

        public static SearchRecordCommand FindContactFromContactList()
        {
            return new SearchRecordCommand();
        }

        public static CreateContactCommand CreateContact()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("a.dropdown-toggle.p-none"));
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
            var str = element.GetAttribute("aria-haspopup");
            if (str.Equals("true"))
            {
                var createNewContactBtn = Driver.Instance.FindElements(By.CssSelector("#related-contacts-section .dropdown-menu.animated.fadeInRight.m-t-xs a"))[1];
                createNewContactBtn.Click();
            }
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "After clicking to add contact to organization, within organization view page, the relative combo box should be expanded, nut it did not.", null);
                throw new Exception();
            }

            return new CreateContactCommand();
        }
    }

}
