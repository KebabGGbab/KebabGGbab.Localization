namespace KebabGGbab.Localization.Exceptions
{
    public class CultureNotSupportedException : Exception
    {
        public CultureInfo? Culture { get; }

        public CultureNotSupportedException()
        {
        }

        public CultureNotSupportedException(CultureInfo? culture)
        {
            Culture = culture;
        }

        public CultureNotSupportedException(string? message) 
            : base(message)
        {
        }

        public CultureNotSupportedException(CultureInfo? culture, string? message)
            : base(message)
        {
            Culture = culture;
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
