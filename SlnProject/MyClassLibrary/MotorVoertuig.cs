using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public enum Transmissie
    {
        Manueel,
        Automatisch
    }

    public enum Brandstof
    {
        Benzine,
        Diesel,
        LPG
    }
    internal class MotorVoertuig : Voertuig
    {
        public Transmissie? Transmissie { get; set; }
        public Brandstof? Brandstof { get; set; }
    }
}
