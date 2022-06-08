using System.Windows;
using System.Windows.Controls;
using MyWPFApp.Model;
using MyWPFApp.ViewModel;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour SpotView.xaml
    /// </summary>
    public partial class SpotView : UserControl
    {
        private MainWindowViewModel _viewModel;
        public SpotView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            SliderAmountCrypto.DataContext = _viewModel.MyWallet.TradeSpots[0];
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


        private void BuySpot(object sender, RoutedEventArgs e)
        {
            
            double money = SliderAmount.Value;
            _viewModel.NewTradeSpot(SelectCrypto.Text, money);
        }

        private void SellSpot(object sender, RoutedEventArgs e)
        {
            double shares = SliderAmountCrypto.Value;
            _viewModel.SellTradeSpot(SelectCrypto.Text,shares);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.RefreshComboboxSpot((Cryptocurrency) SelectCrypto.SelectedItem, SelectedPrice, SliderAmountCrypto);
        }
    }
}
