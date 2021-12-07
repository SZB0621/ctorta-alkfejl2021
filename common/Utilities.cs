using System;
using System.Collections.Generic;

namespace common
 {
    /// <summary>
    /// Utilities class.
    /// Contains readonly string literals and other constants, eg. HTTP Connection URI's and parameters.
    /// </summary>
    public static class Utilities
    {    
        static private readonly string Host = "localhost";
        static private readonly string Port = "32876";
        static private readonly string Address = "http://" + Host + ":" + Port + "/";

        /// <summary>
        /// Client Log Request URI.
        /// </summary>
        static public readonly string LogAddress = Address + "logs/";

        /// <summary>
        /// Robot Samples Request URI.
        /// </summary>
        static public readonly string DataAddress = Address + "data/";

        /// <summary>
        /// Command Post URI. (unused)
        /// </summary>
        static public readonly string CommandAddress = Address + "cmd/";

        /// <summary>
        /// Used to convert MessageTypes to Uri's.
        /// </summary>
        public static readonly Dictionary<MessageType, string> HttpUris = new Dictionary<MessageType, string>
        {
            { MessageType.Echo, CommandAddress },
            { MessageType.StartSimCircle, CommandAddress },
            { MessageType.StartSimDistance, CommandAddress },
            { MessageType.GetLogData, LogAddress },
            { MessageType.GetRobotData, DataAddress }
        };
    }
}
