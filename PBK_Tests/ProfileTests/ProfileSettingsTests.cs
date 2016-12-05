using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPB_Framework.Navigation;
using JPB_Framework.Pages;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ProfileTests
{

    [TestClass]
    public class ProfileSettingsTests : PbkBaseTest
    {

        /// <summary>
        /// Assert that user can change his profile picture
        /// </summary>
        [TestMethod]
        public void Can_Change_Profile_Picture()
        {
//            LeftSideMenu.GoToProfile();
//            ProfileEditor.ChangePhoto();
//            AssertThat.IsTrue(ProfileEditor.IsPhotoChangedSuccessfully, "Profile photo was not changed successfully");
        }

        /// <summary>
        /// Assert that user is not able to assign an invalid file as his new profile picture
        /// </summary>
        [TestMethod]
        public void Cannot_Assign_Invalid_File_As_Profile_Picture()
        {

        }

        /// <summary>
        /// Assert that user profile settings and fields can be changed
        /// </summary>
        [TestMethod]
        public void Can_Edit_User_Profile()
        {
            LeftSideMenu.GoToProfile();
            ProfileEditor.EditAllProfileSettings();
            AssertThat.IsTrue(ProfileEditor.AreProfileChangesSavedCorrectly, "Profile changes did not save successfully after edit.");

        }

        /// <summary>
        /// Assert that user is unable to change either his role or his email field values
        /// </summary>
        [TestMethod]
        public void Cannot_Change_User_Role_And_Email()
        {
            
        }


    }
}
