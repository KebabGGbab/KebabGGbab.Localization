using System.Resources;
using KebabGGbab.Localization.CultureService;
using KebabGGbab.Localization.Manager;
using KebabGGbab.Localization.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KebabGGbab.Localization
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services,
            CultureInfo? currentUICulture = null, ICultureService? cultureService = null)
        {
            ArgumentNullException.ThrowIfNull(services);

            LocalizationManager manager = LocalizationManager.Instance;
            services.TryAddSingleton<ILocalizationManager>((s) =>
            {
                return manager;
            });

            if (cultureService != null)
            {
                manager.CultureService = cultureService;
            }

            if (currentUICulture != null)
            {
                manager.CurrentUICulture = currentUICulture;
            }


            return services;
        }

        public static IServiceCollection AddResxLocalization(this IServiceCollection services, 
            ResourceManager resourceManager, IEnumerable<CultureInfo> supportedCultures)
        {
            ArgumentNullException.ThrowIfNull(services);

            LocalizationManager manager = LocalizationManager.Instance;
            services.TryAddSingleton<ILocalizationManager>((s) =>
            {
                return manager;
            });
            ResxLocalizationProvider provider = new(resourceManager, supportedCultures);
            manager.AddProvider(provider);

            return services;
        }
    }
}
