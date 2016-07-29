using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;

namespace JPB_Framework.Pages
{
    public class InvitePage
    {

        /// <summary>
        /// Check if browser is at invite co-workers page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Invite Co-workers");
    }
}
