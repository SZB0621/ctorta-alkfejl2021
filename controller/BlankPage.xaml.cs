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


namespace ControlApp
{
    /// <summary>
    /// Blank help page.
    /// Contains a short description of the project.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        /// <summary>
        /// Constructor of the BlankPage.
        /// Displays a greeting and a short description.
        /// </summary>
        public BlankPage()
        {
            this.InitializeComponent();

            textBlockGreet.Text =
                "Circling: \r\n" +
                " - runs a testing sequence where the robot makes a full circle \r\n" +
                " - parameters: \r\n" +
                "    + desired velocity \r\n" +
                "    + turning radius \r\n" +
                "    + turning direction \r\n" +
                " - plots the robot's position and orientation \r\n" +
                "SensorTest: \r\n" +
                " - runs a test for the ultrasonic sensors \r\n" +
                " - parameters: \r\n" +
                "    + desired velocity \r\n" +
                "    + stopping distance from obstacle \r\n" +
                " - plots the robot's sensor values \r\n" +
                "Logs: \r\n" +
                " - page for logs";
        }
    }
}
