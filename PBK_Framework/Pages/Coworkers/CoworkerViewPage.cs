using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;

namespace JPB_Framework.Pages.Coworkers
{
    public class CoworkerViewPage
    {

        /// <summary>
        /// Check if browser is at coworkers view page
        /// </summary>
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Co-workers  /  Co-worker View");


        /// <summary>
        /// Checks if the input for the share window email address field complies with the email format. 
        /// Returns true if the Share button is enabled.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsCoworkerShareableTo(string email) => Commands.IsRecordShareableTo(email);


        public static string FirstName => GetFieldValueFor("First Name");

        public static string LastName => GetFieldValueFor("Last Name");

        public static string MobilePhone => GetFieldValueFor("Mobile Phone");
        public static bool IsMobilePhoneCallable => IsTelephoneLinkActive("Mobile Phone", () => MobilePhone);

        public static string Department => GetFieldValueFor("Department");

        public static string WorkPhone => GetFieldValueFor("Work Phone");
        public static bool IsWorkPhoneCallable => IsTelephoneLinkActive("Work Phone", () => WorkPhone);

        public static string WorkPhone2 => GetFieldValueFor("Work Phone 2");
        public static bool IsWorkPhone2Callable => IsTelephoneLinkActive("Work Phone 2", () => WorkPhone2);

        public static string HomePhone => GetFieldValueFor("Home Phone");
        public static bool IsHomePhoneCallable => IsTelephoneLinkActive("Home Phone", () => HomePhone);

        public static string WorkEmail => GetFieldValueFor("Email");
        public static bool IsWorkEmailEmailable => IsEmailLinkActive("Email", () => WorkEmail);


        public static string PersonalEmail => GetFieldValueFor("Personal Email");
        public static bool IsPersonalEmailEmailable => IsEmailLinkActive("Personal Email", () => PersonalEmail);

        public static string JobTitle => GetFieldValueFor("Job Title");

        public static string UserRole => GetFieldValueFor("User Role");









        private static bool IsEmailLinkActive(string fieldName, Func<string> coworkerViewPageField)
        {
            var element =
                  Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}'] a.ng-scope"));
            var href = element.GetAttribute("href");
            var expectedEmailLink = $"mailto:{coworkerViewPageField()}";
            return (href == expectedEmailLink);
        }
        private static bool IsTelephoneLinkActive(string fieldName, Func<string> coworkerViewPageField)
        {
            var element =
                  Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}']"));
            var tmp = element.GetAttribute("myattr");
            if (string.IsNullOrEmpty(tmp)) return true;

            var phoneElement = element.FindElement(By.CssSelector("a.ng-scope"));
            var href = phoneElement.GetAttribute("href");
            var expectedEmailLink = $"tel:{coworkerViewPageField()}";
            return (href == expectedEmailLink);
        }

        private static string GetFieldValueFor(string fieldName)
        {
            if (!IsFieldVisible(fieldName)) return string.Empty;
            var element = Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}']"));
            var text = element.GetAttribute("myattr");
            if (text != null)
                return text;
            return string.Empty;
        }

        private static bool IsFieldVisible(string fieldName)
        {
            try
            {
                Driver.NoWait(() => Driver.Instance.FindElement(By.CssSelector($"[mytitle='{fieldName}']")));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}
