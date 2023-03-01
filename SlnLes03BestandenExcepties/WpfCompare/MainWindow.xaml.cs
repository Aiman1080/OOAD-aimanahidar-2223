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

        private void lbxBestanden1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(folderPath, "myfile.txt");

            string[] regels = File.ReadAllLines(filePath);

            
        }
    }
}
