namespace KebabGGbab.Localization.CultureService
{
    public interface ICultureService
    {
        CultureInfo CurrentUICulture { get; }

        bool ChangeCurrentUICulture(CultureInfo newCulture);
    }
}
