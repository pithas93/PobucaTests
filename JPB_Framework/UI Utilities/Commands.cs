using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework.UI_Utilities
{
    public class Commands
    {

        /// <summary>
        /// It clicks the save button. Save button is available through new/edit Contact And organization pages
        /// </summary>
        public static void ClickSave()
        {
            var saveBtn = Driver.Instance.FindElement(By.Id("save-entity"));
            saveBtn.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// It clicks the edit button. Edit button is available through the Contact And Organization ViewPages
        /// </summary>
        internal static void ClickEdit()
        {
            var editBtn = Driver.Instance.FindElement(By.Id("edit-entity"));
            editBtn.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// It clicks the delete button. Delete button is available through the Contact And Organization ViewPages
        /// </summary>
        public static void ClickDelete()
        {
            var deleteBtn = Driver.Instance.FindElement(By.CssSelector("i.fa.fa-trash-o"));
            deleteBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// It clicks searchbox And inputs given text for search. It can only be used on pages with the searchbox field
        /// </summary>
        public static void SearchFor(string keyword)
        {
            var searchbox = Driver.Instance.FindElement(By.Id("search-input-related"));
            var searchBoxField =
                searchbox.FindElement(
                    By.TagName("input"));
            searchBoxField.Clear();
            searchBoxField.SendKeys(keyword);
            Driver.Wait(TimeSpan.FromSeconds(5));
        }



        /// <summary>
        /// Selects a record from a list page, identified by its position in the list
        /// </summary>
        /// <param name="position"></param>
        public static void OpenRecordFromListBySequence(int position)
        {
            var record = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[3]/div[" + position + "]"));
            record.Click();
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Navigates browser to the import dialog box which is available through contacts And organizations list pages
        /// </summary>
        public static void ClickImport()
        {
            var importCombo = Driver.Instance.FindElement(By.CssSelector("i.fa.fa-file-text-o.jp-light-blue.f20"));
            importCombo.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));

            var importOption =
                Driver.Instance.FindElement(By.PartialLinkText("Import"));
            importOption.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Checks a list of contacts Or organization to find a record matching the given title
        /// </summary>
        /// <param name="title">Matches either an organization name Or a first-last name</param>
        /// <returns>Returns true if there is at list one record matching the criteria. Returns false if search reach the list end without matching records</returns>
        public static bool FindIfRecordExists(string title)
        {
            // Next statement will also retrieve elements that are null. These elements are located at the end of the records collection, so when you encounter the first of them, 
            //just break And return false, excpet if you have find at least one record matching given title
            var records = Driver.Instance.FindElements(By.CssSelector(".font-bold.ng-binding"));
            foreach (var record in records)
            {
                if (record.Text.Equals("") || record.Text == null) break;
                if (record.Text.Equals(title))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  Selects/deselects a single record within a record list by checking its checkbox
        /// </summary>
        /// <param name="record">The web element that corresponds to the record that will be selected</param>
        public static void SelectRecord(IWebElement record)
        {
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(record);
            action.Perform();
            record.FindElement(By.CssSelector(".icheckbox")).Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Selects a range of records whose name match the given string parameter
        /// </summary>
        /// <param name="s">The string criteria with which the records will be found</param>
        public static void SelectRecordsMatching(string s)
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            foreach (var record in records)
            {
                var contactName = record.FindElement(By.CssSelector(".font-bold.ng-binding"));
                if (contactName.Text.Equals(s))
                {
                    Commands.SelectRecord(record);
                }
                else break;
            }
        }

        /// <summary>
        /// It clicks the "Filter By" button which is located above the contact/organization lists
        /// </summary>
        public static void ClickFilterBy()
        {
            var filterByBtn = Driver.Instance.FindElement(By.CssSelector("div#contacts-filters button.button.multiSelectButton.ng-binding"));
            filterByBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// It clicks the "Sort By" button which is located above the contact/organization lists
        /// </summary>
        public static void ClickSortBy()
        {
            var sortrByBtn = Driver.Instance.FindElement(By.CssSelector("div#ribbon-sort-by div.dropdown.profile-element2"));
            sortrByBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }
    }
}
