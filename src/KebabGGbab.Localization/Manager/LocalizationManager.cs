using KebabGGbab.Localization.CultureService;
using KebabGGbab.Localization.Providers;

namespace KebabGGbab.Localization.Manager
{
    public sealed class LocalizationManager : ILocalizationManager
    {
        private readonly List<ILocalizationProvider> _providers;
        private readonly ICultureService _cultureService;

        public CultureInfo CurrentUICulture
        {
            get => CultureInfo.CurrentUICulture;
            set
            {
                CultureInfo oldCulture = CurrentUICulture;
                
                if (_cultureService.ChangeCurrentUICulture(value))
                {
                    CurrentUICultureChangedEventArgs args = new(value, oldCulture);
                    OnCurrentUICultureChanged(args);
                }
            }
        }

        public IReadOnlyList<ILocalizationProvider> Providers => _providers.AsReadOnly();

        public IReadOnlyList<CultureInfo> Cultures => _providers.SelectMany(p => p.SupportedCultures).ToList();

        public event EventHandler<ILocalizationManager,CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;

        public LocalizationManager(CultureInfo? culture = null, ICultureService? cultureService = null) 
        {
            _providers = [];
            _cultureService = cultureService ?? new ThreadCultureService();

            if (culture != null)
            {
                CurrentUICulture = culture;
            }
        }

        public object Localize(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            foreach (ILocalizationProvider provider in _providers)
            {
                if (provider.TryLocalize(key, _cultureService.CurrentUICulture, out object? result))
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
