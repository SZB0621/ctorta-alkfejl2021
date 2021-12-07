using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using ControlApp.Utils;

namespace ControlApp.Model
{
    /// <summary>
    /// RobotModel class, representing a Robot that contains the simulation results.
    /// <remark>
    /// Inherits from ObservableObject.
    /// </remark>
    /// </summary>
    public class RobotModel : ObservableObject
    {
        /// <summary>
        /// Constant defining the number of ultrasound sensors on the robot.
        /// (Currently 1 at the front and 1 at the rear of the robot.)
        /// </summary>
        public const int SENSOR_COUNT = 2;
        /// <summary>
        /// DataPoint array that contains all the X and Y coordinates of the robot from the simulation sequence.
        /// </summary>
        private DataPoint[] position;
        /// <summary>
        /// DataPoint array that contains the orientation of the robot from the simulation sequence for every timestep.
        /// </summary>
        private DataPoint[] orientation;
        /// <summary>
        /// DataPoint array that contains the velocity of the robot from the simulation sequence for every timestep.
        /// </summary>
        private DataPoint[] velocity;
        /// <summary>
        /// 2D DataPoint array that contains the 2 sensors' values of the robot from the simulation sequence for every timestep.
        /// </summary>
        private DataPoint[][] sensorValues;

        /// <summary>
        /// Constructor of the RobotModel.
        /// Sets the properties with initial, placeholder 0 values.
        /// </summary>
        public RobotModel()
        {
        
            this.Position = new DataPoint[1];
            this.Position[0] = new DataPoint(0, 0);

            this.Velocity = new DataPoint[1];
            this.Velocity[0] = new DataPoint(0, 0);

            this.Orientation = new DataPoint[1];
            this.Orientation[0] = new DataPoint(0, 0);

            this.SensorValues = new DataPoint[SENSOR_COUNT][];
            this.SensorValues[0] = new DataPoint[1];
            this.SensorValues[0][0] = new DataPoint(0, 0);
            this.SensorValues[1] = new DataPoint[1];
            this.SensorValues[1][0] = new DataPoint(0, 0);

        }

        /// <summary>
        /// The <c>Orientation</c> represents a DataPoint array for the orientation values and timesteps.
        /// </summary>
        public DataPoint[] Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                Notify();
            }
        }

        /// <summary>
        /// The <c>Velocity</c> represents a DataPoint array for the velocity values and timesteps.
        /// </summary>
        public DataPoint[] Velocity
        {
            get => velocity;
            set
            {
                velocity = value;
                Notify();
            }
        }

        /// <summary>
        /// The <c>Position</c> represents a DataPoint array for the position coordinates.
        /// </summary>
        public DataPoint[] Position
        {
            get => position;
            set
            {
                position = value;
                Notify();
            }
        }

        /// <summary>
        /// The <c>SensorValues</c> represents a 2D DataPoint array for the 2 sensors' values and timesteps.
        /// </summary>
        public DataPoint[][] SensorValues
        {
            get => sensorValues;
            set
            {
                sensorValues = value;
                Notify();
            }
        }
    }
}

