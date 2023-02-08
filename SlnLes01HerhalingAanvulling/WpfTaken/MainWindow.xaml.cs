using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
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

namespace WpfTaken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ListBoxItem> taken = new List<ListBoxItem>();
        List<ListBoxItem> verwijderen = new List<ListBoxItem>();
        List<DateTime> time = new List<DateTime>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            if (rdAdam.IsChecked == true)
            {
                text = " door: Adam)";
            }
            else if (rdBilal.IsChecked == true)
            {
                text = " door: Bilal)";
            }
            else if (rdChelsey.IsChecked == true)
            {
                text = " door: Chelsey)";
            }
            else
            {
                txtNaamKiezen.Text = "gelieve een uitvoerder te kiezen";

            }

            if (DateDatum.SelectedDate == null)
            {
                txtDatumKiezen.Text = "gelieve een deadline te kiezen";
                return;
            }

            string taak = txtTaak.Text;
            DateTime selectedDate = DateDatum.SelectedDate.Value;

            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = taak + " (deadline: " + selectedDate.ToString("dd/MM/yyyy;") + text;
            lstLijst.Items.Add(listBoxItem);

            kleuren();
            reset();
        }


        //private void DatumToevoegen(DatePicker DateDatum, ListBox lstLijst)
        //{
        //    if (DateDatum.SelectedDate != null)
        //    {
        //        DateTime selectedDate = DateDatum.SelectedDate.Value;
        //        time.Add(selectedDate);
        //        lstLijst.Items.Add(selectedDate.ToString("dd/MM/yyyy;"));
        //    }
        //    else
        //    {
        //        txtDatumKiezen.Text = "gelieve een deadline te kiezen";
        //    }
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstLijst.SelectedIndex >= 0) 
            {
                lstLijst.Items.RemoveAt(lstLijst.SelectedIndex);
                btnVerwijderen.IsEnabled = true;
            }
        }

        private void lstLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLijst.SelectedItem != null)
            {
                btnVerwijderen.IsEnabled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (lstLijst.SelectedIndex >= 0)
            {
                verwijderen.Add((ListBoxItem)lstLijst.SelectedItem);
                lstLijst.Items.RemoveAt(lstLijst.SelectedIndex);
                btnVerwijderen.IsEnabled = true;
            }
        }

        private void kleuren()
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cmbPrioriteit.SelectedItem;
            Brush brush = Brushes.Transparent;
            if (cmbPrioriteit.SelectedIndex == 0)
            {
                brush = Brushes.Green;
            }
            else if (cmbPrioriteit.SelectedIndex == 1)
            {
                brush = Brushes.Yellow;
            }
            else if (cmbPrioriteit.SelectedIndex == 2)
            {
                brush = Brushes.Red;
            }

            foreach (ListBoxItem item in lstLijst.Items)
            {
                Brush currentBrush = (Brush)item.Tag;
                if (currentBrush == null)
                {
                    item.Background = brush;
                    item.Tag = brush;
                }
                else
                {
                    item.Background = currentBrush;
                }
            }
        }
        private void reset()
        {
            txtTaak.Text = "";
            DateDatum.SelectedDate = null;
            rdAdam.IsChecked = false;
            rdBilal.IsChecked = false;
            rdChelsey.IsChecked = false;
            txtNaamKiezen.Text = "";
            txtDatumKiezen.Text = "";
            cmbPrioriteit.SelectedIndex = -1;
        }
    }
}
