using KebabGGbab.Localization.CultureService;
using KebabGGbab.Localization.Providers;

namespace KebabGGbab.Localization.Manager
{
    public sealed class LocalizationManager : ILocalizationManager
    {
        public static LocalizationManager Instance { get; } = new();

        private readonly List<ILocalizationProvider> _providers;

        public ICultureService CultureService
        {
            get;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                field = value;
            }
        }

        public CultureInfo CurrentUICulture
        {
            get => CultureService.CurrentUICulture;
            set
            {
                CultureInfo oldCulture = CurrentUICulture;
                
                if (CultureService.ChangeCurrentUICulture(value))
                {
                    CurrentUICultureChangedEventArgs args = new(value, oldCulture);
                    OnCurrentUICultureChanged(args);
                }
            }
        }

        public IReadOnlyList<ILocalizationProvider> Providers => _providers.AsReadOnly();

        public IReadOnlyList<CultureInfo> Cultures => _providers.SelectMany(p => p.SupportedCultures).ToList().AsReadOnly();

        public event EventHandler<ILocalizationManager, CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;

        private LocalizationManager() 
        {
            _providers = [];
            CultureService = new ThreadCultureService();
        }

        public object Localize(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            foreach (ILocalizationProvider provider in _providers)
            {
                if (provider.TryLocalize(key, CultureService.CurrentUICulture, out object? result))
                {
                    return result;
                }
            }

            throw new ResourceNotFoundException(key, null, null);
        }

        public void AddProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            _providers.Add(provider);
        }

        public void RemoveProvider(ILocalizationProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            _providers.Remove(provider);
        }

        private void OnCurrentUICultureChanged(CurrentUICultureChangedEventArgs args)
        {
            CurrentUICultureChanged?.Invoke(this, args);
        }
    }
}
