using System.Globalization;
using KebabGGbab.Localization.Exceptions;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class CultureNotSupportedExceptionTest
    {
        [TestMethod]
        public void Ctor_WithoutArgument_CultureIsNull()
        {
            CultureNotSupportedException ex = new();

            Assert.IsNull(ex.Culture);
        }

        [TestMethod]
        public void Ctor_WithCulture_ParameterSavedInProperty()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");

            CultureNotSupportedException ex = new(culture);

            Assert.AreEqual(culture, ex.Culture);
        }

        [TestMethod]
        public void Ctor_WithCultureIsNull_ParameterSavedInProperty()
        {
            CultureInfo? culture = null;

            CultureNotSupportedException ex = new(culture);

            Assert.IsNull(ex.Culture);
        }

        [TestMethod]
        public void Ctor_WithMessage_ParameterSavedInProperty()
        {
            string message = "exception message";

            CultureNotSupportedException ex = new(message);

            Assert.IsNull(ex.Culture);
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void Ctor_WithCultureAndMessage_ParametersSavedInProperties()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");
            string message = "exception message";

            CultureNotSupportedException ex = new(culture, message);

            Assert.AreEqual(culture, ex.Culture);
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void Ctor_WithMessageAndInnerException_ParametersSavedInProperties()
        {
            string message = "exception message";
            Exception innerException = new();

            CultureNotSupportedException ex = new(message, innerException);

            Assert.IsNull(ex.Culture);
            Assert.AreEqual(message, ex.Message);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void Ctor_WithCultureAndInnerException_ParametersSavedInProperties()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");
            Exception innerException = new();

            CultureNotSupportedException ex = new(culture, innerException);

            Assert.AreEqual(culture, ex.Culture);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void Ctor_WithCultureAndMessageAndInnerException_ParametersSavedInProperties()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");
            string message = "exception message";
            Exception innerException = new();

            CultureNotSupportedException ex = new(culture, message, innerException);

            Assert.AreEqual(culture, ex.Culture);
            Assert.AreEqual(message, ex.Message);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void Ctor_WithCultureAndMessageAreNullAndInnerException_ParametersSavedInProperties()
        {
            CultureInfo? culture = null;
            string? message = null;
            Exception innerException = new();

            CultureNotSupportedException ex = new(culture, message, innerException);

            Assert.AreEqual(culture, ex.Culture);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void ThrowIfCultureNotSupported_IsSupported_NotThrow()
        {
            bool isSupported = true;

            CultureNotSupportedException.ThrowIfCultureNotSupported(isSupported, CultureInfo.GetCultureInfo("en-US"));
        }

        [TestMethod]
        public void ThrowIfCultureNotSupported_IsNotSupported_Throw()
        {
            bool isSupported = false;

            void action() => CultureNotSupportedException.ThrowIfCultureNotSupported(isSupported, CultureInfo.GetCultureInfo("en-US"));

            Assert.ThrowsExactly<CultureNotSupportedException>(action);
        }
    }
}
