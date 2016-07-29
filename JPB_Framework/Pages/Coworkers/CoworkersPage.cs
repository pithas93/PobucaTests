using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Coworkers
{
    public class CoworkersPage
    {

        /// <summary>
        /// Check if browser is at coworkers list page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Co-workers");

        /// <summary>
        /// Checks whether or not, the coworkers are sorted firstly by their first name, then by their last name, in ascending order
        /// </summary>
        public static bool IsCoworkerListSortedByFirstNameAscending => Driver.CheckIfRecordListIsSortedBy(SortCoworkersCommand.SortField.FirstName, SortCoworkersCommand.SortOrder.Ascending);

        /// <summary>
        /// Checks whether or not, the coworkers are sorted firstly by their first name, then by their last name, in descending order
        /// </summary>
        public static bool IsCoworkerListSortedByFirstNameDescending => Driver.CheckIfRecordListIsSortedBy(SortCoworkersCommand.SortField.FirstName, SortCoworkersCommand.SortOrder.Descending);

        /// <summary>
        /// Checks whether or not, the coworkers are sorted firstly by their last name, then by their first name, in ascending order
        /// </summary>
        public static bool IsCoworkerListSortedByLastNameAscending => Driver.CheckIfRecordListIsSortedBy(SortCoworkersCommand.SortField.LastName, SortCoworkersCommand.SortOrder.Ascending);

        /// <summary>
        /// Checks whether or not, the coworkers are sorted firstly by their last name, then by their first name, in descending order
        /// </summary>
        public static bool IsCoworkerListSortedByLastNameDescending => Driver.CheckIfRecordListIsSortedBy(SortCoworkersCommand.SortField.LastName, SortCoworkersCommand.SortOrder.Descending);

        /// <summary>
        /// Returns the value of label showing the total number of coworkers currently displayed
        /// </summary>
        /// <returns></returns>
        public static int TotalCoworkersCountByLabel
        {
            get
            {
                var element = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/span"));
                var text = element.Text;
                return  int.Parse(text.Split(' ')[0]);
            }
        }

        /// <summary>
        /// Returns the total number of coworkers currently being displayed
        /// </summary>
        public static int TotalCoworkersCount => Commands.TotalRecordsCount();

        /// <summary>
        /// Returns true if the Departments filter has its elements arranged alphabetically by default
        /// </summary>
        public static bool AreFilterByDepartmentsInCorrectState
        {
            get
            {
                var filterByDepartmentBtn = Driver.Instance.FindElement(By.CssSelector("[default-label='Department']"));


                var departmentsOptionList =
                   filterByDepartmentBtn.FindElements(By.CssSelector("span.ng-binding"));

                for (int i = 1; i < departmentsOptionList.Count; i++)
                {
                    string previousDepartmentName = departmentsOptionList[i - 1].Text;
                    string currentDepartmentName = departmentsOptionList[i].Text;
                    if (string.Compare(previousDepartmentName, currentDepartmentName) == 1)
                    {
                        Report.Report.ToLogFile(MessageType.Message,
                                        $"Department:'{previousDepartmentName}' is before department:'{currentDepartmentName}' which is wrong. The department list must be sorted alphabetically.",
                                        null);
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Checks whether or not coworker home phone is callable from within contact list page
        /// </summary>
        public static bool IsCoworkerMobilePhoneCallable => Commands.IsRecordMobilePhoneCallable;

        /// <summary>
        /// Checks whether or not coworker home phone is callable from within contact list page
        /// </summary>
        public static bool IsCoworkerWorkPhoneCallable => Commands.IsRecordWorkPhoneCallable;


        /// <summary>
        /// Issue a search command to find one or more coworkers
        /// </summary>
        /// <returns>A search command with upon which you can search additional fields that match first name</returns>
        public static SearchCoworkerCommand FindCoworker()
        {
            return new SearchCoworkerCommand(LeftSideMenu.GoToCoworkers);
        }

        /// <summary>
        /// Issue a FilterBy command. Selects the department filter button from coworkers list page to reveal the department options
        /// </summary>
        /// <returns>A command upon which the filterby criteria are being build</returns>
        public static FilterCoworkersCommand FilterBy()
        {
            if (!IsAt) LeftSideMenu.GoToCoworkers();
            return new FilterCoworkersCommand();
        }

        /// <summary>
        /// Issue a SortBy command. Selects the sortby button from coworker list page to reveal the sortby options
        /// </summary>
        /// <returns>A command upon which the sortby criteria are being build</returns>
        public static SortCoworkersCommand SortBy()
        {
            if (!IsAt) LeftSideMenu.GoToCoworkers();
            Commands.ClickSortBy();
            return new SortCoworkersCommand();
        }

        /// <summary>
        /// If browser is at Coworkers List Page and there are filters set, it clears those filters
        /// </summary>
        public static void ResetFilters()
        {
            if (IsAt)
                Commands.ResetFilters();
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Opens the first coworker from the contact list, to view its details
        /// </summary>
        public static void OpenFirstCoworker()
        {
            if (!IsAt) LeftSideMenu.GoToCoworkers();
            Commands.OpenRecordFromListBySequence(1);
        }

        /// <summary>
        ///  Clicks "Invite more Co-Workers" button 
        /// </summary>
        public static void ClickInviteCoworkers()
        {
            if (!IsAt) LeftSideMenu.GoToCoworkers();
            
            Driver.Instance.FindElement(By.CssSelector("i[title='Invite Co-workers']")).Click();

        }
    }
}
