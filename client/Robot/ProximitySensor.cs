using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Robot
{
    /// <summary>
    /// Describes a Laser Sensor
    /// </summary>
    public class ProximitySensor
    { 
        /// <summary>
        /// Maximum measuring distance
        /// </summary>
        public double MaxDistance { get; }
        /// <summary>
        /// Handler of the sensor in the given simulation
        /// </summary>
        public int Handler { get; }

        private double? Distance;
        private char Detected;

        /// <summary>
        /// Detection parameters
        /// </summary>
        public float[] DetectedPoint = { -1,-1,-1};
        /// <summary>
        /// Exact position of the sensor
        /// </summary>
        public float[] SensorPosition;
        private float[] NormalVector;
        private int DetectedObjectHandler;

        /// <summary>
        /// Constructor of a laser sensor, after declaring a sensor it automaticly measure's the distance once to set every variable
        /// </summary>
        /// <param name="MaxDistance">Mximum measuring distance</param>
        /// <param name="Handler">Handler of the sensor in the given simulation environment</param>
        public ProximitySensor(double MaxDistance, int Handler)
        {
            this.MaxDistance = MaxDistance;
            this.Handler = Handler;
            CoppeliaSim.simx_error ProximityRet = CoppeliaSim.RemoteAPIWrapper.simxReadProximitySensor(CoppeliaSim.Simulation.ClientID, Handler,
                                                         ref Detected, DetectedPoint, ref DetectedObjectHandler, NormalVector, CoppeliaSim.simx_opmode.oneshot_wait );
            if (DetectedPoint != null)
            {
                Distance = (double)DetectedPoint[2];
            }
            else
            {
                Distance = null;
            }
        }

        /// <summary>
        /// Updates Distance and Detected variables
        /// </summary>
        /// <param name="OutDistance">Nullable out parameter, containing the measured distance if there's an object in range</param>
        /// /// <remarks>
        /// Throws InvalidOperationException in this case
        /// <list type="bullet">
        /// <item>
        /// <description>There was an error querying the distance value from the simulation</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>True if object is detected, and detection was successful</returns>
        public bool Measure(out double? OutDistance)
        {

            CoppeliaSim.simx_error ProximityRet = CoppeliaSim.RemoteAPIWrapper.simxReadProximitySensor(CoppeliaSim.Simulation.ClientID, Handler,
                                                         ref Detected, DetectedPoint, ref DetectedObjectHandler, NormalVector, CoppeliaSim.simx_opmode.oneshot_wait);
            if (DetectedPoint != null)
            {
                Distance = (double)DetectedPoint[2];
            }
            else
            {
                Distance = null;
                if(ProximityRet != CoppeliaSim.simx_error.noerror) { throw new InvalidOperationException("There was an error in quering the proximity sensors value, returning to Idle state!"); }
            }
            OutDistance = Distance;
            return Detected != 0; 
        }
    }
}
