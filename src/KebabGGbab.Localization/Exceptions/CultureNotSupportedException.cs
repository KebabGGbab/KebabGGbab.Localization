using System.Text;

namespace KebabGGbab.Localization.Exceptions
{
    public class CultureNotSupportedException : Exception
    {
        private static readonly CompositeFormat _cultureNotSupportedExceptionMessage = CompositeFormat.Parse(ExceptionMessages.ResourceNotSupportedExceptionMessage);

        public CultureInfo? Culture { get; }

        public CultureNotSupportedException()
        {
        }

        public CultureNotSupportedException(CultureInfo? culture)
            : this(culture, null, null)
        {
        }

        public CultureNotSupportedException(string? message) 
            : base(message)
        {
        }

        public CultureNotSupportedException(CultureInfo? culture, string? message)
            : this(culture, message, null)
        {
        }

        public CultureNotSupportedException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }

        public CultureNotSupportedException(CultureInfo? culture, Exception? innerException)
            : this(culture, null, innerException) 
        {
        }

        public CultureNotSupportedException(CultureInfo? culture, string? message, Exception? innerException)
            : base(message ?? string.Format(CultureInfo.InvariantCulture, _cultureNotSupportedExceptionMessage, culture?.Name), innerException)
        {
            Culture = culture;
        }
    }
}
