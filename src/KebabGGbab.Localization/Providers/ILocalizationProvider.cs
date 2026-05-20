using System.Diagnostics.CodeAnalysis;

namespace KebabGGbab.Localization.Providers
{
    public interface ILocalizationProvider
    {
        IReadOnlyList<CultureInfo> Cultures { get; }

        bool TryLocalize(string key, [NotNullWhen(true)] out object? result);
    }
}
