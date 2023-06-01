using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Security.Cryptography;
using MyClassLibrary;

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /*Om naar de homepage te kunnen gaan*/
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtGebruikersnaam.Text;
            string password = txtPaswoord.Password;

            Gebruiker gebruiker = Gebruiker.GetLogin(email, GetSHA256Hash(password));
            if (gebruiker != null)
            {
                MainWindow mainWindow = new MainWindow(gebruiker); 
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("De gebruikersnaam / paswoord combinatie is niet correct");
            }
        }

        /*genereren met chatgpt*/
        public string GetSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
