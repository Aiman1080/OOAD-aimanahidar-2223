using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    internal class Deck
    {
        int aantal = 52;
        Random random = new Random();

        public List<Kaart> Kaarten { get; set; } = new List<Kaart>();

        public Deck()
        {
            for (int i = 1; i < 13; i++)
            {
                Kaarten.Add(new Kaart(i, "S"));
                Kaarten.Add(new Kaart(i, "C"));
                Kaarten.Add(new Kaart(i, "H"));
                Kaarten.Add(new Kaart(i, "D"));
            }
        }
        public void Schudden()
        {
            int index;
            for (int tempIndex = 0; tempIndex < Kaarten.Count; tempIndex++)
            {
                index = random.Next(0, Kaarten.Count);
                Kaart kaart = Kaarten[tempIndex];
                Kaarten[tempIndex] = Kaarten[index];
                Kaarten[index] = kaart;
            }

            for (int i = 0; i < Kaarten.Count; i++)
            {
                index = random.Next(0, Kaarten.Count);
                Kaarten[i] = Kaarten[index];
            }
        }

        public Kaart NeemKaart()
        {
            Kaart gekozenKaart = Kaarten[0];
            Kaarten.RemoveAt(0);
            return gekozenKaart;
        }
    }
}
