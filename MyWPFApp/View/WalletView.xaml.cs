using System.Windows;
using System.Windows.Controls;
using MyWPFApp.Model;
using MyWPFApp.ViewModel;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour WalletView.xaml
    /// </summary>
    public partial class WalletView : UserControl
    {
        private MainWindowViewModel _viewModel;
        public WalletView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            GridAll.DataContext = _viewModel.MyWallet;
        }

        private void RefreshCrypto(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as TradeSpot;
            int nb = _viewModel.ReturnNumberOfCrypto(rowItem.Action);
            rowItem.Pnl = (rowItem.Shares * _viewModel.MyCryptocurrencies[nb].Price) - (rowItem.Shares * rowItem.EntryPrice);
        }
        private void RefreshPnl(object sender, RoutedEventArgs e)
        {
            _viewModel.MyWallet.RefreshAllPnlSpot(_viewModel.MyCryptocurrencies);
        }
    }
}
