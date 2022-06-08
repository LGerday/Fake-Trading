using System.Windows;

namespace MyWPFApp.View
{
    /// <summary>
    /// Logique d'interaction pour PopUpAgree.xaml
    /// </summary>
    public partial class PopUpAgree : Window
    {
        public bool Choice;
        public PopUpAgree()
        {
            InitializeComponent();
            Choice = false;
        }

        private void DoChange(object sender, RoutedEventArgs e)
        {
            Choice = true;
            Close();
        }

        private void StopChange(object sender, RoutedEventArgs e)
        {
            Choice = false;
            Close();
        }
    }
}
