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
using MyClassLibrary;
using System.IO;

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        private Gebruiker gebruiker;
        public PageHome(Gebruiker gebruiker)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;
            ToonVoertuig();
        }

        /*kiezen welke categorie*/
        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            ToonVoertuig();
        }

        /*naar de detailpage te gaan van getrokken of motor voertuigen*/
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Voertuig voertuig = (Voertuig)button.Tag;

            if (voertuig.Type == 1)
            {
                PageDetailsGemotoriseerd pageDetails = new PageDetailsGemotoriseerd(voertuig, gebruiker);
                Details window = new Details();
                window.detailspage.Navigate(pageDetails);
                window.Show();
            }
            else if (voertuig.Type == 2)
            {
                PageDetailsGetrokken trailerDetails = new PageDetailsGetrokken(voertuig, gebruiker);
                Details window = new Details();
                window.detailspage.Navigate(trailerDetails);
                window.Show();
            }
        }

        /*Filter de voertuigen*/
        private List<Voertuig> FilterType()
        {
            List<Voertuig> filteredVoertuigen = new List<Voertuig>();

            if (cbGemotoriseerd.IsChecked == false && cbGetrokken.IsChecked == false)
            {
                return filteredVoertuigen;
            }

            List<Voertuig> voertuigen = Voertuig.GetAll();

            voertuigen = voertuigen.Where(v => v.Eigenaar.Id != this.gebruiker.Id).ToList();

            if (cbGemotoriseerd?.IsChecked == true)
            {
                filteredVoertuigen.AddRange(voertuigen.Where(v => v.Type == 1));
            }

            if (cbGetrokken?.IsChecked == true)
            {
                filteredVoertuigen.AddRange(voertuigen.Where(v => v.Type == 2));
            }

            return filteredVoertuigen;
        }

        /*maak de voertuig*/
        private void MaakVoertuig(List<Voertuig> voertuigen)
        {
            if (voertuigen == null) return;

            VehicleWrapPanel.Children.Clear();

            foreach (var voertuig in voertuigen)
            {
                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    Width = 300,
                    Height = 150
                };

                Grid card = new Grid();
                card.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                card.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

                Foto foto = Foto.GetAllFotoByVoertuigId(voertuig.Id).FirstOrDefault();

                if (foto != null)
                {
                    Image photo = new Image();
                    photo.Source = ConvertBytesToImageSource(foto.Data);
                    photo.Height = 100;
                    Grid.SetColumn(photo, 0);
                    card.Children.Add(photo);
                }

                StackPanel textPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical
                };
                Grid.SetColumn(textPanel, 1);

                TextBlock name = new TextBlock
                {
                    Text = voertuig.Name,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                };
                textPanel.Children.Add(name);

                textPanel.Children.Add(new TextBlock { Height = 20 });

                textPanel.Children.Add(new TextBlock { Text = string.IsNullOrEmpty(voertuig.Merk) ? "Merk: onbekend" : "Merk: " + voertuig.Merk });
                textPanel.Children.Add(new TextBlock { Text = string.IsNullOrEmpty(voertuig.Model) ? "Model: onbekend" : "Model: " + voertuig.Model });
                card.Children.Add(textPanel);

                Image infoImage = new Image
                {
                    Source = new BitmapImage(new Uri("img/info.png", UriKind.Relative)),
                    Width = 20,
                };

                Button button = new Button
                {
                    Content = infoImage,
                    Tag = voertuig,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 10, 0),
                    Background = Brushes.White
                };
                button.Click += InfoButton_Click;

                textPanel.Children.Add(button);

                border.Child = card;
                VehicleWrapPanel.Children.Add(border);
            }
        }

        /*Toon alle voertuigen*/
        private void ToonVoertuig()
        {
            if (VehicleWrapPanel == null) return;
            List<Voertuig> voertuigen = FilterType();
            MaakVoertuig(voertuigen);
        }

        /*genereren met chatgpt*/
        private ImageSource ConvertBytesToImageSource(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
