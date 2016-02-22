using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class EditContactPage
    {
        /// <summary>
        ///  Check if browser is at contact's edit page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Edit Contact"); } }

        /// <summary>
        ///  Navigates browser through the available edit button to the contact's edit page
        /// </summary>
        public static void GoTo() { Commands.ClickEdit(); }
    }
}
