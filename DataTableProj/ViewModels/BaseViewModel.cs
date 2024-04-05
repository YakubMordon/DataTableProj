// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.ViewModels
{
    using System.ComponentModel;

    /// <summary>
    /// Base View Model.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method for handling property change.
        /// </summary>
        /// <param name="propertyName">Name of property, which was changed.</param>
        protected virtual void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}