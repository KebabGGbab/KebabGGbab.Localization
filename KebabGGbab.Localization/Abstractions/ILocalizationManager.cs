using System.Globalization;

namespace KebabGGbab.Localization.Abstractions
{
    public interface ILocalizationManager : ILocalizationProvider
    {
        CultureInfo CurrentUICulture { get; set; }

        event EventHandler<CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;
    }
}
