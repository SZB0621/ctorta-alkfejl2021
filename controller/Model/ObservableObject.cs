using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ControlApp.Model
{
    /// <summary>
    /// Auxiliary ObservableObject class for making the usage of INotifyPropoertyChange more convenient throughout the project.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// EventHandler for PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify method for invoking PorpetyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
