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

namespace WpfGebruiker
{
    /// <summary>
    /// Interaction logic for PageOntleningen.xaml
    /// </summary>
    public partial class PageOntleningen : Page
    {
        private List<Ontlening> ontleningenList = new List<Ontlening>();
        private List<Ontlening> aanvragen = new List<Ontlening>();

        private Gebruiker gebruiker;
        public PageOntleningen(Gebruiker gebruiker)
        {
            InitializeComponent();
            this.gebruiker = gebruiker;

            ToonAanvragen();
            ToonOntlening();
        }

        /*alle ontleningen tonen*/
        private void ToonOntlening()
        {
            ontleningenList = Ontlening.GetAll(gebruiker.Id).OrderByDescending(o => o.Vanaf).ToList();

            lbxOntlening.Items.Clear();
            foreach (var ontlening in ontleningenList)
            {
                string voertuigNaam = Voertuig.GetVoertuigById(ontlening.Voertuig_Id)?.Name ?? "Onbekend";
                string ontleningText = $"{voertuigNaam} -> {ontlening.Vanaf.ToString("dd-MM-yyyy")} tot {ontlening.Tot.ToString("dd-MM-yyyy")} (Status: {ontlening.Status.ToString()})";
                lbxOntlening.Items.Add(ontleningText);
            }
        }

        /*Alle aanvragen tonen*/
        private void ToonAanvragen()
        {
            aanvragen = Ontlening.GetAllByVoertuigOwnerId(gebruiker.Id).OrderByDescending(a => a.Vanaf).ToList();

            lbxAanvraag.Items.Clear();
            foreach (var aanvraag in aanvragen)
            {
                Gebruiker persoon = Gebruiker.GetById(aanvraag.Aanvragen_Id);
                string voertuigNaam = Voertuig.GetVoertuigById(aanvraag.Voertuig_Id)?.Name ?? "Onbekend";
                string aanvraagText = $"{voertuigNaam} -> {aanvraag.Vanaf.ToString("dd-MM-yyyy")} tot {aanvraag.Tot.ToString("dd-MM-yyyy")}: {persoon.Voornaam} {persoon.Achternaam} (Status: {aanvraag.Status.ToString()})";
                lbxAanvraag.Items.Add(aanvraagText);
            }
        }

        /*om de ontleningen te verwijderen*/
        private void btnAnuleren_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lbxOntlening.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < ontleningenList.Count)
            {
                Ontlening selectedOntlening = ontleningenList[selectedIndex];
                Ontlening.Delete(selectedOntlening.Id);
                ToonOntlening();
            }
        }

        /*Om de status te veranderen naar Goedgekeurd*/
        private void btnAccepteren_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = lbxAanvraag.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                int selectedIndex = lbxAanvraag.SelectedIndex;
                Ontlening selectedOntlening = aanvragen[selectedIndex];
                selectedOntlening.Status = Status.Goedgekeurd;
                Ontlening.Update(selectedOntlening);
                ToonAanvragen();
            }
        }

        /*Om de status te veranderen naar Verworpen*/
        private void btnAfwijzen_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = lbxAanvraag.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedItem))
            {
                int selectedIndex = lbxAanvraag.SelectedIndex;
                Ontlening selectedOntlening = aanvragen[selectedIndex];
                selectedOntlening.Status = Status.Verworpen;
                Ontlening.Update(selectedOntlening);
                ToonAanvragen();
            }
        }

        /*Om de informatie te tonen*/
        private void lbxAanvraag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxAanvraag.SelectedItem != null)
            {
                string selectedItem = lbxAanvraag.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedItem))
                {
                    int selectedIndex = lbxAanvraag.SelectedIndex;
                    Ontlening selectedOntlening = aanvragen[selectedIndex];
                    DateTime endDate = selectedOntlening.Tot;
                    if (endDate < DateTime.Now)
                    {
                        btnAfwijzen.IsEnabled = false;
                        btnAccepteren.IsEnabled = false;
                    }
                    else
                    {
                        btnAfwijzen.IsEnabled = true;
                        btnAccepteren.IsEnabled = true;
                    }

                    string voertuigNaam = Voertuig.GetVoertuigById(selectedOntlening.Voertuig_Id)?.Name ?? "Onbekend";
                    lblVoertuig.Content = $"Voertuig: {voertuigNaam}";

                    string periodeText = $"{selectedOntlening.Vanaf.ToString("dd-MM-yyyy")} tot {selectedOntlening.Tot.ToString("dd-MM-yyyy")}";
                    lblPeriode.Content = $"Periode: {periodeText}";

                    Gebruiker aanvrager = Gebruiker.GetById(selectedOntlening.Aanvragen_Id);
                    string aanvragerNaam = aanvrager != null ? $"{aanvrager.Voornaam} {aanvrager.Achternaam}" : "Onbekend";
                    lblAanvrager.Content = $"Aanvrager: {aanvragerNaam}";

                    lblBericht.Content = $"Bericht: {selectedOntlening.Bericht}";
                }
            }
        }

        private void lbxOntlening_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxOntlening.SelectedItem != null)
            {
                string selectedItem = lbxOntlening.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selectedItem))
                {
                    int selectedIndex = lbxOntlening.SelectedIndex;
                    Ontlening selectedOntlening = ontleningenList[selectedIndex];
                    DateTime endDate = selectedOntlening.Tot;
                    if (endDate < DateTime.Now)
                    {
                        btnAnuleren.IsEnabled = false;
                    }
                    else
                    {
                        btnAnuleren.IsEnabled = true;
                    }
                }
            }
        }
    }
}
