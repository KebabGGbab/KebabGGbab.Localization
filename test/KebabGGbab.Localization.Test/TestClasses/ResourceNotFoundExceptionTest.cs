using System.Globalization;
using KebabGGbab.Localization.Exceptions;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class ResourceNotFoundExceptionTest
    {
        [TestMethod]
        public void Ctor_WithoutArguments_KeyIsNull()
        {
            ResourceNotFoundException ex = new();

            Assert.IsNull(ex.Key);
        }

        [TestMethod]
        public void Ctor_WithMessage_ParameterSavedInProperty()
        {
            string message = "exception message";

            ResourceNotFoundException ex = new(message);

            Assert.IsNull(ex.Key);
            Assert.AreEqual(message, ex.Message);
        }

        [TestMethod]
        public void Ctor_WithKeyAndMessage_ParametersSavedInProperties()
        {
            string key = "localizaion key";
            string message = "exception message";

            ResourceNotFoundException ex = new(key, message);

            Assert.AreEqual(key, ex.Key);
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
        public void Ctor_WithKeyAndMessageAndInnerException_ParametersSavedInProperties()
        {
            string key = "localizaion key";
            string message = "exception message";
            Exception innerException = new();

            ResourceNotFoundException ex = new(key, message, innerException);

            Assert.AreEqual(key, ex.Key);
            Assert.AreEqual(message, ex.Message);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void Ctor_WithKeyAndMessageAreNullAndInnerException_ParametersSavedInProperties()
        {
            string? key = null;
            string? message = null;
            Exception innerException = new();

            ResourceNotFoundException ex = new(key, message, innerException);

            Assert.AreEqual(key, ex.Key);
            Assert.AreEqual(innerException, ex.InnerException);
        }

        [TestMethod]
        public void ThrowIfResourceNotFound_IsFounded_NotThrow()
        {
            bool isFounded = true;

            ResourceNotFoundException.ThrowIfResourceNotFound(isFounded, "localizaion key");
        }

        [TestMethod]
        public void ThrowIfResourceNotFound_IsNotSupported_Throw()
        {
            bool isFounded = false;

            void action() => ResourceNotFoundException.ThrowIfResourceNotFound(isFounded, "localizaion key");

            Assert.ThrowsExactly<ResourceNotFoundException>(action);
        }
    }
}
