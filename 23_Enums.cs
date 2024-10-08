﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    enum LogLevel
    {
        Trace = 1,
        Debug = 2,
        Info = 4,
        Warning = 5,
        Error = 6,
        Fatal = 42,
        Unknown = 0

    }

    static class LogLine
    {
        public static LogLevel ParseLogLevel(string logLine)
        {
            switch (logLine[1..4])
            {
                case "TRC":
                    return LogLevel.Trace;
                case "DBG":
                    return LogLevel.Debug;
                case "INF":
                    return LogLevel.Info;
                case "WRN":
                    return LogLevel.Warning;
                case "ERR":
                    return LogLevel.Error;
                case "FTL":
                    return LogLevel.Fatal;
                default:
                    return LogLevel.Unknown;
            }
        }

        public static string OutputForShortLog(LogLevel logLevel, string message) =>  $"{(int)logLevel}:{message}";

    }
}
