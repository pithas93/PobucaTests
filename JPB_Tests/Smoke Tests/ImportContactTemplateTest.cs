using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Pages.Contacts;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.Smoke_Tests
{
    [TestClass]
    public class ImportContactTemplateTest : JpbBaseTest
    {

        [TestMethod]
        public void Can_Import_Contact_Template()
        {
            ImportContactsWindow.ImportFile("Contacts1.xls");

        }

    }
}
