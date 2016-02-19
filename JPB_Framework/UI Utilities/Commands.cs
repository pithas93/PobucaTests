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
        public static void ClickSave()
        {
            var saveBtn = Driver.Instance.FindElement(By.Id("save-entity"));
            saveBtn.Click();
        }

        internal static void ClickEdit()
        {
            var editBtn = Driver.Instance.FindElement(By.Id("edit-entity"));
            editBtn.Click();
        }
    }
}
