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
            //var firstContact = Driver.Instance.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[3]/div[1]"));
            //firstContact.Click();
        }
    }
}
