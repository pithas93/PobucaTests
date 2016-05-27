using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Framework.Report
{
    /// <summary>
    /// A class to output messages Or test output to a console, file, etc
    /// </summary>
    public class Report
    {
        private const string ReportFilePath = "C:\\Selenium\\test_report.html";

        private const string ReportFileHeader =
            "<!DOCTYPE html>" +
            "<html> " +
            "	<head>" +
            "		<meta charset='utf-8'/>" +
            "		<style>" +
            "			.results_table{" +
            "				border-collapse: collapse;" +
            "				table-layout: fixed;" +
            "				width: 2000px;" +
            "				text-align: left;" +
            "				overflow-x:auto;" +
            "			}" +
            "			th, td {" +
            "				border: 1px solid black;" +
            "				overflow: hidden;" +
            "				width: 100%;" +
            "			}			" +
            "           thead {" +
            "               border: 1px solid black;" +
            "				background-color: #777777;" +
            "   		}" +
            "           tbody {" +
            "               border: 1px solid black;" +
            "				outline-color:blue;" +
            "   		}" +
            "			.single_value{" +
            "				width: 50px;" +
            "				margin: 0 auto;" +
            "			}			" +
            "			.short_text {" +
            "				margin: 0 auto;" +
            "				width: 75px;" +
            "			}			" +
            "			.medium_text {" +
            "				margin: 0 auto;" +
            "				width: 200px;" +
            "				word-break:break-all;" +
            "			}			" +
            "			.long_text {" +
            "				" +
            "				margin: 0 auto;" +
            "				word-break:break-all;" +
            "			}			" +
            "			.error {background-color:#FF0000; } " +
            "			.groove {outline-style: groove; }" +
            "		</style>" +
            "	</head>" +
            "	<body>" +
            "			<table class='results_table'>" +
            "               <thead>" +
            "   				<tr>" +
            "   					<th class='medium_text'>Scenario</th>" +
            "   					<th class='long_text'>Case</th>" +
            "   					<th class='short_text'>Date</th>" +
            "	    				<th class='short_text'>Time</th>" +
            "	    				<th class='medium_text'>Step/Result</th>" +
            "	    				<th class='long_text'>Message</th>" +
            "	    				<th class='medium_text'>Test/Class</th>" +
            "	    				<th class='medium_text'>Method</th>" +
            "	    				<th class='single_value'>Line</th>" +
            "				    </tr>" +
            "               </thead>";

        private const string ReportFileFooter = "</table></body></html>";

        private static List<ReportLine> TestCaseReportLines;
        private static string Scenario { get; set; }
        private static string Case { get; set; }

        /// <summary>
        /// Collect info about the class And the method that threw the assertion error
        /// </summary>
        /// <returns></returns>
        private static string MessageType(MessageType type, ReportLine reportLine)
        {
            string messageType = "";
            switch (type)
            {
                case JPB_Framework.Report.MessageType.AssertionError:
                    {
                        messageType = "Assertion error";
                        reportLine.Error = true;
                        break;
                    }
                case JPB_Framework.Report.MessageType.VerificationError:
                    {
                        messageType = "Verification error";
                        reportLine.Error = true;
                        break;
                    }
                case JPB_Framework.Report.MessageType.Exception:
                    {
                        messageType = "Exception thrown";
                        reportLine.Error = true;
                        break;
                    }
                case JPB_Framework.Report.MessageType.Message:
                    {
                        messageType = "Message";
                        break;
                    }
                case JPB_Framework.Report.MessageType.Empty:
                    {
                        messageType = "";
                        break;
                    }
            }
            return messageType;
        }

        private static void WriteReportFile()
        {
            var firstLine = new StringBuilder();
            firstLine.Append("<tbody class='groove'>");
            firstLine.Append("<tr>");
            firstLine.Append($"<td class='medium_text' rowspan='{TestCaseReportLines.Count}'>{Scenario}</td>");
            firstLine.Append($"<td class='long_text' rowspan='{TestCaseReportLines.Count}'>{Case}</td>");
            firstLine.Append($"<td class='short_text'>{TestCaseReportLines[0].Date}</td>");
            firstLine.Append($"<td class='short_text'>{TestCaseReportLines[0].Time}</td>");
            firstLine.Append($"<td class='medium_text'>{TestCaseReportLines[0].Step}</td>");
            firstLine.Append($"<td class='long_text'>{TestCaseReportLines[0].Message}</td>");
            firstLine.Append($"<td class='medium_text'>{TestCaseReportLines[0].Class}</td>");
            firstLine.Append($"<td class='medium_text'>{TestCaseReportLines[0].Method}</td>");
            firstLine.Append($"<td class='single_value'>{TestCaseReportLines[0].Line}</td>");
            firstLine.Append("</tr>");

            File.AppendAllText(ReportFilePath,firstLine.ToString());

            for (var i=1; i<TestCaseReportLines.Count; i++)
            {
                File.AppendAllText(ReportFilePath, TestCaseReportLines[i].InputLine);
            }
            File.AppendAllText(ReportFilePath, "</tbody>");

        }

        /// <summary>
        /// Output message to report file.
        /// </summary>
        /// <param name="type"> Message type</param>
        /// <param name="message"> Message to write to report file</param>
        /// <param name="e"></param>>
        public static void ToLogFile(MessageType type, string message, Exception e)
        {
            var reportLine = new ReportLine();
            reportLine.Date = DateTime.Now.ToShortDateString();
            reportLine.Time = DateTime.Now.ToLongTimeString();


            if (type != JPB_Framework.Report.MessageType.Empty)
            {
                reportLine.Step = MessageType(type, reportLine);
                reportLine.Message = message;
                if (type == JPB_Framework.Report.MessageType.Exception)
                {
                    reportLine.Message = $"{reportLine.Message} {e.Message} {e.StackTrace}";
                }
                else
                {
                    var stack = new StackTrace(true);
                    var frame = stack.GetFrame(2);
                    var reflectedType = frame.GetMethod().ReflectedType;
                    if (reflectedType != null)
                        reportLine.Class = reflectedType.FullName;
                    reportLine.Method = frame.GetMethod().Name;
                    reportLine.Line = frame.GetFileLineNumber().ToString();
                }
            }
            else
            {
                reportLine.Step = message;
                reportLine.Message = "";
                reportLine.Class = "";
                reportLine.Method = "";
                reportLine.Line = "";
            }
  
            TestCaseReportLines.Add(reportLine);
        }

        /// <summary>
        /// If report file does not exist, create it
        /// </summary>
        public static void InitializeReportFile()
        {
            if (!File.Exists(ReportFilePath)) File.AppendAllText(ReportFilePath, ReportFileHeader);
        }

        public static void FinalizeReportFile()
        {
            File.AppendAllText(ReportFilePath, ReportFileFooter);
        }

        /// <summary>
        /// Log test initialization to test results file.
        /// </summary>
        public static void Initialize(string testClassName, string testMethodName)
        {
            TestCaseReportLines = new List<ReportLine>();
            var str = testClassName.Split('.');
            Scenario = str[str.Length-1];
            Case = testMethodName;

            ToLogFile(JPB_Framework.Report.MessageType.Empty, "Test Started", null);
        }

        /// <summary>
        /// Log test finalization to test results file
        /// </summary>
        /// <param name="testClassName"></param>
        /// <param name="testMethodName"></param>
        /// <param name="testOutput"></param>
        public static void Finalize(UnitTestOutcome testOutput)
        {
            ToLogFile(JPB_Framework.Report.MessageType.Empty, $"Test {testOutput}", null);
            WriteReportFile();
        }
    }

    internal class ReportLine
    {

        internal string InputLine
        {
            get
            {
                var text = new StringBuilder();

                if (Error) text.Append("<tr class='error'>");
                else text.Append("<tr>");
                text.Append($"<td class='short_text'>{Date}</td>");
                text.Append($"<td class='short_text'>{Time}</td>");
                if (Step.Equals("Test Failed")) text.Append($"<td class='medium_text error'>{Step}</td>");
                else text.Append($"<td class='medium_text'>{Step}</td>");
                text.Append($"<td class='long_text'>{Message}</td>");
                text.Append($"<td class='medium_text'>{Class}</td>");
                text.Append($"<td class='medium_text'>{Method}</td>");
                text.Append($"<td class='single_value'>{Line}</td>");
                text.Append("</tr>");

                return text.ToString();
            }
        }

        internal string Date { get; set; }
        internal string Time { get; set; }
        internal string Step { get; set; }
        internal string Message { get; set; }
        internal string Class { get; set; }
        internal string Method { get; set; }
        internal string Line { get; set; }
        internal bool Error;
    }
}
