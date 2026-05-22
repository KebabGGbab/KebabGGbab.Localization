namespace KebabGGbab.Localization.Exceptions
{
    public class CultureNotSupportedException : Exception
    {
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

        public CultureNotSupportedException(CultureInfo? culture, string? message, Exception? innerException)
            : base(message, innerException)
        {
            Culture = culture;
        }
    }
}
