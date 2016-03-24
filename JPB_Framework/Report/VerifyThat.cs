using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Selenium
{
    /// <summary>
    /// Assert without interrupting test execution and output to log file. For use to identify minor defects
    /// </summary>
    public class VerifyThat
    {

        public static void IsFalse(bool v, string message)
        {
            try
            {
                Assert.IsFalse(v);
            }
            catch (AssertFailedException)
            {
                Report.ToLogFile(MessageType.VerificationError, message, null);
            }
        }

        public static void IsTrue(bool v, string message)
        {
            try
            {
                Assert.IsTrue(v);
            }
            catch (AssertFailedException)
            {
                Report.ToLogFile(MessageType.VerificationError, message, null);
            }
        }

        public static void AreEqual(string a, string b, string message)
        {
            try
            {
                Assert.AreEqual(a,b,message);
            }
            catch (AssertFailedException e)
            {
                Report.ToLogFile(MessageType.VerificationError, message, e);
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
                Report.ToLogFile(MessageType.VerificationError, message, e);
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
                Report.ToLogFile(MessageType.VerificationError, message, e);
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
                Report.ToLogFile(MessageType.VerificationError, message, e);
            }
        }
    }
}
