using System.Windows;
using System.Windows.Controls;
using MyWPFApp.Model;
using MyWPFApp.ViewModel;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour FutureView.xaml
    /// </summary>
    public partial class FutureView : UserControl
    {
        private MainWindowViewModel _viewModel;
        public FutureView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            BitcoinPanel.DataContext = _viewModel.MyCryptocurrencies[0];
            EthereumPanel.DataContext = _viewModel.MyCryptocurrencies[1];
            BnbPanel.DataContext = _viewModel.MyCryptocurrencies[2];
            SolanaPanel.DataContext = _viewModel.MyCryptocurrencies[3];
            XrpPanel.DataContext = _viewModel.MyCryptocurrencies[4];
            AvaxPanel.DataContext = _viewModel.MyCryptocurrencies[5];
            FantomPanel.DataContext = _viewModel.MyCryptocurrencies[6];
            SelectedPrice.DataContext = _viewModel.MyCryptocurrencies[0];
        }
        private void BitcoinRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(0);
        }

        private void EthereumRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(1);
        }

        private void BNBRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(2);
        }

        private void SolanaRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(3);
        }

        private void XRPRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(4);
        }

        private void AVAXRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(5);
        }

        private void FantomRefresh(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(6);
        }

        private void RefreshAll(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCrypto(7);
        }
        private void LongFuture(object sender, RoutedEventArgs e)
        {
            double amount = SliderAmount.Value;
            int leverage = (int)LeverageAmount.Value;
            _viewModel.BuyTradeFuture(SelectCrypto.Text,leverage,amount,"Long");
        }
        private void ShortFuture(object sender, RoutedEventArgs e)
        {
            double amount = SliderAmount.Value;
            int leverage = (int)LeverageAmount.Value;
            _viewModel.BuyTradeFuture(SelectCrypto.Text, leverage, amount,"Short");
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.RefreshComboboxFuture((Cryptocurrency)SelectCrypto.SelectedItem, SelectedPrice);
        }

        private void SellFuture(object sender, RoutedEventArgs e)
        {
            var rowItem = ((Button)sender).DataContext as TradeFuture;
            PopupFuture popup = new PopupFuture(rowItem.TotalAmount);
            popup.ShowDialog();
            if(PopupFuture._choice && rowItem != null)
                _viewModel.SellTradeFuture((TradeFuture)rowItem,PopupFuture._pourcent);
        }
    }
}
