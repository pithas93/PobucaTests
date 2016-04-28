using System;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class SortRecordsCommand
    {
        protected string field;
        protected string order;

        protected const string AscendingOrder = "f10 fa fa-arrow-up";
        protected const string DescendingOrder = "f10 fa fa-arrow-down";

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
            var sortOptions = Driver.Instance.FindElements(By.CssSelector("li[ng-click='setSortAttribute(sortAttr);']"));
            var currentOption = Driver.Instance.FindElement(By.CssSelector("div#ribbon-sort-by span.clear.ng-binding")).Text;
            var tmp = Driver.Instance.FindElement(By.CssSelector("span[class*='f10 fa fa-arrow-']"));
            string currentSortOrder;
            if (tmp.GetAttribute("class") == AscendingOrder) currentSortOrder = "ascending";
                else currentSortOrder = "descending";

            // if requested sorting is already set, do nothing
            if (currentOption.Contains(field) && (currentSortOrder.Equals(order)))
            {
                Commands.ClickSortBy();
                Driver.Wait(TimeSpan.FromSeconds(3));
                return;
            }

            foreach (var webElement in sortOptions)
            {
                if (webElement.Text == field)
                {
                    if (currentOption.Contains(field) && currentSortOrder != order)
                    {
                        webElement.Click();
                        Driver.Wait(TimeSpan.FromSeconds(2));
                        return;
                    }
                    if (!currentOption.Contains(field) && order == SortOrder.Ascending)
                    {
                        webElement.Click();
                        Driver.Wait(TimeSpan.FromSeconds(2));
                        return;
                    }
                    if (!currentOption.Contains(field) && order == SortOrder.Descending)
                    {
                        webElement.Click();
                        Commands.ClickSortBy();
                        Sort();
                        return;
                    }
                }
            }

//            if (currentSortOrder.Equals(AscendindOrder) && order == SortOrder.Descending)
//                foreach (var webElement in sortOptions)
//                {
//                    if (field == webElement.Text) webElement.Click();
//                }
//            else if (currentSortOrder.Equals(DescendindOrder) && order == SortOrder.Ascending)
//                foreach (var webElement in sortOptions)
//                {
//                    if (field == webElement.Text) webElement.Click();
//                }
//            else
                
        }

        /// <summary>
        /// Fields used by contact and organization lists to sort their records
        /// </summary>
        public class SortField
        {
            public const string FirstName = "First Name";
            public const string LastName = "Last Name";
            public const string ModificationDate = "Modification date";
            public const string OrganizationName = "Name";
            public const string City = "City";
            public const string Profession = "Profession";
        }

        /// <summary>
        /// Types of sorting order being used by contact and organization lists to sort their records
        /// </summary>
        public class SortOrder
        {
            public const string Ascending = "ascending";
            public const string Descending = "descending";
        }
    }

    public class SortContactsCommand : SortRecordsCommand
    {

        public SortContactsCommand()
        {
            field = SortField.FirstName;
            order = SortOrder.Ascending;
        }

        public SortContactsCommand FirstName()
        {
            field = SortField.FirstName;
            return this;
        }

        public SortContactsCommand LastName()
        {
            field = SortField.LastName;
            return this;
        }

        

       



        
    }

    public class SortOrganizationsCommand : SortRecordsCommand
    {


        public SortOrganizationsCommand()
        {
            field = SortField.OrganizationName;
            order = SortOrder.Ascending;
        }

        public SortOrganizationsCommand OrganizationName()
        {
            field = SortField.OrganizationName;
            return this;
        }

        public SortOrganizationsCommand City()
        {
            field = SortField.City;
            return this;
        }

        public SortOrganizationsCommand Profession()
        {
            field = SortField.Profession;
            return this;
        }


    }
}