using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp.Model
{
    [Serializable]
    public class TradeSpot :TradeBaseClass, INotifyPropertyChanged
    {
        private double _shares;
        public TradeSpot()
        {
            Shares = 0;
        }
        public TradeSpot(string action, double entryPrice, double shares, double exitPrice,string type) : base(type,action,0,entryPrice,0,DateTime.MinValue,exitPrice)
        {
            
            Shares = shares;
        }
        public double Shares
        {
            get => _shares;
            set
            {
                _shares = value;
                RefreshAmount();
                PropertyChangedEventHandler();
            } 
        }
        public void RefreshAmount()
        {
            if(EntryPrice > 0 && Shares > 0)
                Amount = Shares * EntryPrice;
        }
    }
}
