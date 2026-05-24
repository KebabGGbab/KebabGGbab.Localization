using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using KebabGGbab.Localization.WPF.Resources;

namespace KebabGGbab.Localization.WPF
{
    [MarkupExtensionReturnType(typeof(object))]
    public class LocalizationExtension : MarkupExtension
    {
        public required string Key { get; set; }
        public object[]? Arguments { get; set; }

        public LocalizationExtension() { }

        public LocalizationExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

            IProvideValueTarget? target = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget));

            ArgumentNullException.ThrowIfNull(target, nameof(target));

            LocalizationListener listener = new(Key, Arguments);
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
