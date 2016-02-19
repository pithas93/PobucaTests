using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class NewOrganizationPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Add Organization"); } }
        
        public static void GoTo()
        {
            var newOrganizationBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[4]/ul/li[1]/a/i"));
            newOrganizationBtn.Click();
        }

        public static CreateOrganizationCommand CreateOrganization(string organization_name)
        {
            return new CreateOrganizationCommand(organization_name);
        }
    }

    public class CreateOrganizationCommand
    {
        private string organization_name;

        public CreateOrganizationCommand(string organization_name)
        {
            this.organization_name = organization_name;
        }

        public void Create()
        {
            var organizationNameField = Driver.Instance.FindElement(By.Id("Organization Name"));
            var saveBtn = Driver.Instance.FindElement(By.Id("save-entity"));

            organizationNameField.SendKeys(organization_name);
            Driver.Wait(TimeSpan.FromSeconds(1));
            saveBtn.Click();
        }
    }
}
