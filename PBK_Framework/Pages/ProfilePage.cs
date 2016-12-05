using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Report;
using JPB_Framework.Selenium;
using JPB_Framework.UI_Utilities;
using OpenQA.Selenium;
using JPB_Framework.Workflows;
using NUnit.Framework.Constraints;

namespace JPB_Framework.Pages
{
    public class ProfilePage
    {
        public static bool IsAt => Driver.CheckIfIsAt("Home  /  Edit Profile");

        public static bool IsProfileSavedSuccessfully { get; set; }


        public static EditProfileCommand EditProfile()
        {
            return new EditProfileCommand();
        }




        public static string FirstName
        {
            get { return GetFieldValueFor(ProfileFields.FirstName); }
            set { SetFieldValueFor(ProfileFields.FirstName, value); }
        }

        public static string LastName
        {
            get { return GetFieldValueFor(ProfileFields.LastName); }
            set { SetFieldValueFor(ProfileFields.LastName, value); }
        }

        public static string WorkPhone
        {
            get { return GetFieldValueFor(ProfileFields.WorkPhone); }
            set { SetFieldValueFor(ProfileFields.WorkPhone, value); }
        }

        public static string WorkPhoneExt
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("[mymodel$='workPhoneExtension'] [id='Ext.']"));
                return element.GetAttribute("value");
            }
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("[mymodel$='workPhoneExtension'] [id='Ext.']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string WorkPhone2
        {
            get { return GetFieldValueFor(ProfileFields.WorkPhone2); }
            set { SetFieldValueFor(ProfileFields.WorkPhone2, value); }
        }

        public static string WorkPhone2Ext
        {
            get
            {
                var element = Driver.Instance.FindElement(By.CssSelector("[mymodel$='workPhone2Extension'] [id='Ext.']"));
                return element.GetAttribute("value");
            }
            set
            {
                var element = Driver.Instance.FindElement(By.CssSelector("[mymodel$='workPhone2Extension'] [id='Ext.']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string HomePhone
        {
            get { return GetFieldValueFor(ProfileFields.HomePhone); }
            set { SetFieldValueFor(ProfileFields.HomePhone, value); }
        }

        public static string MobilePhone
        {
            get { return GetFieldValueFor(ProfileFields.MobilePhone); }
            set { SetFieldValueFor(ProfileFields.MobilePhone, value); }
        }

        public static string JobTitle
        {
            get { return GetFieldValueFor(ProfileFields.JobTitle); }
            set { SetFieldValueFor(ProfileFields.JobTitle, value); }
        }

        public static string Department
        {
            get
            {

                var departmentList =
                  Driver.Instance.FindElements(
                      By.CssSelector("my-select[myname='Department'] div select option.ng-binding.ng-scope"));
                foreach (var department in departmentList)
                {
                    var isSelected = department.GetAttribute("ng-selected");
                    if (isSelected.Equals("true")) return department.Text;
                }
                return string.Empty;

            }
            set
            {
                var departmentList =
                  Driver.Instance.FindElements(
                      By.CssSelector("my-select[myname='Department'] div select option.ng-binding.ng-scope"));
                SelectFromListByName(departmentList, value);
            }
        }

        public static string Email
        {
            get { return GetFieldValueFor(ProfileFields.Email); }
            set { SetFieldValueFor(ProfileFields.Email, value); }
        }

        public static string PersonalEmail
        {
            get { return GetFieldValueFor(ProfileFields.PersonalEmail); }
            set { SetFieldValueFor(ProfileFields.PersonalEmail, value); }
        }

        public static string Role
        {
            get { return GetFieldValueFor(ProfileFields.Role); }
            set { SetFieldValueFor(ProfileFields.Role, value); }
        }

        public static string WorkStreet
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input#Street"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string WorkCity
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input#City"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string WorkState
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string WorkPostalCode
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string WorkCountry
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("workAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Work);
                }
                var element = workAddrElements.FindElement(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string HomeStreet
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input#Street"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Home);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#Street"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string HomeCity
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input#City"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Home);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input#City"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string HomeState
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Home);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='State / Province']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string HomePostalCode
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Home);
                }
                var element = workAddrElements.FindElement(By.CssSelector("input[id='Postal Code']"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static bool AreWorkAddressFieldsVisible => IsAddressFieldVisible("work");
        public static bool AreHomeAddressFieldsVisible => IsAddressFieldVisible("home");

        public static string HomeCountry
        {
            get
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed) return string.Empty;

                var element = workAddrElements.FindElement(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                return element.GetAttribute("value");
            }
            set
            {
                var workAddrElements = Driver.Instance.FindElement(By.Id("homeAddress"));
                if (!workAddrElements.Displayed)
                {
                    ExpandAddressCategories();
                    InsertAddressField(AddressFields.Home);
                }
                var element = workAddrElements.FindElement(By.CssSelector("my-select[myname='Country'] div select option.ng-binding.ng-scope"));
                element.Clear();
                element.SendKeys(value);
            }
        }

        public static string CurrentPassword { get; internal set; }
        public static string NewPassword { get; internal set; }
        public static string ConfirmPassword { get; internal set; }


        private static bool IsAddressFieldVisible(string type)
        {
            var element = Driver.Instance.FindElement(By.Id($"{type}Address"));
            var tmp = element.GetAttribute("aria-hidden");
            if (tmp != null) return tmp.Equals("true");
            throw new Exception();
        }

        private static string GetFieldValueFor(string field)
        {
            var element = Driver.Instance.FindElement(By.CssSelector($"[id='{field}']"));
            return element.GetAttribute("value");
        }

        private static void SetFieldValueFor(string field, string value)
        {
            var element = Driver.Instance.FindElement(By.CssSelector($"[id='{field}']"));
            element.Clear();
            element.SendKeys(value);
        }

        private static void ExpandAddressCategories()
        {
            Driver.Instance.FindElement(By.CssSelector("span[class^='jp-light-blue']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }


        private static void InsertAddressField(string category)
        {
            Driver.Instance.FindElement(By.CssSelector($"a[ng-click^='userInfo.{category}']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        private static void SelectFromListByName(ReadOnlyCollection<IWebElement> elementList, string value)
        {
            var optionExists = false;
            foreach (var option in elementList)
            {
                if (string.Equals(option.Text, value))
                {
                    option.Click();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                    optionExists = true;
                    break;
                }
            }
            if (optionExists)
            {
                return;
            }
            Commands.PressEscapeKey();
            Report.Report.ToLogFile(MessageType.Message, $"The option {value} does not exist within the list.", null);
        }

        private static class AddressFields
        {
            public const string Work = "work";
            public const string Home = "home";

        }

        public class EditProfileCommand
        {
            private string firstName;
            private string lastName;

            private string mobilePhone;
            private string email;
            private string personalEmail;

            private string department;
            private string jobTitle;
            private string role;
            private string workPhone;
            private string workPhoneExt;
            private string workPhone2;
            private string workPhone2Ext;
            private string homePhone;


            private string workStreet;
            private string workCity;
            private string workState;
            private string workPostalCode;
            private string workCountry;
            private string homeStreet;
            private string homeCity;
            private string homeState;
            private string homePostalCode;


            private string currentPassword;
            private string newPassword;
            private string confirmPassword;

            public EditProfileCommand WithFirstName(string firstName)
            {
                this.firstName = firstName;
                return this;
            }

            public EditProfileCommand WithLastName(string lastName)
            {
                this.lastName = lastName;
                return this;
            }

            public EditProfileCommand WithDepartment(string department)
            {
                this.department = department;
                return this;
            }

            public EditProfileCommand WithMultipleValues(List<RecordField> userInfoFields, List<RecordField> addressFields)
            {
                firstName = userInfoFields.Find(x => x.Label.Contains(ProfileFields.FirstName)).Value;
                lastName = userInfoFields.Find(x => x.Label.Contains(ProfileFields.LastName)).Value;
                mobilePhone = userInfoFields.Find(x => x.Label.Contains(ProfileFields.MobilePhone)).Value;
                personalEmail = userInfoFields.Find(x => x.Label.Contains(ProfileFields.PersonalEmail)).Value;
                department = userInfoFields.Find(x => x.Label.Contains(ProfileFields.Department)).Value;
                jobTitle = userInfoFields.Find(x => x.Label.Contains(ProfileFields.JobTitle)).Value;
                workPhone = userInfoFields.Find(x => x.Label.Contains(ProfileFields.WorkPhone)).Value;
                workPhoneExt = userInfoFields.Find(x => x.Label.Contains(ProfileFields.WorkPhoneExt)).Value;
                workPhone2 = userInfoFields.Find(x => x.Label.Contains(ProfileFields.WorkPhone2)).Value;
                workPhone2Ext = userInfoFields.Find(x => x.Label.Contains(ProfileFields.WorkPhone2Ext)).Value;
                homePhone = userInfoFields.Find(x => x.Label.Contains(ProfileFields.HomePhone)).Value;
                workStreet = addressFields.Find(x => x.Label.Contains(ProfileFields.WorkStreet)).Value;
                workCity = addressFields.Find(x => x.Label.Contains(ProfileFields.WorkCity)).Value;
                workState = addressFields.Find(x => x.Label.Contains(ProfileFields.WorkState)).Value;
                workPostalCode = addressFields.Find(x => x.Label.Contains(ProfileFields.WorkPostalCode)).Value;
                workCountry = addressFields.Find(x => x.Label.Contains(ProfileFields.WorkCountry)).Value;
                homeStreet = addressFields.Find(x => x.Label.Contains(ProfileFields.HomeStreet)).Value;
                homeCity = addressFields.Find(x => x.Label.Contains(ProfileFields.HomeCity)).Value;
                homeState = addressFields.Find(x => x.Label.Contains(ProfileFields.HomeState)).Value;
                homePostalCode = addressFields.Find(x => x.Label.Contains(ProfileFields.HomePostalCode)).Value;
                HomeCountry = addressFields.Find(x => x.Label.Contains(ProfileFields.HomeCountry)).Value;

                return this;
            }

            public void Edit()
            {
                if (firstName != null) FirstName = firstName;
                if (lastName != null) LastName = lastName;

                if (mobilePhone != null) MobilePhone = mobilePhone;
                if (email != null) Email = email;
                if (personalEmail != null) PersonalEmail = personalEmail;

                if (department != null) Department = department;
                if (jobTitle != null) JobTitle = jobTitle;
                if (role != null) Role = role;
                if (workPhone != null) WorkPhone = workPhone;
                if (workPhoneExt != null) WorkPhoneExt = workPhoneExt;
                if (workPhone2 != null) WorkPhone2 = workPhone2;
                if (workPhone2Ext != null) WorkPhone2Ext = workPhone2Ext;
                if (homePhone != null) HomePhone = homePhone;


                if (workStreet != null) WorkStreet = workStreet;
                if (workCity != null) WorkCity = workCity;
                if (workState != null) WorkState = workState;
                if (workPostalCode != null) WorkPostalCode = workPostalCode;
                if (workCountry != null) WorkCountry = WorkCountry;
                if (homeStreet != null) HomeStreet = HomeStreet;
                if (homeCity != null) HomeCity = homeCity;
                if (homeState != null) HomeState = HomeState;
                if (homePostalCode != null) HomePostalCode = homePostalCode;


                if (currentPassword != null) CurrentPassword = currentPassword;
                if (newPassword != null) NewPassword = newPassword;
                if (confirmPassword != null) ConfirmPassword = confirmPassword;


                Driver.Wait(TimeSpan.FromSeconds(5));
                IsProfileSavedSuccessfully = Commands.ClickSave();
            }


        }

    }
}
