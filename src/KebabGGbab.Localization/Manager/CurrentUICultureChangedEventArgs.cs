namespace KebabGGbab.Localization.Manager
{
    public class CurrentUICultureChangedEventArgs : EventArgs
    {
        public CultureInfo NewCulture { get; }
        public CultureInfo OldCulture { get; }

        public CurrentUICultureChangedEventArgs(CultureInfo newCulture, CultureInfo oldCulture)
        {
            ArgumentNullException.ThrowIfNull(newCulture);
            ArgumentNullException.ThrowIfNull(oldCulture);

            NewCulture = newCulture;
            OldCulture = oldCulture;
        }
    }
}
