using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Files1();
            Files2();
        }
        private void Files1()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string startfolder1 = System.IO.Path.Combine(folderPath, "test1");

            string[] files = Directory.GetFiles(startfolder1, "*.txt");

            foreach (string fileName in files)
            {
                lbxBestanden1.Items.Add(System.IO.Path.GetFileName(fileName));
            }
        }
        private void Files2()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string startfolder2 = System.IO.Path.Combine(folderPath, "test2");

            string[] files = Directory.GetFiles(startfolder2, "*.txt");

            foreach (string fileName in files)
            {
                lbxBestanden2.Items.Add(System.IO.Path.GetFileName(fileName));
            }
        }

        private void Weergave1(string filePath)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            lbxLines1.ItemsSource = lines;
        }
        private void Weergave2(string filePath)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            lbxLines2.ItemsSource = lines;
        }

        private void lbxBestanden1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test1", (string)lbxBestanden1.SelectedItem);
            Weergave1(selectedFile);
        }

        private void lbxBestanden2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFile = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test2", (string)lbxBestanden2.SelectedItem);
            Weergave2(selectedFile);
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lbxLines1.Items.Count && i < lbxLines2.Items.Count; i++)
            {
                ListBoxItem item1 = lbxLines1.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                ListBoxItem item2 = lbxLines2.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                if (item1 != null && item2 != null)
                {
                    string[] words1 = item1.Content.ToString().Split(' ');
                    string[] words2 = item2.Content.ToString().Split(' ');

                    for (int j = 0; j < words1.Length && j < words2.Length; j++)
                    {
                        if (words1[j] != words2[j])
                        {
                            item1.Background = Brushes.Red;
                            item2.Background = Brushes.Red;
                        }
                    }
                }
            }
        }
    }
}
