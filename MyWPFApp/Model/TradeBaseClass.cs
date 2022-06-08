using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MyWPFApp.Model
{
    [Serializable]
    public class TradeBaseClass : INotifyPropertyChanged
    {
        private string _type;
        private string _action;
        private double _amount;
        private double _entryPrice;
        private double _pnl;
        private DateTime _actionDate;
        private double _exitPrice;

        public TradeBaseClass()
        {
            _type = "";
            _action = "";
            Amount = 0;
            EntryPrice = 0;
            Pnl = 0;
            ActionDate = DateTime.MinValue;
            ExitPrice = 0;
        }
        public TradeBaseClass(string type,string action,double amount,double entryPrice,double pnl,DateTime date,double exitPrice)
        {
            _type = type;
            _action = action;
            Amount = amount;
            EntryPrice = entryPrice;
            Pnl = pnl;
            ActionDate = date;
            ExitPrice = exitPrice;
        }
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                PropertyChangedEventHandler();
            }
        }
        public string Action
        {
            get => _action;
            set
            {
                _action = value;
                PropertyChangedEventHandler();
            }
        }
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                PropertyChangedEventHandler();
            }
        }
        public double EntryPrice
        {
            get => _entryPrice;
            set
            {
                _entryPrice = value;
                if(Type == "Long" || Type == "Short")
                    PropertyChangedEventHandler("LiquidationPrice");
                PropertyChangedEventHandler();
            }
        }
        public double Pnl
        {
            get => _pnl;
            set
            {
                _pnl = value;
                PropertyChangedEventHandler();
            }
        }
        public DateTime ActionDate
        {
            get => _actionDate;
            set
            {
                _actionDate = value;
                PropertyChangedEventHandler();

            }
        }
        public double ExitPrice
        {
            get => _exitPrice;
            set
            {
                _exitPrice = value;
                PropertyChangedEventHandler();
            }
        }
        protected void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
