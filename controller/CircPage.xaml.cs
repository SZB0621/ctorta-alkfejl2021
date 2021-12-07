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
using ControlApp.ViewModel;
using ControlApp.Model;
using ControlApp.Commands;
using OxyPlot;
using OxyPlot.Series;

namespace ControlApp
{
    /// <summary>
    /// Circling simulation page.
    /// <remark>
    /// - Sliders for setting simulation parameters.
    /// - Plots for visualizing the simulation results.
    /// </remark>
    /// </summary>
    public sealed partial class CircPage : Page
    {
        /// <summary>
        /// GetResultsCommand for requesting the simulation results through HTTP.
        /// Visible after a simulation has been started.
        /// </summary>
        GetResultsCommand GetResCommand;
        /// <summary>
        /// SimulateCommand for posting the simulation setup through HTTP.
        /// </summary>
        SimulateCommand SimCommand;
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
        /// Constructor of the CircPage.
        /// Instanciates the ViewModels and Commands.
        /// </summary>
        public CircPage()
        {
            this.InitializeComponent();

            MsgVM = new MessageViewModel(new MessageModel(0.5, 1, 0));
            RobotVM = new RobotViewModel();
            SimCommand = new SimulateCommand(MsgVM.MsgModel);
            GetResCommand = new GetResultsCommand(RobotVM.RobotModel, MsgVM.MsgModel);
        }

        /// <summary>
        /// Slider event handler for setting the robot's velocity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderVelocityCirc_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MsgVM.Velocity = e.NewValue / 100 + 0.5;
        }

        /// <summary>
        /// Slider event handler for setting the circling radius.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderRadius_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MsgVM.TurningRadius = e.NewValue / 10 + 1;
        }
       
        /// <summary>
        /// Button click event handler for sending the simulation setup through HTTP to the simulation environment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCircSim_Click(object sender, RoutedEventArgs e)
        {
            MsgVM.SimMode = SimulationMode.CIRCLING_SIM;
        }

        /// <summary>
        /// Button click event handler for setting the turning direction to counter-clockwise.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCWRadioButton_Check(object sender, RoutedEventArgs e)
        {
            MsgVM.TurnDir = TurningDirection.CCW;
            MsgVM.TurningRadius = MsgVM.TurningRadius;
        }

        /// <summary>
        /// Button click event handler for setting the turning direction to clockwise.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CWRadioButton_Check(object sender, RoutedEventArgs e)
        {
            MsgVM.TurnDir = TurningDirection.CW;
            MsgVM.TurningRadius = MsgVM.TurningRadius;
        }

        /// <summary>
        /// Button click event handler for refershing the plots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PlotVM.UpdatePositionPlot(RobotVM);
            PlotVM.UpdateOrientationPlot(RobotVM);
        }
    }
}
