using System.Globalization;
using KebabGGbab.Localization.Exceptions;
using KebabGGbab.Localization.Manager;
using KebabGGbab.Localization.Providers;
using KebabGGbab.Localization.Test.Mocks;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class LocalizationManagerTest
    {
        [TestMethod]
        public void Ctor_WithoutArguments_CultureUICultureEqualsCurrentUICultureThread()
        {
            CultureInfo culture = CultureInfo.CurrentUICulture;

            LocalizationManager manager = new();

            Assert.AreEqual(culture, manager.CurrentUICulture);
        }

        [TestMethod]
        public void Ctor_WithStartCultureAndCultureService_StartCultureSetInLocalizationManagerAndCultureService()
        {
            CultureInfo startCulture = CultureInfo.GetCultureInfo("fr-FR");
            MockCultureService service = new();

            LocalizationManager manager = new(startCulture, service);

            Assert.AreEqual(startCulture, manager.CurrentUICulture);
            Assert.AreEqual(startCulture, service.CurrentUICulture);
        }

        [TestMethod]
        public void AddProvider_ProviderIsNull_Throw()
        {
            LocalizationManager manager = new();
            ILocalizationProvider provider = null!;

            void action() => manager.AddProvider(provider);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void AddProvider_AddOneProvider_AddedProvider()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider providerOne = new();

            manager.AddProvider(providerOne);

            Assert.ContainsSingle(manager.Providers);
            Assert.Contains(providerOne, manager.Providers);
        }

        [TestMethod]
        public void AddProvider_AddMultipleDifferentProviders_AddedAll()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider providerOne = new();
            NameLocalizationProvider providerTwo = new();

            manager.AddProvider(providerOne);
            manager.AddProvider(providerTwo);

            Assert.Contains(providerOne, manager.Providers);
            Assert.Contains(providerTwo, manager.Providers);
        }

        [TestMethod]
        public void AddProvider_AddMultipleSameProviders_AddedAll()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider providerOne = new();
            MockLocalizationProvider providerTwo = new();

            manager.AddProvider(providerOne);
            manager.AddProvider(providerTwo);

            Assert.Contains(providerOne, manager.Providers);
            Assert.Contains(providerTwo, manager.Providers);
        }

        [TestMethod]
        public void RemoveProvider_ProviderIsNull_Throw()
        {
            LocalizationManager manager = new();
            ILocalizationProvider provider = null!;

            void action() => manager.RemoveProvider(provider);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void RemoveProvider_ProviderNotExist_Nothing()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider provider = new();

            manager.RemoveProvider(provider);

            Assert.IsEmpty(manager.Providers);
        }

        [TestMethod]
        public void RemoveProvider_ProviderExist_RemovedProvider()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider provider = new();
            manager.AddProvider(provider);

            manager.RemoveProvider(provider);

            Assert.IsEmpty(manager.Providers);
        }

        [TestMethod]
        public void Cultures_AddSomeProviders_CulturesAllProviders()
        {
            LocalizationManager manager = new();
            MockLocalizationProvider providerOne = new();
            NameLocalizationProvider providerTwo = new();
            manager.AddProvider(providerOne);
            manager.AddProvider(providerTwo);

            IEnumerable<CultureInfo> allSupportedCultures = providerOne.SupportedCultures.Concat(providerTwo.SupportedCultures);

            CollectionAssert.AreEquivalent(allSupportedCultures.ToList(), manager.Cultures.ToList());
        }

        [TestMethod]
        public void Cultures_CastingToList_Throw()
        {
            LocalizationManager manager = new();

            void action() => _ = (List<CultureInfo>)manager.Cultures;

            Assert.ThrowsExactly<InvalidCastException>(action);
        }

        [TestMethod]
        public void Providers_CastingToList_Throw()
        {
            LocalizationManager manager = new();

            void action() => _ = (List<ILocalizationProvider>)manager.Providers;

            Assert.ThrowsExactly<InvalidCastException>(action);
        }

        [TestMethod]
        public void CurrentUICultureChange_ChangeCulture_EventInvoked()
        {
            bool isInvoked = false;
            MockCultureService service = new();
            LocalizationManager manager = new(cultureService: service);
            manager.CurrentUICultureChanged += (m, e) => isInvoked = true;
            CultureInfo newCulture = CultureInfo.GetCultureInfo("ru-RU");

            manager.CurrentUICulture = newCulture;

            Assert.IsTrue(isInvoked);
        }

        [TestMethod]
        public void CurrentUICultureChanged_ChangeCulture_OldAndNewCultureCorrespondReality()
        {
            CultureInfo newCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo oldCulture = CultureInfo.GetCultureInfo("fr-FR");
            MockCultureService service = new();
            LocalizationManager manager = new(oldCulture, service);
            CultureInfo actualNewCulture = null!;
            CultureInfo actualOldCulture = null!;
            manager.CurrentUICultureChanged += (m, e) =>
            {
                actualNewCulture = e.NewCulture;
                actualOldCulture = e.OldCulture;
            };

            manager.CurrentUICulture = newCulture;

            Assert.AreEqual(newCulture, actualNewCulture);
            Assert.AreEqual(oldCulture, actualOldCulture);
        }

        [TestMethod]
        public void Localize_KeyIsNull_Throw()
        {
            string key = null!;
            MockCultureService service = new();
            CultureInfo culture = CultureInfo.GetCultureInfo("fr-FR");
            LocalizationManager manager = new(culture, service);
            MockLocalizationProvider provider = new();
            manager.AddProvider(provider);

            void action() => _ = manager.Localize(key);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Localize_KeyIsNotExist_Throw()
        {
            string key = "MyKey";
            MockCultureService service = new();
            CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
            LocalizationManager manager = new(culture, service);
            MockLocalizationProvider provider = new();
            manager.AddProvider(provider);

            void action() => _ = manager.Localize(key);

            Assert.ThrowsExactly<ResourceNotFoundException>(action);
        }

        [TestMethod]
        public void Localize_KeyExist_LocalizedValue()
        {
            string key = "1";
            MockCultureService service = new();
            CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
            LocalizationManager manager = new(culture, service);
            MockLocalizationProvider provider = new();
            manager.AddProvider(provider);

            string value = (string)manager.Localize(key);

            Assert.AreEqual("One", value);
        }

        [TestMethod]
        public void Localize_MultipleProvidersHaveKey_ValueReturnFirstAddedProvider()
        {
            string key = "Name";
            MockCultureService service = new();
            CultureInfo culture = CultureInfo.GetCultureInfo("ru-RU");
            LocalizationManager manager = new(culture, service);
            MockLocalizationProvider provider = new();
            NameLocalizationProvider secondProvider = new();
            manager.AddProvider(provider);
            manager.AddProvider(secondProvider);

            string value = (string)manager.Localize(key);

            Assert.AreEqual("Имя", value);
        }
    }
}
