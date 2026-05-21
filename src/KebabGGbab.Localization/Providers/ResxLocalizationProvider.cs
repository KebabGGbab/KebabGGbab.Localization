using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.Text;

namespace KebabGGbab.Localization.Providers
{
    public class ResxLocalizationProvider : ILocalizationProvider
    {
        private static readonly CompositeFormat _cultureNotSupportedExceptionMessage = CompositeFormat.Parse(ExceptionMessages.ResourceNotSupportedExceptionMessage);

        private readonly ResourceManager _resourceManager;

        public IReadOnlyList<CultureInfo> SupportedCultures { get; }

        public ResxLocalizationProvider(ResourceManager resourceManager, IEnumerable<CultureInfo> supportedCultures)
        {
            ArgumentNullException.ThrowIfNull(resourceManager);
            ArgumentNullException.ThrowIfNull(supportedCultures);

            _resourceManager = resourceManager;
            SupportedCultures = [.. supportedCultures];
        }

        public bool TryLocalize(string key, CultureInfo culture, [NotNullWhen(true)] out object? result)
        {
            ArgumentNullException.ThrowIfNull(culture);
            if (!SupportedCultures.Contains(culture))
            {
                throw new CultureNotSupportedException(culture, string.Format(CultureInfo.InvariantCulture, _cultureNotSupportedExceptionMessage, culture.Name));
            }

            result = _resourceManager.GetObject(key, culture);

            return result != null;
        }
    }
}
