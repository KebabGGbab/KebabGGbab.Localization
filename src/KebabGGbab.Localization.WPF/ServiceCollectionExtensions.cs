using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace KebabGGbab.Localization.WPF
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalizationWPF(this IServiceCollection services, CultureInfo? startCulture = null)
        {
            return services.AddLocalization(startCulture, new DispatcherCultureService());
        }
    }
}
