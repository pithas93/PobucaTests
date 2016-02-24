using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class NewOrganizationPage
    {
        /// <summary>
        /// Check if browser is at organization form page that allows to create a new organization
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Add Organization"); } }

        /// <summary>
        /// Navigates browser to an organization form page that allows to create a new organization
        /// </summary>
        public static void GoTo()
        {
            var newOrganizationBtn = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[4]/ul/li[1]/a/i"));
            newOrganizationBtn.Click();
            if (!IsAt) Console.WriteLine("Failed to open new organization");
        }

        /// <summary>
        /// Issue a create new organization command with given organization name
        /// </summary>
        /// <param name="organization_name"></param>
        /// <returns> A command upon which the parameters for the new organization are specified</returns>
        public static CreateOrganizationCommand CreateOrganization(string organization_name)
        {
            GoTo();
            return new CreateOrganizationCommand(organization_name);
        }
    }

    public class CreateOrganizationCommand
    {
        private string organization_name;

        /// <summary>
        /// Sets the organization name for the new organization
        /// </summary>
        /// <param name="organization_name"></param>
        public CreateOrganizationCommand(string organization_name)
        {
            this.organization_name = organization_name;
        }

        /// <summary>
        /// Creates the new organization with the given name
        /// </summary>
        public void Create()
        {
            var organizationNameField = Driver.Instance.FindElement(By.Id("Organization Name"));
            
            organizationNameField.SendKeys(organization_name);
            Driver.Wait(TimeSpan.FromSeconds(1.5));
            Commands.ClickSave();
        }
    }
}
