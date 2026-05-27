using System.Globalization;
using KebabGGbab.Localization.Samples.WPF.Resources;
using KebabGGbab.Localization.WPF;
using Microsoft.Extensions.DependencyInjection;

namespace KebabGGbab.Localization.Samples.WPF.Service.Localization
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationWithResx(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddLocalizationWPF();
            services.AddResxLocalization(StringsUI.ResourceManager, [CultureInfo.GetCultureInfo("ru-RU"), CultureInfo.GetCultureInfo("en-US")]);

            return services;
        }
    }
}
