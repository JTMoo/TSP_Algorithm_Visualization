using MEM_TSP.Kernel.Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;


namespace MEM_TSP.Kernel.Model.Extensions
{
    /// ****************************************************************************************************************
    /// <summary>
    /// Erweiterungsklasse für ein Enum
    /// </summary>
    /// ****************************************************************************************************************
    public static class EnumExtension
    {
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Description Parameter in string parsen
        /// </summary>
		/// ------------------------------------------------------------------------------------------------------------
        /// <param name="value">Enum</param>
		/// ------------------------------------------------------------------------------------------------------------
        /// <returns>Description als string</returns>
		/// ------------------------------------------------------------------------------------------------------------
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
            {
                return (attributes.First() as DescriptionAttribute).Description;
            }

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(textInfo.ToLower(value.ToString().Replace("_", " ")));
        }

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Alle Values und Descriptions eines Enums ausgeben
        /// </summary>
		/// ------------------------------------------------------------------------------------------------------------
        /// <param name="type">Enum Typ</param>
		/// ------------------------------------------------------------------------------------------------------------
        /// <returns>Values und Descriptions als Tupel</returns>
		/// ------------------------------------------------------------------------------------------------------------
        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(type)} must be an enum type");
            }

            return Enum.GetValues(type).Cast<Enum>().Select(enumItem => new ValueDescription() { Value = enumItem, Description = enumItem.Description()}).ToList();
        }
    }
}
