using System.Globalization;
using KebabGGbab.Localization.CultureService;
using KebabGGbab.Localization.Exceptions;
using KebabGGbab.Localization.Manager;
using KebabGGbab.Localization.Providers;
using KebabGGbab.Localization.Test.Mocks;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    [DoNotParallelize]
    public sealed class LocalizationManagerTest
    {
        private readonly static CultureInfo _defaultCulture = CultureInfo.CurrentCulture;
        private readonly static ICultureService _defaultCultureService = LocalizationManager.Instance.CultureService;

        [TestCleanup]
        public void TestTearDown()
        {
            CultureInfo.CurrentUICulture = _defaultCulture;
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CurrentUICulture = _defaultCulture;
            manager.CultureService = _defaultCultureService;

            foreach (ILocalizationProvider provider in manager.Providers.ToList())
            {
                manager.RemoveProvider(provider);
            }
        }

        [TestMethod]
        public void CurrentUICulture_DefaultCultureService_CultureUICultureEqualsCurrentUICultureThread()
        {
            CultureInfo culture = CultureInfo.CurrentUICulture;

            LocalizationManager manager = LocalizationManager.Instance;

            Assert.AreEqual(culture, manager.CurrentUICulture);
        }

        [TestMethod]
        public void CurrentUICulture_ChangeToNotSupportedCulture_Changed()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            manager.AddProvider(new MockLocalizationProvider());
            CultureInfo newCulture = CultureInfo.GetCultureInfo("de-DE");

            manager.CurrentUICulture = newCulture;

            Assert.AreEqual(newCulture, manager.CurrentUICulture);
        }

        [TestMethod]
        public void CultureService_Change_ServiceChanged()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            ICultureService newService = new MockCultureService();

            manager.CultureService = newService;

            Assert.AreEqual(newService, manager.CultureService);
        }

        [TestMethod]
        public void AddProvider_ProviderIsNull_Throw()
        {
            LocalizationManager manager = LocalizationManager.Instance;

            void action() => manager.AddProvider(null!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void AddProvider_AddOneProvider_AddedProvider()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            MockLocalizationProvider providerOne = new();

            manager.AddProvider(providerOne);

            Assert.ContainsSingle(manager.Providers);
            Assert.Contains(providerOne, manager.Providers);
        }

        [TestMethod]
        public void AddProvider_AddMultipleDifferentProviders_AddedAll()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            MockLocalizationProvider providerOne = new();
            NameLocalizationProvider providerTwo = new();

            manager.AddProvider(providerOne);
            manager.AddProvider(providerTwo);

            Assert.HasCount(2, manager.Providers);
            Assert.Contains(providerOne, manager.Providers);
            Assert.Contains(providerTwo, manager.Providers);
        }

        [TestMethod]
        public void AddProvider_AddMultipleSameTypeProviders_AddedAll()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            MockLocalizationProvider providerOne = new();
            MockLocalizationProvider providerTwo = new();

            manager.AddProvider(providerOne);
            manager.AddProvider(providerTwo);

            Assert.HasCount(2, manager.Providers);
            Assert.Contains(providerOne, manager.Providers);
            Assert.Contains(providerTwo, manager.Providers);
        }

        
        [TestMethod]
        public void AddProvider_AddOneProviderMultipleTimes_AddedOnlyOne()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            MockLocalizationProvider provider = new();

            manager.AddProvider(provider);
            manager.AddProvider(provider);

            Assert.ContainsSingle(manager.Providers);
            Assert.Contains(provider, manager.Providers);
        }

        [TestMethod]
        public void RemoveProvider_ProviderIsNull_Throw()
        {
            LocalizationManager manager = LocalizationManager.Instance;

            void action() => manager.RemoveProvider(null!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void RemoveProvider_ProviderNotExist_Nothing()
        {
            LocalizationManager manager = LocalizationManager.Instance;

            manager.RemoveProvider(new MockLocalizationProvider());

            Assert.IsEmpty(manager.Providers);
        }

        [TestMethod]
        public void RemoveProvider_ProviderExist_RemovedProvider()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            MockLocalizationProvider provider = new();
            manager.AddProvider(provider);

            manager.RemoveProvider(provider);

            Assert.IsEmpty(manager.Providers);
        }

        [TestMethod]
        public void Cultures_AddSomeProviders_CulturesAllProviders()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            manager.AddProvider(new MockLocalizationProvider());
            manager.AddProvider(new NameLocalizationProvider());

            IEnumerable<CultureInfo> allSupportedCultures = manager.Providers.SelectMany(p => p.SupportedCultures);

            CollectionAssert.AreEquivalent(allSupportedCultures.ToList(), manager.Cultures.ToList());
        }

        [TestMethod]
        public void Cultures_CastingToList_Throw()
        {
            LocalizationManager manager = LocalizationManager.Instance;

            void action() => _ = (List<CultureInfo>)manager.Cultures;

            Assert.ThrowsExactly<InvalidCastException>(action);
        }

        [TestMethod]
        public void Providers_CastingToList_Throw()
        {
            LocalizationManager manager = LocalizationManager.Instance;

            void action() => _ = (List<ILocalizationProvider>)manager.Providers;

            Assert.ThrowsExactly<InvalidCastException>(action);
        }

        [TestMethod]
        public void CurrentUICultureChange_ChangeCulture_EventInvoked()
        {
            bool isInvoked = false;
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CultureService = new MockCultureService();
            manager.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            manager.CurrentUICultureChanged += (m, e) => isInvoked = true;

            manager.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.IsTrue(isInvoked);
        }

        [TestMethod]
        public void CurrentUICultureChanged_ChangeCulture_OldAndNewCultureCorrespondReality()
        {
            CultureInfo newCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo oldCulture = CultureInfo.GetCultureInfo("fr-FR");
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CultureService = new MockCultureService();
            manager.CurrentUICulture = oldCulture;
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
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            manager.CultureService = new MockCultureService();
            manager.AddProvider(new MockLocalizationProvider());

            void action() => _ = manager.Localize(null!);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Localize_KeyIsNotExist_Throw()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            manager.CultureService = new MockCultureService();
            manager.AddProvider(new MockLocalizationProvider());

            void action() => _ = manager.Localize("MyKey");

            Assert.ThrowsExactly<ResourceNotFoundException>(action);
        }

        [TestMethod]
        public void Localize_KeyExist_LocalizedValue()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            manager.CultureService = new MockCultureService();
            manager.AddProvider(new MockLocalizationProvider());

            string value = (string)manager.Localize("1");

            Assert.AreEqual("One", value);
        }

        [TestMethod]
        public void Localize_MultipleProvidersHaveKey_ValueReturnFirstAddedProvider()
        {
            LocalizationManager manager = LocalizationManager.Instance;
            manager.CultureService = new MockCultureService();
            manager.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            manager.AddProvider(new MockLocalizationProvider());
            manager.AddProvider(new NameLocalizationProvider());

            string value = (string)manager.Localize("Name");

            Assert.AreEqual("Имя", value);
        }
    }
}
