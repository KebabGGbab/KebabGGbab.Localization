using KebabGGbab.Localization.Providers;

namespace KebabGGbab.Localization.Manager
{
    public interface ILocalizationManagerBuilder
    {
        ILocalizationManagerBuilder AddProvider(ILocalizationProvider provider);

        ILocalizationManagerBuilder SetUICulture(CultureInfo culture);

        ILocalizationManager Build();
    }
}
