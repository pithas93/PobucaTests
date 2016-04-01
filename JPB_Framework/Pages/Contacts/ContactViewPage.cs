using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using JPB_Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JPB_Framework
{
    public class ContactViewPage
    {
        /// <summary>
        /// Check if browser is at the selected contact's detail view page
        /// </summary>
        public static bool IsAt { get { return Driver.CheckIfIsAt("Contact View"); } }

        public static bool AreContactFieldValuesCorrect
        {
            get
            {
                bool notOK = false;

                if (DummyData.FirstName != FirstName) notOK = NotOk("First Name", FirstName, DummyData.FirstName);
                if (DummyData.LastName != LastName) notOK = NotOk("Last Name", LastName, DummyData.LastName);
                if (DummyData.MiddleName != MiddleName) notOK = NotOk("Middle Name", MiddleName, DummyData.MiddleName);
                if (DummyData.Suffix != Suffix) notOK = NotOk("Suffix", Suffix, DummyData.Suffix);
                if (DummyData.OrganizationNameExisting != OrganizationName) notOK = NotOk("Organization Name", OrganizationName, DummyData.OrganizationNameExisting);
                if (DummyData.Department != Department) notOK = NotOk("Department", Department, DummyData.Department);

                if (DummyData.WorkPhone != WorkPhone) notOK = NotOk("Work Phone", WorkPhone, DummyData.WorkPhone);
                if (DummyData.WorkPhone2 != WorkPhone2) notOK = NotOk("Work Phone 2", WorkPhone2, DummyData.WorkPhone2);
                if (DummyData.MobilePhone != MobilePhone) notOK = NotOk("Mobile Phone", MobilePhone, DummyData.MobilePhone);
                if (DummyData.MobilePhone2 != MobilePhone2) notOK = NotOk("Mobile Phone 2", MobilePhone2, DummyData.MobilePhone2);
                if (DummyData.HomePhone != HomePhone) notOK = NotOk("Home Phone", HomePhone, DummyData.HomePhone);
                if (DummyData.HomePhone2 != HomePhone2) notOK = NotOk("Home Phone 2", HomePhone2, DummyData.HomePhone2);
                if (DummyData.HomeFax != HomeFax) notOK = NotOk("Home Fax", HomeFax, DummyData.HomeFax);
                if (DummyData.WorkFax != WorkFax) notOK = NotOk("Work Fax", WorkFax, DummyData.WorkFax);
                if (DummyData.OtherPhone != OtherPhone) notOK = NotOk("Other Phone", OtherPhone, DummyData.OtherPhone);

                if (DummyData.Email != Email) notOK = NotOk("Email", Email, DummyData.Email);
                if (DummyData.PersonalEmail != PersonalEmail) notOK = NotOk("Personal Email", PersonalEmail, DummyData.PersonalEmail);
                if (DummyData.OtherEmail != OtherEmail) notOK = NotOk("Other Email", OtherEmail, DummyData.OtherEmail);

                if (DummyData.WorkStreet != WorkStreet) notOK = NotOk("Work Street", WorkStreet, DummyData.WorkStreet);
                if (DummyData.WorkCity != WorkCity) notOK = NotOk("Work City", WorkCity, DummyData.WorkCity);
                if (DummyData.WorkState != WorkState) notOK = NotOk("Work State", WorkState, DummyData.WorkState);
                if (DummyData.WorkPostalCode != WorkPostalCode) notOK = NotOk("Work Postal Code", WorkPostalCode, DummyData.WorkPostalCode);
                if (DummyData.WorkCountry != WorkCountry) notOK = NotOk("Work Country", WorkCountry, DummyData.WorkCountry);

                if (DummyData.HomeStreet != HomeStreet) notOK = NotOk("Home Street", HomeStreet, DummyData.HomeStreet);
                if (DummyData.HomeCity != HomeCity) notOK = NotOk("Home City", HomeCity, DummyData.HomeCity);
                if (DummyData.HomeState != HomeState) notOK = NotOk("Home State", HomeState, DummyData.HomeState);
                if (DummyData.HomePostalCode != HomePostalCode) notOK = NotOk("Home Postal Code", HomePostalCode, DummyData.HomePostalCode);
                if (DummyData.HomeCountry != HomeCountry) notOK = NotOk("Home Country", HomeCountry, DummyData.HomeCountry);

                if (DummyData.OtherStreet != OtherStreet) notOK = NotOk("Other Street", OtherStreet, DummyData.OtherStreet);
                if (DummyData.OtherCity != OtherCity) notOK = NotOk("Other City", OtherCity, DummyData.OtherCity);
                if (DummyData.OtherState != OtherState) notOK = NotOk("Other State", OtherState, DummyData.OtherState);
                if (DummyData.OtherPostalCode != OtherPostalCode) notOK = NotOk("Other Postal Code", OtherPostalCode, DummyData.OtherPostalCode);
                if (DummyData.OtherCountry != OtherCountry) notOK = NotOk("Other Country", OtherCountry, DummyData.OtherCountry);

                if (DummyData.Salutation != Salutation) notOK = NotOk("Salutation", Salutation, DummyData.Salutation);
                if (DummyData.Nickname != Nickname) notOK = NotOk("Nickname", Nickname, DummyData.Nickname);
                if (DummyData.JobTitle != JobTitle) notOK = NotOk("Job Title", JobTitle, DummyData.JobTitle);

                if (DummyData.Website != Website) notOK = NotOk("Website", Website, DummyData.Website);
                if (DummyData.Religion != Religion) notOK = NotOk("Religion", Religion, DummyData.Religion);
                if (DummyData.Birthdate != Birthdate) notOK = NotOk("Birthdate", Birthdate, DummyData.Birthdate);
                if (DummyData.Gender != Gender) notOK = NotOk("Gender", Gender, DummyData.Gender);

                if (DummyData.Comments != Comments) notOK = NotOk("Comments", Comments, DummyData.Comments);
                if (DummyData.AllowSMS != AllowSMS) notOK = NotOk("Allow SMS", AllowSMS, DummyData.AllowSMS);
                if (DummyData.AllowPhones != AllowPhones) notOK = NotOk("Allow Phones", AllowPhones, DummyData.AllowPhones);
                if (DummyData.AllowEmails != AllowEmails) notOK = NotOk("Allow Emails", AllowEmails, DummyData.AllowEmails);

                if (notOK) return false;
                return true;
            }
        }

        /// <summary>
        /// Issue delete command from a contact's detail view page
        /// </summary>
        /// <returns></returns>
        public static DeleteRecordCommand DeleteContact()
        {
            return new DeleteRecordCommand();
        }

        private static bool NotOk(string fieldName, string field, string dummyField)
        {
            Report.ToLogFile(MessageType.Message, $"Field: {fieldName} has value='{field}' but was expected to have value='{dummyField}'", null);
            return true;
        }

        private static bool NotOk(string fieldName, bool field, bool dummyField)
        {
            Report.ToLogFile(MessageType.Message, $"Field: {fieldName} has value='{field}' but was expected to have value='{dummyField}'", null);
            return true;
        }


        // REQUIRED FIELDS START ////////////////////////////////////////////////

        public static string FirstName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='First Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string LastName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Last Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string MiddleName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Middle Name']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Suffix
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Suffix']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string MobilePhone
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Mobile Phone']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string Email
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Email']"));
                string text = element.GetAttribute("myattr");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        public static string OrganizationName
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("my-required-info[mytitle='Organization Name']"));
                string text = element.GetAttribute("myitem");
                if (text != null)
                    return text;
                return string.Empty;
            }
        }

        // REQUIRED FIELDS END ////////////////////////////////////////////////

        public static string Department
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.departmentID'] span"));
                if (!string.IsNullOrEmpty(element.Text))
                {
                    string str = element.Text;
                    return str.Substring(12);
                }
                return string.Empty;
            }
        }



        // EXTRA FIELDS START ////////////////////////////////////////////////

        public static string WorkPhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () =>
                            element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkPhone2
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Phone 2']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }
        
        public static string MobilePhone2
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Mobile Phone 2']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomePhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomePhone2
        {
            get
            {

                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Phone 2']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkFax
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Work Fax']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomeFax
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Home Fax']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherPhone
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        
        public static string PersonalEmail
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Personal Email']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherEmail
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Other Email']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        
        public static string WorkStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string WorkCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showWorkAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }


        public static string HomeStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomeCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomeState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomePostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string HomeCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showHomeAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }


        public static string OtherStreet
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstreet']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherCity
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresscity']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherState
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddressstate']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherPostalCode
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("span[ng-show='myaddresspostalcode']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string OtherCountry
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("div[ng-if='showOtherAddress(contact);']")));
                    var element2 = element.FindElement(By.CssSelector("div[ng-show='myaddresscountry']"));
                    string text = element2.Text;
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }


        public static string Salutation
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Tile / Salutation']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Nickname
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Nickname']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string JobTitle
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Job Title']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Website
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Website']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Religion
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Religion']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Birthdate
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Birthday']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Gender
        {
            get
            {
                try
                {
                    IWebElement element = null;
                    Driver.NoWait(
                        () => element = Driver.Instance.FindElement(By.CssSelector("my-extra-info[mytitle='Gender']")));
                    string text = element.GetAttribute("myattr");
                    if (text != null)
                        return text;
                    return string.Empty;
                }
                catch (NoSuchElementException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Comments
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.comments'] .feed-element.b-b-none.ng-binding"));
                if (!string.IsNullOrEmpty(element.Text))
                    return element.Text;
                return string.Empty;
            }
        }

        public static bool AllowSMS
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowSMS']"));
                string text = element.GetAttribute("class");
                if (String.IsNullOrEmpty(text)) return true;
                if (String.Equals(text, "ng-hide")) return false;
                throw new Exception();
            }
        }

        public static bool AllowPhones
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowPhones']"));
                string text = element.GetAttribute("class");
                if (String.IsNullOrEmpty(text)) return true;
                if (String.Equals(text, "ng-hide")) return false;
                throw new Exception();
            }
        }

        public static bool AllowEmails
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("div[ng-show='contact.allowEmails']"));
                string text = element.GetAttribute("class");
                if (String.IsNullOrEmpty(text)) return true;
                if (String.Equals(text, "ng-hide")) return false;
                throw new Exception();
            }
        }

        // EXTRA FIELDS END ////////////////////////////////////////////////


    }

}
