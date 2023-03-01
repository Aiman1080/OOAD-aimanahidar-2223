using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCsv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            string csvPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\wedstrijden.csv";

            const int aantalWedstrijden = 100;

            string[] spellen = { "schaak", "dammen", "backgammon" };
            string[] spelers = { "Zakaria", "Saleha", "Indra", "Ralph", "Francisco", "Marie" };

            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                for (int i = 0; i < aantalWedstrijden; i++)
                {
                    int resultaat1 = random.Next(3);
                    int resultaat2 = 2 - resultaat1;

                    string speler1 = spelers[random.Next(spelers.Length)];
                    string speler2 = spelers[random.Next(spelers.Length)];

                    while (speler1 == speler2)
                    {
                        speler2 = spelers[random.Next(spelers.Length)];
                    }

                    string spel = spellen[random.Next(spellen.Length)];

                    writer.WriteLine($"{i + 1};{speler1};{speler2};{spel};{resultaat1}-{resultaat2};");
                }
            }

            Console.WriteLine($"Weggeschreven naar {csvPath}");

            Console.ReadLine();
        }
    }
}
