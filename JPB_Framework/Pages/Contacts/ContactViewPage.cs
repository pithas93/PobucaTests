using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class ContactViewPage
    {
        /// <summary>
        /// Check if browser is at the selected contact's detail view page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Contact View"); } }

        public static string FirstName
        {
            get
            {
                var firstName =
                    Driver.Instance.FindElement(
                        By.XPath(
                            "/html/body/div[4]/div/div[2]/div[2]/div[3]/div[2]/div[2]/div[2]/div[1]/div[2]/div[1]/div[1]/my-required-info/div/div/div"));
                if (firstName != null)
                    return firstName.Text;
                return string.Empty;
            }

        }

        public static string LastName
        {
            get
            {
                var lastName =
                    Driver.Instance.FindElement(
                        By.XPath(
                            "/html/body/div[4]/div/div[2]/div[2]/div[3]/div[2]/div[2]/div[2]/div[1]/div[2]/div[1]/div[2]/my-required-info/div/div/div"));
                if (lastName != null)
                    return lastName.Text;
                return string.Empty;
            }

        }

        /// <summary>
        /// Issue delete command from a contact's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteContact()
        {
            return new DeleteRecordCommand();
        }
    }

}
