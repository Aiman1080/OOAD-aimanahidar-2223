using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVeiling
{
    internal class Koper
    {
        public string Naam { get; set; }

        public List<Item> LstItem { get; set; }

        // constructor
        public Koper() { }
        public Koper(string n) 
        {
            Naam = n;
            LstItem = new List<Item>();
        }

        public void AddItem(Item i)
        {
            LstItem.Add(i);
        }
    }
}
