using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using common;

namespace controller
{
    /// <summary>
    /// A custom wrapper for System.Net.HttpClient.
    /// HttpWrapper introduces a simple interface to send and receive Messages over HTTP,
    /// and also has an internal state variable to store HTTP connection state.
    /// </summary>
    public class HttpWrapper
    {        
        private static bool Connected;
        private static HttpClient Client = new HttpClient();

        /// <summary>
        /// HTTP Post Message.
        /// </summary>
        /// <param name="Msg">Send Message via HTTP Post</param>
        /// <returns>HTTP Response</returns>
        public async Task<HttpResponseMessage> Post(Message Msg)
        {
            HttpResponseMessage Result = await Client.PostAsync(Utilities.HttpUris[Msg.MsgType()], new StringContent(Msg.MsgContent()));
            Connected = Result.IsSuccessStatusCode;

            return Result;
        }

        /// <summary>
        /// HTTP Put Message.
        /// </summary>
        /// <param name="Msg">Send Message via HTTP Put</param>
        /// /// <returns>HTTP Response</returns>
        public async Task<HttpResponseMessage> Put(Message Msg)
        {
            HttpResponseMessage Result = await Client.PutAsync(Utilities.HttpUris[Msg.MsgType()], new StringContent(Msg.MsgContent()));
            Connected = Result.IsSuccessStatusCode;

            return Result;
        }

        /// <summary>
        /// HTTP Get Message.
        /// </summary>
        /// <param name="Msg">Send Message via HTTP Get</param>
        /// <returns>HTTP Response</returns>
        public async Task<HttpResponseMessage> Get(Message Msg)
        {
            HttpResponseMessage Result = await Client.GetAsync(Utilities.HttpUris[Msg.MsgType()]);
            Connected = Result.IsSuccessStatusCode;

            return Result;
        }

        /// <summary>
        /// Verify HTTP connection async. Uses Put internally.
        /// </summary>
        /// <returns>true if HTTP connection can be made</returns>
        public async Task<bool> Connect()
        {
            Client = new HttpClient();

            Message Echo = new Message(MessageType.Echo);

            await Put(Echo);

            return Connected;
        }

        /// <returns>true if previous HTTP transaction was successful</returns>
        public bool IsConnected()
        {
            return Connected;
        }
    }
}
