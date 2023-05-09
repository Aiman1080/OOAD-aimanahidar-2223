using ConsoleKassaTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEscapeGame
{
    internal class LockableItem : Item
    {
        //public string Name;
        //public string Description;
        //public string Name { get; set; }
        //public bool IsLocked { get; set; }

        public LockableItem(string nm, bool il) : base(nm, il)
        {

        }

        public LockableItem(string nm, string ds) : base(nm, ds) { }

        //public LockableItem(string nm, bool islocked)
        //{
        //    Name = nm;
        //    IsLocked = islocked;
        //}
    }
}
