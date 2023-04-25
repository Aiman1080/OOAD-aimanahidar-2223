using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleKassaTicket
{

    internal class Product
    {
        public string Naam { get; set; }
        public decimal Eenheidsprijs { get; set; }

        private string ontvangCode;
        public string code
        {
            get { return ontvangCode; }
            set
            {
                if (ValideerCode(value))
                {
                    ontvangCode = value;
                }
                else
                {
                    return;
                }
            }
        }

        private bool ValideerCode(string value)
        {
            return value.Length == 6 && value.StartsWith("P");
        }

        public override string ToString()
        {
            return "(" + code + ") " + Naam + " " + Eenheidsprijs;
        }

        public Product(string c, string n, decimal e)
        {
            code = c;
            Naam = n;
            Eenheidsprijs = e;
        }
    }
}
