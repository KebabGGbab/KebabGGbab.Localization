using System.Globalization;
using KebabGGbab.Localization.Abstractions;

namespace KebabGGbab.Localization
{
    public sealed class LocalizationManager : ILocalizationManager
    {
        private static LocalizationManager? _instance;

        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private readonly HashSet<ILocalizationProvider> _providers = [];

        public CultureInfo CurrentUICulture
        {
            get => Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (CurrentUICulture == value)
                {
                    return;
                }

                Thread.CurrentThread.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
                OnCurrentUICultureChanged();
            }
        }
        public IReadOnlyList<CultureInfo> Cultures
        {
            get => _providers.SelectMany(p => p.Cultures).ToList();
        }


        public event EventHandler? CurrentUICultureChanged;

        private LocalizationManager() { }

        public bool TryLocalize(string key, out object? result)
        {
            foreach (ILocalizationProvider provider in _providers)
            {
                if (provider.TryLocalize(key, out result))
                {
                    return true;
                }
            }

            result = default;

            return false;
        }

        public void AddProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            _providers.Add(provider);
        }

        private void OnCurrentUICultureChanged()
        {
            CurrentUICultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
