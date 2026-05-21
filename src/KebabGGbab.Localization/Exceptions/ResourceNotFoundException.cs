namespace KebabGGbab.Localization.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public string? Key { get; }

        public ResourceNotFoundException() 
            : base() 
        { 
        }

        public ResourceNotFoundException(string? message) 
            : base(message) 
        { 
        }

        public ResourceNotFoundException(string key,  string? message) 
            : base(message) 
        { 
            Key = key;
        }

        public ResourceNotFoundException(string? message, Exception? innerException) 
            : base(message, innerException) 
        { 
        }

        public ResourceNotFoundException (string key, string? message, Exception? innerException)
            : base (message, innerException)
        {
            Key = key;
        }
    }
}
