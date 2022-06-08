using System;
using System.Diagnostics;


namespace MyWPFApp.Model
{
    [Serializable]
    public class TradeFuture :TradeBaseClass
    {
        private int _leverage;
        private double _totalAmount;
        public TradeFuture()
        {
            Action = "";
            Type = "";
            EntryPrice = 0;
            Amount = 0;
            _leverage = 0;
            ExitPrice = 0;
            ActionDate = DateTime.MinValue;
            Pnl = 0;
            _totalAmount = 0;
        }
        public TradeFuture(string action, double entryPrice, double amount, int leverage, double exitPrice,DateTime actionDate,string type)
        {
            Action = action;
            EntryPrice = entryPrice;
            Amount = amount;
            Leverage = leverage;
            ExitPrice = exitPrice;
            ActionDate = actionDate;
            Type = type;
            Pnl=0;
            TotalAmount = 0;
        }
        public double LiquidationPrice
        {
            get
            {
                if (this.Type == "Long")
                {
                    return (EntryPrice * (TotalAmount / EntryPrice) - Amount) / (TotalAmount / EntryPrice);
                }
                return (EntryPrice * (TotalAmount / EntryPrice) + Amount) / (TotalAmount / EntryPrice);
            }
        }
        public double TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                PropertyChangedEventHandler("LiquidationPrice");
                PropertyChangedEventHandler();
            }
        }
        public int Leverage
        {
            get => _leverage;
            set
            {
                _leverage = value;
                PropertyChangedEventHandler();
            }
    }

        public void ShowTrade()
        {
            Debug.WriteLine("Amount"+Amount);
            Debug.WriteLine("Leverage" + Leverage);
            Debug.WriteLine("TotalAmount" + TotalAmount);
            Debug.WriteLine("Pnl" + Pnl);

        }
    }
}
