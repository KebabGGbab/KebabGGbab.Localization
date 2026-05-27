using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KebabGGbab.Localization.Manager;

namespace KebabGGbab.Localization.Samples.Shared.ViewModels
{
    public sealed class MainViewModel : ObservableObject
    {
        private readonly ILocalizationManager _localization;

        private CultureInfo _selectedUICulture;

        public IReadOnlyList<CultureInfo> SupportedUICultures { get; }

        public CultureInfo CurrentUICulture
        {
            get => _localization.CurrentUICulture;
            private set => SetProperty(_localization.CurrentUICulture, value, _localization, (m, v) => m.CurrentUICulture = v);
        }

        public CultureInfo SelectedUICulture
        {
            get => _selectedUICulture;
            set
            {
                if (SetProperty(ref _selectedUICulture, value))
                {
                    ChangeUICultureCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand<CultureInfo> ChangeUICultureCommand { get; }

        public MainViewModel(ILocalizationManager localization) 
        {
            ArgumentNullException.ThrowIfNull(localization);

            _localization = localization;
            SupportedUICultures = _localization.Cultures;
            _selectedUICulture = CurrentUICulture;
            ChangeUICultureCommand = new RelayCommand<CultureInfo>(ChangeUICulture, CanChangeUICulture);
        }

        private void ChangeUICulture(CultureInfo? newCulture)
        {
            if (CanChangeUICulture(newCulture) == false)
            {
                return;
            }

            CurrentUICulture = newCulture;
        }

        private static bool CanChangeUICulture([NotNullWhen(true)] CultureInfo? newCulture) => newCulture != null;

    }
}
