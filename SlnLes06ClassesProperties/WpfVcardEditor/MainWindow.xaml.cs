using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        FileDialog openFileDialog = new OpenFileDialog();
        string path = "";
        bool changed = false;
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
        private void clkOpen_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            openFileDialog.InitialDirectory = folderPath;
            openFileDialog.Filter = "Fichiers vCard (*.vcf)|*.vcf";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                txbKaart.Text = $"Huidige kaart: {filePath}";

                Vcard card = ReadVcard(filePath);
                ShowVcard(card);
            }
            clkSave.IsEnabled = true;
        }
        private void clkSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (path != "")
                {
                    Save(path);
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.Filter = "tekstbestand (*.vcf)|*.vcf";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Save(saveFileDialog.FileName);
                        path = saveFileDialog.FileName;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("U heeft geen toestemming om het bestand op te slaan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een onbekende fout opgetreden.");
            }
            clkSave.IsEnabled = false;
        }
        private void Save(string fileLocation)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation))
            {
                Vcard card = ToVcard();
                File.WriteAllText(fileLocation, card.GenerateVcardCode());
            }
        }
        private void clkSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "vCard Files (*.vcf)|*.vcf";
                saveFileDialog.DefaultExt = ".vcf";

                if (saveFileDialog.ShowDialog() == true)
                {
                    path = saveFileDialog.FileName;
                    Save(path);
                }

                clkSave.IsEnabled = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Er is een fout opgetreden bij het opslaan van het bestand.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een onbekende fout opgetreden. ");
            }
        }
        private void Card_Changed(object sender, EventArgs e)
        {
            bool hulp = false;
            if (!string.IsNullOrEmpty(txbJob.Text))
            {
                hulp = true;
            }

            if (datePicker.SelectedDate.HasValue)
            {
                hulp = true;
            }

            if (rdnMan.IsChecked == true)
            {
                hulp = true;
            }
            else if (rdnVrouw.IsChecked == true)
            {
                hulp = true;
            }
            else if (rdnOnbekende.IsChecked == true)
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbPriveEmail.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbPriveTelefoon.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbBedrijf.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbWerkEmail.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbWerkTelefoon.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbLinkdln.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbFacebook.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbInstagram.Text))
            {
                hulp = true;
            }

            if (!string.IsNullOrEmpty(txbYoutube.Text))
            {
                hulp = true;
            }

            if (hulp == true)
            {
                changed = true;
            }

            else
            {
                changed = false;
            }
        }
        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Card_Changed(sender, e);
        }
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Card_Changed(sender, e);
        }
        private void clkNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (changed)
                {
                    MessageBoxResult ant = MessageBox.Show("onopgelsagen wijzigingen ?", "Wil je verder gaan?", MessageBoxButton.OKCancel);

                    if (ant == MessageBoxResult.OK)
                    {
                        // reset fields
                        Reset();
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("InvalidOperationException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnSelecteer_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";
            Nullable<bool> res = dlg.ShowDialog();

            if (res != null)
            {
                string select = dlg.FileName;
                BitmapImage img = new BitmapImage(new Uri(select));
                txtImage.Source = img;
                lblSelecteer.Content = select;
            }
        }

        private void ShowVcard(Vcard card)
        {
            txbAchternaam.Text = card.Achternaam;
            txbVoornaam.Text = card.Voornaam;
            datePicker.SelectedDate = card.DatePicker;
            switch (card.Gender)
            {
                case 'F': 
                    rdnVrouw.IsChecked= true;
                    break;
                case 'M':
                    rdnMan.IsChecked= true;
                    break;
                case 'O':
                    rdnOnbekende.IsChecked= true;
                    break;
            }
            txbPriveEmail.Text = card.PriveEmail;
            txbPriveTelefoon.Text = card.PriveTelefoon;
            txbBedrijf.Text = card.Bedrijf;
            txbJob.Text = card.Job;
            txbWerkEmail.Text = card.WerkEmail;
            txbWerkTelefoon.Text = card.WerkTelefoon;
            txbLinkdln.Text = card.Linkdln;
            txbFacebook.Text = card.Facebook;
            txbInstagram.Text = card.Instagram;
            txbYoutube.Text = card.Youtube;
        }

        private Vcard ReadVcard(string filePath)
        {
            Vcard card = new Vcard();

            string[] vcard = File.ReadAllLines(filePath);

            txbKaart.Text = $"Huidige kaart: {filePath}";

            foreach (string line in vcard)
            {
                if (line.StartsWith("N;"))
                {
                    string[] parts = line.Split(';', ':');
                    card.Achternaam = parts[2];
                    card.Voornaam = parts[3];
                }
                else if (line.StartsWith("BDAY:"))
                {
                    string dateFile = line.Substring(5);
                    DateTime date = DateTime.ParseExact(dateFile, "yyyyMMdd", CultureInfo.InvariantCulture);
                    card.DatePicker = date;
                }
                else if (line.StartsWith("GENDER:"))
                {
                    string[] parts = line.Split(':', ';');
                    card.Gender = Convert.ToChar(parts[1]);
                }
                else if (line.StartsWith("EMAIL;CHARSET=UTF-8;type=HOME"))
                {
                    string[] parts = line.Split(';', ':');
                    card.PriveEmail = parts[3];
                }
                else if (line.StartsWith("TEL;TYPE=HOME"))
                {
                    string[] parts = line.Split(';', ':');
                    card.PriveTelefoon = parts[2];
                }
                else if (line.StartsWith("ORG;"))
                {
                    string[] parts = line.Split(';', ':');
                    card.Bedrijf = parts[2];
                }
                else if (line.StartsWith("TITLE;"))
                {
                    string[] parts = line.Split(';', ':');
                    card.Job = parts[2];
                }
                else if (line.StartsWith("EMAIL;CHARSET=UTF-8;type=WORK"))
                {
                    string[] parts = line.Split(';', ':');
                    card.WerkEmail = parts[3];
                }
                else if (line.StartsWith("TEL;TYPE=WORK"))
                {
                    string[] parts = line.Split(';', ':');
                    card.WerkTelefoon = parts[2];
                }
                else if (line.StartsWith("X-SOCIALPROFILE;TYPE=linkedin:"))
                {
                    string hulp = line.Substring(30);
                    card.Linkdln = hulp;
                }
                else if (line.StartsWith("X-SOCIALPROFILE;TYPE=facebook:"))
                {
                    string hulp = line.Substring(30);
                    card.Facebook = hulp;
                }
                else if (line.StartsWith("X-SOCIALPROFILE;TYPE=instagram:"))
                {
                    string hulp = line.Substring(31);
                    card.Instagram = hulp;
                }
                else if (line.StartsWith("X-SOCIALPROFILE;TYPE=youtube:"))
                {
                    string hulp = line.Substring(29);
                    card.Youtube = hulp;
                }
                else if (line.StartsWith("PHOTO;"))
                {
                    string[] parts = line.Split(';', ':');
                    string type = parts[2];
                    string info = parts[3];
                    byte[] image = Convert.FromBase64String(info);
                    using (MemoryStream memo = new MemoryStream(image))
                    {
                        BitmapImage img = new BitmapImage();
                        img.BeginInit();
                        img.StreamSource = memo;
                        img.CacheOption = BitmapCacheOption.OnLoad;
                        img.EndInit();
                        txtImage.Source = img;
                    }
                }
            }
            return card;
        }

        private Vcard ToVcard()
        {
            Vcard card = new Vcard();
            card.Voornaam = txbVoornaam.Text;
            card.Achternaam = txbAchternaam.Text;
            card.DatePicker = datePicker.SelectedDate ?? DateTime.MinValue;
            if (rdnMan.IsChecked == true)
            {
                card.Gender = 'M';
            }
            else if (rdnVrouw.IsChecked == true)
            {
                card.Gender = 'F';
            }
            else if (rdnOnbekende.IsChecked == true)
            {
                card.Gender = 'O';
            }
            card.PriveEmail = txbPriveEmail.Text;
            card.PriveTelefoon = txbPriveTelefoon.Text;
            card.Bedrijf = txbBedrijf.Text;
            card.Job = txbJob.Text;
            card.WerkEmail = txbWerkEmail.Text;
            card.WerkTelefoon = txbWerkTelefoon.Text;
            card.Linkdln = txbLinkdln.Text;
            card.Facebook = txbFacebook.Text;
            card.Instagram = txbInstagram.Text;
            card.Youtube = txbYoutube.Text;

            return card;
        }

        private void Reset()
        {
            txbAchternaam.Text = "";
            txbVoornaam.Text = "";
            datePicker.SelectedDate = null;
            rdnMan.IsChecked = false;
            rdnVrouw.IsChecked = false;
            rdnOnbekende.IsChecked = false;
            txbPriveEmail.Text = "";
            txbPriveTelefoon.Text = "";
            txbBedrijf.Text = "";
            txbWerkEmail.Text = "";
            txbWerkTelefoon.Text = "";
            txbLinkdln.Text = "";
            txbFacebook.Text = "";
            txbInstagram.Text = "";
            txbYoutube.Text = "";
        }
    }
}
