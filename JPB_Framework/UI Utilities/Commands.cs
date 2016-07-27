using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace JPB_Framework.UI_Utilities
{

    public class Commands
    {


        /// <summary>
        /// Presses the keyboard Escape key
        /// </summary>
        public static void PressEscapeKey()
        {
            var action = new Actions(Driver.Instance);
            action.SendKeys(Keys.Escape);
            Driver.Wait(TimeSpan.FromSeconds(1));
        }


        //////////////////////////////////////
        ///  Click a UI Button
        //////////////////////////////////////


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
        /// It clicks the delete button. Delete button is available through the Contact View Page, Organization View Page, Contacts Page and Organizations Page
        /// </summary>
        public static void ClickDelete()
        {
            var deleteBtn = Driver.Instance.FindElement(By.CssSelector("i.fa.fa-trash-o"));
            deleteBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// It clicks the share button. Share button is available through the Contact and Organization ViewPages
        /// </summary>
        public static void ClickShare()
        {
            var shareBtn = Driver.Instance.FindElement(By.Id("share-entity"));
            shareBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// It clicks the favorite button. Favorite button is available only through the Contact View Page
        /// </summary>
        public static void ClickFavorite()
        {
            string cssSelector;
            if (ContactViewPage.Favorite == true.ToString()) cssSelector = "span#favorite-entity";
            else cssSelector = "span#unfavorite-entity";
            Driver.Instance.FindElement(By.CssSelector(cssSelector)).Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        //////////////////////////////////////
        ///  Click a record list filter
        //////////////////////////////////////


        /// <summary>
        /// It clicks the "Filter By" button located above the contact list in contact list page
        /// </summary>
        public static void ClickFilterBy()
        {
            var filterByBtn = Driver.Instance.FindElement(By.CssSelector("div#contacts-filters button.button.multiSelectButton.ng-binding"));
            filterByBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// It clicks the "Account Type" filter button located above the organization list in organization list page
        /// </summary>
        public static void ClickAccountTypeFilter()
        {
            var filterByBtn = Driver.Instance.FindElement(By.CssSelector("div#filter-account-type button.button.multiSelectButton.ng-binding"));
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

        //////////////////////////////////////
        ///  Organization view contact list commands
        //////////////////////////////////////


        /// <summary>
        /// Applicable only within organization view page. Clicks the 'Add Existing Contacts' button from organization's contact list
        /// </summary>
        public static void ClickAddExistingContactsToOrganizationButton()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("a.dropdown-toggle.p-none"));
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
            var str = element.GetAttribute("aria-haspopup");
            if (str.Equals("true"))
            {
                var addNewContactBtn = Driver.Instance.FindElements(By.CssSelector("#related-contacts-section .dropdown-menu.animated.fadeInRight.m-t-xs a"))[0];
                addNewContactBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "After clicking to add contact to organization, within organization view page, the relative combo box should be expanded, nut it did not.", null);
                throw new Exception();
            }
        }

        /// <summary>
        /// Applicable only within organization view page. Clicks the 'Create New Contact' button from organization's contact list
        /// </summary>
        public static void ClickCreateNewContactForOrganizationButton()
        {
            var element = Driver.Instance.FindElement(By.CssSelector("a.dropdown-toggle.p-none"));
            element.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
            var str = element.GetAttribute("aria-haspopup");
            if (str.Equals("true"))
            {
                var addNewContactBtn = Driver.Instance.FindElements(By.CssSelector("#related-contacts-section .dropdown-menu.animated.fadeInRight.m-t-xs a"))[1];
                addNewContactBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "After clicking to add contact to organization, within organization view page, the relative combo box should be expanded, nut it did not.", null);
                throw new Exception();
            }
        }

        /// <summary>
        /// Applicable only within organization view page. Make a contact primary for the currently viewed organization
        /// </summary>
        /// <param name="record">Contact to be made primary</param>
        public static void ClickContactPrimaryButton(IWebElement record)
        {
            Driver.MoveToElement(record);
            record.FindElement(By.CssSelector("div[action='makePrimary(group, contact)']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Applicable only within organization view page. Remove a contact from currently viewed organization's contact list.
        /// </summary>
        /// <param name="record">Contact to be removed from organization's contact list</param>
        public static void ClickContactRemoveButton(IWebElement record)
        {
            Driver.MoveToElement(record);
            record.FindElement(By.CssSelector("div[action='removeRelatedContact(contact)']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Applicable only within organization/contact list pages. If there are filters set, it clears those filters
        /// </summary>
        public static void ResetFilters()
        {
            try
            {
                var element = Driver.Instance.FindElement(By.CssSelector("img[ng-click='resetFilters();']"));
                var tmp = element.GetAttribute("class");
                if (tmp.Contains("ng-hide")) return;
                element.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (NoSuchElementException)
            {
            }
        }

        //////////////////////////////////////
        ///  Search - Select - Open records commands
        //////////////////////////////////////


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
        /// Clears searchbox value. It can only be used on pages with the searchbox field
        /// </summary>
        public static void ClearSearchbox()
        {
            var searchbox = Driver.Instance.FindElement(By.Id("search-input-related"));
            var searchBoxField =
                searchbox.FindElement(
                    By.TagName("input"));
            searchBoxField.Clear();
            Driver.Wait(TimeSpan.FromSeconds(2));
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
        ///  Selects/deselects a single record within a record list by checking its checkbox
        /// Method returns true if checkbox is checked and false if it is unchecked
        /// </summary>
        /// <param name="record">The web element that corresponds to the record that will be selected</param>
        public static bool SelectRecord(IWebElement record)
        {
            Driver.MoveToElement(record);
            var checkBox = record.FindElement(By.CssSelector(".icheckbox"));
            checkBox.Click();
            var tmp = checkBox.GetAttribute("class");
            Driver.Wait(TimeSpan.FromSeconds(1));
            if (tmp.Equals("icheckbox") || tmp.Equals("icheckbox hover")) return false;
            if (tmp.Equals("icheckbox checked") || tmp.Equals("icheckbox hover checked")) return true;
            throw new Exception();
        }

        /// <summary>
        /// Checks or unchecks the favorite checkbox for a contact within contact list
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static bool ClickContactFavorite(IWebElement record)
        {
            Driver.MoveToElement(record);
            var favoriteCheckBox = record.FindElement(By.CssSelector("div.favoriteButton"));
            favoriteCheckBox.Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
            var tmp = favoriteCheckBox.GetAttribute("class");
            if (tmp.Equals("favoriteButton ng-scope is-favorite")) return true;
            if (tmp.Equals("favoriteButton ng-scope")) return false;
            throw new Exception();
        }

        /// <summary>
        /// Selects a range of records whose name match the given string parameter
        /// </summary>
        /// <param name="s">The string criteria with which the records will be found</param>
        /// <returns>Returns the number of records selected</returns>
        public static int SelectRecordsMatching(string s)
        {
            var recordCount = 0;
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var record in records)
            {
                var recordName = record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (string.IsNullOrEmpty(recordName)) break;
                if (!recordName.StartsWith(s)) continue;
                SelectRecord(record);
                recordCount++;
            }
            return recordCount;
        }


        /// <summary>
        /// Selects a given or a random number of up to 20 records from a list. If records are more than 40, the selected are among the first 40. 
        /// </summary>
        /// <param name="num">Defines the number of records to be selected. If it is zero, method chooses a random number of records</param>
        /// <returns>The count of records that where selected</returns>
        public static void SelectRandomNumberOfRecords(int num)
        {
            // The range of records from where the selection will be
            int range;
            // How many records will be selected
            int numOfRecordsToBeSelected;
            var rand = new Random();
            var records = Driver.Instance.FindElements(By.CssSelector("div.col-md-6.col-lg-4.col-xl-3.ng-scope"));

            // Do not use a range greater than 40 for the selection
            if (40 < TotalRecordsCount()) range = 40;
            else range = TotalRecordsCount();


            // If there is no defined number for the num of contacts to be selected, choose one randomly
            if (num==0) numOfRecordsToBeSelected = rand.Next(1, 20);
            else numOfRecordsToBeSelected = num;

            for (var i = 0; i < numOfRecordsToBeSelected; i++)
            {
                var tmp = rand.Next(1, range) - 1;
                var isChecked = SelectRecord(records[tmp]);
                if (!isChecked) i-=2;

            }
        }

        /// <summary>
        /// Checks/unchecks the favorite checkbox for one or more contacts within contact list whose name matches exactly the given string parameter 
        /// </summary>
        /// <param name="s">The string criteria with which the records will be found</param>
        public static void ClickFavoriteForContactsMatching(string s)
        {
            var records = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));
            foreach (var record in records)
            {
                var recordName = record.FindElement(By.CssSelector("font[class^='name font-regular'][class*='m-b-sm']")).Text;
                if (string.IsNullOrEmpty(recordName)) break;
                if (!recordName.Equals(s)) continue;
                ClickContactFavorite(record);
            }
        }

        //////////////////////////////////////
        ///  Get value - status for something commands
        //////////////////////////////////////

        /// <summary>
        /// Returns true if a record can be shared with a given email. If the email is of invalid form, returns false.
        /// </summary>
        public static bool IsRecordShareableTo(string email)
        {
            var shareModalWindow = Driver.Instance.FindElement(By.CssSelector("div#showShareVCardModal"));
            var isModalShown = shareModalWindow.GetAttribute("class");

            // If modal dialog is being shown, close it by pressing Cancel
            if (isModalShown == "modal fade ng-scope in")
            {
                shareModalWindow.FindElement(By.CssSelector("button.btn.btn-default")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }

            Commands.ClickShare();

            var shareEmailField = shareModalWindow.FindElement(By.CssSelector("input#shareVcardInput"));

            // If email input field within modal dialog, is not empty, report it and clear it
            if (shareEmailField.GetAttribute("value") != "")
            {
                Report.Report.ToLogFile(MessageType.Message, "Share contact input email field was not empty.", null);
                shareEmailField.Clear();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }

            shareEmailField.SendKeys(email);
            var shareBtn = shareModalWindow.FindElement(By.CssSelector("button#shareBtn"));
            Driver.Wait(TimeSpan.FromSeconds(2));

            var isShareable = shareBtn.Enabled;

            // Close modal dialog by pressing Cancel
            shareModalWindow.FindElement(By.CssSelector("button.btn.btn-default")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            return isShareable;
        }

        /// <summary>
        /// Returns the number of records contained in the record list currently displayed
        /// </summary>
        /// <returns></returns>
        public static int TotalRecordsCount()
        {
            var recordList = Driver.Instance.FindElements(By.CssSelector("div.col-md-6.col-lg-4.col-xl-3.ng-scope"));


            // Check if there is at least one record displayed in the record list or else there is no point in continuing
            var recordName = recordList[0].GetAttribute("style");
            if (recordName.Equals("display: none;")) return 0;

            // Count how many records are displayed
            var recordListCount = 0;
            foreach (var record in recordList)
            {
                var tmp = record.GetAttribute("style");
                if (!string.IsNullOrEmpty(tmp)) break;
                recordListCount++;
            }

            // Make page load every single record so that web driver can access them through their WebElements
            int previousRecordListCount;
            do
            {
                // Navigate to the last record list item
                Driver.MoveToElement(recordList[recordListCount - 1]);

                // After the record list has load the extra, not shown previously records, get the new record list count
                recordList = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

                // Count how many records are displayed
                recordListCount = 0;
                foreach (var record in recordList)
                {
                    var tmp = record.GetAttribute("style");
                    if (!string.IsNullOrEmpty(tmp)) break;
                    recordListCount++;
                }

                // Save the previousRecordListCount
                previousRecordListCount = recordListCount;

                if (previousRecordListCount > recordListCount)
                {
                    Report.Report.ToLogFile(MessageType.Message, "It seems that there is somethign wrong while counting records from the list", null);
                    throw new Exception();
                }

                // There is no change in the newRecordListCount, we have probably reached its bottom
            } while (previousRecordListCount != recordListCount);


            return recordListCount;
        }

        /// <summary>
        /// Returns the number of selected records contained in the record list currently displayed
        /// </summary>
        /// <returns></returns>
        public static int SelectedRecordsCount()
        {
            var selectedRecords = 0;

            // We call TotalRecordsCount to load all the contacts
            TotalRecordsCount();

            var recordList = Driver.Instance.FindElements(By.CssSelector(".col-md-6.col-lg-4.col-xl-3.ng-scope"));

            foreach (var record in recordList)
            {
                var checkBox = record.FindElement(By.CssSelector(".icheckbox"));
                var tmp = checkBox.GetAttribute("class");
                if (tmp.Equals("icheckbox checked") || tmp.Equals("icheckbox hover checked")) selectedRecords++;
            }
            return selectedRecords;
        }

        /// <summary>
        /// Returns the value of label showing the total number of records currently displayed
        /// </summary>
        /// <returns></returns>
        public static int TotalRecordsCountByLabel()
        {
            var totalRecordsLbl = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[1]/span/span[2]"));
            return Int32.Parse(totalRecordsLbl.Text);
        }
        
        /// <summary>
        /// Returns the value of label showing the number of selected records
        /// </summary>
        /// <returns></returns>
        public static int SelectedRecordsCountByLabel()
        {
            var selectedRecordsLbl = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[1]/span/span[1]"));
            return Int32.Parse(selectedRecordsLbl.Text);
        }

        /// <summary>
        /// Returns true if the given list of text values is sorted alphabetically.
        /// Method also returns false in case one of the text values is a GUID.
        /// </summary>
        /// <param name="valuesList"></param>
        /// <returns></returns>
        public static bool CheckIfListIsSorted(ReadOnlyCollection<IWebElement> valuesList)
        {
            string guid_pattern = @"\A\{[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}\}\z";
            Regex reg = new Regex(guid_pattern, RegexOptions.IgnoreCase);
            var match = reg.Match(valuesList[0].Text);
            if (match.Success)
            {
                Report.Report.ToLogFile(MessageType.Message, "Guid value found inside given list values!", null);
                return false;
            }

            for (var i = 1; i < valuesList.Count; i++)
            {
                var previousDepartment = valuesList[i - 1].Text;
                var currentDepartment = valuesList[i].Text;
                match = reg.Match(currentDepartment);
                if (match.Success)
                {
                    Report.Report.ToLogFile(MessageType.Message, "Guid value found inside given list values!", null);
                    return false;
                }

                if (string.Compare(previousDepartment, currentDepartment) == 1) return false;
            }
            return true;
        }


    }
}
