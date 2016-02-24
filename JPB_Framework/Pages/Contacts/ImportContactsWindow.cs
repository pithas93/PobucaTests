using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Contacts
{
    public class ImportContactsWindow
    {
        public static void ImportFile(string contactsXls)
        {
            var importCombo = Driver.Instance.FindElement(By.CssSelector("i.fa.fa-file-text-o.jp-light-blue.f20"));
            importCombo.Click();
            var importOption =
                Driver.Instance.FindElement(
                    By.XPath("/html/body/div[4]/div/div[2]/div[2]/div[5]/div[2]/div[1]/div/div[5]/ul/li[4]/div/ul/li/a"));
            importOption.Click();

            var BrowseButton = Driver.Instance.FindElement(By.Id("file1"));
            BrowseButton.SendKeys("D:\\Google Drive\\Work\\Testing files - local temp\\JustPhoneBook Webpage\\Test Scenarios\\test_scenario_files\\Contacts2.xls");
            Driver.Wait(TimeSpan.FromSeconds(5));
            var NextBtn = Driver.Instance.FindElement(By.Id("next-step-import-btn"));
            NextBtn.Click();
        }
    }
}
