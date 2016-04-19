using System;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JPB_Framework.UI_Utilities
{

    public class Commands
    {

        /// <summary>
        /// It clicks the save button. Save button is available through new/edit Contact And organization pages
        /// </summary>
        /// <returns>True if  save button was clicked successfully</returns>
        public static bool ClickSave()
        {

            var saveBtn = Driver.Instance.FindElement(By.CssSelector("#save-entity"));
            var attr = saveBtn.GetAttribute("disabled");
            if (attr == null)
            {
                saveBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                return true;
            }

            Report.Report.ToLogFile(MessageType.Message, "Save button is not active, so record cannot be saved.", null);
            return false;

        }

        /// <summary>
        /// It clicks the edit button. Edit button is available through the Contact And Organization ViewPages
        /// </summary>
        internal static void ClickEdit()
        {
            var editBtn = Driver.Instance.FindElement(By.Id("edit-entity"));
            editBtn.Click();
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
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Selects the record from the list that corresponds to the given position
        /// </summary>
        /// <param name="position">The position of the record to be selected inside the record list</param>
        /// <returns>Returns true if the record was selected and false if it was deselected</returns>
        private static bool SelectRecordFromListBySequence(int position)
        {
            var record = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[3]/div[" + position + "]"));
            return SelectRecord(record);
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
                if (record.Text.StartsWith(title))
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
        public static bool SelectRecord(IWebElement record)
        {
            var action = new Actions(Driver.Instance);
            action.MoveToElement(record);
            action.Perform();
            var checkBox = record.FindElement(By.CssSelector(".icheckbox"));
            checkBox.Click();
            var tmp = checkBox.GetAttribute("class");
            Driver.Wait(TimeSpan.FromSeconds(1));
            if (tmp.Equals("icheckbox") || tmp.Equals("icheckbox hover")) return false;
            else if (tmp.Equals("icheckbox checked") || tmp.Equals("icheckbox hover checked")) return true;
            else throw new Exception();
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
                if (contactName.Text.StartsWith(s))
                {
                    SelectRecord(record);
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

        /// <summary>
        /// Selects a random number of up to 20 records from a list of no more than 40 records. 
        /// </summary>
        /// <returns>The count of records that where selected</returns>
        public static int SelectRandomNumberOfRecords()
        {
            // The range of records from where the selection will be
            int range;
            var maxNumberOfRecordsToBeSelected = 40;

            if (maxNumberOfRecordsToBeSelected < Driver.GetRecordListCount())
            {
                range = maxNumberOfRecordsToBeSelected;
            }
            else
            {
                range = Driver.GetRecordListCount();
            }

            Random rand = new Random();
            int selectedContacts = 0;

            int numOfContactsToBeSelected = rand.Next(1, 20);

            for (var i = 0; i < numOfContactsToBeSelected; i++)
            {

                var positionOfRecord = rand.Next(1, range);
                var isChecked = SelectRecordFromListBySequence(positionOfRecord);

                if (isChecked) selectedContacts++;
                else selectedContacts--;
            }

            return selectedContacts;
        }


    }
}
