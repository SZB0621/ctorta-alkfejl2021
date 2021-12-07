using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.CoppeliaSim
{
    /// <summary>
    /// Singleton class managing the simulation 
    /// </summary>
    static class Simulation
    { 
        /// <summary>
        /// ID of the simulation
        /// </summary>
        public static int ClientID;
        /// <summary>
        /// ID of the Reference box
        /// </summary>
        public static int ReferenceBox;
        /// <summary>
        /// ID of the Robot
        /// </summary>
        public static int RobotHandler;
        /// <summary>
        /// ID of RightMotor
        /// </summary>
        public static int RightMotorHandler;
        /// <summary>
        /// ID of LeftMotor
        /// </summary>
        public static int LeftMotorHandler;
        /// <summary>
        /// ID of FrontSensor
        /// </summary>
        public static int FrontProximityHandler;
        /// <summary>
        /// ID of RearSensor
        /// </summary>
        public static int RearProximityHandler;
        /// <summary>
        /// Port of the current connection
        /// </summary>
        public static ScenePortNumbers CurrentConnectionPort;

        /// <summary>
        /// Only true if every simulation parameter is initialized correctly
        /// </summary>
        public static bool DataValidationFlag = false;

        /// <summary>
        /// Constructor, connects to coppelia in an Idle state initializes every handle
        /// </summary>
        static Simulation()
        {
            ClientID = RemoteAPIWrapper.simxStart("127.0.0.1", (int)ScenePortNumbers.idle_scene_port, true, true, 5000, 5);
            if (ClientID != -1)
            {
                client.HttpServer.ClientLog.Add("Connection Succesful to CoppeliaSim - " + System.DateTime.Now.ToString() + " !");
                simx_error RobotHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx", out RobotHandler, simx_opmode.oneshot_wait);
                simx_error RightMotorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx_rightMotor", out RightMotorHandler, simx_opmode.oneshot_wait);
                simx_error LeftMotoHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx_leftMotor", out LeftMotorHandler, simx_opmode.oneshot_wait);
                simx_error FrontSensorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Proximity_sensor_front", out FrontProximityHandler, simx_opmode.oneshot_wait);
                simx_error RearSensorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Proximity_sensor_rear", out RearProximityHandler, simx_opmode.oneshot_wait);
                simx_error ReferenceBoxHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "ReferenceBox", out ReferenceBox, simx_opmode.oneshot_wait);
                simx_error StartSimRet = RemoteAPIWrapper.simxStartSimulation(ClientID, simx_opmode.oneshot_wait);

                if (RobotHandlerRet == 0 && RightMotorHandlerRet == 0 && LeftMotoHandlerRet == 0 && FrontSensorHandlerRet == 0 && RearSensorHandlerRet == 0 && ReferenceBoxHandlerRet == 0 && StartSimRet == 0)
                {
                    DataValidationFlag = true;
                    CurrentConnectionPort = ScenePortNumbers.idle_scene_port;
                }
            }
        }

        /// <summary>
        /// Writes brief informations about the connection status to CoppeliSim 
        /// </summary>
        public static void CoppeliaConnectionStatus()
        {
            if (DataValidationFlag)
            {
                client.HttpServer.ClientLog.Add("Connected CoppeliaSim via (IP:PORT) - " + "127.0.0.1:" + (int)CurrentConnectionPort + "!");
                client.HttpServer.ClientLog.Add("Handlers: ");
                client.HttpServer.ClientLog.Add(" - RobotHandler: " + RobotHandler);
                client.HttpServer.ClientLog.Add(" - RightMotorHandler: " + RightMotorHandler);
                client.HttpServer.ClientLog.Add(" - LeftMotorHandler: " + LeftMotorHandler);
                client.HttpServer.ClientLog.Add(" - FrontProximityHandler: " + FrontProximityHandler);
                client.HttpServer.ClientLog.Add(" - RearProximityHandler: " + RearProximityHandler);
                client.HttpServer.ClientLog.Add(" - ReferenceBox: " + ReferenceBox);
            }
            else
            {
                client.HttpServer.ClientLog.Add("There isn't any open connections to CoppeliaSim !");
            }
            
        }


        /// <summary>
        /// Reconnects and re-initializes handlers to a scene in a given port
        /// </summary>
        /// <param name="ScenePortNumber">The port of the scene to which we want to connect</param>
        /// <returns>True if initialization was successful</returns>
        public static bool ReconnectToScene(ScenePortNumbers ScenePortNumber)
        {
            ClientID = RemoteAPIWrapper.simxStart("127.0.0.1", (int)ScenePortNumber, true, true, 5000, 5);
            if (ClientID != -1)
            {
                simx_error RobotHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx", out RobotHandler, simx_opmode.oneshot_wait);
                simx_error RightMotorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx_rightMotor", out RightMotorHandler, simx_opmode.oneshot_wait);
                simx_error LeftMotoHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Pioneer_p3dx_leftMotor", out LeftMotorHandler, simx_opmode.oneshot_wait);
                simx_error FrontSensorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Proximity_sensor_front", out FrontProximityHandler, simx_opmode.oneshot_wait);
                simx_error RearSensorHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "Proximity_sensor_rear", out RearProximityHandler, simx_opmode.oneshot_wait);
                simx_error ReferenceBoxHandlerRet = RemoteAPIWrapper.simxGetObjectHandle(ClientID, "ReferenceBox", out ReferenceBox, simx_opmode.oneshot_wait);
                simx_error StartSimRet = RemoteAPIWrapper.simxStartSimulation(ClientID, simx_opmode.oneshot_wait);

                if (RobotHandlerRet == 0 && RightMotorHandlerRet == 0 && LeftMotoHandlerRet == 0 && FrontSensorHandlerRet == 0 && RearSensorHandlerRet == 0 && ReferenceBoxHandlerRet == 0 && StartSimRet == 0)
                {
                    DataValidationFlag = true;
                    CurrentConnectionPort = ScenePortNumber;
                    CoppeliaConnectionStatus();
                    return true;
                }
                else
                {
                    DataValidationFlag = false;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the scene and re-initializes it's handlers and parameters
        /// </summary>
        /// <param name="NewScene">Name of the scene to which we want to connect</param>
        /// <param name="Path">Path to the coppelia scene's in a given environment</param>
        /// <remarks>
        /// Using the setter actually changes the scene in in the coppelia simulation environment
        /// also re-initializes every handler appropriately
        /// Throws InvalidOperationException in this case
        /// <list type="bullet">
        /// <item>
        /// <description>Reconnection to a scene failed</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>True if initialization was successful</returns>
        public static bool ChangeScene(SceneNames NewScene,string Path)
        {
            string AbsolutePath = Path;

            simx_error StopSimRet = RemoteAPIWrapper.simxStopSimulation(ClientID, simx_opmode.oneshot_wait);
            AbsolutePath += RemoteAPIWrapper.WrapSceneNames(NewScene);
            simx_error LoadSceneRet = RemoteAPIWrapper.simxLoadScene(ClientID, AbsolutePath, 0x01, simx_opmode.oneshot_wait);
            RemoteAPIWrapper.simxFinish(ClientID);

            if(!ReconnectToScene(RemoteAPIWrapper.WrapSceneNamesToPortNumbers(NewScene)))
            {
                throw new InvalidOperationException("Connection unsuccessful!");
            }
            else
            {
                client.HttpServer.ClientLog.Add("Reconnection was successful!");
            }

            simx_error StartSimRet = RemoteAPIWrapper.simxStartSimulation(ClientID, simx_opmode.oneshot_wait);

            if (StopSimRet == 0 && LoadSceneRet == 0 && StartSimRet == 0)
            {
                client.HttpServer.ClientLog.Add("Simulation started!");
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Ends connection to coppelia
        /// </summary>
        /// <remarks>
        /// Throws Exception in this case
        /// <list type="bullet">
        /// <item>
        /// <description>Disconnection failed</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>True if disconnection was successful</returns>
        public static bool DestroyConnection()
        {
            simx_error StopSimRet = RemoteAPIWrapper.simxStopSimulation(ClientID, simx_opmode.oneshot_wait);
            if (StopSimRet == 0)
            {
                RemoteAPIWrapper.simxFinish(ClientID);
                return true;
            }
            else
            {
                throw new System.Exception("Destroying connection is not possible!");
            }
        }

        /// <summary>
        /// Get the object's position in the simulation referenced to the ReferenceBox
        /// </summary>
        /// <param name="Handler">Object's ID</param>
        /// <param name="p">Point to store the position</param>
        /// <returns>True if query was succesful</returns>
        public static bool GetObjectPositionAsPoint(int Handler,out common.Point p)
        {
            float[] Position = { 0, 0, 0 };
            p = new();
            CoppeliaSim.simx_error ret = CoppeliaSim.RemoteAPIWrapper.simxGetObjectPosition(ClientID, Handler, ReferenceBox, Position, CoppeliaSim.simx_opmode.oneshot_wait);
            if (ret == 0)
            {
                p.X = (double)Position[0];
                p.Y = (double)Position[1];
                return true;
            }
            p.X = 0.0;
            p.Y = 0.0;
            return false;
        }
    }
}
