using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using common;

namespace ControlApp.Utils
{
    /// <summary>
    /// Auxiliary class with methods that convert the different types of data arrays in a List of RobotSample to a DataPoint array for RobotModel.
    /// Used for plotting with OxyPlot.
    /// </summary>
    public class ConvertToDataPoint
    {
        /// <summary>
        /// Converts LocOri.Point values to DataPoints.
        /// Used for plotting the robot's position.
        /// </summary>
        /// <param name="simResultList">List of RobotSamples contaning the simulation's results.</param>
        /// <returns>DataPoint array, which contains the robot's X and Y position coordinates from the simulation.</returns>
        public DataPoint[] GetPositionArray(List<RobotSample> simResultList)
        {
            DataPoint[] posArray = new DataPoint[simResultList.Count];
            for (int i = 0; i < simResultList.Count; i++)
            {
                posArray[i] = new DataPoint(simResultList[i].LocOri.Location.X, simResultList[i].LocOri.Location.Y);
            }
            return posArray;
        }

        /// <summary>
        /// Converts RobotSample.TimeStamp and LocOri.Orientation values to DataPoints.
        /// Used for plotting the robot's orientation with respect to the timesteps of the simulation.
        /// </summary>
        /// <param name="simResultList">List of RobotSamples contaning the simulation's results.</param>
        /// <returns>DataPoint array, which contains the robot's orientation for every timestep.</returns>
        public DataPoint[] GetOrientationArray(List<RobotSample> simResultList)
        {
            DataPoint[] oriArray = new DataPoint[simResultList.Count];
            for (int i = 0; i < simResultList.Count; i++)
            {
                oriArray[i] = new DataPoint(simResultList[i].Stamp, (simResultList[i].LocOri.Orientation * 180 / Math.PI));
            }
            return oriArray;
        }

        /// <summary>
        /// Converts RobotSample.TimeStamp and Velocity values to DataPoints.
        /// </summary>
        /// <param name="simResultList">List of RobotSamples contaning the simulation's results.</param>
        /// <returns>DataPoint array, which contains the robot's velocity for every timestep.</returns>
        public DataPoint[] GetVelocityArray(List<RobotSample> simResultList)
        {
            DataPoint[] veloArray = new DataPoint[simResultList.Count];
            for (int i = 0; i < simResultList.Count; i++)
            {
                veloArray[i] = new DataPoint(simResultList[i].Stamp, simResultList[i].Speed);
            }
            return veloArray;
        }

        /// <summary>
        /// Converts RobotSample.TimesStamp and SensorValues from the 2 ultrasound sensors of the robot to DataPoints.
        /// Used for plotting the robot's distance from an obstacle with respect to the timesteps of the simulation.
        /// </summary>
        /// <param name="simResultList">List of RobotSamples contaning the simulation's results.</param>
        /// <returns>2D DataPoint array, which contains the sensor values form the 2 sensors of the robot for every timestep.</returns>
        public DataPoint[][] GetSensorsArray(List<RobotSample> simResultList)
        {
            int sensorCount = 2;
            DataPoint[][] sensArray = new DataPoint[sensorCount][];
            sensArray[0] = new DataPoint[simResultList.Count];
            sensArray[1] = new DataPoint[simResultList.Count];

            for (int i = 0; i < sensorCount; i++)
            {
                for (int j = 0; j < simResultList.Count; j++)
                {
                    sensArray[i][j] = new DataPoint(simResultList[j].Stamp, simResultList[j].GetSensorData(i));
                }
            }
            return sensArray;

        }
    }
}
