using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace KebabGGbab.Localization.AvaloniaUI
{
    public class Localization : MarkupExtension
    {
        public required string Key { get; set; }

        public Localization(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            TopLevel topLevel = (TopLevel)serviceProvider.GetRequiredService<IRootObjectProvider>().RootObject;
            LocalizationListener listener = new(topLevel, Key);
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