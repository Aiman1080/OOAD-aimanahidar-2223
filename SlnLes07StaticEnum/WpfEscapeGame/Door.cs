using ConsoleKassaTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEscapeGame
{
    internal class Door
    {
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public Item Key { get; set; }
        public Room AnotherRoom { get; set; }

        // constructor
        public Door() { }

        public Door(string nm, bool il) {
            Name = nm;
            IsLocked= il;
        }

        public Door(string n, bool islocked, Item k) : this(n, islocked)
        {
            Key = k;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
