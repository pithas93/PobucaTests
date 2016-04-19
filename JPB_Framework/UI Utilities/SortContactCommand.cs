using System;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class SortRecordsCommand
    {
        private SortField field;
        private SortOrder order;
        private const string AscendindOrder = "f10 fa fa-arrow-up";
        private const string DescendindOrder = "f10 fa fa-arrow-down";

        public SortRecordsCommand()
        {
            field = SortField.FirstName;
            order = SortOrder.Ascending;
        }

        public SortRecordsCommand FirstName()
        {
            field = SortField.FirstName;
            return this;
        }

        public SortRecordsCommand LastName()
        {
            field = SortField.LastName;
            return this;
        }

        public SortRecordsCommand Ascending()
        {
            order = SortOrder.Ascending;
            return this;
        }

        public SortRecordsCommand Descending()
        {
            order = SortOrder.Descending;
            return this;
        }

        /// <summary>
        /// Selects one of the available sorting options. The selected option is determined from the attributes of this class. By default, records are sorted by their displayed name, in ascending order.
        /// </summary>
        public void Sort()
        {
            var sortOption = Driver.Instance.FindElements(By.CssSelector("li[ng-click='setSortAttribute(sortAttr);']"));
            var currentSortOrderElement = Driver.Instance.FindElement(By.CssSelector("span[class*='f10 fa fa-arrow-']"));
            string currentSortOrder = currentSortOrderElement.GetAttribute("class");

            if (currentSortOrder.Equals(AscendindOrder) && order == SortOrder.Descending)
                switch (field)
                {
                    case SortField.FirstName:
                    {
                        sortOption[0].Click();
                        break;
                    }
                    case SortField.LastName:
                    {
                        sortOption[1].Click();
                        break;
                    }
                    case SortField.ModificationDate:
                    {
                        sortOption[2].Click();
                        break;
                    }
                    default:
                    {
                        sortOption[0].Click();
                        break;
                    }
                }
            else if (currentSortOrder.Equals(DescendindOrder) && order == SortOrder.Ascending)
                switch (field)
                {
                    case SortField.FirstName:
                    {
                        sortOption[0].Click();
                        break;
                    }
                    case SortField.LastName:
                    {
                        sortOption[1].Click();
                        break;
                    }
                    case SortField.ModificationDate:
                    {
                        sortOption[2].Click();
                        break;
                    }
                    default:
                    {
                        sortOption[0].Click();
                        break;
                    }
                }
            else
                Commands.ClickSortBy();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Fields used by contact and organization lists to sort their records
        /// </summary>
        public enum SortField
        {
            FirstName,
            LastName,
            ModificationDate,
            OrganizationName,
            City,
            Profession,
            update
        }

        /// <summary>
        /// Types of sorting order being used by contact and organization lists to sort their records
        /// </summary>
        public enum SortOrder
        {
            Ascending,
            Descending,
        }
    }
}