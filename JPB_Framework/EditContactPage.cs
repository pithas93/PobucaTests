using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework
{
    public class EditContactPage
    {
        public static bool IsAt { get { return Driver.CheckIfIsAt("Edit Contact"); } }

        public static void GoTo()
        {
            var editBtn = Driver.Instance.FindElement(By.Id("edit-entity"));
            editBtn.Click();
        }
    }
}
