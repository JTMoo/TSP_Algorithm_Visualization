using System;
using System.Globalization;
using System.Windows.Data;


namespace MEM_TSP.Gui.View.Converter
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Converter der Wert mit Null vergleicht
    /// </summary>
    /// ****************************************************************************************************************
    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
