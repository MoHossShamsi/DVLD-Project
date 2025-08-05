using System;
using System.Diagnostics;

namespace SolutionGlobalClasses
{
    public static class clsLogging
    {
        private static readonly string sourceName = "DVLD App";
        private static readonly object lockObject = new object();

        // Initialize once when the class is first used
        static clsLogging()
        {
            InitializeEventSource();
        }

        private static void InitializeEventSource()
        {
            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
            }
            catch
            {
                // Silent failure - event source initialization failed
            }
        }

        public static string LogException(Exception ex)
        {
            if (ex == null) return string.Empty;

            string message = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}";

            lock (lockObject)
            {
                try
                {
                    EventLog.WriteEntry(sourceName, message, EventLogEntryType.Error);
                }
                catch
                {
                    // Silent failure - logging failed
                }
            }

            return message;
        }

        public static void LogInfo(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            lock (lockObject)
            {
                try
                {
                    EventLog.WriteEntry(sourceName, message, EventLogEntryType.Information);
                }
                catch
                {
                    // Silent failure - logging failed
                }
            }
        }

        public static void LogWarning(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            lock (lockObject)
            {
                try
                {
                    EventLog.WriteEntry(sourceName, message, EventLogEntryType.Warning);
                }
                catch
                {
                    // Silent failure - logging failed
                }
            }
        }
    }
}