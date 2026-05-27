using System.Globalization;
using System.Windows.Threading;
using KebabGGbab.Localization.CultureService;

namespace KebabGGbab.Localization.WPF
{
    public sealed class DispatcherCultureService : ICultureService
    {
        public CultureInfo CurrentUICulture
        {
            get => Dispatcher.CurrentDispatcher.Thread.CurrentUICulture;
            private set => Dispatcher.CurrentDispatcher.Thread.CurrentUICulture = value;
        }

        public bool ChangeCurrentUICulture(CultureInfo newCulture)
        {
            if (CurrentUICulture == newCulture)
            {
                return false;
            }

            CurrentUICulture = newCulture;

            return true;
        }
    }
}
