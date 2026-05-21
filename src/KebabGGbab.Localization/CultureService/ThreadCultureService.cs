namespace KebabGGbab.Localization.CultureService
{
    public class ThreadCultureService : ICultureService
    {
        public CultureInfo CurrentUICulture 
        {
            get => CultureInfo.CurrentUICulture;
            private set
            { 
                CultureInfo.CurrentCulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
            }
        }

        public bool ChangeCurrentUICulture(CultureInfo newCulture)
        {
            if (CurrentUICulture == newCulture)
            {
                return false;
            }

            CurrentUICulture = newCulture;

            return true;
        }
    }
}