using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKassaTicket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string naam = "Annie";

            Product eerste = new Product("P02384", " bananen: ", 1.75m);
            Product tweede = new Product("P01820", " brood: ", 2.10m);
            Product derde = new Product("P45612", " kaas: ", 3.99m);
            Product vierde = new Product("P98754", " koffie : ", 4.10m);

            Product[] producten = new Product[] { eerste, tweede, derde, vierde};

            Ticket ticket = new Ticket(producten, Betaalwijze.Visa, naam);
            ticket.DrukTicket();

            Console.ReadLine();
        }
    }
}
