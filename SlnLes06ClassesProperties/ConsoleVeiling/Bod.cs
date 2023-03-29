using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVeiling
{
    internal class Bod
    {
        public Koper Koper { get; set; }
        public double Bedrag { get; set; }

        // constructor
        public Bod() { }
        public Bod(Koper k, int b)
        {
            Koper = k;
            Bedrag = b;
        }
    }
}
