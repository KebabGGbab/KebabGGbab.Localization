using System.Windows;
using KebabGGbab.Localization;
using KebabGGbab.Localization.Abstractions;

namespace KebabGGbab.WPF.Extensions.Services.Localization
{
    public sealed class CurrentUICultureChangedEventManager : WeakEventManager
    {
        private static CurrentUICultureChangedEventManager CurrentManager
        {
            get
            {
                Type managerType = typeof(CurrentUICultureChangedEventManager);
                CurrentUICultureChangedEventManager manager = (CurrentUICultureChangedEventManager)GetCurrentManager(managerType);

                if (manager == null)
                {
                    manager = new CurrentUICultureChangedEventManager();
                    SetCurrentManager(managerType, manager);
                } 

                return manager;
            }
        }

        private CurrentUICultureChangedEventManager() { }

        public static void AddListener(ILocalizationManager source, IWeakEventListener listener)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(listener, nameof(listener));

            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(ILocalizationManager source, IWeakEventListener listener)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(listener, nameof(listener));

            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        protected override void StartListening(object source)
        {
            ILocalizationManager manager = (ILocalizationManager)source;
            manager.CurrentUICultureChanged += OnCurrentUICultureChanged;
        }

        protected override void StopListening(object source)
        {
            ILocalizationManager manager = (ILocalizationManager)source;
            manager.CurrentUICultureChanged -= OnCurrentUICultureChanged;
        }

        private void OnCurrentUICultureChanged(object? sender, CurrentUICultureChangedEventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }
}
