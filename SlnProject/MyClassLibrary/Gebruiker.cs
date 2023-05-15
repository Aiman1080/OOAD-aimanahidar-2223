using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public enum Geslacht
    {
        Man,
        Vrouw
    }
    internal class Gebruiker
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Paswoord { get; set; }
        public DateTime Aanmaakdatum { get; set; }
        public byte[] Profielfoto { get; set; }
        public Geslacht Geslacht { get; set; }
    }
}
