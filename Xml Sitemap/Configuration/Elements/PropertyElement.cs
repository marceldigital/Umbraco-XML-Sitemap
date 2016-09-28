using System.ComponentModel;
using System.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Converters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Enums;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Elements {
    /// <summary>
    ///     Configuration element that represents properties that should be filtered on.
    /// </summary>
    public class PropertyElement : ConfigurationElement {
        private const string AliasKey = "alias";
        private const string ValueKey = "value";
        private const string OperatorKey = "operator";
        private const string RequiredKey = "required";

        /// <summary>
        ///     The alias of the property to filter on
        /// </summary>
        [ConfigurationProperty(AliasKey, IsRequired = true, IsKey = true)]
        public string Alias {
            get { return this[AliasKey] as string; }
            set { this[AliasKey] = value; }
        }

        /// <summary>
        ///     The value to check the properties value against
        /// </summary>
        [ConfigurationProperty(ValueKey, IsRequired = true)]
        public string Value {
            get { return this[ValueKey] as string; }
            set { this[ValueKey] = value; }
        }

        /// <summary>
        ///     The operator type to check the values against
        ///     [equal (Default), unequal]
        /// </summary>
        [TypeConverter(typeof(FilterOperationConverter))]
        [ConfigurationProperty(OperatorKey, IsRequired = false, DefaultValue = "equals")]
        public FilterOperator Operator {
            get { return (FilterOperator) this[OperatorKey]; }
            set { this[OperatorKey] = value; }
        }

        /// <summary>
        ///     If the node does not have the property, automatically remove
        ///     [Default: false]
        /// </summary>
        [ConfigurationProperty(RequiredKey, IsRequired = false, DefaultValue = false)]
        public bool Required {
            get { return (bool) this[RequiredKey]; }
            set { this[RequiredKey] = value; }
        }
    }
}