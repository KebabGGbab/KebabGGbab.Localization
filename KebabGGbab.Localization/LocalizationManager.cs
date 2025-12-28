using System.Globalization;
using System.Text;
using KebabGGbab.Localization.Abstractions;
using KebabGGbab.Localization.Resources;

namespace KebabGGbab.Localization
{
    public sealed class LocalizationManager : ILocalizationManager
    {
        private static readonly CompositeFormat _localizationKeyNotFound = CompositeFormat.Parse(Strings.LocalizationKeyNotFound);

        private static LocalizationManager? _instance;

        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private readonly HashSet<ILocalizationProvider> _providers = [];

        public CultureInfo CurrentUICulture
        {
            get => CultureInfo.CurrentUICulture;
            set
            {
                if (CurrentUICulture == value)
                {
                    return;
                }

                CurrentUICultureChangedEventArgs args = new(value, CurrentUICulture);
                CultureInfo.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
                OnCurrentUICultureChanged(args);
            }
        }
        public IReadOnlyList<CultureInfo> Cultures
        {
            get => _providers.SelectMany(p => p.Cultures).ToList();
        }


        public event EventHandler<CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;

        private LocalizationManager() { }

        public object Localize(string key)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            foreach (ILocalizationProvider provider in _providers)
            {
                if (provider.TryLocalize(key, out object? result))
                {
                    return result!;
                }
            }

            throw new ResourceNotFoundException(key, string.Format(CultureInfo.CurrentCulture, _localizationKeyNotFound, key));
        }

        public bool AddProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            return _providers.Add(provider);
        }

        public bool RemoveProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            return _providers.Remove(provider);
        }

        private void OnCurrentUICultureChanged(CurrentUICultureChangedEventArgs args)
        {
            CurrentUICultureChanged?.Invoke(this, args);
        }
    }
}
