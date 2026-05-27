using System.Globalization;
using KebabGGbab.Localization.Samples.WPF.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace KebabGGbab.Localization.Samples.WPF.Extensions
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationWithResx(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddLocalization();
            services.AddResxLocalization(StringsUI.ResourceManager, [CultureInfo.GetCultureInfo("ru-RU"), CultureInfo.GetCultureInfo("en-US")]);

            return services;
        }
    }
}
