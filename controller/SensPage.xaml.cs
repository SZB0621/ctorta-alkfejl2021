using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ControlApp.Commands;
using ControlApp.ViewModel;
using ControlApp.Model;

namespace ControlApp
{
    /// <summary>
    /// Sensor test simulation page.
    /// <remark>
    /// - Sliders for setting simulation parameters.
    /// - Plots for visualizing the simulation results.
    /// </remark>
    /// </summary>
    public sealed partial class SensPage : Page
    {
        /// <summary>
        /// GetResultsCommand for requesting the simulation results through HTTP.
        /// Visible after a simulation has been started.
        /// </summary>
        SimulateCommand SimCommand;
        /// <summary>
        /// SimulateCommand for posting the simulation setup through HTTP.
        /// </summary>
        GetResultsCommand GetResCommand;
        /// <summary>
        /// MessageViewModel for managing a Message.
        /// </summary>
        MessageViewModel MsgVM;
        /// <summary>
        /// RobotViewModel for managin a RobotModel.
        /// </summary>
        RobotViewModel RobotVM;
        /// <summary>
        /// PlotViewModel for plotting with OxyPlot.
        /// </summary>
        PlotViewModel PlotVM => (PlotViewModel)DataContext;

        /// <summary>
        /// Constructor of the SensPage.
        /// Instanciates the ViewModels and Commands.
        /// </summary>
        public SensPage()
        {
            this.InitializeComponent();
            MsgVM = new MessageViewModel(new MessageModel(0, 0, 0.2));
            RobotVM = new RobotViewModel();
            SimCommand = new SimulateCommand(MsgVM.MsgModel);
            GetResCommand = new GetResultsCommand(RobotVM.RobotModel, MsgVM.MsgModel);
        }

        /// <summary>
        /// Slider event handler for setting the robot's velocity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderVelocitySens_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MsgVM.Velocity = e.NewValue / 100;
        }

        /// <summary>
        /// Slider event handler for setting the stopping distance from the obstacle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderStop_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MsgVM.StoppingDistance = e.NewValue / 100 + 0.2;
        }

        /// <summary>
        /// Button click event handler for sending the simulation setup through HTTP to the simulation environment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSensSim_Click(object sender, RoutedEventArgs e)
        {
            MsgVM.SimMode = SimulationMode.SENSOR_TEST_SIM;
        }

        /// <summary>
        /// Button click event handler for refershing the plots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PlotVM.UpdateSensFrontPlot(RobotVM);
            PlotVM.UpdateSensRearPlot(RobotVM);
        }
    }
}
