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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for PageDetailsGetrokken.xaml
    /// </summary>
    public partial class PageDetailsGetrokken : Page
    {
        public PageDetailsGetrokken(Voertuig voertuig)
        {
            InitializeComponent();

            lblBeschrijving.Content = $"Beschrijving: {voertuig.Beschrijving}";
            lblMerk.Content = $"Merk: {voertuig.Merk}";
            lblModel.Content = $"Model: {voertuig.Model}";
            lblBouwjaar.Content = $"Bouwjaar: {voertuig.Bouwjaar}";

            if (voertuig is GetrokkenVoertuig getrokken)
            {
                lblGewicht.Content = $"Gewicht: {getrokken.Gewicht?.ToString()}";
                lblBelasting.Content = $"Belasting: {getrokken.Maxbelasting?.ToString()}";
                lblGeremd.Content = $"Geremd: {getrokken.Geremd?.ToString()}";
                lblAfmeting.Content = $"Afmeting: {getrokken.Afmeting}";
            }

            Gebruiker eigenaar = Gebruiker.GetById(voertuig.Eigenaar.Id);
            if (eigenaar != null)
            {
                lblEigenaar.Content = eigenaar.Voornaam + " " + eigenaar.Achternaam;
            }

            List<Foto> fotos = Foto.GetAllFotoByVoertuigId(voertuig.Id);

            if (fotos.Count > 0)
            {
                photo1.Source = LoadImage(fotos[0].Data);
            }
            if (fotos.Count > 1)
            {
                photo2.Source = LoadImage(fotos[1].Data);
            }
            if (fotos.Count > 2)
            {
                photo3.Source = LoadImage(fotos[2].Data);
            }
        }
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
    }
}
