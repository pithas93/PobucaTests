using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Report
{
    /// <summary>
    /// Sames use as the original Assert. In case of AssertionFailedException, outputs to log filee And terminates test execution.
    /// </summary>
    public class AssertThat
    {

        public static void IsFalse(bool v, string message)
        {
            try
            {
                Assert.IsFalse(v);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, null);
                throw e;
            }
        }



        public static void IsTrue(bool v, string message)
        {
            try
            {
                Assert.IsTrue(v);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, null);
                throw e;
            }
        }

        public static void AreEqual(string a, string b, string message)
        {
            try
            {
                Assert.AreEqual(a, b, message);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, e);
                throw e;
            }
        }

        public static void AreEqual(int a, int b, string message)
        {
            try
            {
                Assert.AreEqual(a, b, message);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, e);
                throw e;
            }
        }

        public static void AreEqual(float a, float b, string message)
        {
            try
            {
                Assert.AreEqual(a, b, message);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, e);
                throw e;
            }
        }

        public static void AreEqual(double a, double b, string message)
        {
            try
            {
                Assert.AreEqual(a, b, message);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.AssertionError, message, e);
                throw e;
            }
        }
    }


}
