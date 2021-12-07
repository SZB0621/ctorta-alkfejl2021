using System;
using System.Diagnostics;
using System.Windows.Input;
using ControlApp.Model;

using common;
using controller;
using System.Collections.Generic;
using System.Net.Http;

namespace ControlApp.Utils
{
    public class SimulateCommand : ICommand
    {
        private HttpWrapper HttpClient = new HttpWrapper();
        public DataToTransmit Data;
        public SimulationMode SimMode;

        public SimulateCommand(DataToTransmit data, SimulationMode simMode = SimulationMode.IDLE)
        {
            this.Data = data;
            this.SimMode = simMode;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            // execution is only allowed if the model's properties have been changed
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
                    MsgParams.Add("Velocity", Data.Velocity.ToString());

                    if (SimMode == SimulationMode.SENSOR_TEST_SIM)
                    {
                        MsgType = MessageType.StartSimDistance;
                        MsgParams.Add("StoppingDistance", Data.StoppingDistance.ToString());
                    }
                    else if (SimMode == SimulationMode.CIRCLING_SIM)
                    {
                        MsgType = MessageType.StartSimCircle;
                        MsgParams.Add("TurningRadius", Data.TurningRadius.ToString());
                    }

                    Message Msg = new Message(MsgType, MsgParams);
                    Console.WriteLine("Attempting HTTP with Message: " + Msg.MsgContent());

                    HttpResponseMessage Response = await HttpClient.Post(Msg);
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // TODO: Log Http transaction successful
                        Console.WriteLine("HTTP Transaction successful");
                        // TODO: Log Response content
                        Console.WriteLine(Response.Content);
                    }
                    else
                    {
                        // TODO: Log Http error: message was not parsed correctly
                        Console.WriteLine("HTTP Transaction failed: Message parse error");
                    }
                }
                else
                {
                    //TODO: Log Http error: no connection can be established, so not sending simu params
                    Console.WriteLine("HTTP connection could not be established");
                }
            }
            catch
            {
                Console.WriteLine("Oops");
            }
        }
    }
}
