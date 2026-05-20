using System.Globalization;
using Localization.Abstractions;

namespace KebabGGbab.Localization.Abstractions
{
    public interface ILocalizationManagerBuilder
    {
        ILocalizationManagerBuilder AddProvider(ILocalizationProvider provider);

        ILocalizationManagerBuilder SetUICulture(CultureInfo culture);

        ILocalizationManager Build();
    }
}
