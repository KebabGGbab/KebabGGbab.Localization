using System.Windows;
using KebabGGbab.Localization.Manager;

namespace KebabGGbab.Localization.WPF
{
    public sealed class CurrentUICultureChangedWeakEventManager : WeakEventManager
    {
        private static CurrentUICultureChangedWeakEventManager CurrentManager
        {
            get
            {
                Type managerType = typeof(CurrentUICultureChangedWeakEventManager);
                CurrentUICultureChangedWeakEventManager manager = (CurrentUICultureChangedWeakEventManager)GetCurrentManager(managerType);

                if (manager == null)
                {
                    manager = new CurrentUICultureChangedWeakEventManager();
                    SetCurrentManager(managerType, manager);
                } 

                return manager;
            }
        }

        private CurrentUICultureChangedWeakEventManager() { }

        public static void AddListener(ILocalizationManager source, IWeakEventListener listener)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(listener);

            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(ILocalizationManager source, IWeakEventListener listener)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(listener);

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
