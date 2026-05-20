namespace KebabGGbab.Localization
{
    public class ResourceNotFoundException : Exception
    {
        public string? Key { get; set; }

        public ResourceNotFoundException() : base() { }

        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException(string key,  string message) : base(message) 
        { 
            Key = key;
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public ResourceNotFoundException (string message, string key, Exception innerException) : base (message, innerException)
        {
            Key = key;
        }
    }
}
