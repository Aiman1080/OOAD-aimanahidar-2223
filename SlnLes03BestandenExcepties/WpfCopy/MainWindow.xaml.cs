using Microsoft.Win32;
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

namespace WpfCopy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string chosenFileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnKies_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
            
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                // user picked a file and pressed OK
                chosenFileName = dialog.FileName;
                txtKies.Text = chosenFileName;
            }

        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(chosenFileName))
            {
                string content = File.ReadAllText(chosenFileName);

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
                dialog.FileName = "savefile.txt";

                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    txtGo.Text = "bestand is overgezet";
                    File.WriteAllText(dialog.FileName, content);
                }
            }

        }
    }
}
