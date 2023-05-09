using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEscapeGame;

namespace ConsoleKassaTicket
{
    internal class Room : Actor
    {
        // public string Name { get; }
        // public string Description { get; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Door> EasyEntryDoors { get; set; } = new List<Door>();
        public string RoomImage { get; set; }

        // constructor
        public Room() { }
        public Room(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public Room(string name, string desc, Door d) : this(name, desc)
        {
            EasyEntryDoors.Add(d);
        }

        public Room(string name, string desc, List<Door> door) : this(name, desc)
        {
            for (int i = 0; i < door.Count; i++)
            {
                EasyEntryDoors.Add(door[i]);
            }
        }
    }
}
