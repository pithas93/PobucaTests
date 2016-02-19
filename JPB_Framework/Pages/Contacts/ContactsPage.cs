using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class ContactsPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Contacts"); } }
        
        public static void SelectContact()
        {
            Driver.SelectRecordFromListBySequence(1);
        }
    }
}
