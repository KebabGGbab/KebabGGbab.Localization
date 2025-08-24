using System.Globalization;

namespace KebabGGbab.Localization.Abstractions
{
    public interface ILocalizationManagerBuilder
    {
        ILocalizationManagerBuilder AddProvider(ILocalizationProvider provider);

        ILocalizationManagerBuilder SetUICulture(CultureInfo culture);

        ILocalizationManager Build();
    }
}
