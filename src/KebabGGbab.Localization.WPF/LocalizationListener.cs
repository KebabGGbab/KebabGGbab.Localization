using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using KebabGGbab.Localization.Exceptions;
using KebabGGbab.Localization.Manager;
using KebabGGbab.Localization.WPF.Resources;

namespace KebabGGbab.Localization.WPF
{
    public sealed class LocalizationListener : INotifyPropertyChanged, IWeakEventListener, IDisposable
    {
        private static readonly CompositeFormat _resourcePlaceholder = CompositeFormat.Parse(Strings.LocalizationListenerPlaceholderFormat);

        private bool _disposed;

        public string Key { get; }

        public object Value 
        { 
            get;
            set => SetProperty(ref field, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationListener(string key) 
        {
            ArgumentNullException.ThrowIfNull(key);

            Key = key;
            Value = GetValue();
            // Я бы хотел уйти от Singlton. Это произойдет тогда, когда я решу как лучше передавать ILocalizationManager в расширение xaml
            CurrentUICultureChangedWeakEventManager.AddListener(LocalizationManager.Instance, this);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CurrentUICultureChangedWeakEventManager))
            {
                Value = GetValue();

                return true;
            }

            return false;
        }

        private object GetValue()
        {
            try
            {
                return LocalizationManager.Instance.Localize(Key);
            }
            catch (ResourceNotFoundException ex)
            {
                return string.Format(CultureInfo.InvariantCulture, _resourcePlaceholder, ex.Key);
            }
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
             _disposed = true;

            if (disposing)
            {
                CurrentUICultureChangedWeakEventManager.RemoveListener(LocalizationManager.Instance, this);
            }
        }

        ~LocalizationListener()
        {
            Dispose(false);
        }
    }
}