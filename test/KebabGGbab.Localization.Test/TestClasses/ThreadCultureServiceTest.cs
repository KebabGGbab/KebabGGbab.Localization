using System.Globalization;
using KebabGGbab.Localization.CultureService;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class ThreadCultureServiceTest
    {
        [TestMethod]
        public void CurrentUICulture_GetCurrentUICulture_EqualThreadCurrentUICulture()
        {
            ThreadCultureService service = new();

            Assert.AreEqual(CultureInfo.CurrentUICulture, service.CurrentUICulture);
            Assert.AreEqual(CultureInfo.DefaultThreadCurrentUICulture, service.CurrentUICulture);
        }

        [TestMethod]
        public void ChangeCurrentUICulture_OtherCulture_Changed()
        {
            ThreadCultureService service = new();
            CultureInfo newCulture = CultureInfo.GetCultureInfo("fr-FR");

            bool result = service.ChangeCurrentUICulture(newCulture);

            Assert.AreEqual(CultureInfo.CurrentUICulture, newCulture);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangeCurrentUICulture_PassCurrentCulture_NotChanged()
        {
            ThreadCultureService service = new();
            CultureInfo newCulture = CultureInfo.CurrentUICulture;

            bool result = service.ChangeCurrentUICulture(newCulture);

            Assert.AreEqual(CultureInfo.CurrentUICulture, newCulture);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangeCurrentUICulture_PassNull_Throw()
        {
            ThreadCultureService service = new();
            CultureInfo newCulture = null!;

            void action() => service.ChangeCurrentUICulture(newCulture);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }
    }
}
