using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Selenium
{
    /// <summary>
    /// A class to output messages Or test output to a console, file, etc
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Collect info about the class And the method that threw the assertion error
        /// </summary>
        /// <returns></returns>
        private static string MessageType(MessageType type)
        {
            string messageType = "";
            switch (type)
            {
                case Selenium.MessageType.AssertionError:
                    {
                        messageType = $"Assertion error, message= ";
                        break;
                    }
                case Selenium.MessageType.VerificationError:
                    {
                        messageType = $"Verification error, message= ";
                        break;
                    }
                case Selenium.MessageType.Exception:
                    {
                        messageType = $"Exception thrown, message= ";
                        break;
                    }
                case Selenium.MessageType.Message:
                    {
                        messageType = "Message= ";
                        break;
                    }
                case Selenium.MessageType.Empty:
                    {
                        messageType = "";
                        break;
                    }
            }
            return messageType;
        }

        /// <summary>
        /// Output message to report file.
        /// </summary>
        /// <param name="type"> Message type</param>
        /// <param name="message"> Message to write to report file</param>
        /// <param name="e"></param>>
        public static void ToLogFile(MessageType type, string message, Exception e)
        {
            StringBuilder reportOutput = new StringBuilder();
            //string reportOutput = DateTime.Now.ToShortDateString();
            reportOutput.Append(DateTime.Now.ToShortDateString());
            reportOutput.Append(" - ");
            reportOutput.Append(DateTime.Now.ToLongTimeString());
            reportOutput.Append('\n');
            reportOutput.Append(MessageType(type));
            reportOutput.Append(message);
            reportOutput.Append('\n');

            if (!(type == Selenium.MessageType.Empty))
                if (e != null)
                {
                    reportOutput.Append('\t');
                    reportOutput.Append(e.Message);
                    reportOutput.Append("\n\t");
                    reportOutput.Append(e.StackTrace);
                }
                else
                {
                    StackTrace stack = new StackTrace(true);
                    StackFrame frame = stack.GetFrame(2);

                    reportOutput.Append("\tClass = ");
                    reportOutput.Append(frame.GetMethod().ReflectedType.FullName);
                    reportOutput.Append('\n');
                    reportOutput.Append("\tTest = ");
                    reportOutput.Append(frame.GetMethod().Name);
                    reportOutput.Append('\n');
                    reportOutput.Append("\tLine = ");
                    reportOutput.Append(frame.GetFileLineNumber());
                }

            reportOutput.Append("\n\n");
            File.AppendAllText("C:\\Selenium\\test_report.txt", reportOutput.ToString());
        }

        /// <summary>
        /// Open report file to show test results
        /// </summary>
        public static void ShowReport()
        {
            System.Diagnostics.Process.Start("CMD.exe", "notepad++ C:\\Selenium\\test_report.txt");
        }

        /// <summary>
        /// Clear report file to output test results
        /// </summary>
        public static void Initialize(string testClassName, string testMethodName)
        {
            Report.ToLogFile(Selenium.MessageType.Empty, $"{testClassName} \n{testMethodName}\nTest Started", null);
        }

        public static void Finalize(string testClassName, string testMethodName, UnitTestOutcome testOutput)
        {
            Report.ToLogFile(Selenium.MessageType.Empty, $"{testClassName} \n{testMethodName}\nTest {testOutput}", null);
        }
    }
}
