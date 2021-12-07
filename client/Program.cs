using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using common;


namespace client
{
    enum ProgramState
    {
        Idle,
        Simulation
    }
    /// <summary>
    /// Program class implementing the client side functionalities
    /// </summary>
    class Program
    {
        /// <summary>
        /// HTTP server class
        /// </summary>
        public HttpServer NetworkManager;
        /// <summary>
        /// Enum for storing program state
        /// </summary>
        public ProgramState State;
        /// <summary>
        /// Robot class
        /// </summary>
        public Robot.Robot Robot;

        public Program()
        {
            NetworkManager = new HttpServer();
            Robot = new();
            State = ProgramState.Idle;
            CoppeliaSim.Simulation.CoppeliaConnectionStatus();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("CTortak Client App");

            Program Client = new();
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            while (true)
            {
                if (Client.State == ProgramState.Idle)
                {
                    while (Client.NetworkManager.IsMessageQueueEmpty() == false)
                    {
                        
                        Message Msg = Client.NetworkManager.GetMessageFromQueue();
                        switch (Msg.MsgType())
                        {
                            case MessageType.Echo:
                                break;

                            case MessageType.StartSimDistance:
                                double Velocity = Convert.ToDouble(Msg.ParsedContent["Velocity"], provider);
                                double Distance = Convert.ToDouble(Msg.ParsedContent["StoppingDistance"], provider);
                                Console.WriteLine($"Starting Sensor Simulation with params: {Velocity}, {Distance}");
                                Client.State = ProgramState.Simulation;
                                
                                // Start simu
                                List<RobotSample> SimDistanceSamples = new List<RobotSample>();
                                Client.Robot.DoSensorTestActions(out SimDistanceSamples, Distance, Velocity);
                                
                                // Simu finished
                                HttpServer.RobotSamples = SimDistanceSamples;
                                break;

                            case MessageType.StartSimCircle:
                                double CircleVelocity = Convert.ToDouble(Msg.ParsedContent["Velocity"], provider);
                                double Radius = Convert.ToDouble(Msg.ParsedContent["TurningRadius"], provider);
                                Console.WriteLine($"Starting Circular Simulation with params: {CircleVelocity}, {Radius}");
                                Client.State = ProgramState.Simulation;
                                
                                // Start simu
                                List<RobotSample> SimCircleSamples = new List<RobotSample>();
                                Client.Robot.DoCirculationActions(out SimCircleSamples, Radius);
                                
                                // Simu finished
                                HttpServer.RobotSamples = SimCircleSamples;
                                break;

                            case MessageType.GetLogData:
                                // Logs already sent from NetworkManager class
                                Console.WriteLine("Local logs requested");
                                HttpServer.ClientLog.Add("Local logs requested");
                                

                                Client.NetworkManager.ClearClientLog();
                                break;

                            case MessageType.GetRobotData:
                                // Samples already sent from NetworkManager class
                                Console.WriteLine("Previous simu Robot samples requested");
                                HttpServer.ClientLog.Add("Previous simu Robot samples requested");

                                Client.NetworkManager.ClearRobotSamples();
                                break;

                            default:
                                Console.WriteLine("Unrecognised message received: " + Msg.MsgContent());
                                HttpServer.ClientLog.Add("Unrecognised message received: " + Msg.MsgContent());
                                break;
                        }
                    }

                    Client.State = ProgramState.Idle;
                }
            }
        }
    }
}
