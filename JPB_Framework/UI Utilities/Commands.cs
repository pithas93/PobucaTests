using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework.UI_Utilities
{
    public class Commands
    {

        /// <summary>
        /// It clicks the save button. Save button is available through new/edit Contact and organization pages
        /// </summary>
        public static void ClickSave()
        {
            var saveBtn = Driver.Instance.FindElement(By.Id("save-entity"));
            saveBtn.Click();
        }

        /// <summary>
        /// It clicks the edit button. Edit button is available through the Contact and Organization ViewPages
        /// </summary>
        internal static void ClickEdit()
        {
            var editBtn = Driver.Instance.FindElement(By.Id("edit-entity"));
            editBtn.Click();
        }

        /// <summary>
        /// It clicks the delete button. Delete button is available through the Contact and Organization ViewPages
        /// </summary>
        public static void ClickDelete()
        {
            var deleteBtn = Driver.Instance.FindElement(By.Id("delete-entity"));
            deleteBtn.Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// It clicks searchbox and inputs given text for search. It can only be used on pages with the searchbox field
        /// </summary>
        public static void SearchFor(string keyword)
        {
            var searchBoxField =
                Driver.Instance.FindElement(
                    By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[4]/div/input"));
            searchBoxField.SendKeys(keyword);
        }

        /// <summary>
        /// Selects a record from a list page, identified by its position in the list
        /// </summary>
        /// <param name="position"></param>
        public static void SelectRecordFromListBySequence(int position)
        {
            var record = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[3]/div[" + position + "]"));
            record.Click();
        }
    }
}
