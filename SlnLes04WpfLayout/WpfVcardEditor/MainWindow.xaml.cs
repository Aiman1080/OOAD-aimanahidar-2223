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
                string file = openFileDialog.FileName;
                string[] vcard = File.ReadAllLines(file);

                string filePath = openFileDialog.FileName;
                txbKaart.Text = $"Huidige kaart: {filePath}";

                foreach (string line in vcard)
                {
                    if (line.StartsWith("N;"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbAchternaam.Text = parts[2];
                        txbVoornaam.Text = parts[3];
                    }
                    else if (line.StartsWith("BDAY:"))
                    {
                        string dateFile = line.Substring(5);
                        DateTime date = DateTime.ParseExact(dateFile, "yyyyMMdd", CultureInfo.InvariantCulture);
                        datePicker.SelectedDate = date;
                    }
                    else if (line.StartsWith("GENDER:"))
                    {
                        string hulp = line.Substring(7);
                        if (hulp == "M")
                        {
                            rdnMan.IsChecked = true;
                        }
                        else if (hulp == "F")
                        {
                            rdnVrouw.IsChecked = true;
                        }
                        else if (hulp == "O")
                        {
                            rdnOnbekende.IsChecked = true;
                        }
                    }
                    else if (line.StartsWith("EMAIL;CHARSET=UTF-8;type=HOME"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbPriveEmail.Text = parts[3];
                    }
                    else if (line.StartsWith("TEL;TYPE=HOME"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbPriveTelefoon.Text = parts[2];
                    }
                    else if (line.StartsWith("LABEL;"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbBedrijf.Text = parts[3];
                    }
                    else if (line.StartsWith("ROLE;"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbJob.Text = parts[2];
                    }
                    else if (line.StartsWith("EMAIL;CHARSET=UTF-8;type=WORK"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbWerkEmail.Text = parts[3];
                    }
                    else if (line.StartsWith("TEL;TYPE=WORK"))
                    {
                        string[] parts = line.Split(';', ':');
                        txbWerkTelefoon.Text = parts[2];
                    }
                    else if (line.StartsWith("X-SOCIALPROFILE;TYPE=linkedin:"))
                    {
                        string hulp = line.Substring(30);
                        txbLinkdln.Text = hulp;
                    }
                    else if (line.StartsWith("X-SOCIALPROFILE;TYPE=facebook:"))
                    {
                        string hulp = line.Substring(30);
                        txbFacebook.Text = hulp;
                    }
                    else if (line.StartsWith("X-SOCIALPROFILE;TYPE=instagram:"))
                    {
                        string hulp = line.Substring(31);
                        txbInstagram.Text = hulp;
                    }
                    else if (line.StartsWith("X-SOCIALPROFILE;TYPE=youtube:"))
                    {
                        string hulp = line.Substring(29);
                        txbYoutube.Text = hulp;
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
                            img.CacheOption= BitmapCacheOption.OnLoad;
                            img.EndInit();
                            txtImage.Source = img;
                        }
                    }
                }
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
                MessageBox.Show("Er is een onbekende fout opgetreden. Neem contact op met de technische ondersteuning.");
            }
            clkSave.IsEnabled = false;
        }
        private void Save(string fileLocation)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation))
            {
                writer.WriteLine("BEGIN:VCARD");
                writer.WriteLine("VERSION:3.0");
                writer.WriteLine($"FN;CHARSET=UTF-8:{txbVoornaam.Text} {txbAchternaam.Text}");
                writer.WriteLine($"N;CHARSET=UTF-8:{txbAchternaam.Text};{txbVoornaam.Text};;;");

                if (txbJob.Text != "")
                {
                    writer.WriteLine($"ROLE:{txbJob.Text}");
                }

                if (datePicker.SelectedDate.HasValue)
                {
                    string bday = datePicker.SelectedDate.Value.ToString("yyyyMMdd");
                    writer.WriteLine($"BDAY:{bday}");
                }

                if (rdnMan.IsChecked == true)
                {
                    writer.WriteLine("GENDER:M");
                }
                else if (rdnVrouw.IsChecked == true)
                {
                    writer.WriteLine("GENDER:F");
                }
                else if (rdnOnbekende.IsChecked == true)
                {
                    writer.WriteLine("GENDER:O");
                }

                if (txbPriveEmail.Text != "")
                {
                    writer.WriteLine($"EMAIL;CHARSET=UTF-8;type=HOME:{txbPriveEmail.Text}");
                }

                if (txbPriveTelefoon.Text != "")
                {
                    writer.WriteLine($"TEL;TYPE=HOME:{txbPriveTelefoon.Text}");
                }

                if (txbBedrijf.Text != "")
                {
                    writer.WriteLine($"LABEL;CHARSET=UTF-8;TYPE=WORK:{txbBedrijf.Text}");
                }

                if (txbWerkEmail.Text != "")
                {
                    writer.WriteLine($"EMAIL;CHARSET=UTF-8;type=WORK:{txbWerkEmail.Text}");
                }

                if (txbWerkTelefoon.Text != "")
                {
                    writer.WriteLine($"TEL;TYPE=WORK:{txbWerkTelefoon.Text}");
                }

                if (txbLinkdln.Text != "")
                {
                    writer.WriteLine($"X-SOCIALPROFILE;TYPE=linkedin:{txbLinkdln.Text}");
                }

                if (txbFacebook.Text != "")
                {
                    writer.WriteLine($"X-SOCIALPROFILE;TYPE=facebook:{txbFacebook.Text}");
                }

                if (txbInstagram.Text != "")
                {
                    writer.WriteLine($"X-SOCIALPROFILE;TYPE=instagram:{txbInstagram.Text}");
                }

                if (txbYoutube.Text != "")
                {
                    writer.WriteLine($"X-SOCIALPROFILE;TYPE=youtube:{txbYoutube.Text}");
                }

                writer.WriteLine("END:VCARD");
                MessageBox.Show("Document werd goed opgeslagen");
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
                MessageBox.Show("Er is een fout opgetreden bij het opslaan van het bestand. Probeer het later opnieuw.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een onbekende fout opgetreden. Neem contact op met de technische ondersteuning.");
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
    }
}
