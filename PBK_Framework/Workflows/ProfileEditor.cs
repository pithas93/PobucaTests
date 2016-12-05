using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;

namespace JPB_Framework.Workflows
{
    public class ProfileEditor
    {

        private static Profile UserProfile { get; set; }
    


        public static void Initialize()
        {
            LeftSideMenu.GoToProfile();
            UserProfile = new Profile();
        }

        public static void ReadProfileSettings()
        {
            foreach (var infoField in UserProfile.UserInfoFields) infoField.Value = infoField.RecordViewPageFieldValue;
            foreach (var addressField in UserProfile.AddressFields) addressField.Value = addressField.RecordViewPageFieldValue;
        }

        public static bool IsProfileSavedSuccessfully => ProfilePage.IsProfileSavedSuccessfully;

        public static bool AreProfileChangesSavedCorrectly => UserProfile.AreProfileChangesSavedCorrectly; 

        public static bool IsPhotoChangedSuccessfully { get; set; }



        public static void EditProfile()
        {
            Initialize();
            ReadProfileSettings();

            var firstName = DummyData.SimpleWord;
            var lastName = DummyData.SimpleWord;
            var department = DummyData.DepartmentValue;

            ProfilePage.EditProfile()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithDepartment(department)
                .Edit();

            UserProfile.IsProfileSavedAfterEdit = IsProfileSavedSuccessfully;

        }

        public static void EditAllProfileSettings()
        {
            Initialize();
            ReadProfileSettings();

            var tmp = new Profile();

            tmp.SetFieldValue(ProfileFields.FirstName, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.LastName, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.MobilePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ProfileFields.PersonalEmail, DummyData.EmailValue);
            tmp.SetFieldValue(ProfileFields.Department, DummyData.DepartmentValue);
            tmp.SetFieldValue(ProfileFields.JobTitle, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.WorkPhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ProfileFields.WorkPhoneExt, DummyData.NumericValue);
            tmp.SetFieldValue(ProfileFields.WorkPhone2, DummyData.PhoneValue);
            tmp.SetFieldValue(ProfileFields.WorkPhone2Ext, DummyData.NumericValue);
            tmp.SetFieldValue(ProfileFields.HomePhone, DummyData.PhoneValue);
            tmp.SetFieldValue(ProfileFields.WorkStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ProfileFields.WorkCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.WorkState, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.WorkPostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ProfileFields.WorkCountry, DummyData.CountryValue);
            tmp.SetFieldValue(ProfileFields.HomeStreet, DummyData.AddressValue);
            tmp.SetFieldValue(ProfileFields.HomeCity, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.HomeState, DummyData.SimpleWord);
            tmp.SetFieldValue(ProfileFields.HomePostalCode, DummyData.NumericValue);
            tmp.SetFieldValue(ProfileFields.HomeCountry, DummyData.CountryValue);

            ProfilePage.EditProfile()
                .WithMultipleValues(tmp.UserInfoFields, tmp.AddressFields)
                .Edit();

            UserProfile.IsProfileSavedAfterEdit = IsProfileSavedSuccessfully;

            if (!UserProfile.IsProfileSavedAfterEdit) return;
            UserProfile.Clone(tmp);

        }

        public static void ChangePhoto()
        {

            Initialize();

            
        }

    }
}
