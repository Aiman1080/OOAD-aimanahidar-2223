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
using System.Windows.Shapes;

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        /*Alle variablen*/
        private Gebruiker gebruiker;
        private Voertuig selectedVehicle;
        private bool edit;
        public PopupWindow(Gebruiker gebruiker, bool edit, Voertuig selectedVehicle)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;
            this.selectedVehicle = selectedVehicle;
            this.edit = edit;
        }

        /*naar toevoegenGemotoriseerdWindow om een voertuig te maken*/
        private void btnGemotoriseerd_Click(object sender, RoutedEventArgs e)
        {
            ToevoegenGemotoriseerd toevoegenGemotoriseerdWindow = new ToevoegenGemotoriseerd(gebruiker, edit, selectedVehicle);
            toevoegenGemotoriseerdWindow.Show();
            this.Close();
        }

        /*naar toevoegenGetrokkenWindow om een voertuig te maken*/
        private void btnGetrokken_Click(object sender, RoutedEventArgs e)
        {
            ToevoegenGetrokken toevoegenGetrokkenWindow = new ToevoegenGetrokken(gebruiker, edit, selectedVehicle);
            toevoegenGetrokkenWindow.Show();
            this.Close();
        }
    }
}
