using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace KebabGGbab.Localization.Abstractions
{
    public interface ILocalizationProvider
    {
        IReadOnlyList<CultureInfo> Cultures { get; }

        bool TryLocalize(string key, [NotNullWhen(true)] out object? result);
    }
}
