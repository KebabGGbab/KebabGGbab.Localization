using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using KebabGGbab.Localization.Abstractions;

namespace KebabGGbab.Localization
{
    internal class ResxLocalizationProvider : ILocalizationProvider
    {
        private readonly ResourceManager _resourceManager;

        public IReadOnlyList<CultureInfo> Cultures { get; }

        public ResxLocalizationProvider(ResourceManager resourceManager, IEnumerable<CultureInfo> cultures)
        {
            _resourceManager = resourceManager;
            Cultures = [.. cultures];
        }

        public bool TryLocalize(string key, [NotNullWhen(true)] out object? result)
        {
            result = _resourceManager.GetObject(key, CultureInfo.InvariantCulture);

            return result != null;
        }
    }
}
