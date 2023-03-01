using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfFileInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBestand_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
            string chosenFileName;
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                // gebruiker koos een bestand en drukte op OK
                chosenFileName = dialog.FileName;
                FileInfo fi = new FileInfo(chosenFileName);

                string fileContent = File.ReadAllText(chosenFileName); // bestandsinhoud lezen

                // tel het aantal woorden in het document
                int teller = 0;
                MatchCollection matches = Regex.Matches(fileContent, @"[\S]+");
                teller = matches.Count;

                // vind de drie meest voorkomende woorden
                var frequencyList = new Dictionary<string, int>();
                string[] words = fileContent.Split(' ');
                foreach (string word in words)
                {
                    if (frequencyList.ContainsKey(word))
                    {
                        frequencyList[word]++;
                    }
                    else
                    {
                        frequencyList.Add(word, 1);
                    }
                }
                var topThreeWords = frequencyList.OrderByDescending(pair => pair.Value).Take(3);

                txtText.Text = $@"
bestandsnaam: {fi.Name}
extensie: {fi.Extension}
gemaakt op: {fi.CreationTime.ToString()}
mapnaam: {fi.Directory.Name}
aantal woorden: {teller}
meest voorkomend: {string.Join(", ", topThreeWords.Select(pair => pair.Key))}
";
            }
        }
    }
}
