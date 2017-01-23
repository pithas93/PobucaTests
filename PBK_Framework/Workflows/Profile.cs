using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Report;

namespace JPB_Framework.Workflows
{
    public class Profile
    {

        internal List<RecordField> UserInfoFields;
        internal List<RecordField> AddressFields; 

        public bool IsProfileSavedAfterEdit { get; set; }

        public bool AreProfileChangesSavedCorrectly
        {
            get
            {

                if (!ProfilePage.IsAt) LeftSideMenu.GoToProfile();

                var notOk = false;

                foreach (var infoField in UserInfoFields)
                {
                    var valuesAreEqual = infoField.Value == infoField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = string.IsNullOrEmpty(infoField.Value) && string.IsNullOrEmpty(infoField.RecordViewPageFieldValue);

                    if (valuesAreEqual || valuesAreBothEmpty) continue;

                    Report.Report.ToLogFile(MessageType.Message,
                        $"Field: {infoField.Label} has value='{infoField.RecordViewPageFieldValue}' but was expected to have value='{infoField.Value}'",
                        null);
                    notOk = true;

                }

                foreach (var addressField in AddressFields)
                {
                    var valuesAreEqual = addressField.Value == addressField.RecordViewPageFieldValue;
                    var valuesAreBothEmpty = (addressField.Value == null) && string.IsNullOrEmpty(addressField.RecordViewPageFieldValue);

                    if (!valuesAreEqual && !valuesAreBothEmpty)
                    {
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {addressField.Label} has value='{addressField.RecordViewPageFieldValue}' but was expected to have value='{addressField.Value}'", null);
                        notOk = true;
                    }
                    else if (valuesAreBothEmpty && addressField.RecordViewPageIsFieldVisible)
                        Report.Report.ToLogFile(MessageType.Message, $"Field: {addressField.Label} has no value but its field is shown in contact's detail view page with value '{addressField.RecordViewPageFieldValue}'", null);

                }

                return !notOk;
            }   
        }

        public Profile()
        {
            UserInfoFields = new List<RecordField>();
            AddressFields = new List<RecordField>();

            UserInfoFields.Add(new RecordField(ProfileFields.FirstName , null, () => ProfilePage.FirstName, null));
            UserInfoFields.Add(new RecordField(ProfileFields.LastName , null, () => ProfilePage.LastName, null));
            UserInfoFields.Add(new RecordField(ProfileFields.WorkPhone , null, () => ProfilePage.WorkPhone, null));
            UserInfoFields.Add(new RecordField(ProfileFields.WorkPhoneExt , null, () => ProfilePage.WorkPhoneExt, null));
            UserInfoFields.Add(new RecordField(ProfileFields.WorkPhone2 , null, () => ProfilePage.WorkPhone2, null));
            UserInfoFields.Add(new RecordField(ProfileFields.WorkPhone2Ext , null, () => ProfilePage.WorkPhone2Ext, null));
            UserInfoFields.Add(new RecordField(ProfileFields.HomePhone , null, () => ProfilePage.HomePhone, null));
            UserInfoFields.Add(new RecordField(ProfileFields.MobilePhone , null, () => ProfilePage.MobilePhone, null));
            UserInfoFields.Add(new RecordField(ProfileFields.JobTitle , null, () => ProfilePage.JobTitle, null));
            UserInfoFields.Add(new RecordField(ProfileFields.Department , null, () => ProfilePage.Department, null));
            UserInfoFields.Add(new RecordField(ProfileFields.Email , null, () => ProfilePage.Email, null));
            UserInfoFields.Add(new RecordField(ProfileFields.PersonalEmail , null, () => ProfilePage.PersonalEmail, null));
            UserInfoFields.Add(new RecordField(ProfileFields.Role , null, () => ProfilePage.Role, null));

            AddressFields.Add(new RecordField(ProfileFields.WorkStreet , null, () => ProfilePage.WorkStreet, () => ProfilePage.AreWorkAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.WorkCity , null, () => ProfilePage.WorkCity, () => ProfilePage.AreWorkAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.WorkState , null, () => ProfilePage.WorkState, () => ProfilePage.AreWorkAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.WorkPostalCode , null, () => ProfilePage.WorkPostalCode, () => ProfilePage.AreWorkAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.WorkCountry , null, () => ProfilePage.WorkCountry, () => ProfilePage.AreWorkAddressFieldsVisible));

            AddressFields.Add(new RecordField(ProfileFields.HomeStreet , null, () => ProfilePage.HomeStreet, () => ProfilePage.AreHomeAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.HomeCity , null, () => ProfilePage.HomeCity, () => ProfilePage.AreHomeAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.HomeState , null, () => ProfilePage.HomeState, () => ProfilePage.AreHomeAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.HomePostalCode , null, () => ProfilePage.HomePostalCode, () => ProfilePage.AreHomeAddressFieldsVisible));
            AddressFields.Add(new RecordField(ProfileFields.HomeCountry , null, () => ProfilePage.HomeCountry, () => ProfilePage.AreHomeAddressFieldsVisible));

            UserInfoFields.Add(new RecordField(ProfileFields.CurrentPassword , null, () => ProfilePage.CurrentPassword, null));
            UserInfoFields.Add(new RecordField(ProfileFields.NewPassword , null, () => ProfilePage.NewPassword, null));
            UserInfoFields.Add(new RecordField(ProfileFields.ConfirmPassword , null, () => ProfilePage.ConfirmPassword, null));
        }


        internal string SetFieldValue(string fieldLabel, string newValue)
        {
            if (UserInfoFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                UserInfoFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else if (AddressFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                AddressFields.Find(x => x.Label.Contains(fieldLabel)).Value = newValue;
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
                Report.Report.AbruptFinalize();
                throw new Exception();
            }
            return newValue;
        }

        internal string GetFieldValue(string fieldLabel)
        {
            if (UserInfoFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return UserInfoFields.Find(x => x.Label.Contains(fieldLabel)).Value;
            else if (AddressFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return AddressFields.Find(x => x.Label.Contains(fieldLabel)).Value;
            Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
            Report.Report.AbruptFinalize();
            throw new Exception();
        }

        internal string SetFieldPreviousValue(string fieldLabel, string previousValue)
        {
            if (UserInfoFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                UserInfoFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else if (AddressFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                AddressFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue = previousValue;
            else
            {
                Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
                Report.Report.AbruptFinalize();
                throw new Exception();
            }
            return previousValue;
        }

        internal string GetFieldPreviousValue(string fieldLabel)
        {
            if (UserInfoFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return UserInfoFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            else if (AddressFields.Find(x => x.Label.Contains(fieldLabel)) != null)
                return AddressFields.Find(x => x.Label.Contains(fieldLabel)).PreviousValue;
            Report.Report.ToLogFile(MessageType.Message, "Something went wrong.", null);
            Report.Report.AbruptFinalize();
            throw new Exception();
        }

        public void Clone(Profile tmp)
        {
            throw new NotImplementedException();
        }
    }

    internal class ProfileFields
    {
        internal const string FirstName = "First Name";
        internal const string LastName = "Last Name";

        internal const string MobilePhone = "Mobile Phone";
        internal const string Email = "Email";
        internal const string PersonalEmail = "Personal Email";

        internal const string Department = "Department";
        internal const string JobTitle = "Job Title";
        internal const string Role = "Role";
        internal const string WorkPhone = "Work Phone";
        internal const string WorkPhoneExt = "Work Phone Ext";
        internal const string WorkPhone2 = "Work Phone2";
        internal const string WorkPhone2Ext = "Work Phone 2 Ext";
        internal const string HomePhone = "Home Phone";


        internal const string WorkStreet = "Work Street";
        internal const string WorkCity = "Work City";
        internal const string WorkState = "Work State";
        internal const string WorkPostalCode = "Work Postal Code";
        internal const string WorkCountry = "Work Country";
        internal const string HomeStreet = "Home Street";
        internal const string HomeCity = "Home City";
        internal const string HomeState = "Home State";
        internal const string HomePostalCode = "Home Postal Code";
        internal const string HomeCountry = "Home Country";

        internal const string CurrentPassword = "Current Password";
        internal const string NewPassword = "New Password";
        internal const string ConfirmPassword = "Confirm Password";


    }
}
