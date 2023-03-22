using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKaartspel1
{
    internal class Kaart
    {
        private int _nummer;
        private string _kleur;
        public int Nummer 
        { 
            get { return _nummer; }
            set 
            {
                if (value < 1 || value > 13)
                {
                    throw new ArgumentOutOfRangeException("kies tussen 1 en 13");
                }
                _nummer = value;
            }
        }
        public string Kleur
        {
            get { return _kleur; }
            set
            {
                string[] kleurList = new string[] { "C", "S", "D", "H" };

                if (!kleurList.Contains(value))
                {
                    throw new ArgumentOutOfRangeException("kies tussen C, S, D of H");
                }
                _kleur = value;
            }
        }

        public Kaart() { }
        public Kaart(int n, string k)
        {
            Nummer = n;
            Kleur = k;
        }
    }
}
