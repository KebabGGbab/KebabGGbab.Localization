using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using KebabGGbab.Localization;

namespace KebabGGbab.WPF.Extensions.Services.Localization
{
    public abstract class BaseLocalizationListener : ObservableObject, IWeakEventListener, IDisposable
    {
        private bool _disposed;

        public BaseLocalizationListener() 
        {
            CurrentUICultureChangedEventManager.AddListener(LocalizationManager.Instance, this);
        }

        public abstract bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
             _disposed = true;

            if (disposing)
            {
                CurrentUICultureChangedEventManager.RemoveListener(LocalizationManager.Instance, this);
            }
        }

        ~BaseLocalizationListener()
        {
            Dispose(false);
        }
    }
}
