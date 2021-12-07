using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ControlApp.Model;
using OxyPlot;

namespace ControlApp.ViewModel
{
    /// <summary>
    /// ViewModel that manages the RobotModel. Used for plotting the simulation results.
    /// <remark>
    /// Inhertis from ObservableObject, so it implements INPC.
    /// </remark>
    /// </summary>
    public class RobotViewModel : ObservableObject
    {
        
        /// <summary>
        /// RobotModel which contains the simulation data in DataPoint arrays.
        /// </summary>
        public RobotModel RobotModel;

        /// <summary>
        /// Constructor of the RobotViewModel.
        /// Instanciates a new RobotModel and subscribes for the Model's PropertyChanged event.
        /// </summary>
        public RobotViewModel()
        {
            this.RobotModel = new RobotModel();
            RobotModel.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// The <c>Position</c> represents a DataPoint array containing the robot's X and Y coordinates.
        /// </summary>
        public DataPoint[] Position
        {
            get => RobotModel.Position;
            set
            {
                RobotModel.Position = value;
            }
        }

        /// <summary>
        /// The <c>Orientation</c> represents a DataPoint array containing the robot's orientation values for each simulation timestep.
        /// </summary>
        public DataPoint[] Orientation
        {
            get => RobotModel.Orientation;
            set
            {
                RobotModel.Orientation = value;
            }
        }

        /// <summary>
        /// The <c>Velocity</c> represents a DataPoint array containing the robot's velocity values for each simulation timestep.
        /// </summary>
        public DataPoint[] Velocity
        {
            get => RobotModel.Velocity;
            set
            {
                RobotModel.Velocity = value;
            } 
        }

        /// <summary>
        /// The <c>SensorValues</c> represents a 2D DataPoint array containing the 2 robot sensors' values for each simulation timestep.
        /// </summary>
        public DataPoint[][] SensorValues
        {
            get => RobotModel.SensorValues;
            set
            {
                RobotModel.SensorValues = value;
            }
        }

        /// <summary>
        /// Methods for INotifyPropertyChanged forwarding.
        /// </summary>
        #region INPC forwarding
        private readonly string[] DependentPropertyNames = {
            nameof(Model.RobotModel.Position),
            nameof(Model.RobotModel.Orientation),
            nameof(Model.RobotModel.SensorValues),
            nameof(Model.RobotModel.Velocity)
            };

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
