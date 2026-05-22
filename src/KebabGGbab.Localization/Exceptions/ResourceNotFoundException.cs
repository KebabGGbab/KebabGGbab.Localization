namespace KebabGGbab.Localization.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
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
            : base (message, innerException)
        {
            Key = key;
        }
    }
}
