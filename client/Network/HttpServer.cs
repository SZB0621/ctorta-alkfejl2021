using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using common;
using Newtonsoft.Json;

namespace client
{
    /// <summary>
    /// Wrapper class to implement a HTTP Server. The GUI app connects transmits Messages through an instance of this class.
    /// HTTP Connection properties are stored in Common class library's Utilities class.
    /// The constructor creates a new thread (async Task), which listens to incoming HTTP requests in an infinite loop, and processes them if any message is received.
    /// Since HTTP communication is asyncrhonous, it is possible to receive multiple Messages while the previous one is still being processed, eg. a simulation is still running, and a data request Message is received.
    /// To prevent any issues, HTTP Messages are put into a Queue, and later processed async in another task, the main function of the client app.
    /// Since this class is multithreaded, a lock object is also managed to prevent concurrent write of common variables, eg. RobotSamples buffer.
    /// </summary>
    public class HttpServer
    {
        private static Queue<Message> MsgQueue;
        private static object OutputLock;
        private static HttpListener Listener;
        
        private Task NetworkTask;

        public static List<string> ClientLog = new List<string>();
        public static List<RobotSample> RobotSamples = new List<RobotSample>();

        /// <summary>
        /// Class constructor
        /// Starts HTTP Listener Task at specified URI's
        /// </summary>
        public HttpServer()
        {
            OutputLock = new object();
            Listener = new HttpListener();
            MsgQueue = new Queue<Message>();

            foreach (string Uri in Utilities.HttpUris.Values)
            {
                Listener.Prefixes.Add(Uri);
            }            

            NetworkTask = Network();
        }

        /// <summary>
        /// Main HTTP Listener Task
        /// </summary>
        private static async Task Network()
        {
            Listener.Start();
            Console.WriteLine("HTTP Listening Started");

            while (Listener.IsListening)
            {
                var Context = await Listener.GetContextAsync();
                try
                {
                    await InputHandlerMethodAsync(Context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            Listener.Close();
            Console.WriteLine("HTTP Listening Stopped");
        }


        /// <summary>
        /// Async Input Handler task.
        /// Processes incoming HTTP Requests based on Message content and HTTP Request type (Get, Post, Put, etc).
        /// If the incoming Message is valid, the output handler task is called.
        /// </summary>
        /// <param name="Ctx">Http Listener output, if a connection is made</param>
        /// <returns>InputHandler Task</returns>
        private static async Task InputHandlerMethodAsync(HttpListenerContext Ctx)
        {
            HttpListenerRequest HttpReq = Ctx.Request;
            HttpListenerResponse HttpResp = Ctx.Response;

            Console.WriteLine("Message received");

            Message Msg = null;

            if (HttpReq.HttpMethod.Equals("GET"))
            {
                if (HttpReq.Url.ToString().Equals(Utilities.LogAddress))
                {
                    Msg = new Message(MessageType.GetLogData);
                }
                else if (HttpReq.Url.ToString().Equals(Utilities.DataAddress))
                {
                    Msg = new Message(MessageType.GetRobotData);
                }
            }
            else
            {
                Msg = await MessageParser.ParseRequest(HttpReq);
            }            

            if (Msg == null)
            {
                Console.WriteLine("Unrecognised message");
                return;
            }

            await OutputHandlerMethodAsync(HttpResp, HttpReq.ContentEncoding, Msg);
            MsgQueue.Enqueue(Msg);
        }


        /// <summary>
        /// Async Output Handler task.
        /// Creates and transmits a HTTP Response from the HTTP Request, HTTP Request Encoding, and Message content.
        /// </summary>
        /// <param name="Resp">HTTP Response structure</param>
        /// <param name="Enc">HTTP Encoding</param>
        /// <param name="Msg">Message to transmit</param>
        /// <returns>OutputHandler Task</returns>
        private static async Task OutputHandlerMethodAsync(HttpListenerResponse Resp, Encoding Enc, Message Msg)
        {
            Resp.StatusCode = 200;
            string ResponseContent;

            lock (OutputLock)
            {
                switch (Msg.MsgType())
                {
                    case MessageType.Echo:
                    case MessageType.StartSimCircle:
                    case MessageType.StartSimDistance:
                        ResponseContent = "[MSG=OK]:";
                        break;

                    case MessageType.GetLogData:
                        ResponseContent = JsonConvert.SerializeObject(ClientLog); 
                        break;

                    case MessageType.GetRobotData:
                        ResponseContent = JsonConvert.SerializeObject(RobotSamples);
                        break;

                    default:
                        ResponseContent = "[MSG=NOK]:";
                        break;
                }
            }

            byte[] OutputBuffer = Enc.GetBytes(ResponseContent);
            Resp.ContentLength64 = OutputBuffer.Length;

            await Resp.OutputStream.WriteAsync(OutputBuffer);
            Resp.OutputStream.Close();
        }

        /// <summary>
        /// Check MessageQueue status
        /// </summary>
        /// <returns>true if MessageQueue is empty</returns>
        public bool IsMessageQueueEmpty()
        {
            return (MsgQueue.Count == 0);
        }

        /// <summary>
        /// Pops last Message from MessageQueue.
        /// </summary>
        /// <returns>Message</returns>
        public Message GetMessageFromQueue()
        {
            return MsgQueue.Dequeue();
        }

        /// <summary>
        /// Clears internal RobotSamples buffer. Uses lock object for syncing with other threads.
        /// </summary>
        public void ClearRobotSamples()
        {
            lock (OutputLock)
            {
                // Wait for the other threads to release the RobotSamples List
            }

            RobotSamples.Clear();
        }

        /// <summary>
        /// Clears internal ClientLog buffer. Uses lock object for syncing with other threads.
        /// </summary>
        public void ClearClientLog()
        {
            lock (OutputLock)
            {
                // Wait for the other threads to release the ClientLog List
            }

            ClientLog.Clear();
        }
    }
}
