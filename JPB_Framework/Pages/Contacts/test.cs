using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace JPB_Framework.Pages.Contacts
{
    [TestClass]
    public class test 
    {
        [TestMethod]
        public void testcase()
        {
            IWebDriver driver = new OperaDriver("C:/Selenium/Opera_Driver/");
            driver.Navigate().GoToUrl("https://www.facebook.com");
            var email = driver.FindElement(By.Id("email"));
            var pass = driver.FindElement(By.Id("pass"));
            var loginBtn = driver.FindElement(By.Id("loginbutton"));
            email.SendKeys("coryfaios@hotmail.com");
            pass.SendKeys("p@n@gof31645928");
            loginBtn.Click();
        }
    }
}
