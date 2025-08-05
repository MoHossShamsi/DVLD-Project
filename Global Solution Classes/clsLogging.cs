using System;
using System.Diagnostics;

namespace Global_Solution_Classes
{
    public static class EventLogger
    {
        private static readonly string sourceName = "DVLD App";

        static EventLogger()
        {
            if (!EventLog.SourceExists(sourceName))
            {
                // May require admin rights
                EventLog.CreateEventSource(sourceName, "Application");
            }
        }

        public static void LogException(Exception ex)
        {
            string message = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}";
            EventLog.WriteEntry(sourceName, message, EventLogEntryType.Error);
        }

        public static void LogInfo(string message)
        {
            EventLog.WriteEntry(sourceName, message, EventLogEntryType.Information);
        }
    }
}