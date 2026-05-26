using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace KebabGGbab.Avalonia.Extensions.Services.Localization
{
    public class Localization : MarkupExtension
    {
        public required string Key { get; set; }

        public string[]? Arguments { get; set; }

        public Localization()
        {
        }

        public Localization(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IRootObjectProvider? root = (IRootObjectProvider?)serviceProvider.GetService(typeof(IRootObjectProvider));
            ArgumentNullException.ThrowIfNull(root, nameof(root));

            TopLevel topLevel = (TopLevel)root.RootObject;
            LocalizationListener listener = new(topLevel, Key, Arguments);
            Binding binding = new()
            {
                Source = listener,
                Path = nameof(LocalizationListener.Value),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            
            return binding;
        }
    }
}