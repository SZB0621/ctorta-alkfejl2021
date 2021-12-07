using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using ControlApp.Model;

namespace ControlApp.ViewModel
{
    /// <summary>
    /// ViewModel that handles LogModels for logging.
    /// <remark>
    /// Inherits from ObservableObject.
    /// </remark>
    /// </summary>
    public class LogViewModel : ObservableObject
    {
        /// <summary>
        /// LogModel which contains the requested log messages from the simulation environment.
        /// </summary>
        public LogModel LogModel;

        /// <summary>
        /// Constructor of the LogViewModel.
        /// Sets the LogModel property and subscribes for the Model's PropertyChanged event.
        /// </summary>
        /// <param name="log">LogModel to set the property of the same name.</param>
        public LogViewModel(LogModel log)
        {
            this.LogModel = log;

            LogModel.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// The <c>LogList</c> represents a string containing the log message.
        /// <remark>
        /// In order to write out the logs in a TextBlock, the string List of the log messages is converted to a simple string.
        /// </remark>
        /// </summary>
        public string LogList
        {
            get
            {
               return string.Join("\r\n", LogModel.LogList.ToArray()); 
            }
        }
 
        /// <summary>
        /// Methods for INotifyPropertyChanged forwarding.
        /// </summary>
        #region INPC forwarding
        private readonly string[] DependentPropertyNames = {
            nameof(Model.LogModel.LogList) };

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
