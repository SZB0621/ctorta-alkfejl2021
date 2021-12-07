using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Model
{
    /// <summary>
    /// LogModel class, representing a log message from the simulation environment.
    /// <remark>
    /// Inherts from ObservableObject.
    /// </remark>
    /// </summary>
    public class LogModel : ObservableObject
    {
        /// <summary>
        /// List of strings for containing the log messages.
        /// </summary>
        private List<string> logList;

        /// <summary>
        /// Constructor of the LogModel.
        /// Sets the LogList property with a placeholder text.
        /// </summary>
        public LogModel()
        {
            this.LogList = new List<string> { "[Placeholder log text]" };
        }

        /// <summary>
        /// The <c>LogList</c> represents a List of strings of the log messages.
        /// </summary>
        public List<string> LogList
        {
            get => logList;
            set
            {
                if (logList != value)
                {
                    logList = value;
                    Notify();
                }
            }
        }
    }
}
