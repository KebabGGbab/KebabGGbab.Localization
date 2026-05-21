using System.Diagnostics.CodeAnalysis;

namespace KebabGGbab.Localization.Providers
{
    public interface ILocalizationProvider
    {
        IReadOnlyList<CultureInfo> SupportedCultures { get; }

        bool TryLocalize(string key, CultureInfo culture, [NotNullWhen(true)] out object? result);
    }
}
