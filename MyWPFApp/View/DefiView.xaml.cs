using System;
using System.Windows;
using System.Windows.Controls;
using MyWPFApp.Model;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour DefiView.xaml
    /// </summary>
    public partial class DefiView : UserControl
    {
        public Vault MyVault { get; set; }
        public DefiView()
        {
            InitializeComponent();
            MyVault = new Vault();
            DataContext = MyVault;
        }

        private void Calcul(object sender, RoutedEventArgs e)
        {
            if(InitBal.Text != "")
                MyVault.InitBalance = Double.Parse(InitBal.Text);
            if (ApyText.Text != "")
                MyVault.Apy = Double.Parse(ApyText.Text);
            switch (CmpPeriod.Text)
            {
                case "Daily":
                {
                    MyVault.Compound = 365;
                }
                    break;
                case "Weekly":
                {
                    MyVault.Compound = 52;
                }
                    break;
                case "Monthly":
                {
                    MyVault.Compound = 12;
                }
                    break;
                case "Half Yearly":
                {
                    MyVault.Compound = 6;
                }
                    break;
                case "Yearly":
                {
                    MyVault.Compound = 1;
                }
                    break;
            }
            MyVault.Calculate();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            MyVault.Reset();
        }
    }
}
