using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Model
 {
    /// <summary>
    /// Enum for setting the simulation type.
    /// <remark>
    /// - IDLE: default, initial mode.
    /// - CIRCLING_SIM: circling simulation with a given velocity, circle radius and direction.
    /// - SENSOR_TEST_SIM: sensor test when the robot stops in a given distance from an obstacle.
    /// </remark>
    /// </summary>
    public enum SimulationMode {
        IDLE = 0,
        CIRCLING_SIM = 1,
        SENSOR_TEST_SIM = 2,
    }

    /// <summary>
    /// MessageModel class, representing a setup message for the simulation environment.
    /// <remark>
    /// Inherits from ObservableObject.
    /// </remark>
    /// </summary>
    public class MessageModel : ObservableObject
    {
        /// <summary>
        /// Double that defines the robot's velocity during simulation.
        /// </summary>
        private double velocity;
        /// <summary>
        /// Double that defines the robot's turning radius during circling simulation.
        /// </summary>
        private double turningRadius;
        /// <summary>
        /// Double that defines the stopping distance from the obstacle during sensor test.
        /// </summary>
        private double stoppingDistance;
        /// <summary>
        /// SimulationMode enum that defines the selected simulation type.
        /// </summary>
        private SimulationMode simMode;
        
        /// <summary>
        /// Constructor of the MessageModel.
        /// Sets the Velocity, TurningRadius, StoppingDistance and SimMode property values.
        /// </summary>
        /// <param name="velocity">Velocity of the robot.</param>
        /// <param name="turningRadius">Turning radius for circling simulation.</param>
        /// <param name="stoppingDistance">Stopping distance from obstacle for sensor test.</param>
        /// <param name="simMode">Simulation mode.</param>
        public MessageModel(double velocity = 0, double turningRadius = 0, double stoppingDistance = 0, SimulationMode simMode = SimulationMode.IDLE)
        {
            this.Velocity = velocity;
            this.TurningRadius = turningRadius;
            this.StoppingDistance = stoppingDistance;
            this.SimMode = simMode;
        }

        /// <summary>
        /// The <c>SimMode</c> represents a SimulationMode enum for setting the desired simulation type.
        /// </summary>
        public SimulationMode SimMode
        {
            get => simMode;
            set
            {
                simMode = value;
                Notify();
            }
        }

        /// <summary>
        /// The <c>Velocity</c> represents a double for setting the robot's speed during simulation.
        /// </summary>
        public double Velocity
        {
            get => velocity;
            set
            {
                if (velocity != value)
                {
                    velocity = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// The <c>TurningRadius</c> represents a double for setting the circling radius.
        /// </summary>
        public double TurningRadius
        {
            get => turningRadius;
            set
            {
                if (turningRadius != value)
                {
                    turningRadius = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// The <c>StoppingDistance</c> represents a double for setting stopping distance from an obstacle.
        /// </summary>
        public double StoppingDistance
        {
            get => stoppingDistance;
            set
            {
                if (stoppingDistance != value)
                {
                    stoppingDistance = value;
                    Notify();
                }
            }
        }
    }
}
