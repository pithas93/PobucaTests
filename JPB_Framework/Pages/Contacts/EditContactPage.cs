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
        public static bool IsAt { get { return Driver.CheckIfIsAt("Edit Contact"); } }

        public static void GoTo() { Commands.ClickEdit(); }
    }
}
