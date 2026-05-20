using System.Globalization;

namespace KebabGGbab.Localization
{
    public class CurrentUICultureChangedEventArgs : EventArgs
    {
        public CultureInfo NewCulture { get; }
        public CultureInfo OldCulture { get; }

        public CurrentUICultureChangedEventArgs(CultureInfo newCulture, CultureInfo oldCulture)
        {
            ArgumentNullException.ThrowIfNull(newCulture, nameof(newCulture));
            ArgumentNullException.ThrowIfNull(oldCulture, nameof(oldCulture));

            NewCulture = newCulture;
            OldCulture = oldCulture;
        }
    }
}
