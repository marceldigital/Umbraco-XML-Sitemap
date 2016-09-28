using System;
using System.ComponentModel;
using System.Globalization;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Converters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace XmlSitemap.Test.Configuration.Converters {
    [TestClass]
    public class FilterOperatorConverterTest {
        private const string ValidOpertaor = "equals";
        private const string InvalidOperator = "random";
        private const int InvalidType = 1;
        private FilterOperationConverter _sut;
        private ITypeDescriptorContext _typeDescriptorContext;

        [TestInitialize]
        public void Setup() {
            _sut = new FilterOperationConverter();
            _typeDescriptorContext = new Mock<ITypeDescriptorContext>().Object;
        }

        [TestCleanup]
        public void Teardown() {
            _sut = null;
        }

        [TestMethod]
        public void TestValidTypeCheck() {
            Assert.IsTrue(_sut.CanConvertFrom(_typeDescriptorContext, typeof(string)));
        }

        [TestMethod]
        public void TestInvalidTypeCheck() {
            Assert.IsFalse(_sut.CanConvertFrom(_typeDescriptorContext, typeof(int)));
        }

        [TestMethod]
        public void TestConvertValidType() {
            var result =
                (FilterOperator) _sut.ConvertFrom(_typeDescriptorContext, CultureInfo.CurrentCulture, ValidOpertaor);
            Assert.AreEqual(FilterOperator.Equals, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestConvertInvalidType() {
            _sut.ConvertFrom(_typeDescriptorContext, CultureInfo.CurrentCulture, InvalidType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConvertInvalidOperator() {
            _sut.ConvertFrom(_typeDescriptorContext, CultureInfo.CurrentCulture, InvalidOperator);
        }

        [TestMethod]
        public void TestValidConvertTo() {
            var result =
                (string) _sut.ConvertTo(_typeDescriptorContext, CultureInfo.CurrentCulture, FilterOperator.Equals,
                    typeof(string));
            Assert.AreEqual(ValidOpertaor, result.ToLowerInvariant());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestInvalidConvertTo() {
            _sut.ConvertTo(_typeDescriptorContext, CultureInfo.CurrentCulture, FilterOperator.Equals,
                typeof(int));
        }
    }
}