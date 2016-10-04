using System;
using System.Collections.Generic;
using JPB_Framework.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class FilterContactsCommand
    {
        private bool favorites;
        private bool department;
        private bool allowEmail;
        private bool allowSMS;
        private bool allowPhones;
        private bool orphans;
        private List<string> selectedDepartments;

        public FilterContactsCommand()
        {
            favorites = false;
            department = false;
            allowEmail = false;
            allowSMS = false;
            allowPhones = false;
            orphans = false;
            selectedDepartments = new List<string>();
        }


        /// <summary>
        /// Set the allow email criteria true
        /// </summary>
        /// <returns></returns>
        public FilterContactsCommand SelectingAllowEmail()
        {
            allowEmail = true;
            return this;
        }

        /// <summary>
        /// Set the allow sms criteria true
        /// </summary>
        /// <returns></returns>
        public FilterContactsCommand SelectingAllowSMS()
        {
            allowSMS = true;
            return this;
        }

        /// <summary>
        /// Set the allow phones criteria true
        /// </summary>
        /// <returns></returns>
        public FilterContactsCommand SelectingAllowPhones()
        {
            allowPhones = true;
            return this;
        }

        /// <summary>
        /// Set the orphan criteria true
        /// </summary>
        /// <returns></returns>
        public FilterContactsCommand SelectingOrphans()
        {
            orphans = true;
            return this;
        }

        /// <summary>
        /// Add a department to the existing filter by criteria
        /// </summary>
        /// <param name="department">The name of department to be chosen. For example, Department.Logistics</param>
        /// <returns></returns>
        public FilterContactsCommand SelectingDepartment(string department)
        {
            this.department = true;
            selectedDepartments.Add(department);
            return this;
        }

        /// <summary>
        /// Execute the filter command with the given options
        /// </summary>
        public void Filter()
        {
            var filterByOptionList =
                Driver.Instance.FindElements(By.CssSelector(".checkboxLayer.show .checkBoxContainer .multiSelectItem.ng-scope.vertical"));

            // Select the filter by oprtions 

            if (favorites) { filterByOptionList[0].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }
            if (department) { filterByOptionList[1].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }
            if (allowEmail) { filterByOptionList[2].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }
            if (allowSMS) { filterByOptionList[3].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }
            if (allowPhones) { filterByOptionList[4].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }
            if (orphans) { filterByOptionList[5].Click(); Driver.Wait(TimeSpan.FromSeconds(1)); }

            Commands.ClickFilterBy();
            Driver.Wait(TimeSpan.FromSeconds(1));

            // if department option is selected, select from department list the departments that where added and finally close the department list.
            // else if department option is not selected, close the filter by list

            if (department)
            {
                var departmentListBtn = Driver.Instance.FindElement(By.CssSelector("div#department-dropdown .button.multiSelectButton.ng-binding"));
                departmentListBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));

                var departmentsOptionList =
                    Driver.Instance.FindElements(By.CssSelector("div#department-dropdown .checkBoxContainer .multiSelectItem.ng-scope.vertical div label span"));

                if (selectedDepartments.Count > 0)
                {
                    // Select the Department option
                    foreach (var selectedDepartment in selectedDepartments)
                    {
                        foreach (var webElement in departmentsOptionList)
                        {
                            if (webElement.Text != selectedDepartment) continue;
                            webElement.FindElement(By.XPath("../../..")).Click();
                            Driver.Wait(TimeSpan.FromSeconds(1));
                            break;
                        }

                    }

                    departmentListBtn.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                }
            }

            Driver.Wait(TimeSpan.FromSeconds(3));

        }

    }

    public class FilterCoworkersCommand
    {

        private List<string> selectedDepartments;

        public FilterCoworkersCommand()
        {
            selectedDepartments = new List<string>();
        }

        /// <summary>
        /// Add a department to the existing filter by criteria
        /// </summary>
        /// <param name="department">The name of department to be chosen. For example, Department.Logistics</param>
        /// <returns></returns>
        public FilterCoworkersCommand SelectingDepartment(string department)
        {
            selectedDepartments.Add(department);
            return this;
        }

        /// <summary>
        /// Execute the filter command with the given options
        /// </summary>
        public void Filter()
        {

            var filterByDepartmentBtn = Driver.Instance.FindElement(By.CssSelector("[default-label='Department']"));

            filterByDepartmentBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));


            var departmentsOptionList =
               filterByDepartmentBtn.FindElements(By.CssSelector("span.ng-binding"));

            if (selectedDepartments.Count > 0)
            {
                // Select the Department option
                foreach (var selectedDepartment in selectedDepartments)
                {
                    foreach (var webElement in departmentsOptionList)
                    {
                        if (webElement.Text != selectedDepartment) continue;
                        Driver.Wait(TimeSpan.FromSeconds(1));
                        webElement.FindElement(By.XPath("../../..")).Click();
                        Driver.Wait(TimeSpan.FromSeconds(1));
                        break;
                    }

                }

                filterByDepartmentBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }


        }

    }

    public class FilterOrganizationsCommand
    {
        private List<string> selectedAccountTypes;

        public FilterOrganizationsCommand()
        {
            selectedAccountTypes = new List<string>();
        }

        /// <summary>
        /// Add an account type to the existing filter by criteria
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public FilterOrganizationsCommand SelectingAccountType(string accountType)
        {
            selectedAccountTypes.Add(accountType);
            return this;
        }

        /// <summary>
        /// Execute the filter command with the given options
        /// </summary>
        public void Filter()
        {
            var accountTypeOptionList =
                Driver.Instance.FindElements(By.CssSelector(".checkboxLayer.show .checkBoxContainer .multiSelectItem.ng-scope.vertical div label span"));

            // Select the Account Type options 
            if (selectedAccountTypes.Count > 0)
            {
                foreach (var selectedAccountType in selectedAccountTypes)
                {
                    foreach (var webElement in accountTypeOptionList)
                    {
                        if (webElement.Text != selectedAccountType) continue;
                        webElement.FindElement(By.XPath("../../..")).Click();
                        Driver.Wait(TimeSpan.FromSeconds(1));
                        break;
                    }

                }
            }

            Commands.ClickAccountTypeFilter();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

    }

    public class Department
    {
        public static readonly string Logistics = " Logistics";
        public static readonly string RnD = " Research and Development";
        public static readonly string Administration = " Administration";
        public static readonly string Consulting = " Consulting";
        public static readonly string Sales = " Sales";
    }

    public class AccountType
    {
        public static readonly string Consultant = " Consultant";
        public static readonly string Customer = " Customer";
        public static readonly string Influencer = " Influencer";
        public static readonly string Investor = " Investor";
        public static readonly string Lead = " Lead";
        public static readonly string Other = " Other";
        public static readonly string Partner = " Partner";
        public static readonly string Press = " Press";
        public static readonly string Prospect = " Prospect";
        public static readonly string Reseller = " Reseller";
        public static readonly string Supplier = " Supplier";
        public static readonly string Vendor = " Vendor";

    }

}