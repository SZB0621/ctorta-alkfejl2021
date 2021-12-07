using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class RobotSample
    {
        /// <summary>
        /// Robot data base class.
        /// Contains any robot related data eg. current sensor data, current coordinates, velocity, etc.
        /// Used by RobotModel in Hf.Controller
        /// </summary>
        public static readonly string Manufacturer = "Pioneer";
        public static readonly string Type = "Mobile-with-differential-drive";

        private int SensorCount;

        private double[] sensordata;
        private LocOri LocationAndOrientation;
        private double Velocity;
        private int TimeStamp = 0; // Increase with every simulation timestep
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="LocationAndOrientation"></param>
        /// <param name="Velocity"></param>
        /// <param name="SensorCount"></param>
        /// <param name="TimeStamp"></param>
        public RobotSample(LocOri LocationAndOrientation,double Velocity = 0.0, int SensorCount = 2,int TimeStamp = 0)
        {
            this.Velocity = Velocity;
            this.SensorCount = SensorCount;
            this.TimeStamp = TimeStamp;
            this.LocationAndOrientation = LocationAndOrientation;
            sensordata = new double[SensorCount];            
        }
        /// <summary>
        /// Sensor's value (measured distance)
        /// </summary>
        public double[] SensorData
        {
            get { return sensordata; }
            set { sensordata = value; }
        }
        /// <summary>
        /// Position and Orientation values
        /// </summary>
        public LocOri LocOri
        {
            get { return LocationAndOrientation; }
            set { LocationAndOrientation = value; }
        }
        /// <summary>
        /// How many sensors the robot has
        /// </summary>
        public int SensorC
        {
            get { return SensorCount; }
            set { SensorCount = value; }
        }
        /// <summary>
        /// Velocity of the robot
        /// </summary>
        public double Speed
        {
            get { return Velocity; }
            set { Velocity = value; }
        }
        /// <summary>
        /// The sample's timestamp
        /// </summary>
        public int Stamp
        {
            get { return TimeStamp; }
            set { TimeStamp = value; }
        }
        /// <summary>
        /// Setting the sensor's values
        /// </summary>
        /// <param name="Index">Which sensor to set (0-front,1-rear)</param>
        /// <param name="Distance">The Distance to set</param>
       public void SetSensorData(int Index, double? Distance = 0.0)
        {
            if (Index >= 0 && Index < SensorCount)
            {
                SensorData[Index] = Distance ?? 0.0;
            }
            else
            {
                throw new ArgumentException("Index is out of SensorData's range");
            }
        }
        /// <summary>
        /// Get te sensor value
        /// </summary>
        /// <param name="Index">Which sensor to get (0-front,1-rear)</param>
        /// <returns>Distance</returns>
        public double GetSensorData(int Index)
        {
            if (Index >= 0 && Index < SensorCount)
            {
                return SensorData[Index];
            }
            else
            {
                return 0;
            }
        }
    }
}
