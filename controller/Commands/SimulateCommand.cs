using System;
using System.Diagnostics;
using System.Windows.Input;
using ControlApp.Model;

using common;
using controller;
using System.Collections.Generic;
using System.Net.Http;

namespace ControlApp.Commands
{
    /// <summary>
    /// Command for posting simulation setting for the simulation environment through HTTP.
    /// <remark>
    /// Inherits from ICommand.
    /// </remark>
    /// </summary>
    public class SimulateCommand : ICommand
    {
        public MessageModel MsgModel;
        private HttpWrapper HttpClient = new HttpWrapper();

        public SimulateCommand(MessageModel msgModel)
        {
            this.MsgModel = msgModel;
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
                    MessageType MsgType = MessageType.Echo;
                    Dictionary<string, string> MsgParams = new Dictionary<string, string>();
                    MsgParams.Add("Velocity", MsgModel.Velocity.ToString());

                    if (MsgModel.SimMode == SimulationMode.SENSOR_TEST_SIM)
                    {
                        MsgType = MessageType.StartSimDistance;
                        MsgParams.Add("StoppingDistance", MsgModel.StoppingDistance.ToString());
                    }
                    else if (MsgModel.SimMode == SimulationMode.CIRCLING_SIM)
                    {
                        MsgType = MessageType.StartSimCircle;
                        MsgParams.Add("TurningRadius", MsgModel.TurningRadius.ToString());
                    }

                    Message Msg = new Message(MsgType, MsgParams);
                    Debug.WriteLine("Attempting HTTP with Message: " + Msg.MsgContent());

                    HttpResponseMessage Response = await HttpClient.Post(Msg);
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Debug.WriteLine("HTTP Transaction successful");
                        Debug.WriteLine(Response.Content);
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
