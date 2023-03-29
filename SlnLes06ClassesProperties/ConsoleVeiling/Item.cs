using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVeiling
{
    internal class Item
    {
        public List<Bod> LstBod { get; set; }
        public Koper LaaststeKoper { get; set; }
        public string Naam { get; set; }
        public double Minimum { get; set; }
        public bool LaatstePrijs { get; set; }
        public bool IsVerkocht { get; set; }

        // constructor
        public Item() { }
        public Item(string naam, int minPrijs)
        {
            Naam = naam;
            Minimum = minPrijs;
            LaatstePrijs = false;
            IsVerkocht = false;
            LaaststeKoper = null;
            LstBod = new List<Bod>();
        }

        public void NieuweBod(Bod bod)
        {
            if (LaatstePrijs)
            {
                Console.WriteLine("De veiling is nu toe.");
            }
            if (bod.Bedrag < Minimum)
            {
                Console.WriteLine("Dit artikel heeft een minimumprijs.");
            }
            LstBod.Add(bod);
        }

        private Koper Winnaar()
        {
            if (LstBod.Count == 0)
            {
                Console.WriteLine("Er zijn geen biedingen gedaan.");
            }
            Bod prijs = LstBod[0];
            foreach (Bod b in LstBod)
            {
                if (prijs.Bedrag < b.Bedrag)
                {
                    prijs = b;
                }
            }
            return prijs.Koper;
        }
        public void Gesloten()
        {
            if (LaatstePrijs)
            {
                Console.WriteLine("Error: De veiling is nu toe.");
            }
            LaatstePrijs = true;
            LaaststeKoper = Winnaar();
            LaaststeKoper.AddItem(this);
            IsVerkocht = true; 
        }

       
    }
}

