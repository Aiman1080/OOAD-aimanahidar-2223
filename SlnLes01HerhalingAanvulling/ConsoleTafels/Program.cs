using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTafels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DrukTafel(4, 8);
            DrukTafel(2, 5);

            VraagPositiefgetal();

            Console.ReadKey();
        }
        public static void DrukTafel(double getal, double tafel)
        {
            Console.WriteLine(getal + "x" + tafel + " tafel:");
            for (int i = 1; i <= tafel; i++)
            {
                double antwoord = getal * i;
                Console.WriteLine(getal + " x " + i + " = " + antwoord);
            }
            Console.WriteLine();
        }
        public static void VraagPositiefgetal()
        {
            Console.Write("Geef een getal: ");
            double getal = Convert.ToDouble(Console.ReadLine());

            if (getal <= 0)
            {
                Console.Write("Het getal moet positief zijn! ");
                VraagPositiefgetal();
            }
            else
            {
                Console.Write("geef de lengte: ");
                double tafel = Convert.ToDouble(Console.ReadLine());
                DrukTafel(getal, tafel);
            }
        }
    }
}
