using System;
using System.Collections.Generic;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class FilterRecordCommand
    {
        private bool favorites;
        private bool department;
        private bool allowEmail;
        private bool allowSMS;
        private bool allowPhones;
        private bool orphans;
        private List<Department> selectedDepartments;

        public FilterRecordCommand()
        {
            favorites = false;
            department = false;
            allowEmail = false;
            allowSMS = false;
            allowPhones = false;
            orphans = false;
            selectedDepartments = new List<Department>();
        }


        /// <summary>
        /// Set the allow email criteria true
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand AllowEmail()
        {
            allowEmail = true;
            return this;
        }

        /// <summary>
        /// Set the allow sms criteria true
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand AllowSMS()
        {
            allowSMS = true;
            return this;
        }

        /// <summary>
        /// Set the allow phones criteria true
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand AllowPhones()
        {
            allowPhones = true;
            return this;
        }

        /// <summary>
        /// Set the orphan criteria true
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand Orphans()
        {
            orphans = true;
            return this;
        }

        /// <summary>
        /// Add a department to the existing filter by criteria
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public FilterRecordCommand DepartmentIs(Department department)
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
                    Driver.Instance.FindElements(By.CssSelector("div#department-dropdown .checkBoxContainer .multiSelectItem.ng-scope.vertical"));

                if (selectedDepartments.Count > 0)
                {
                    foreach (var dep in selectedDepartments)
                    {
                        IWebElement departmentOption;
                        switch (dep)
                        {
                            case Department.Administration:
                            {
                                departmentOption = departmentsOptionList[0];
                                break;
                            }
                            case Department.Consulting:
                            {
                                departmentOption = departmentsOptionList[1];
                                break;
                            }
                            case Department.Logistics:
                            {
                                departmentOption = departmentsOptionList[6];
                                break;
                            }
                            case Department.RnD:
                            {
                                departmentOption = departmentsOptionList[13];
                                break;
                            }
                            case Department.Sales:
                            {
                                departmentOption = departmentsOptionList[14];
                                break;
                            }
                            default:
                            {
                                departmentOption = departmentsOptionList[0];
                                break;
                            }
                        }
                        departmentOption.Click();
                        Driver.Wait(TimeSpan.FromSeconds(1));
                    }

                    departmentListBtn.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                }                
            }

            Driver.Wait(TimeSpan.FromSeconds(3));

        }

        /// <summary>
        /// Exists just to improve test code readability
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand And()
        {
            return this;
        }

        /// <summary>
        /// Exists just to improve test code readability
        /// </summary>
        /// <returns></returns>
        public FilterRecordCommand Or()
        {
            return this;
        }
    }

    public enum Department
    {
        Logistics,
        RnD,
        Administration,
        Consulting,
        Sales
    }
}