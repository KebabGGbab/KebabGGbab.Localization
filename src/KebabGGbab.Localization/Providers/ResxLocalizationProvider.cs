using System.Diagnostics.CodeAnalysis;
using System.Resources;

namespace KebabGGbab.Localization.Providers
{
    public class ResxLocalizationProvider : ILocalizationProvider
    {
        private readonly ResourceManager _resourceManager;

        public IReadOnlyList<CultureInfo> Cultures { get; }

        public ResxLocalizationProvider(ResourceManager resourceManager, IEnumerable<CultureInfo> cultures)
        {
            ArgumentNullException.ThrowIfNull(resourceManager);
            ArgumentNullException.ThrowIfNull(cultures);

            _resourceManager = resourceManager;
            Cultures = [.. cultures];
        }

        public bool TryLocalize(string key, [NotNullWhen(true)] out object? result)
        {
            result = _resourceManager.GetObject(key, CultureInfo.CurrentUICulture);

            return result != null;
        }
    }
}
