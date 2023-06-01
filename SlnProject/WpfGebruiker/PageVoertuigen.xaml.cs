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
    /// Interaction logic for PageVoertuigen.xaml
    /// </summary>
    public partial class PageVoertuigen : Page
    {
        private Gebruiker gebruiker;
        public PageVoertuigen(Gebruiker gebruiker)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;

            List<Voertuig> gebruikerVoertuigen = Voertuig.GetByGebruikerId(gebruiker.Id);

            VoertuigMaken(gebruikerVoertuigen);
        }

        /*het maken van de voertuig*/
        private void VoertuigMaken(List<Voertuig> voertuigen)
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

                StackPanel buttonPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = Brushes.White
                };

                Button btnEdit = new Button
                {
                    Tag = voertuig,
                    Margin = new Thickness(0, 0, 10, 0),
                    Background = Brushes.White
                };
                Image imgEdit = new Image
                {
                    Source = new BitmapImage(new Uri("img/edit.png", UriKind.Relative)),
                    Width = 20,
                    Height = 20
                };
                btnEdit.Content = imgEdit;
                btnEdit.Click += BtnEdit_Click;
                buttonPanel.Children.Add(btnEdit);

                Button btnDelete = new Button
                {
                    Tag = voertuig,
                    Margin = new Thickness(0, 0, 10, 0),
                    Background = Brushes.White
                };
                Image imgDelete = new Image
                {
                    Source = new BitmapImage(new Uri("img/delete.png", UriKind.Relative)),
                    Width = 20,
                    Height = 20
                };
                btnDelete.Content = imgDelete;
                btnDelete.Click += BtnDelete_Click;
                buttonPanel.Children.Add(btnDelete);

                Button btnInfo = new Button
                {
                    Tag = voertuig,
                    Margin = new Thickness(0, 0, 10, 0),
                    Background = Brushes.White
                };
                Image imgInfo = new Image
                {
                    Source = new BitmapImage(new Uri("img/info.png", UriKind.Relative)),
                    Width = 20,
                    Height = 20
                };
                btnInfo.Content = imgInfo;
                btnInfo.Click += BtnInfo_Click;
                buttonPanel.Children.Add(btnInfo);

                textPanel.Children.Add(buttonPanel);

                border.Child = card;
                VehicleWrapPanel.Children.Add(border);
            }
        }

        /*naar de PopupWindow om te kiezen tussen getrokken en motor*/
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            bool edit = false;
            Voertuig selectedVehicle = null;
            PopupWindow popup = new PopupWindow(gebruiker, edit, selectedVehicle);
            popup.Show();
        }

        /*om naar de edit te gaan*/
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Voertuig selectedVehicle = (Voertuig)((Button)sender).Tag;
            bool edit = true;

            if (selectedVehicle.Type == 1)
            {
                ToevoegenGemotoriseerd window = new ToevoegenGemotoriseerd(gebruiker, edit, selectedVehicle);
                window.Show();
            } else if (selectedVehicle.Type == 2)
            {
                ToevoegenGetrokken window = new ToevoegenGetrokken(gebruiker, edit, selectedVehicle);
                window.Show();
            }
        }

        /*om de voertuig te Deleten en refresh deze pagina*/
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Voertuig selectedVehicle = (Voertuig)((Button)sender).Tag;

            Ontlening.DeleteByVoertuigId(selectedVehicle.Id);

            Foto.DeleteByVoertuigId(selectedVehicle.Id);

            Voertuig.Delete(selectedVehicle.Id);

            List<Voertuig> gebruikerVoertuigen = Voertuig.GetByGebruikerId(gebruiker.Id);
            VoertuigMaken(gebruikerVoertuigen);
        }

        /*Om de informatie te kunnen zien*/
        private void BtnInfo_Click(object sender, RoutedEventArgs e)
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
