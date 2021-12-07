using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;


namespace client.CoppeliaSim
{
    /// <summary>
    /// Wrapper class for the coppelia sim's remoteApi.dll, wrapping the .C API librarys for the use in C#
    /// More information can be found here: https://www.coppeliarobotics.com/helpFiles/en/remoteApiFunctions.htm
    /// </summary>
    public static class RemoteAPIWrapper
    {
        //public static string ScenePath = "C:/Dev/Projects/alkfejl2021-ctorta/coppeliasimscene/"; // Ákos Path
        /// <summary>
        /// Path to the Coppelia Scenes
        /// </summary>
        public static string ScenePath = "C:/Users/LENOVO/Documents/University/Alkalmazasfejlesztes/ctorta-master-docfx/coppeliasimscene/"; // Benő Path

        /// <summary>
        /// Connect to simulation
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="waitForConnection"></param>
        /// <param name="reconnectOnDisconnect"></param>
        /// <param name="timeoutMS"></param>
        /// <param name="cycleTimeMS"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern int simxStart(string ip, int port, bool waitForConnection, bool reconnectOnDisconnect, int timeoutMS, int cycleTimeMS);

        /// <summary>
        /// End simulation
        /// </summary>
        /// <param name="clientID"></param>
        [DllImport("remoteApi.dll")]
        public static extern void simxFinish(int clientID);

        /// <summary>
        /// Get the object's position realtive to another
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="jointHandle"></param>
        /// <param name="relativeToHandle"></param>
        /// <param name="positions"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxGetObjectPosition(int clientID, int jointHandle, int relativeToHandle, float[] positions, simx_opmode opmode);

        /// <summary>
        /// Get the object's handle in the given simulation
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="objectName"></param>
        /// <param name="handle"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxGetObjectHandle(int clientID, string objectName, out int handle, simx_opmode opmode);

        /// <summary>
        /// Start simulation
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxStartSimulation(int clientID, simx_opmode opmode);

        /// <summary>
        /// Stop simulation
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxStopSimulation(int clientID, simx_opmode opmode);

        /// <summary>
        /// Set the velocity of an object
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="jointHandle"></param>
        /// <param name="velocity"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxSetJointTargetVelocity(int clientID, int jointHandle, float velocity, simx_opmode opmode);

        /// <summary>
        /// Get the orientation of an object in radian
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="jointHandle"></param>
        /// <param name="relativeToHandle"></param>
        /// <param name="orientations"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public static extern simx_error simxGetObjectOrientation(int clientID, int jointHandle, int relativeToHandle, float[] orientations, simx_opmode opmode);

        /// <summary>
        /// Get the proximity sensor's value
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="sensorHandle"></param>
        /// <param name="detectionState"></param>
        /// <param name="detectionPoint"></param>
        /// <param name="objectHandle"></param>
        /// <param name="normalVector"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public extern static simx_error simxReadProximitySensor(int clientID, int sensorHandle,
                                                         ref char detectionState, float[] detectionPoint, ref int objectHandle, float[] normalVector, simx_opmode opmode);
        /// <summary>
        /// Load a different scene in Coppelia
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="scenePathAndName"></param>
        /// <param name="options"></param>
        /// <param name="opmode"></param>
        /// <returns></returns>
        [DllImport("remoteApi.dll")]
        public extern static simx_error simxLoadScene(int clientID, string scenePathAndName, byte options, simx_opmode opmode);

        /// <summary>
        /// Choses the filename based on the enum's value
        /// </summary>
        /// <param SceneName="n"></param>
        /// <returns>String containing the chosen file's name</returns>
        public static string WrapSceneNames(SceneNames n)
        {
            return n switch
            {
                SceneNames.idle_scene_name => "IdleScene.ttt",
                SceneNames.circulation_scene_name => "CirculationScene.ttt",
                SceneNames.sensortest_scene_name => "SensorTestScene.ttt",
                _ => "IdleScene.ttt",
            };
        }

        /// <summary>
        /// Choses the correct port for the chosen scene
        /// </summary>
        /// <param SceneName="n">Name of the chosen scene</param>
        /// <returns>ScenePortNumbers enum containing the appropriate port number</returns>
        public static ScenePortNumbers WrapSceneNamesToPortNumbers(SceneNames n)
        {
            return n switch
            {
                SceneNames.idle_scene_name => ScenePortNumbers.idle_scene_port,
                SceneNames.circulation_scene_name => ScenePortNumbers.circulation_scene_port,
                SceneNames.sensortest_scene_name => ScenePortNumbers.sensortest_scene_port,
                _ => ScenePortNumbers.idle_scene_port,
            };
        }
        /// <summary>
        /// Choses the correct scene's name for the chosen state
        /// </summary>
        /// <param name="s">Name of the chosen state</param>
        /// <returns>The name of the chosen scene</returns>
        public static SceneNames WrapStatesToSceneNames(States s)
        {
            return s switch
            {
                States.idle => SceneNames.idle_scene_name,
                States.circulation=> SceneNames.circulation_scene_name,
                States.sensortest => SceneNames.sensortest_scene_name,
                _ => SceneNames.idle_scene_name,
            };
        }
    }
}
