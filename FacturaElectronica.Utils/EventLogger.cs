using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace FacturaElectronica.Utils
{
    static public class EventLogger
    {
        static private EventLog serviceEventLog = new EventLog();
        static private EventLog commonEventLog = new EventLog();

        static EventLogger()
        {
            try
            {
                // check for log entry on event viewer
                if (!System.Diagnostics.EventLog.SourceExists(Settings.LoggerSource))
                {
                    System.Diagnostics.EventLog.CreateEventSource(Settings.LoggerSource, Settings.LoggerLog);
                }
                serviceEventLog.Source = Settings.LoggerSource;
                serviceEventLog.Log = Settings.LoggerLog;
            }
            catch (Exception ex)
            {
                commonEventLog.WriteEntry("Error Fatal EventLogger(): " + ex.Message);
            }
        }

        static public void WriteError(Exception Ex)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            StringBuilder strMessage = new StringBuilder();

            strMessage.Append("Method: " + methodBase.Name);
            strMessage.Append("\n\n");
            strMessage.Append("Error:\n" + Ex.Message);
            strMessage.Append("\n\n");
            strMessage.Append("Trace:\n" + Ex.StackTrace);
            strMessage.Append("\n\n");
            strMessage.Append("Source:\n" + Ex.Source);

            serviceEventLog.WriteEntry(strMessage.ToString(), System.Diagnostics.EventLogEntryType.Error);
        }

        static public void WriteError(string Ex)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            StringBuilder strMessage = new StringBuilder();

            strMessage.Append("Method: " + methodBase.Name);
            strMessage.Append("\n\n");
            strMessage.Append("Error:\n" + Ex);

            serviceEventLog.WriteEntry(strMessage.ToString(), System.Diagnostics.EventLogEntryType.Error);
        }

        static public void WriteInformation(string InformationMessage)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            StringBuilder strMessage = new StringBuilder();

            strMessage.Append("Method: " + methodBase.Name);
            strMessage.Append("\n\n");
            strMessage.Append("Description:\n" + InformationMessage);

            serviceEventLog.WriteEntry(strMessage.ToString(), System.Diagnostics.EventLogEntryType.Information);
        }

        static public void WriteWarning(string WarningMessage)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            StringBuilder strMessage = new StringBuilder();

            strMessage.Append("Method: " + methodBase.Name);
            strMessage.Append("\n\n");
            strMessage.Append("Description:\n" + WarningMessage);

            serviceEventLog.WriteEntry(strMessage.ToString(), System.Diagnostics.EventLogEntryType.Warning);
        }
    }
}
