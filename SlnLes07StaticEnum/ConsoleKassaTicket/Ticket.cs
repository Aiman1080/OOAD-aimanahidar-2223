using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKassaTicket
{
    public enum Betaalwijze
    {
        Visa,
        Cash,
        Bancontact
    }
    internal class Ticket
    {
        public Product[] Producten { get; set; }
        public Betaalwijze Betaald { get; set; }
        public string Kassier { get; set; }

        // Constructor
        public Ticket(Product[] p, Betaalwijze b, string k)
        {
            Producten = p;
            Betaald = b;
            Kassier = k;
        }
        public Ticket(Product[] p, Betaalwijze b)
        {
            Producten = p;
            Betaald = b;
        }

        // Methode om ticket af te drukken
        public void DrukTicket()
        {
            Console.WriteLine("KASSATICKET");
            Console.WriteLine("===========");
            Console.WriteLine($"Uw kassier: {Kassier}");
            Console.WriteLine("");
            decimal totaal = 0;
            foreach (var product in Producten)
            {
                Console.WriteLine($"({product.code}){product.Naam}{product.Eenheidsprijs}");
                totaal += product.Eenheidsprijs;
            }

            decimal transactieKosten = 0;

            switch (Betaald)
            {
                case Betaalwijze.Visa:
                    transactieKosten = 0.12m;
                    break;
                case Betaalwijze.Cash:
                    break;
                case Betaalwijze.Bancontact:
                    break;
            }
            Console.WriteLine("--------------");
            Console.WriteLine($"Visa Kosten: {transactieKosten}");
            Console.WriteLine($"Totaal: {transactieKosten + totaal}");
        }
    }
}
