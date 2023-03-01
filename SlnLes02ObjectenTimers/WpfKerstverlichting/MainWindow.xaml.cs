using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace WpfKerstverlichting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ellipse> lijst = new List<Ellipse>();
        bool btn = true;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            AddEllipses();

        }

        private void AddEllipses()
        {
            Random random = new Random();

            int loop = 0;
            while (loop < 40)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = Brushes.Gray; 
                ellipse.Width = 5;
                ellipse.Height = 5;

                double x = random.Next(0, 299);
                double y = random.Next(0, 424);

                if (!PixelIsWhite(imgTree, (int)x, (int)y))
                {
                    cnvTree.Children.Add(ellipse);

                    ellipse.SetValue(Canvas.LeftProperty, (double)x);
                    ellipse.SetValue(Canvas.TopProperty, (double)y);

                    loop++;

                    lijst.Add(ellipse);
                }
            }
        }

        public static bool PixelIsWhite(Image img, int x, int y)
        {
            BitmapSource source = img.Source as BitmapSource;
            Color color = Colors.White;
            CroppedBitmap cb = new CroppedBitmap(source, new Int32Rect(x, y, 1, 1));
            byte[] pixels = new byte[4];
            cb.CopyPixels(pixels, 4, 0);
            color = Color.FromRgb(pixels[2], pixels[1], pixels[0]);
            return color.ToString() == "#FFFFFFFF";
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (btn == true)
            {
                timer = new DispatcherTimer();

                timer.Interval = TimeSpan.FromMilliseconds(500);
                timer.Tick += Kleuren;
                timer.Start();
                btnSwitch.Content = "SWITCH OF";
                btn = false;
            } 
            else
            {
                foreach (Ellipse element in lijst)
                {
                    element.Fill = Brushes.Gray;
                }

                timer.Stop();
                btnSwitch.Content = "SWITCH ON";
                btn = true;
            }
        }

        private void Kleuren(object sender, EventArgs e)
        {
            Random random = new Random();

            foreach (Ellipse element in lijst)
            {
                SolidColorBrush brush;

                if (random.Next(2) == 0)
                {
                    brush = Brushes.White;
                }
                else
                {
                    brush = Brushes.Gray;
                }

                element.Fill = brush;
            }
        }
    }
}
