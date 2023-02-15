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
            Console.WriteLine(DrukTafel(4, 8));
            Console.WriteLine(DrukTafel(2, 5));

            double eersteGetal = VraagPositiefGetal("Geef een getal: ");
            double tweedeGetal = VraagPositiefGetal("Geef een lengte: ");
            Console.WriteLine(DrukTafel(eersteGetal, tweedeGetal));

            Console.ReadKey();
        }

        public static string DrukTafel(double getal, double tafel)
        {
            string output = $"{getal}x{tafel} tafel:";
            for (double i = 1; i <= tafel; i++)
            {
                double antwoord = getal * i;
                output += Environment.NewLine + $"{getal} x {i} = {antwoord}";
            }
            return output + Environment.NewLine;
        }

        private static double VraagPositiefGetal(string bericht)
        {
            double getal = -1;
            do
            {
                Console.Write(bericht);
                bool teken = double.TryParse(Console.ReadLine(), out getal);
                if (!teken || getal < 0)
                {
                    Console.WriteLine("Het getal moet positief zijn!");
                }
            }
            while (getal < 0);
            return getal;
        }
    }
}