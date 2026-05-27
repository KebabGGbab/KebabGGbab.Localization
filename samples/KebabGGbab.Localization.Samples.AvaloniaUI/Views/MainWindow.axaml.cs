using Avalonia.Controls;
using KebabGGbab.Localization.Samples.Shared.ViewModels;

namespace KebabGGbab.Localization.Samples.AvaloniaUI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}