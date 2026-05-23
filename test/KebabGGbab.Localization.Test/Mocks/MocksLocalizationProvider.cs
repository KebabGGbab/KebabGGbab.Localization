using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using KebabGGbab.Localization.Providers;

namespace KebabGGbab.Localization.Test.Mocks
{
    internal sealed class MockLocalizationProvider : ILocalizationProvider
    {
        private readonly Row[] _data;

        public IReadOnlyList<CultureInfo> SupportedCultures { get; }

        public MockLocalizationProvider()
        {
            SupportedCultures = [CultureInfo.GetCultureInfo("ru-RU"), CultureInfo.GetCultureInfo("en-US")];
            _data = [
                new Row("1", "One", "en-US"), 
                new Row("1", "Один", "ru-RU"), 
                new Row("Name", "Name", "en-US"), 
                new Row("Name", "Имя", "ru-RU")];
        }

        public bool TryLocalize(string key, CultureInfo culture, [NotNullWhen(true)] out object? result)
        {
            result = _data.FirstOrDefault(c => c.Key == key && c.CultureName == culture.Name)?.Value;

            return result != null;
        }

        private class Row
        {
            public string Key { get; }

            public string? Value { get; }

            public string CultureName { get; }

            public Row(string key,  string value, string cultureName)
            {
                Key = key;
                Value = value;
                CultureName = cultureName;
            }
        }
    }

    internal sealed class NameLocalizationProvider : ILocalizationProvider
    {
        public IReadOnlyList<CultureInfo> SupportedCultures => [CultureInfo.GetCultureInfo("ru-RU")];

        public bool TryLocalize(string key, CultureInfo culture, [NotNullWhen(true)] out object? result)
        {
            if (key == "Name" && culture == CultureInfo.GetCultureInfo("ru-RU"))
            {
                result = "Моё имя";

                return true;
            }

            result = null;

            return false;
        }
    }
}