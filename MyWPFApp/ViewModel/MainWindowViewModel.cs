using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MyWPFApp.Model;
using MyWPFApp.View;
using Window = System.Windows.Window;

namespace MyWPFApp.ViewModel
{
    public class MainWindowViewModel : Window, INotifyPropertyChanged
    {
        public Wallet MyWallet { get; set; }
        public ObservableCollection<Cryptocurrency> MyCryptocurrencies { get; set; }

        public WalletView Wallet { get; set; }
        public SpotView Spot { get; set; }
        public FutureView Future { get; set; }
        public DefiView Defi { get; set; }
        public Settings Parameters { get; set; }
        public MainWindowViewModel()
        {
            MyCryptocurrencies = new ObservableCollection<Cryptocurrency>();
            string? test = Path.GetDirectoryName(
                Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
            test += "\\Picture\\";
            MyCryptocurrencies.Add(new Cryptocurrency("Bitcoin", 42050, "bitcoin",test+"bitcoin.png"));
            MyCryptocurrencies.Add(new Cryptocurrency("Ethereum", 3420, "ethereum", @"..\Picture\ethereum.png"));
            MyCryptocurrencies.Add(new Cryptocurrency("BNB", 400, "binancecoin", test + "bnb.png"));
            MyCryptocurrencies.Add(new Cryptocurrency("Solana", 90.80, "solana", test + "solana.png")); 
            MyCryptocurrencies.Add(new Cryptocurrency("XRP", 2, "ripple", test + "xrp.png"));
            MyCryptocurrencies.Add(new Cryptocurrency("Avax", 80, "avalanche-2", test + "avax.png"));
            MyCryptocurrencies.Add(new Cryptocurrency("Fantom", 3, "fantom", test + "fantom.png"));
            RefreshCrypto(7);
            Parameters = new Settings();
            MyWallet = new Wallet(50000);
            if(Parameters.IsSaveAvb)
            {
                MyWallet.Deserializable(Parameters.FilenameSave);
                
            }
            else
            {
                MyWallet.Serializable(Parameters.FilenameSave);
            }
            Spot = new SpotView(this);
            Wallet = new WalletView(this);
            Future = new FutureView(this);
            Defi = new DefiView();
        }

        public void SettingChange(SettingEventArgs e)
        {
            switch(e.Action)
            {
                case 1:
                    {
                        //change wallet amount
                        MyWallet.Amount = Convert.ToDouble(e.Data);
                    }
                    break;
                case 2:
                    {
                        //change path save
                        Parameters.ChangeFilename(e.Data);
                    }
                    break;
                case 3:
                    {
                        // delete all
                        Parameters.DeleteSave();
                        MyWallet.DeleteWallet();
                    }
                    break;
                case 4:
                    {
                        // delete all
                        if(Parameters.FolderPath != null)
                            MyWallet.ExportSpotHistory(Parameters.FolderPath);
                    }
                    break;
            }
        }
        private void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void SellTradeSpot(string crypto, double shares)
        {
            int number = ReturnNumberOfCrypto(crypto);
            TradeSpot newTrade = new TradeSpot(crypto, 0, shares, MyCryptocurrencies[number].Price, "Sell");
            newTrade.Amount = shares * MyCryptocurrencies[number].Price;
            MyWallet.SellTradeSpot(newTrade, number, Parameters.FilenameSave);
        }
        public void NewTradeSpot(string crypto, double amount)
        {
            int number = ReturnNumberOfCrypto(crypto);
            TradeSpot newTrade = new TradeSpot(crypto, MyCryptocurrencies[number].Price, amount, 0,"Buy");
            MyWallet.AddTradeSpot(newTrade, number, Parameters.FilenameSave);
        }

        public void BuyTradeFuture(string crypto, int leverage,double amount,string type)
        {
            int number = ReturnNumberOfCrypto(crypto);
            TradeFuture newTrade = new TradeFuture(crypto, MyCryptocurrencies[number].Price, amount, leverage,0, DateTime.Now,type);
            newTrade.ExitPrice = MyCryptocurrencies[ReturnNumberOfCrypto(crypto)].Price;
            newTrade.TotalAmount = newTrade.Amount * newTrade.Leverage;
            MyWallet.AddTradeFuture(newTrade,Parameters.FilenameSave);
        }

        public void SellTradeFuture(TradeFuture trade,int pourcent)
        {
            MyWallet.SellTradeFuture(trade,pourcent,Parameters.FilenameSave,MyCryptocurrencies[ReturnNumberOfCrypto(trade.Action)].Price);
        }
        public int ReturnNumberOfCrypto(string crypto)
        {

            switch (crypto)
            {
                case "Bitcoin":
                {
                    return 0;
                }
                
                case "Ethereum":
                {
                    return 1;
                }
                
                case "BNB":
                {
                    return 2;
                }
                
                case "Solana":
                {
                    return 3;
                }
                
                case "XRP":
                {
                    return 4;
                }
                
                case "Avax":
                {
                    return 5;
                }
                
                case "Fantom":
                {
                    return 6;
                }
                default: return 0;
            }
        }
        public void RefreshComboboxSpot(Cryptocurrency crypto, TextBlock textBlock, Slider slider)
        {
            int number = ReturnNumberOfCrypto(crypto.Crypto);
            textBlock.DataContext = MyCryptocurrencies[number];
            slider.DataContext = MyWallet.TradeSpots[number];
        }
        public void RefreshComboboxFuture(Cryptocurrency crypto, TextBlock textBlock)
        {
            int number = ReturnNumberOfCrypto(crypto.Crypto);
            textBlock.DataContext = MyCryptocurrencies[number];
        }
        public void RefreshCrypto(int nb)
        {
            //_ =  permet d'ignorer l'appel vu que la fonction refresh price est en fonction Task (execution en arriere plan)
            if (nb == 7)
            {
                _ = MyCryptocurrencies[0].RefreshPrice();
                _ = MyCryptocurrencies[1].RefreshPrice();
                _ = MyCryptocurrencies[2].RefreshPrice();
                _ = MyCryptocurrencies[3].RefreshPrice();
                _ = MyCryptocurrencies[4].RefreshPrice();
                _ = MyCryptocurrencies[5].RefreshPrice();
                _ = MyCryptocurrencies[6].RefreshPrice();
            }
            else
            {
                _ = MyCryptocurrencies[nb].RefreshPrice();
            }

        }
    }
}
