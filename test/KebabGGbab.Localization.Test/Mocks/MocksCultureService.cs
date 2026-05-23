using System.Globalization;
using KebabGGbab.Localization.CultureService;

namespace KebabGGbab.Localization.Test.Mocks
{
    internal sealed class MockCultureService : ICultureService
    {
        public CultureInfo CurrentUICulture { get; private set; } = CultureInfo.GetCultureInfo("en-US");

        public bool ChangeCurrentUICulture(CultureInfo newCulture)
        {
            CurrentUICulture = newCulture;

            return true;
        }
    }
}
