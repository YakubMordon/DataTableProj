// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Extensions
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Class, which include extension methods for <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public static class NotifyPropertyChangedExtensions
    {
        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="instance">The object implementing the <see cref="INotifyPropertyChanged"/> interface.</param>
        /// <param name="eventHandler">The event handler for the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.</param>
        /// <param name="propertyName">The name of the property that changed. This value is optional and can be automatically provided by the compiler.</param>
        public static void NotifyPropertyChanged(this INotifyPropertyChanged instance, PropertyChangedEventHandler eventHandler, [CallerMemberName] string propertyName = null)
        {
            if (eventHandler != null)
            {
                eventHandler(instance, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}