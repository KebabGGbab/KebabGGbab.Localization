using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using KebabGGbab.Localization.WPF.Resources;

namespace KebabGGbab.Localization.WPF
{
    [MarkupExtensionReturnType(typeof(object))]
    public class LocalizationExtension : MarkupExtension
    {

        public string Key { get; set; }

        public LocalizationExtension(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            LocalizationListener listener = new(Key);
            Binding binding = new()
            {
                Source = listener,
                Path = new PropertyPath(nameof(LocalizationListener.Value)),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                FallbackValue = Strings.LocalizationListenerPlaceholderFormat,
            };

            return binding.ProvideValue(serviceProvider);
        }
    }
}
