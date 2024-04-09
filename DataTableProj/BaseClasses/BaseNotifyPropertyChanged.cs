// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.BaseClasses
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base class implementing INotifyPropertyChanged.
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method for notifying property change to event.
        /// </summary>
        /// <param name="propertyName">Property Name.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}