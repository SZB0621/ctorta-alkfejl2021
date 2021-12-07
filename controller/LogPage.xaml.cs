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

namespace ControlApp
{
    /// <summary>
    /// Log page.
    /// Displays logging information.
    /// </summary>
    public sealed partial class LogPage : Page
    {
        /// <summary>
        /// LogCommand for rquesting log messages through HTTP.
        /// </summary>
        LogCommand LogCommand;
        /// <summary>
        /// LogViewModel for managing a LogModel.
        /// </summary>
        LogViewModel LogVM;

        /// <summary>
        /// Constructor of the LogPage.
        /// Instanciates the LogViewModel and the LogCommand.
        /// </summary>
        public LogPage()
        {
            this.InitializeComponent();
            LogVM = new LogViewModel(new LogModel());
            LogCommand = new LogCommand(LogVM.LogModel);

        }
    }
}
