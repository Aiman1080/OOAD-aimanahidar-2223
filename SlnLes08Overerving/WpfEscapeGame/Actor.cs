using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEscapeGame
{
    internal class Actor
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; } = false;

        public Actor() { }

        public Actor(string n, bool i)
        {
            Name = n;
            IsLocked = i;
        }

        public Actor(string n, string d)
        {
            Name = n;
            Description = d;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
