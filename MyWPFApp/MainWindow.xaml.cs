using System;
using System.Windows;
using MyWPFApp.Model;
using MyWPFApp.ViewModel;
using MyWPFApp.View;

namespace MyWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private SettingsView? _settingsView;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }
        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            _viewModel.MyWallet.Serializable(_viewModel.Parameters.FilenameSave);
        }
        private void StartSetting(object sender, RoutedEventArgs e)
        {
            _settingsView = new SettingsView();
            _settingsView.SettingViewModel.SettingEvent += Settingchange;
            _settingsView.ShowDialog(); 
        }
        public void Settingchange(object sender, SettingEventArgs e)
        {
            _viewModel.SettingChange(e);
        }
    }
}
