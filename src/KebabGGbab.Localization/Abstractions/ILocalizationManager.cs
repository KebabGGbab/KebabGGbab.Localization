using System.Globalization;
using KebabGGbab.Localization;

namespace Localization.Abstractions
{
    public interface ILocalizationManager
    {
        IReadOnlyList<CultureInfo> Cultures { get; }

        CultureInfo CurrentUICulture { get; set; }

        event EventHandler<ILocalizationManager, CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;

        object Localize(string key);
    }
}
