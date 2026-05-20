namespace KebabGGbab.Localization.Manager
{
    public interface ILocalizationManager
    {
        IReadOnlyList<CultureInfo> Cultures { get; }

        CultureInfo CurrentUICulture { get; set; }

        event EventHandler<ILocalizationManager, CurrentUICultureChangedEventArgs>? CurrentUICultureChanged;

        object Localize(string key);
    }
}
