using System.Diagnostics.CodeAnalysis;
using System.Resources;

namespace KebabGGbab.Localization.Providers
{
    public class ResxLocalizationProvider : ILocalizationProvider
    {
        private readonly ResourceManager _resourceManager;

        public IReadOnlyList<CultureInfo> SupportedCultures { get; }

        public ResxLocalizationProvider(ResourceManager resourceManager, IEnumerable<CultureInfo> supportedCultures)
        {
            ArgumentNullException.ThrowIfNull(resourceManager);
            ArgumentNullException.ThrowIfNull(supportedCultures);

            _resourceManager = resourceManager;
            SupportedCultures = supportedCultures.ToList().AsReadOnly();
        }

        public bool TryLocalize(string key, CultureInfo culture, [NotNullWhen(true)] out object? result)
        {
            ArgumentNullException.ThrowIfNull(culture);

            result = _resourceManager.GetObject(key, culture);

            return result != null;
        }
    }
}
