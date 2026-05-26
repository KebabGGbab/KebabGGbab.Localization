using System.Globalization;
using System.Text;
using Avalonia.Controls;

namespace KebabGGbab.Localization.AvaloniaUI
{
    public class LocalizationListener : ObservableObject
    {
        private static readonly CompositeFormat _resourcePlaceholder = CompositeFormat.Parse(S_LocalizationService.ResourcePlaceholder);
        
        private readonly TopLevel _root;

        public string Key { get; }

        public string[]? Arguments { get; }

        public object? Value 
        {
            get => field;
            private set => SetProperty(ref field, value);
        }

        public LocalizationListener(TopLevel root, string key, string[]? arguments = null)
        {
            Key = key;
            Arguments = arguments;
            LocalizeValue();
            _root = root;
            _root.Closed += Root_Closed;
            LocalizationManager.Instance.CurrentUICultureChanged += OnCurrentUICultureChanged;
        }

        private void OnCurrentUICultureChanged(ILocalizationManager sender, CurrentUICultureChangedEventArgs e)
        {
            LocalizeValue();
        }

        private void LocalizeValue()
        {
            try
            {
                object value = LocalizationManager.Instance.Localize(Key);

                if (value is string str && Arguments != null)
                {
                    Value = string.Format(str, Arguments);
                }
                else
                {
                    Value = value;
                }
            }
            catch (ResourceNotFoundException ex)
            {
                Value = string.Format(CultureInfo.InvariantCulture, _resourcePlaceholder, ex.Key);
            }
        }

        private void Root_Closed(object? sender, EventArgs e)
        {
            _root.Closed -= Root_Closed;
            LocalizationManager.Instance.CurrentUICultureChanged -= OnCurrentUICultureChanged;
        }
    }
}
