using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    /// <summary>
    /// Custom debug stuff for dev purposes
    /// </summary>
    public static class Debug
    {
        private static readonly string debugLogFile = "runtimelog.txt";
        private static readonly int debugLevel = 1;

        /// <summary>
        /// Writes a debug message to the log file (no level validation)
        /// </summary>
        /// <param name="message">message to write to log file</param>
        public static void DebugMessage(string message)
        {
            File.AppendAllText(debugLogFile, "[ Debug* ] "+ DateTime.Now + message + "\n");
        }
        /// <summary>
        /// Writes a debug message to the log file if the debug level is greater than the message level
        /// </summary>
        /// <param name="message">message to write to log file</param>
        /// <param name="level">The debug level of the message - 1: Errors only, 2: + Events, 3: + Warnings, 4: + Debug information</param>
        public static void DebugMessage(string message, int level)
        {
            if (level <= debugLevel)
            {
                switch (level)
                {
                    case 1:
                        File.AppendAllText(debugLogFile, "[ ERROR ] " + DateTime.Now + " " + message + "\n");
                        break;
                    case 2:
                        File.AppendAllText(debugLogFile, "[ Event ] " + DateTime.Now + " " + message + "\n");
                        break;
                    case 3:
                        File.AppendAllText(debugLogFile, "[ Warning ] " + DateTime.Now + " " + message + "\n");
                        break;
                    case 4:
                        File.AppendAllText(debugLogFile, "[ Debug ] " + DateTime.Now + " " + message + "\n");
                        break;
                }
            }
        }
    }
}
