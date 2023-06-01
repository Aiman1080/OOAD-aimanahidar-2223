using Microsoft.Win32;
using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ToevoegenGetrokken.xaml
    /// </summary>
    public partial class ToevoegenGetrokken : Window
    {
        /*Alle variablen*/
        private Gebruiker gebruiker;
        private Voertuig selectedVoertuig;
        private bool edit;
        private List<byte[]> imageByteDataList = new List<byte[]>();

        public ToevoegenGetrokken(Gebruiker gebruiker, bool edit, Voertuig selectedVoertuig)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;
            this.edit = edit;
            if (edit == true)
            {
                this.selectedVoertuig = selectedVoertuig;
                LoadInfo();
            }
            else if (edit == false)
            {
                this.selectedVoertuig = new GetrokkenVoertuig();
            }
        }

        /*elementen tonen voor de update*/
        private void LoadInfo()
        {
            if (selectedVoertuig is GetrokkenVoertuig getrokkenVoertuig)
            {
                txtGewicht.Text = getrokkenVoertuig.Gewicht.ToString();
                txtAfmetingen.Text = getrokkenVoertuig.Afmeting;
                TxtMaxGewicht.Text = getrokkenVoertuig.Maxbelasting.ToString();
                rdnGeremdJa.IsChecked = getrokkenVoertuig.Geremd;
            }

            txtNaam.Text = selectedVoertuig.Name;
            txtBeschrijving.Text = selectedVoertuig.Beschrijving;
            txtMerk.Text = selectedVoertuig.Merk;
            txtModel.Text = selectedVoertuig.Model;
            txtBouwjaar.Text = selectedVoertuig.Bouwjaar.ToString();

            List<Foto> fotos = Foto.GetAllFotoByVoertuigId(selectedVoertuig.Id);

            if (fotos.Count > 0)
            {
                foto1.Source = LoadImage(fotos[0].Data);
            }
            if (fotos.Count > 1)
            {
                foto2.Source = LoadImage(fotos[1].Data);
            }
            if (fotos.Count > 2)
            {
                foto3.Source = LoadImage(fotos[2].Data);
            }
        }

        /*Aanmaken van een nieuwe voertuig*/
        private void CreateVoertuig()
        {
            if (selectedVoertuig is GetrokkenVoertuig getrokkenVoertuig)
            {
                getrokkenVoertuig.Gewicht = int.TryParse(txtGewicht.Text, out int gewicht) ? gewicht : 0;
                getrokkenVoertuig.Afmeting = txtAfmetingen.Text;
                getrokkenVoertuig.Maxbelasting = int.TryParse(TxtMaxGewicht.Text, out int maxbelasting) ? maxbelasting : 0;
                getrokkenVoertuig.Geremd = rdnGeremdJa.IsChecked == true;
            }

            if (selectedVoertuig.Eigenaar == null)
            {
                selectedVoertuig.Eigenaar = new Gebruiker();
            }
            selectedVoertuig.Eigenaar.Id = gebruiker.Id;
            selectedVoertuig.Eigenaar.Id = gebruiker.Id;
            selectedVoertuig.Type = 2;
            selectedVoertuig.Name = txtNaam.Text;
            selectedVoertuig.Beschrijving = txtBeschrijving.Text;
            selectedVoertuig.Merk = txtMerk.Text;
            selectedVoertuig.Model = txtModel.Text;

            if (int.TryParse(txtBouwjaar.Text, out int bouwjaar))
            {
                selectedVoertuig.Bouwjaar = bouwjaar;
            }
        }

        /*Alles opslaan update en create*/
        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                CreateVoertuig();
                if (edit == true)
                {
                    Voertuig.Update(selectedVoertuig);
                    Foto.DeleteByVoertuigId(selectedVoertuig.Id);
                    MessageBox.Show("Het voertuig is succesvol aangepast!");
                }
                else
                {
                    int newVoertuigId = Voertuig.Create(selectedVoertuig);
                    selectedVoertuig.Id = newVoertuigId;
                    MessageBox.Show("Het voertuig is succesvol geregistreerd!");
                }

                UploadImages(selectedVoertuig.Id);

                this.Close();
            }
        }

        /*Afbeeldingen Uploaden*/
        private void btnUploaden_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string[] filePaths = openFileDialog.FileNames;

                if (filePaths.Length > 3)
                {
                    MessageBox.Show("U kunt niet meer dan 3 afbeeldingen uploaden.");
                    return;
                }

                for (int i = 0; i < filePaths.Length; i++)
                {
                    string filePath = filePaths[i];
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = new Uri(filePath);
                    bitmapImage.EndInit();

                    byte[] imageData = ConvertImage(bitmapImage);

                    imageByteDataList.Add(imageData);

                    if (i == 0)
                    {
                        foto1.Source = bitmapImage;
                    }
                    else if (i == 1)
                    {
                        foto2.Source = bitmapImage;
                    }
                    else if (i == 2)
                    {
                        foto3.Source = bitmapImage;
                    }
                }
            }
        }

        /*genereren met chatgpt*/
        public static byte[] ConvertImage(ImageSource imageSource)
        {
            var bitmapSource = (BitmapSource)imageSource;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (var memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /*genereren met chatgpt*/
        private ImageSource LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        /*Uploaden van de afbeeldingen*/
        private void UploadImages(int voertuigId)
        {
            foreach (var imageData in imageByteDataList)
            {
                Foto newFoto = new Foto();
                newFoto.Data = imageData;
                newFoto.Voertuig_id = voertuigId;
                newFoto.UploadImage();
            }
        }

        /*Terug naar de PageVoertuig*/
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*eerste afbeelding verwijderen*/
        private void btnVerwijderen1_Click(object sender, RoutedEventArgs e)
        {
            foto1.Source = null;
            if (imageByteDataList.Count > 0)
                imageByteDataList.RemoveAt(0);
        }

        /*tweede afbeelding verwijderen*/
        private void btnVerwijderen2_Click(object sender, RoutedEventArgs e)
        {
            foto2.Source = null;
            if (imageByteDataList.Count > 1)
                imageByteDataList.RemoveAt(1);
        }

        /*derde afbeelding verwijderen*/
        private void btnVerwijderen3_Click(object sender, RoutedEventArgs e)
        {
            foto3.Source = null;
            if (imageByteDataList.Count > 2)
                imageByteDataList.RemoveAt(2);
        }

        /*Formvalidatie: naam en beschrijving zijn verplichte velden.*/
        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(txtNaam.Text))
            {
                MessageBox.Show("Het veld 'Naam' is verplicht.");
                return false;
            }

            if (string.IsNullOrEmpty(txtBeschrijving.Text))
            {
                MessageBox.Show("Het veld 'Beschrijving' is verplicht.");
                return false;
            }

            return true;
        }
    }
}
