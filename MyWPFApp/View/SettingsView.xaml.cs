using System.Windows;
using MyWPFApp.ViewModel;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingViewModel SettingViewModel { get; set; }
        public SettingsView()
        {
            InitializeComponent();
            SettingViewModel = new SettingViewModel();
        }
        private void ModifyWalletAmount(object sender, RoutedEventArgs e)
        {
            SettingViewModel.SettingChangeWallet(TextBoxWallet.Text);
        }

        private void ModifyPathSave(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                SettingViewModel.SettingSavePathChange(dialog.SelectedPath);
            }
        }

        private void DeleteSave(object sender, RoutedEventArgs e)
        {
            PopUpAgree pop = new PopUpAgree();
            pop.ShowDialog();
            if(pop.Choice)
                SettingViewModel.DeleteAllData();
        }

        private void ExportHisto(object sender, RoutedEventArgs e)
        {
            SettingViewModel.ExportTradeSpot();
        }
    }
}
