using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfVcardEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clkAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutVenster().Show();
        }

        private void clkExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("ben je zeker dat je de applicatie wil afsluiten?", "Toepassing sluiten", MessageBoxButton.OKCancel);

            if (msg == MessageBoxResult.OK) 
            {
                Application.Current.MainWindow.Close();
            }

        }

    }
}
