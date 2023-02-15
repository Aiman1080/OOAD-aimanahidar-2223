using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WpfMatchImages
{
    public partial class MainWindow : Window
    {
        Button bttn;
        int teller = 7;
        DispatcherTimer timer;
        Stopwatch stop;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            stop = new Stopwatch();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string spel = btn.Tag.ToString();

            if (bttn == null)
            {
                bttn = btn;
                stop.Start();
                timer.Start();
                btn.Opacity= 0.5;      
                btn.IsEnabled = false;
            }
            else if (bttn.Tag.ToString() == spel)
            {
                if (teller == 0)
                {
                    txtSpel.Text = "Alles gevonden!";
                    stop.Stop();
                }
                else
                {
                    txtSpel.Text = $"Juist! nog {Convert.ToString(teller--)} te gaan";
                    
                }

                btn.IsEnabled = false;
                btn.Opacity = 0.5;
                bttn = null;
            }
            else
            {
                txtSpel.Text = "fout";
                bttn.IsEnabled = true;
                bttn.Opacity = 1;
                bttn= null;
            }
        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan time = stop.Elapsed;
            txtTimer.Text = time.ToString(@"hh\:mm\:ss\.ff");
        }
    }

}

