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
using ControlApp.Utils;
using ControlApp.Model;

namespace ControlApp
{
    /// <summary>
    /// Main page.
    /// <remark>
    /// - Contains a frame for displaying different pages for each simulation type. 
    /// - Displays buttons for navigation between simulation pages.
    /// </remark>
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Constructor of the MainPage.
        /// Loads BlankPage immediately.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            DisplayFrame.Navigate(typeof(BlankPage));
        }

        /// <summary>
        /// Button click event handler for navigating to the page of the Circling simulation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCirc_Click(object sender, RoutedEventArgs e)
        {
            DisplayFrame.Navigate(typeof(CircPage));
        }
        /// <summary>
        /// Button click event handler for navigating to the page of the Sensor test simulation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSens_Click(object sender, RoutedEventArgs e)
        {
            DisplayFrame.Navigate(typeof(SensPage));
        }
        /// <summary>
        /// Button click event handler for navigating to the page of the simulation Logs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            DisplayFrame.Navigate(typeof(LogPage));
        }
        /// <summary>
        /// Button click event handler for navigating to the Blank, help page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            DisplayFrame.Navigate(typeof(BlankPage));
        }
    }
}
