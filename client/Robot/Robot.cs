using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Robot
{
    /// <summary>
    /// Describes a Robot and it's functionalities, states, parameters
    /// </summary>
    public class Robot
    {
        /// <summary>
        /// Array of Robot's actuators (right,left)
        /// </summary>
        public Motor[] Motors = {null, null};
        /// <summary>
        /// Array of Robot's laser sensors (front,rear)
        /// </summary>
        public ProximitySensor[] Sensors = { null, null };
        /// <summary>
        /// Maximum linear velocity of the Robot's motors'
        /// </summary>
        private readonly double MotorMaxVelocity;
        /// <summary>
        /// Maximum measuring distance of the Robot's laser sensors'
        /// </summary>
        private readonly double SensorMaxDistance;
        /// <summary>
        /// Robot's wheelbase
        /// </summary>
        private readonly double Wheelbase;
        /// <summary>
        /// Struct to store the Robot's position and orientation values
        /// </summary>
        private common.LocOri LocOri;
        private CoppeliaSim.States CurrentStatePrivate;
        /// <summary>
        /// Enum to store the actual state of the robot
        /// <remarks>
        /// Using the setter actually changes the scene in in the coppelia simulation environment
        /// also re-initializes every handler appropriately
        /// Throws Exceptions in these cases
        /// <list type="bullet">
        /// <item>
        /// <description>Argument: False state was given</description>
        /// </item>
        /// <item>
        /// <description>Exception: Scene change failed due to
        /// either Coppelia's server side initialization error
        /// either wrong path was given for the coppelia scenes</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// </summary>
        public CoppeliaSim.States CurrentState
        {
            get { return CurrentStatePrivate; }
            set
            {
                if(!Enum.IsDefined(typeof(CoppeliaSim.States), value))
                {
                    throw new System.ArgumentException(string.Format("Invalid Robot state {0}.", value));
                }
                else
                {
                    if (!CoppeliaSim.Simulation.ChangeScene(CoppeliaSim.RemoteAPIWrapper.WrapStatesToSceneNames(value), CoppeliaSim.RemoteAPIWrapper.ScenePath))
                    {
                        throw new System.Exception("Scene change failed.");
                    }
                    else
                    {
                        Motors[0].Velocity = 0.0;
                        Motors[1].Velocity = 0.0;
                        CurrentStatePrivate = value;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the location and orientation values based on the simulation then returns them
        /// </summary>
        /// <returns>Location and Orientation values</returns>
        public common.LocOri SetAndGetLocOri()
        {
            float[] Orientation = { 0, 0, 0 };
            float[] Position = { 0, 0, 0 };
            CoppeliaSim.RemoteAPIWrapper.simxGetObjectOrientation(CoppeliaSim.Simulation.ClientID, CoppeliaSim.Simulation.RobotHandler, CoppeliaSim.Simulation.ReferenceBox, Orientation, CoppeliaSim.simx_opmode.oneshot_wait);
            CoppeliaSim.RemoteAPIWrapper.simxGetObjectPosition(CoppeliaSim.Simulation.ClientID, CoppeliaSim.Simulation.RobotHandler, CoppeliaSim.Simulation.ReferenceBox, Position, CoppeliaSim.simx_opmode.oneshot_wait);
            LocOri.Location.X = (double)Position[0];
            LocOri.Location.Y = (double)Position[1];
            LocOri.Orientation = (double)Orientation[2];

            return LocOri;
        }
        /// <summary>
        /// Constructor for robot
        /// </summary>
        /// <param name="MotorMaxVelocity">Max velocity of the wheels</param>
        /// <param name="SensorMaxDistance">Max measuring distance of the sensors</param>
        /// <param name="Wheelbase">Robot"s wheelbase</param>
        public Robot(double MotorMaxVelocity = 2.0, double SensorMaxDistance = 2.0, double Wheelbase = 0.331)
        {
            this.MotorMaxVelocity = MotorMaxVelocity;
            this.SensorMaxDistance = SensorMaxDistance;
            this.Wheelbase = Wheelbase;

            float[] Orientation = { 0, 0, 0 };
            float[] Position = { 0, 0, 0 };
            CoppeliaSim.RemoteAPIWrapper.simxGetObjectOrientation(CoppeliaSim.Simulation.ClientID, CoppeliaSim.Simulation.RobotHandler,CoppeliaSim.Simulation.ReferenceBox, Orientation, CoppeliaSim.simx_opmode.oneshot_wait);
            CoppeliaSim.RemoteAPIWrapper.simxGetObjectPosition(CoppeliaSim.Simulation.ClientID, CoppeliaSim.Simulation.RobotHandler, CoppeliaSim.Simulation.ReferenceBox, Position, CoppeliaSim.simx_opmode.oneshot_wait);

            LocOri = new common.LocOri((double)Position[0], (double)Position[1], (double)Orientation[2]);

            Motors[0] = new Motor(this.MotorMaxVelocity, CoppeliaSim.Simulation.RightMotorHandler);
            Motors[1] = new Motor(this.MotorMaxVelocity, CoppeliaSim.Simulation.LeftMotorHandler);
            Sensors[0] = new ProximitySensor(this.SensorMaxDistance, CoppeliaSim.Simulation.FrontProximityHandler);
            Sensors[1] = new ProximitySensor(this.SensorMaxDistance, CoppeliaSim.Simulation.RearProximityHandler);

            HttpServer.ClientLog.Add("Robot declared!");

            CurrentState = CoppeliaSim.States.idle;
        }

        /// <summary>
        /// Executes Idle state
        /// - Wait state, robot is not moving
        /// </summary>
        /// <param name="RobotSamples">List of class <c>common.RobotSample</c>
        /// as an OUT parameter contains the RobotSample values during the simulation</param>
        /// <returns>True if reaching Idle state was successful</returns>
        public bool DoIdleActions(out List<common.RobotSample> RobotSamples)
        {
            int TimeStamp = -1;
            RobotSamples = new();
            try
            {
                CurrentState = CoppeliaSim.States.idle;
                HttpServer.ClientLog.Add("IdleState Reached - " + System.DateTime.Now.ToString() + " !");
                common.RobotSample RobotSampleElement;
                RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), TimeStamp: TimeStamp += 1);
                RobotSamples.Add(RobotSampleElement);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executes Circulation state
        ///  - Makes a circule with a given radius (the radius's sign decides the direction)
        /// </summary>
        /// <param name="Radius">Robot circules around a circle with this given Radius</param>
        /// <param name="Velocity">The velocity of the left wheel the other wheel's velocity gets calculated based on this</param>
        /// <param name="RobotSamples">List of class <c>common.RobotSample</c>
        /// as an OUT parameter contains the RobotSample values during the simulation</param>
        /// <returns>Returns True if Circulation finished and Robot Parameters</returns>
        public bool DoCirculationActions(out List<common.RobotSample> RobotSamples, double Radius = 2.0, double Velocity = 1.0)
        {
            int TimeStamp = -1;
            double Distance = 0.0;
            Radius = (Radius < 0.0) ? Clamp(Radius, -7.0, -1.0) : (Radius > 0.0 || Radius == 0.0) ? Clamp(Radius, 1.0, 7.0) : Radius;
            double VLeftMotor = Clamp(Velocity, -MotorMaxVelocity + 0.2, MotorMaxVelocity - 0.2);
            double VRightMotor = VLeftMotor * ((Radius + Wheelbase) / (Radius - Wheelbase)); // Using forumula: R=((v_2+v_1)/(v_2-v_1))*b considering one of the wheelspeeds const and known
            RobotSamples = new();
            try
            {
                CurrentState = CoppeliaSim.States.circulation;

                HttpServer.ClientLog.Add("Circulationtest Started - " + System.DateTime.Now.ToString() + " !");
                common.RobotSample RobotSampleElement;

                Motors[0].Velocity = VRightMotor;
                Motors[1].Velocity = VLeftMotor;

                var LinearVelocity = (Math.Abs(VRightMotor - VLeftMotor) / Wheelbase);

                CoppeliaSim.Simulation.GetObjectPositionAsPoint(CoppeliaSim.Simulation.RobotHandler, out common.Point StartPosition);
                CoppeliaSim.Simulation.GetObjectPositionAsPoint(CoppeliaSim.Simulation.RobotHandler, out common.Point CurrentPosition);

                while (Distance < 0.5)
                {
                    RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), LinearVelocity, TimeStamp: TimeStamp += 1);
                    RobotSamples.Add(RobotSampleElement);
                    CoppeliaSim.Simulation.GetObjectPositionAsPoint(CoppeliaSim.Simulation.RobotHandler, out CurrentPosition);
                    Distance = Math.Sqrt(Math.Pow(StartPosition.X - CurrentPosition.X, 2) + Math.Pow(StartPosition.Y - CurrentPosition.Y, 2));
                }

                while (Distance > 0.1)
                {
                    RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), LinearVelocity, TimeStamp: TimeStamp += 1);
                    RobotSamples.Add(RobotSampleElement);
                    CoppeliaSim.Simulation.GetObjectPositionAsPoint(CoppeliaSim.Simulation.RobotHandler, out CurrentPosition);
                    Distance = Math.Sqrt(Math.Pow(StartPosition.X - CurrentPosition.X, 2) + Math.Pow(StartPosition.Y - CurrentPosition.Y, 2));
                }

                Motors[0].Velocity = 0.0;
                Motors[1].Velocity = 0.0;

                HttpServer.ClientLog.Add("Circulationtest Ended - " + System.DateTime.Now.ToString() + " !");
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executes SensorTest state
        ///  - Moves to forward/backward till an object is within BoundaryDistance
        ///  - then moves backward/forward till an object is within BoundaryDistance
        /// </summary>
        /// <param name="RobotSamples">List of class <c>common.RobotSample</c>
        /// as an OUT parameter contains the RobotSample values during the simulation</param>
        /// <param name="BoundaryDistance">Describes how far the robot should stop from the obstacle</param>
        /// <param name="Velocity">How fast the robot should move</param>
        /// <returns></returns>
        public bool DoSensorTestActions(out List<common.RobotSample> RobotSamples, double BoundaryDistance = 1, double Velocity = 1.0)
        {
            int TimeStamp = -1;
            double? DistanceFront;
            double? DistanceRear;
            common.RobotSample RobotSampleElement;
            RobotSamples = new();
            try
            {
                CurrentState = CoppeliaSim.States.sensortest;

                HttpServer.ClientLog.Add("Sensortest Started - " + System.DateTime.Now.ToString() + " !");
                Velocity = Clamp(Velocity, -MotorMaxVelocity, MotorMaxVelocity);
                BoundaryDistance = Clamp(BoundaryDistance, 0.5, SensorMaxDistance);
                if (Velocity > 0)
                {
                    Motors[0].Velocity = Velocity;
                    Motors[1].Velocity = Velocity;
                    while (!Sensors[0].Measure(out DistanceFront))
                    {
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    while (DistanceFront > BoundaryDistance)
                    {
                        Sensors[0].Measure(out DistanceFront);
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0, DistanceFront);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    Motors[0].Velocity = -Velocity;
                    Motors[1].Velocity = -Velocity;
                    while (!Sensors[1].Measure(out DistanceRear))
                    {
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    while (DistanceRear > BoundaryDistance)
                    {
                        Sensors[1].Measure(out DistanceRear);
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1, DistanceRear);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    Motors[0].Velocity = 0.0;
                    Motors[1].Velocity = 0.0;

                    HttpServer.ClientLog.Add("Sensortest Ended - " + DateTime.Now.ToString() + " !");
                }
                else
                {
                    Motors[0].Velocity = Velocity;
                    Motors[1].Velocity = Velocity;
                    while (!Sensors[1].Measure(out DistanceRear))
                    {
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    while (DistanceRear > BoundaryDistance)
                    {
                        Sensors[1].Measure(out DistanceRear);
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1, DistanceRear);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    Motors[0].Velocity = -Velocity;
                    Motors[1].Velocity = -Velocity;
                    while (!Sensors[0].Measure(out DistanceFront))
                    {
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    while (DistanceFront > BoundaryDistance)
                    {
                        Sensors[0].Measure(out DistanceFront);
                        RobotSampleElement = new common.RobotSample(SetAndGetLocOri(), Velocity, TimeStamp: TimeStamp += 1);
                        RobotSampleElement.SetSensorData(0, DistanceFront);
                        RobotSampleElement.SetSensorData(1);
                        RobotSamples.Add(RobotSampleElement);
                    }
                    Motors[0].Velocity = 0.0;
                    Motors[1].Velocity = 0.0;

                    HttpServer.ClientLog.Add("Sensortest Ended - " + System.DateTime.Now.ToString() + " !");
                }
            }
            catch (InvalidOperationException)
            {
                CurrentState = CoppeliaSim.States.idle;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saturates value between the given ranges
        /// </summary>
        /// <param name="Value">The value we want to saturate</param>
        /// <param name="Min">Minimum</param>
        /// <param name="Max">Maximum</param>
        /// <returns></returns>
        public static double Clamp(double Value, double Min, double Max)
        {
            HttpServer.ClientLog.Add("Value (" + Value + ") may be saturated: [" + Min + ";" + Max + "]/0 !");
            return (Value < Min) ? Min : (Value > Max || Value == 0.0) ? Max : Value;
        }
    }
}
