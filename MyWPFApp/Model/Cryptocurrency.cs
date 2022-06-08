using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoinGecko.Clients;
using CoinGecko.Interfaces;


namespace MyWPFApp.Model
{
    public class Cryptocurrency : INotifyPropertyChanged
    {
        private string _crypto;
        private string _ids;
        private double _price;
        private double _marketCap;
        private double _change;

        public Cryptocurrency(string name,double price,string ids,string image)
        {
            _crypto = name;
            _price = price;
            _ids = ids;
            _marketCap = 0;
            _change = 0;
            Image = image;
        }

        public string Image { get; set; }

        public string Ids
        {
            get => _ids;
            set => _ids = value;
        }
        public string Crypto
        {
            get => _crypto;
            set
            {
                _crypto = value;
                PropertyChangedEventHandler();
            }
        }
        public double MarketCap
        {
            get => _marketCap;
            set
            {
                if (_marketCap == value || value == 0) return;
                {
                    _marketCap = value;
                    PropertyChangedEventHandler();
                }
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (_price == value || value == 0) return;
                {
                    _price = value;
                    PropertyChangedEventHandler();
                }
            }
        }
        public double Change
        {
            get => _change;
            set
            {
                if (_change == value || value == 0) return;
                {
                    _change = value;
                    PropertyChangedEventHandler();
                }
            }
        }

        public async Task RefreshPrice()
        {
            ICoinGeckoClient client = CoinGeckoClient.Instance;
            var result =await client.SimpleClient.GetSimplePrice(new[] { this._ids }, new[] { "usd" }, true, false, true, false);
            MarketCap = (double) result[Ids]["usd_market_cap"];
            Change = (double) result[Ids]["usd_24h_change"];
            Price = (double) result[Ids]["usd"];

        }
        private void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
