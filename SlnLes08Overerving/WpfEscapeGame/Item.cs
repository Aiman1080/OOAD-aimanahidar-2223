using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEscapeGame;

namespace ConsoleKassaTicket
{
    internal class Item : Actor
    {
        // public string Name { get; set; }
        // public string Description { get; set; }
        // public bool IsLocked { get; set; } = false;
        public Item Key { get; set; }
        public Item HiddenItem { get; set; }
        public bool IsPortable { get; set; } = true;

        public Item(string name, bool islocked) : base(name, islocked)
        {
            Name = name;
            IsLocked = islocked;
        }
        public Item(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
        public Item(string name, string desc, bool isp) : this(name, desc)
        {
            Name = name;
            Description = desc;
            IsPortable = isp;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
