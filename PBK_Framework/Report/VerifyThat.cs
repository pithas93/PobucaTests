﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Report
{
    /// <summary>
    /// Same use as the original Assert but, test execution is not interrupted in case of failed assertion. Outputs message to log file. For use to identify minor defects
    /// </summary>
    public class VerifyThat
    {

        /// <summary>
        /// Works like Assert.IsFalse. Asserts that a given condition is false, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="v">Boolean condition to be checked</param>
        /// <param name="message">Message to be shown in case the condition turns to be true</param>
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

        /// <summary>
        /// Works like Assert.IsTrue. Asserts that a given condition is true, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="v">Boolean condition to be checked</param>
        /// <param name="message">Message to be shown in case the condition turns to be false</param>
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

        /// <summary>
        /// Works like Assert.AreEqual. Asserts that two given strings are equal, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="a">First string to be checked</param>
        /// <param name="b">Second string to be checked</param>
        /// <param name="message">Message to be shown in case the two strings are not equal</param>
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

        /// <summary>
        /// Works like Assert.AreEqual. Asserts that two given integers are equal, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="a">First integer to be checked</param>
        /// <param name="b">Second integer to be checked</param>
        /// <param name="message">Message to be shown in case the two integers are not equal</param>
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

        /// <summary>
        /// Works like Assert.AreEqual. Asserts that two given floats are equal, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="a">First float to be checked</param>
        /// <param name="b">Second float to be checked</param>
        /// <param name="message">Message to be shown in case the two floats are not equal</param>
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

        /// <summary>
        /// Works like Assert.AreEqual. Asserts that two given doubles are equal, otherwise logs a message to test results file and continues test execution.
        /// </summary>
        /// <param name="a">First double to be checked</param>
        /// <param name="b">Second double to be checked</param>
        /// <param name="message">Message to be shown in case the two doubles are not equal</param>
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
