using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyWPFApp.Model
{
    [Serializable]
    public class Vault : INotifyPropertyChanged
    {
        private double _compound;
        private double _apy;
        private double _initBalance;
        private double _interestMonth;
        private double _interest6Month;
        private double _interestYears;
        private double _interest5Years;

        public Vault()
        {
            Compound = 0;
            Apy = 0;
            InitBalance = 0;
            Interest5Years = 0;
            Interest6Month = 0;
            InterestMonth = 0;
            InterestYears = 0;
        }


        public double Apy
        {
            get => _apy;
            set
            {
                _apy = value;
                PropertyChangedEventHandler();
            }
        }
        public double InitBalance
        {
            get => _initBalance;
            set
            {
                _initBalance = value;
                PropertyChangedEventHandler();
            }
        }
        public double Interest6Month
        {
            get => _interest6Month;
            set
            {
                _interest6Month = value;
                PropertyChangedEventHandler();
            }
        }
        public double InterestYears
        {
            get => _interestYears;
            set
            {
                _interestYears = value;
                PropertyChangedEventHandler();
            }
        }
        public double Interest5Years
        {
            get => _interest5Years;
            set
            {
                _interest5Years = value;
                PropertyChangedEventHandler();
            }
        }
        public double InterestMonth
        {
            get => _interestMonth;
            set
            {
                _interestMonth = value;
                PropertyChangedEventHandler();
            }
        }
        public double Compound
        {
            get => _compound;
            set
            {
                _compound = value;
                PropertyChangedEventHandler();
            }
        }

        public void Reset()
        {
            Apy = 0;
            InitBalance = 0;
            Interest5Years = 0;
            Interest6Month = 0;
            InterestMonth = 0;
            InterestYears = 0;
        }

        public void Calculate()
        {
            //P(1 + r / n) ^ nt 
            InterestMonth = InitBalance * Math.Pow((1 + (Apy / 100) / Compound), Compound * 1/12);
            Interest6Month = InitBalance * Math.Pow((1 + (Apy / 100) / Compound), Compound * 0.5);
            InterestYears = InitBalance * Math.Pow((1 + (Apy / 100) / Compound), Compound * 1);
            Interest5Years = InitBalance * Math.Pow((1 + (Apy / 100) / Compound), Compound * 5);

        }
        private void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
