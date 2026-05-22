using System.Globalization;
using KebabGGbab.Localization.Manager;

namespace KebabGGbab.Localization.Test.TestClasses
{
    [TestClass]
    public sealed class CurrentUICultureChangedEventArgsTest
    {
        [TestMethod]
        public void Ctor_NewCultureIsNull_Throw()
        {
            CultureInfo newCUlture = null!;
            CultureInfo oldCulture = CultureInfo.GetCultureInfo("de-DE");

            void action() => _ = new CurrentUICultureChangedEventArgs(newCUlture, oldCulture);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Ctor_OldCultureIsNull_Throw()
        {
            CultureInfo newCUlture = CultureInfo.GetCultureInfo("fr-FR");
            CultureInfo oldCulture = null!;

            void action() => _ = new CurrentUICultureChangedEventArgs(newCUlture, oldCulture);

            Assert.ThrowsExactly<ArgumentNullException>(action);
        }

        [TestMethod]
        public void Ctor_BothCultureAreValid_CanGetFromProperties()
        {
            CultureInfo newCUlture = CultureInfo.GetCultureInfo("fr-FR");
            CultureInfo oldCulture = CultureInfo.GetCultureInfo("de-DE");
            CurrentUICultureChangedEventArgs args = new(newCUlture, oldCulture);

            Assert.AreEqual(newCUlture, args.NewCulture);
            Assert.AreEqual(oldCulture, args.OldCulture);
        }
    }
}
