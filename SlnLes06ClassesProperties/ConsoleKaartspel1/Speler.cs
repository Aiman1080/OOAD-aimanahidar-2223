using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    internal class Speler
    {
        Random random = new Random();
        public List<Kaart> Kaarten { get; set; } = new List<Kaart>();

        public string Naam 
        {
            get;
            set;
        }
        public bool HeeftNogKaarten
        {
            get { return Kaarten.Count > 0; }
        }
        public Speler(string n)
        {
            Naam = n;
        }

        public Speler(string n, List<Kaart> k)
        {
            Naam = n;
            Kaarten = k;
        }

        public Kaart LegKaart()
        {
            Kaart gelegdeKaart = Kaarten[random.Next(0, Kaarten.Count)];
            Kaarten.Remove(gelegdeKaart);
            return gelegdeKaart;
        }

    }
}
