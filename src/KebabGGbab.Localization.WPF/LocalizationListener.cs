using System.ComponentModel;
using System.Globalization;
using System.Text;
using KebabGGbab.Localization;
using KebabGGbab.WPF.Extensions.Resources.Strings;

namespace KebabGGbab.WPF.Extensions.Services.Localization
{
    public class LocalizationListener : BaseLocalizationListener, INotifyPropertyChanged
    {
        private static readonly CompositeFormat _resourcePlaceholder = CompositeFormat.Parse(S_LocalizationService.ResourcePlaceholder);

        private readonly string _key;
        private readonly object[]? _args;

        public object Value 
        { 
            get => field;
            private set => SetProperty(ref field, value);
        }

        public LocalizationListener(string key, object[]? args)
        {
            _key = key;
            _args = args;
            Value = SetValue();
        }

        public override bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CurrentUICultureChangedEventManager))
            {
                Value = SetValue();

                return true;
            }

            return false;
        }

        private object SetValue()
        {
            try
            {
                object value = LocalizationManager.Instance.Localize(_key);

                if (value is string str && _args != null)
                {
                    return string.Format(str, _args);
                }
                else
                {
                    return value;
                }
            }
            catch (ResourceNotFoundException ex)
            {
                return string.Format(CultureInfo.InvariantCulture, _resourcePlaceholder, ex.Key);
            }
        }
    }
}
