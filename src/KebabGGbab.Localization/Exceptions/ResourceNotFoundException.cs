using System.Text;

namespace KebabGGbab.Localization.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        private static readonly CompositeFormat _localizationKeyNotFound = CompositeFormat.Parse(ExceptionMessages.ResourceNotFoundExceptionMessage);

        public string? Key { get; }

        public ResourceNotFoundException()
        { 
        }

        public ResourceNotFoundException(string? message)
            : base(message) 
        { 
        }

        public ResourceNotFoundException(string? key,  string? message) 
            : this(key, message, null) 
        {
        }

        public ResourceNotFoundException(string? message, Exception? innerException) 
            : base(message, innerException) 
        { 
        }

        public ResourceNotFoundException (string? key, string? message, Exception? innerException)
            : base (message ?? string.Format(CultureInfo.InvariantCulture, _localizationKeyNotFound, key), innerException)
        {
            Key = key;
        }
    }
}
