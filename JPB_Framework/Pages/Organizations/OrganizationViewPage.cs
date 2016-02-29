using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Organizations
{
    public class OrganizationViewPage
    {
        /// <summary>
        /// Check if browser is at the selected organization's detail view page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organization View"); } }

        
        public static string OrganizationName {
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
    }

}
