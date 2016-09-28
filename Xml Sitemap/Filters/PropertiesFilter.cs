using System;
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Elements;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Enums;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters based on the properties in the configuration
    /// </summary>
    public class PropertiesFilter : IFilter {
        /// <summary>
        ///     This properties from the configuration
        /// </summary>
        private readonly IList<PropertyElement> _properties;

        public PropertiesFilter(IList<PropertyElement> properties) {
            _properties = properties;
        }

        public IEnumerable<IPublishedContent> Filter(IEnumerable<IPublishedContent> content) {
            return _properties.Aggregate(content,
                (current, property) =>
                    current.Where(c => IsRequiredPropertyPresent(property.Required, property.Alias, c))
                           .Where(c => OperatorEvaluatesTrue(property.Operator, property.Value, property.Alias, c))
                           .ToList());
        }

        /// <summary>
        ///     Checks if the requried property is present
        /// </summary>
        /// <param name="isRequired">If the property is required or not</param>
        /// <param name="alias">The alias of the property</param>
        /// <param name="content">The current piece of content</param>
        /// <returns></returns>
        private static bool IsRequiredPropertyPresent(bool isRequired, string alias, IPublishedContent content)
            => !isRequired || content.HasProperty(alias);

        /// <summary>
        ///     Evaluates the property to the value based on the operator
        /// </summary>
        /// <param name="op">The operation to perform</param>
        /// <param name="value">The value to check the property against</param>
        /// <param name="alias">The alias of the property</param>
        /// <param name="content">The current piece of content</param>
        /// <returns></returns>
        private bool OperatorEvaluatesTrue(FilterOperator op, string value, string alias, IPublishedContent content) {
            if (!content.HasValue(alias)) return true;
            bool result;
            var contentValue = content.GetPropertyValue<string>(alias);
            switch (op) {
                case FilterOperator.Equals:
                    result = value.Equals(contentValue, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case FilterOperator.Unequals:
                    result = !value.Equals(contentValue, StringComparison.InvariantCultureIgnoreCase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }

            return result;
        }
    }
}