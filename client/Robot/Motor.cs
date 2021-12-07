using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Robot
{
    /// <summary>
    /// Describes one Motor
    /// </summary>
    public class Motor
    { 
        /// <summary>
        /// Motor's maximum velocity
        /// </summary>
        public double MaxVelocity { get; }
        /// <summary>
        /// Motor's handler in the actual simulation
        /// </summary>
        public int Handler { get; }

        private double VelocityPrivate;
        /// <summary>
        /// Actual velocity of the motor, defense against false values
        /// Setter sets the velocity of the robot in the simulation as well
        /// </summary>
        public double Velocity
        {
            get { return VelocityPrivate; }
            set
            {
                // Saturation
                if (value > MaxVelocity)
                {
                    VelocityPrivate = MaxVelocity;
                }
                else if (value < -MaxVelocity)
                {
                    VelocityPrivate = -MaxVelocity;
                }
                else
                {
                    VelocityPrivate = value;
                }
                CoppeliaSim.RemoteAPIWrapper.simxSetJointTargetVelocity(CoppeliaSim.Simulation.ClientID, Handler, (float)VelocityPrivate, CoppeliaSim.simx_opmode.oneshot_wait);
            }
        }
        /// <summary>
        /// Constructor of a Motor
        /// </summary>
        /// <param name="MaxVelocity">Maximum motor velocity</param>
        /// <param name="Handler">Handler of the robot in the given simulation</param>
        /// <param name="startVelocity">Default velocity of the robot at the start of the initialization</param>
        public Motor(double MaxVelocity, int Handler, double startVelocity = 0.0)
        {
            this.MaxVelocity = MaxVelocity;
            this.Handler = Handler;
        }
    }
}
