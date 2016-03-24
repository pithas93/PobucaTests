using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework;
using JPB_Framework.Pages.Contacts;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class ImportContactsTests : JpbBaseTest
    {

        // 1. Check that contacts template is successfully downloaded 
        [TestMethod]
        public void Contacts_Template_Is_Downloaded_Successfully()
        {
            ImportContactsWindow.DownloadTemplateFile();

        }

        // 2. Import contacts - all contact fields are filled 

        // 3. Import contacts - only mandatory fields are filled

        // 4. Import contacts - only mandatory fields are left unfilled

        // 5. Import contacts - only mandatory fields are filled. Organization field takes existent value

        // 6. Import contacts - only mandatory fields are filled. Organization field takes nonexistent value

        // 7. Import contacts - Fields are filled with nonsense values

        // 8. Import contacts - Fields are filled with values so that it cause field character overflow

        // 9. Import contacts - Test the max imported contact threshold

        // 10. Import contacts - Birthdate field contains invalid for date

        // 11. Import contacts - Template contains less columns than the original template

        // 12. Import contacts - Template contains more columns that the original template

        // 13. Import contacts - Template contains null rows between normal contact rows

        // 14. Immport contacts - Template does not contain the mandatory field column

        // 15. Import ontacts - Template contains duplicate contacts

        // 16. Import contacts - Template contains columns in different order than that of the original template

    }
}
