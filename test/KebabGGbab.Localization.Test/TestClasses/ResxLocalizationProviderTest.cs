using System.Globalization;
using System.Resources;
using KebabGGbab.Localization.Exceptions;
using KebabGGbab.Localization.Providers;
using KebabGGbab.Localization.Test.Data;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class ResxLocalizationProviderTest
    {
        [TestMethod]
        public void Ctor_ResourceManagerIsNull_Throw()
        {
            ResourceManager manager = null!;
            List<CultureInfo> cultures = [CultureInfo.CurrentCulture, CultureInfo.GetCultureInfo("fr-FR")];

            void action() => _ = new ResxLocalizationProvider(manager, cultures);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Ctor_SupportedCulturesIsNull_Throw()
        {
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = null!;

            void action() => _ = new ResxLocalizationProvider(manager, cultures);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Ctor_SupportedCulturesIsEmpty_NoThrow()
        {
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [];

            _ = new ResxLocalizationProvider(manager, cultures);
        }

        [TestMethod]
        public void SupportedCultures_ChangeSource_NotChangedSupportedCultures()
        {
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [];
            CultureInfo lazyAddedCulture = CultureInfo.GetCultureInfo("fr-FR");
            ResxLocalizationProvider provider = new(manager, cultures);

            cultures.Add(lazyAddedCulture);

            Assert.IsEmpty(provider.SupportedCultures);
        }

        [TestMethod]
        public void SupportedCultures_CastingToList_Throw()
        {
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [];
            ResxLocalizationProvider provider = new(manager, cultures);

            void action() => _ = (List<CultureInfo>)provider.SupportedCultures;

            Assert.ThrowsExactly<InvalidCastException>(action);
        }

        [TestMethod]
        public void TryLocalize_KeyIsNull_Throw()
        {
            string key = null!;
            CultureInfo culture = CultureInfo.CurrentCulture;
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [culture, CultureInfo.GetCultureInfo("fr-FR")];
            ResxLocalizationProvider provider = new(manager, cultures);

            void action() => provider.TryLocalize(key, culture, out object? result);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void TryLocalize_CultureIsNull_Throw()
        {
            string key = "FirstKey";
            CultureInfo culture = null!;
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [CultureInfo.CurrentCulture, CultureInfo.GetCultureInfo("fr-FR")];
            ResxLocalizationProvider provider = new(manager, cultures);

            void action() => provider.TryLocalize(key, culture, out object? result);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void TryLocalize_CultureIsNotSupported_Throw()
        {
            string key = "FirstKey";
            CultureInfo culture = CultureInfo.GetCultureInfo("de-DE");
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [CultureInfo.CurrentCulture, CultureInfo.GetCultureInfo("fr-FR")];
            ResxLocalizationProvider provider = new(manager, cultures);

            void action() => provider.TryLocalize(key, culture, out object? result);

            Assert.ThrowsExactly<CultureNotSupportedException>(action);
        }

        [TestMethod]
        public void TryLocalize_KeyExist_TrueAndValue()
        {
            string key = "FirstKey";
            CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [culture, CultureInfo.GetCultureInfo("fr-FR")];
            ResxLocalizationProvider provider = new(manager, cultures);

            bool isSuccess = provider.TryLocalize(key, culture, out object? result);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual("FirstValue", result);
        }

        [TestMethod]
        public void TryLocalize_KeyNotExist_FalseAndNull()
        {
            string key = "SecondKey";
            CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
            ResourceManager manager = ResxData.ResourceManager;
            List<CultureInfo> cultures = [culture, CultureInfo.GetCultureInfo("fr-FR")];
            ResxLocalizationProvider provider = new(manager, cultures);

            bool isSuccess = provider.TryLocalize(key, culture, out object? result);

            Assert.IsFalse(isSuccess);
            Assert.IsNull(result);
        }
    }
}
