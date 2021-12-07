using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using ControlApp.Model;

using common;
using controller;
using System.Net.Http;
using Newtonsoft.Json;

namespace ControlApp.Commands
{
    /// <summary>
    /// Command for requesting log messages created during the simulation through HTTP.
    /// <remark>
    /// Inhertis from ICommand.
    /// </remark>
    /// </summary>
    public class LogCommand : ICommand
    {
        public LogModel LogData;
        private HttpWrapper HttpClient = new HttpWrapper();

        public LogCommand(LogModel logData)
        {
            this.LogData = logData;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (await HttpClient.Connect() == true)
                {
                    Message Msg = new Message(MessageType.GetLogData);
                    Console.WriteLine("Attempting HTTP with Message: " + Msg.MsgContent());

                    HttpResponseMessage Response = await HttpClient.Post(Msg);
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Debug.WriteLine("HTTP Transaction successful");
                        
                        string Result = await Response.Content.ReadAsStringAsync();
                        Debug.WriteLine(Result);
                        List<string> RemoteLogs = JsonConvert.DeserializeObject<List<string>>(Result);

                        LogData.LogList = new List<string>(RemoteLogs);
                        //Debug.Write("REQUEST log\r\n");
                    }
                    else
                    {
                        Debug.WriteLine("HTTP Transaction failed: Message parse error");
                    }
                }
                else
                {
                    Debug.WriteLine("HTTP connection could not be established");
                }
            }
            catch
            {
                Debug.WriteLine("Oops");
            }
        }
    }
}
