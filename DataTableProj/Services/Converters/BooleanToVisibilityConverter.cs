// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Converters
{
    using System;
    using Serilog;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converts a boolean value to a Visibility value (Visible or Collapsed).
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Method for Convertion a boolean value to a Visibility value.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the target property (Visibility).</param>
        /// <param name="parameter">An optional parameter used for custom conversion (not used in this converter).</param>
        /// <param name="language">The language for which the conversion is applied (not used in this converter).</param>
        /// <returns>Visible if the value is true; Collapsed if the value is false.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Log.Information("Converting value: {value}", value);

            var result = Visibility.Collapsed;

            var visibility = (bool)value;

            var stringParameter = (string)parameter;

            if (stringParameter.Equals("Inverse"))
            {
                visibility = !visibility;
            }

            if (visibility)
            {
                result = Visibility.Visible;
            }

            Log.Information("Visibility for return: {result}", result);

            return result;
        }

        /// <summary>
        /// Method for Convertion a Visibility value back to a boolean value (not supported in this converter).
        /// </summary>
        /// <param name="value">The Visibility value to convert back.</param>
        /// <param name="targetType">The type to convert back to (boolean).</param>
        /// <param name="parameter">An optional parameter used for custom conversion (not used in this converter).</param>
        /// <param name="language">The language for which the conversion is applied (not used in this converter).</param>
        /// <returns>NotImplementedException is thrown since ConvertBack is not supported.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("ConvertBack method is not supported in BooleanToVisibilityConverter.");
        }
    }
}