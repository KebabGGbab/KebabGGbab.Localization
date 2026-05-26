using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia.Controls;
using KebabGGbab.Localization.AvaloniaUI.Resources;
using KebabGGbab.Localization.Exceptions;
using KebabGGbab.Localization.Manager;

namespace KebabGGbab.Localization.AvaloniaUI
{
    public class LocalizationListener : INotifyPropertyChanged
    {
        private static readonly CompositeFormat _resourcePlaceholder = CompositeFormat.Parse(Strings.LocalizationListenerPlaceholderFormat);
        
        private readonly TopLevel _root;

        public string Key { get; }

        public string[]? Arguments { get; }

        public object? Value 
        {
            get;
            private set => SetProperty(ref field, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationListener(TopLevel root, string key, string[]? arguments = null)
        {
            Key = key;
            Arguments = arguments;
            Value = GetValue();
            _root = root;
            _root.Closed += Root_Closed;
            LocalizationManager.Instance.CurrentUICultureChanged += OnCurrentUICultureChanged;
        }

        private void OnCurrentUICultureChanged(ILocalizationManager sender, CurrentUICultureChangedEventArgs e)
        {
            Value = GetValue();
        }

        private object GetValue()
        {
            try
            {
                object value = LocalizationManager.Instance.Localize(Key);

                if (value is string str && Arguments != null)
                {
                    return string.Format(str, Arguments);
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

        private void Root_Closed(object? sender, EventArgs e)
        {
            _root.Closed -= Root_Closed;
            LocalizationManager.Instance.CurrentUICultureChanged -= OnCurrentUICultureChanged;
        }

        private bool SetProperty<T>([NotNullIfNotNull(nameof(newValue))] ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(args);

            PropertyChanged?.Invoke(this, args);
        }
    }
}
