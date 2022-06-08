using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour PopupFuture.xaml
    /// </summary>
    public partial class PopupFuture : Window,INotifyPropertyChanged
    {
        public static bool _choice;
        public static int _pourcent;
        public double _value;
        private double _sliderDollarAmount;
        public PopupFuture(double amount)
        {
            InitializeComponent();
            _choice = false;
            _value = amount;
            SliderAmount.DataContext = this;
            _sliderDollarAmount = (_value / 100) * SliderPourcent.Value;
        }
        public double SliderDollarAmount
        {
            get { return _sliderDollarAmount;}
            set
            {
                _sliderDollarAmount = value;
                PropertyChangedEventHandler();
            }
        }
        private void DoChange(object sender, RoutedEventArgs e)
        {
            _pourcent = (int)SliderPourcent.Value;
            _choice = true;
            this.Close();
        }

        private void StopChange(object sender, RoutedEventArgs e)
        {
            _choice = false;
            this.Close();
        }

        private void SliderChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderDollarAmount = SliderPourcent.Value * _value/100;
        }
        private void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
