using JPB_Framework.Navigation;
using JPB_Framework.Pages.Contacts;
using JPB_Framework.Pages.Organizations;
using JPB_Framework.Report;
using JPB_Framework.Workflows;
using JPB_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests.ContactsTests
{
    [TestClass]
    public class MinorContactTests : ContactsBaseTest
    {

//        Inside contact create/edit there is a comment field and below that there is a text length indicator.Check that it works correctly
//        Oti de otan eisageis hmeromhnia me to datepicker, exei th swsth morfh kai einai egkyrh h hmeromhnia
//          Oti sta combo fields de fernei guid times
//          Oti sta combo oi times einai alphabitika topo8ethmenes
//          Oti otan mpaineis se ena contact mesa apo to organization contact list, to path einai swsto
//          oti mporeis na kaneis call apo thl ths epafh ston contact list

        /// <summary>
        /// Check that clicking a telephone number within a contact, results in showing a dialog to select an app to dial the number or automatically calls the number.
        /// </summary>
        [TestMethod]
        public void Call_A_Contact_Telephone_Number()
        {
            ContactCreator.CreateSimpleContact();
            AssertThat.IsTrue(ContactViewPage.IsMobileNumberCallable, "Contact mobile phone is not callable");
        }

        /// <summary>
        /// Check that a contact can be shared with a valid email and that the share button enabled.
        /// </summary>
        [TestMethod]
        public void Check_Contact_Is_Shareable_With_Valid_Email()
        {
            ContactsPage.OpenFirstContact();
            AssertThat.IsTrue(ContactViewPage.IsContactShareableTo(DummyData.EmailValue), "Though email inserted is of valid syntax, Share button is not enabled.");

        }

        /// <summary>
        /// Check that the filter that checks the validity of the email input in share contact dialog box, prevents the input of invalid format values
        /// </summary>
        [TestMethod]
        public void Check_Share_Contact_Email_Input_Filter()
        {
            ContactsPage.OpenFirstContact();
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo(DummyData.NonsenseValue), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo("_.@.co"), "Email field input does not follows email syntaxt but, it was accepted by the filter.");
            VerifyThat.IsFalse(ContactViewPage.IsContactShareableTo(DummyData.SimpleWord), "Email field input does not follows email syntaxt but, it was accepted by the filter.");

        }




    }
}
