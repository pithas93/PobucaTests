using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class OrganizationViewPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Organization View"); } }
        
        public static string FirstName {
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

    }
}
