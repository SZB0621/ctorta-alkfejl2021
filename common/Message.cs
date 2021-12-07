using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace common
{
    /// <summary>
    /// Enumeration of all possible HTTP Message Types.
    /// </summary>
    public enum MessageType
    {
        Echo,
        StartSimCircle,
        StartSimDistance,
        GetRobotData,
        GetLogData
    }

    /// <summary>
    /// HTTP Message class.
    /// Instances of this class are passed to HttpWrapper and sent/received in a textual form via HTTP Post, Put, Get.
    /// </summary>
    public class Message
    {
        private MessageType Type;
        private string Content;

        /// <summary>
        /// Stores Parsed Message content key-value pairs.
        /// </summary>
        public Dictionary<string, string> ParsedContent;

        /// <summary>
        /// Constructor.
        /// Requires Message Type, and optionally a Dictionary of string-string key-value pairs, which is used to generate the Message's content.
        /// ie. a Dictionary of [{"Speed", "0.5"},{"Radius, "2"}] is converted into "Speed=0.5;Radius=2;" when sending over HTTP.
        /// When transmitting, the HTTP Packet body contains the mssage type as well, in a string form.
        /// A complete Message looks like this: "[MSG=StartSimCircle]:Speed=0.5;Radius=2;".
        /// At the receiving side, a MessageParser object is used to parse the data from a Message via Regex.
        /// </summary>
        /// <param name="T">Type of new Message</param>
        /// <param name="D">Optional Message Content dictionary</param>
        public Message(MessageType T, Dictionary<string, string> D = null)
        {
            Type = T;
            Content = "[MSG=" + Type.ToString() + "]:";
            
            
            ParsedContent = new Dictionary<string, string>();

            if (D != null)
            {
                ParsedContent = D;
                
                foreach (string Entry in D.Keys)
                {
                    Content += Entry + "=" + D[Entry] + ";";
                }
            }
        }

        /// <summary>
        /// Returns Message Type.
        /// </summary>
        public MessageType MsgType()
        {
            return Type;
        }

        /// <summary>
        /// Returns Message Content in string form.
        /// </summary>
        public string MsgContent()
        {
            return Content;
        }
    }

    /// <summary>
    /// The MessageParser class is used to parse received Messages, and gather all data encoded in a string form, in the Message's body.
    /// </summary>
    public class MessageParser
    {
        /// <summary>
        /// Static async function to parse Messages received in HTTP Packets.
        /// Uses Regexes internally.
        /// </summary>
        /// <param name="Req">Incoming HTTP Request</param>
        /// <returns>Parsed Message. Returns null if Message was invalid.</returns>
        public static async Task<Message> ParseRequest(HttpListenerRequest Req)
        {
            string Content = "";
            using (var HttpBodyStream = Req.InputStream)
            {
                using (var HttpStreamReader = new StreamReader(HttpBodyStream, Req.ContentEncoding))
                {
                    Content = await HttpStreamReader.ReadToEndAsync();
                }
            }

            Console.WriteLine("Parsing message: " + Content);
            Regex IdRegex = new Regex(@"(\[MSG=)(\w+)(\]:)((\w+)(=)([+-]?([0-9]*[.])?[0-9]+)(\;))?((\w+)(=)([+-]?([0-9]*[.])?[0-9]+)(\;))?((\w+)(=)([+-]?([0-9]*[.])?[0-9]+)(\;))?");
            MatchCollection Matches = IdRegex.Matches(Content);
            
            foreach (Match IdMatch in Matches)
            {
                GroupCollection Group = IdMatch.Groups;
                if (Group.Count < 3) return null;
                string MsgIdString = Group[2].Value;

                foreach (MessageType MsgIterator in Enum.GetValues(typeof(MessageType)))
                {
                    Console.WriteLine("3: " + MsgIterator.ToString());
                    Console.WriteLine("3: " + MsgIdString);
                    if (MsgIterator.ToString() == MsgIdString)
                    {
                        Message Msg = new Message(MsgIterator);
                        Console.WriteLine("Parsed Msg type: " + Msg.MsgType().ToString());

                        for (int i = 0; i < Group.Count; i++)
                        {
                            // Console.WriteLine($"Member[{i}]: {Group[i].Value}");
                            if (Group[i].Value == "=")
                            {
                                Console.WriteLine($"Parsed Msg parameter: {Group[i - 1].Value}, {Group[i + 1].Value}");
                                Msg.ParsedContent.Add(Group[i - 1].Value, Group[i + 1].Value);
                            }
                        }

                        return Msg;
                    }
                }
            }

            return null;
        }
    }
}
