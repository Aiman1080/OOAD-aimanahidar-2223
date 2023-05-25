using MyClassLibrary;
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

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Gebruiker gebruiker;

        public MainWindow(Gebruiker gebruiker)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;
            Main.Content = new PageHome(gebruiker);
        }

        private void btnHome_Click(object sender, object e)
        {
            Main.Content = new PageHome(gebruiker);
        }

        private void btnOntlening_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageOntleningen(gebruiker);
        }

        private void btnVoertuig_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageVoertuigen();
        }

    }
}
