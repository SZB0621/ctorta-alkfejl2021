using System;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ControlApp.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ControlApp.ViewModel
{
    /// <summary>
    /// Auxiliary ViewModel handling PlotModels for plotting with OxyPlot.
    /// <remark>
    /// Inherits from ObservableObject.
    /// </remark>
    /// </summary>
    public class PlotViewModel : ObservableObject
    {
        /// <summary>
        /// PlotModel for plotting position.
        /// </summary>
        private PlotModel positionPlotModel;
        /// <summary>
        /// PlotModel for plotting the orientation.
        /// </summary>
        private PlotModel orientationPlotModel;
        /// <summary>
        /// PlotModel for plotting the frontal sensor values.
        /// </summary>
        private PlotModel sensFrontPlotModel;
        /// <summary>
        /// PlotModel for plotting the rear sensor values.
        /// </summary>
        private PlotModel sensRearPlotModel;

        /// <summary>
        /// The <c>PositionPlotModel</c> represents a PlotModel for plotting the robot's position.
        /// </summary>
        public PlotModel PositionPlotModel
        {
            get => positionPlotModel;
            set
            {
                if (value != positionPlotModel)
                {
                    positionPlotModel = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// The <c>OrientationPlotModel</c> represents a PlotModel for plotting the robot's orientation.
        /// </summary>
        public PlotModel OrientationPlotModel
        {
            get => orientationPlotModel;
            set
            {
                if (value != orientationPlotModel)
                {
                    orientationPlotModel = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// The <c>SensFrontPlotModel</c> represents a PlotModel for plotting the robot's frontal sensor's values.
        /// </summary>
        public PlotModel SensFrontPlotModel
        {
            get => sensFrontPlotModel;
            set
            {
                if (value != sensFrontPlotModel)
                {
                    sensFrontPlotModel = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// The <c>SensRearPlotModel</c> represents a PlotModel for plotting the robot's rear sensor's values.
        /// </summary>
        public PlotModel SensRearPlotModel
        {
            get => sensRearPlotModel;
            set
            {
                if (value != sensRearPlotModel)
                {
                    sensRearPlotModel = value;
                    Notify();
                }
            }
        }

        /// <summary>
        /// Method for updating the plot of the position values.
        /// </summary>
        /// <param name="robotVM">RobotViewModel that manages the RobotModel which data is used.</param>
        public void UpdatePositionPlot(RobotViewModel robotVM)
        {
            FunctionSeries posSeries = new FunctionSeries();
            PlotModel tempPlotModel = new PlotModel { Title = "Position" };

            LinearAxis XAxis = new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom };
            LinearAxis YAxis = new LinearAxis();
            XAxis.Title = "x [m]";
            YAxis.Title = "y [m]";
            tempPlotModel.Axes.Add(XAxis);
            tempPlotModel.Axes.Add(YAxis);

            for (int i = 0; i < robotVM.Position.Length; i++)
            {
                posSeries.Points.Add(robotVM.Position[i]);
            }
            tempPlotModel.Series.Add(posSeries);
            PositionPlotModel = tempPlotModel;
        }

        /// <summary>
        /// Method for updating the plot of the orientation values.
        /// </summary>
        /// <param name="robotVM">RobotViewModel that manages the RobotModel which data is used.</param>
        public void UpdateOrientationPlot(RobotViewModel robotVM)
        {
            FunctionSeries oriSeries = new FunctionSeries();
            PlotModel tempPlotModel = new PlotModel { Title = "Orientation" };

            LinearAxis XAxis = new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom };
            LinearAxis YAxis = new LinearAxis();
            XAxis.Title = "t [k]";
            YAxis.Title = "Θ [°]";
            tempPlotModel.Axes.Add(XAxis);
            tempPlotModel.Axes.Add(YAxis);

            for (int i = 0; i < robotVM.Orientation.Length; i++)
            {
                oriSeries.Points.Add(robotVM.Orientation[i]);
            }
            tempPlotModel.Series.Add(oriSeries);
            OrientationPlotModel = tempPlotModel;
        }

        /// <summary>
        /// Method for updating the plot of the frontal sensor values.
        /// </summary>
        /// <param name="robotVM">RobotViewModel that manages the RobotModel which data is used.</param>
        public void UpdateSensFrontPlot(RobotViewModel robotVM)
        {
            FunctionSeries sensFrontSeries = new FunctionSeries();
            PlotModel tempPlotModel = new PlotModel { Title = "Frontal sensor values" };

            LinearAxis XAxis = new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom };
            LinearAxis YAxis = new LinearAxis();
            XAxis.Title = "t [k]";
            YAxis.Title = "d [m]";
            tempPlotModel.Axes.Add(XAxis);
            tempPlotModel.Axes.Add(YAxis);

            for (int i = 0; i < robotVM.SensorValues[0].Length; i++)
            {
                sensFrontSeries.Points.Add(robotVM.SensorValues[0][i]);
            }
            tempPlotModel.Series.Add(sensFrontSeries);
            SensFrontPlotModel = tempPlotModel;
        }

        /// <summary>
        /// Method for updating the the plot of the rear sensor values.
        /// </summary>
        /// <param name="robotVM">RobotViewModel that manages the RobotModel which data is used.</param>
        public void UpdateSensRearPlot(RobotViewModel robotVM)
        {
            FunctionSeries sensRearSeries = new FunctionSeries();
            PlotModel tempPlotModel = new PlotModel { Title = "Rear sensor values" };

            LinearAxis XAxis = new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom };
            LinearAxis YAxis = new LinearAxis();
            XAxis.Title = "t [k]";
            YAxis.Title = "d [m]";
            tempPlotModel.Axes.Add(XAxis);
            tempPlotModel.Axes.Add(YAxis);

            for (int i = 0; i < robotVM.SensorValues[1].Length; i++)
            {
                sensRearSeries.Points.Add(robotVM.SensorValues[1][i]);
            }
            tempPlotModel.Series.Add(sensRearSeries);
            SensRearPlotModel = tempPlotModel;
        }
    }
}
