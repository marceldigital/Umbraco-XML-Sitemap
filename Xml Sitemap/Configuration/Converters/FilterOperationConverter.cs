using System;
using System.ComponentModel;
using System.Globalization;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Enums;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Converters {
    /// <summary>
    ///     Converts the string represetnation of an operation to the
    ///     enum version
    /// </summary>
    internal class FilterOperationConverter : TypeConverter {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            if (value is string) {
                return (FilterOperator) Enum.Parse(typeof(FilterOperator), (string) value, true);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType) {
            return destinationType == typeof(string)
                ? ((FilterOperator) value).ToString()
                : base.ConvertTo(context, culture, value, destinationType);
        }
    }
}