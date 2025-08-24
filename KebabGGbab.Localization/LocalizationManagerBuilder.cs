using System.Globalization;
using KebabGGbab.Localization.Abstractions;

namespace KebabGGbab.Localization
{
    public sealed class LocalizationManagerBuilder : ILocalizationManagerBuilder
    {
        private readonly List<ILocalizationProvider> _providers = [];

        private CultureInfo _culture = Thread.CurrentThread.CurrentUICulture;

        public ILocalizationManagerBuilder AddProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            _providers.Add(provider);

            return this;
        }

        public ILocalizationManagerBuilder SetUICulture(CultureInfo culture)
        {
            ArgumentNullException.ThrowIfNull(culture, nameof(culture));

            _culture = culture;

            return this;
        }

        public ILocalizationManager Build()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            _providers.ForEach(manager.AddProvider);
            manager.CurrentUICulture = _culture;

            return manager;
        }
    }
}
