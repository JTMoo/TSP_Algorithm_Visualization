using MEM_TSP.Kernel.Model.Extensions;
using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;


namespace MEM_TSP.Gui.View.Converter
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Converter der alle Descriptions eines Enum ausgibt.
    /// </summary>
    /// ****************************************************************************************************************
    [ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
    public class EnumToCollectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EnumExtension.GetAllValuesAndDescriptions(value.GetType());
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
