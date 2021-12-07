using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlApp.Model;

namespace ControlApp.ViewModel
{
    /// <summary>
    /// Enum for setting the turning direction of the robot in the circling simulation mode.
    /// <remark>
    /// - CW: Clockwise is -1.
    /// - CCW: Counter-clockwise is 1.
    /// </remark>
    /// </summary>
    public enum TurningDirection {
        CW = -1,
        CCW = 1
    }

    /// <summary>
    /// ViewModel that manages the MessageModel which is posted through HTTP.
    /// </summary>
    public class MessageViewModel : ObservableObject
    {
        /// <summary>
        /// MessageModel that contains simulation parameters for the simulation environment.
        /// <remark>
        /// Inhertis from ObservableObject, so it implements INPC.
        /// </remark>
        /// </summary>
        public MessageModel MsgModel;
        /// <summary>
        /// TurningDirection enum for setting the turning direction.
        /// </summary>
        private TurningDirection turnDir;

        /// <summary>
        /// Constructor of the MessageViewModel.
        /// Sets the MsgModel and TurnDir property and subscribes for the Model's PropertyChanged event.
        /// The default value for TurnDir is counter-clockwise.
        /// </summary>
        /// <param name="dataModel">MessageModel to set the property MsgModel.</param>
        public MessageViewModel(MessageModel dataModel)
        {
            this.MsgModel = dataModel;
            this.TurnDir = TurningDirection.CCW;
            MsgModel.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// The <c>SimMode</c> represents a SimulationMode enum which defines the simulation type to run.
        /// </summary>
        public SimulationMode SimMode
        {
            get => MsgModel.SimMode;
            set
            {
                MsgModel.SimMode = value;
            }
        }

        /// <summary>
        /// The <c>TurnDir</c> represents a TruningDirection enum defining the turning direction of the robot during circling.
        /// </summary>
        public TurningDirection TurnDir
        {
            get => turnDir;
            set
            {
                if (turnDir != value)
                {
                    turnDir = value;
                }
            }
        }

        /// <summary>
        /// The <c>Velocity</c> represents a double that defines the robot's speed during simulation.
        /// </summary>
        public double Velocity
        {
            get => MsgModel.Velocity;
            set
            {
                if (MsgModel.Velocity != value)
                {
                    MsgModel.Velocity = value;
                }
            }
        }

        /// <summary>
        /// The <c>TurningRadius</c> represents a double that defines the radius of the circular path of the robot.
        /// If it is positive, the robot moves counter-clockwise.
        /// If it is negative, the robot moves clockwise.
        /// </summary>
        public double TurningRadius
        {
            get => MsgModel.TurningRadius;
            set
            {
                if (MsgModel.TurningRadius != (int)TurnDir * Math.Abs(value))
                {
                    MsgModel.TurningRadius = (int)TurnDir * Math.Abs(value);
                }
            }
        }

        /// <summary>
        /// The <c>StoppingDistance</c> represents a double that defines how far should the robot stop from the obstacle during sensor test.
        /// </summary>
        public double StoppingDistance
        {
            get => MsgModel.StoppingDistance;
            set
            {
                if (MsgModel.StoppingDistance != value)
                {
                    MsgModel.StoppingDistance = value;
                }
            }
        }

        /// <summary>
        /// Methods for INotifyPropertyChanged forwarding.
        /// </summary>
        #region INPC forwarding
        private readonly string[] DependentPropertyNames = {
            nameof(MessageModel.Velocity),
            nameof(MessageModel.TurningRadius),
            nameof(MessageModel.StoppingDistance) };

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DependentPropertyNames.Contains(e.PropertyName))
            {
                Notify(e.PropertyName);
            }
        }
        #endregion
    }
}
