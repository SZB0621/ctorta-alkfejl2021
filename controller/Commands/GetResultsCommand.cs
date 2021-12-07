using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.ComponentModel;
using ControlApp.Model;
using ControlApp.Utils;
using OxyPlot;
using common;

using controller;
using System.Net.Http;
using Newtonsoft.Json;

namespace ControlApp.Commands
{
    /// <summary>
    /// Command for requesting the simulation data through HTTP.
    /// <remark>
    /// Inherits from ICommand. This command can be executed only when a simulation has been initiated.
    /// </remark>
    /// </summary>
    public class GetResultsCommand : ICommand
    {
        public RobotModel RobotModel;
        public MessageModel MsgModel;
        private HttpWrapper HttpClient = new HttpWrapper();

        public GetResultsCommand(RobotModel robotModel, MessageModel msgModel)
        {
            this.RobotModel = robotModel;
            this.MsgModel = msgModel;

            MsgModel.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SimMode")
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return MsgModel.SimMode == SimulationMode.CIRCLING_SIM || MsgModel.SimMode == SimulationMode.SENSOR_TEST_SIM;
        }

        public async void Execute(object parameter)
        {
            try
            {
                if (await HttpClient.Connect() == true)
                {
                    Message Msg = new Message(MessageType.GetRobotData);
                    Debug.WriteLine("Attempting HTTP with Message: " + Msg.MsgContent());

                    HttpResponseMessage Response = await HttpClient.Get(Msg);
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Debug.WriteLine("HTTP Transaction successful");

                        string Result = await Response.Content.ReadAsStringAsync();
                        Debug.WriteLine(Result);
                        List<RobotSample> RobotSamples = JsonConvert.DeserializeObject<List<RobotSample>>(Result);

                        ConvertToDataPoint converter = new ConvertToDataPoint();

                        RobotModel.Position = converter.GetPositionArray(RobotSamples);
                        RobotModel.Velocity = converter.GetVelocityArray(RobotSamples);
                        RobotModel.Orientation = converter.GetOrientationArray(RobotSamples);
                        RobotModel.SensorValues = converter.GetSensorsArray(RobotSamples);
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
