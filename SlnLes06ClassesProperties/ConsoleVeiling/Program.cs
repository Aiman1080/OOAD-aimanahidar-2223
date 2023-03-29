using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVeiling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Koper Grealish = new Koper("Grealish");
            Koper Foden = new Koper("Foden");
            Koper Silva = new Koper("Silva");

            Item Schoenen = new Item("Laatste Nike", 600);
            Item Vaas = new Item("Vaas uit Egypte", 12500);

            try
            {
                Schoenen.NieuweBod(new Bod(Grealish, 900));
                Schoenen.NieuweBod(new Bod(Foden, 880));
                Schoenen.NieuweBod(new Bod(Silva, 600));
                Schoenen.Gesloten();

                Console.WriteLine($"De volgende biedingen zijn gedaan voor {Schoenen.Naam}:");
                foreach (var bod in Schoenen.LstBod)
                {
                    Console.WriteLine($"{bod.Koper.Naam} bood {bod.Bedrag} euro.");
                }

                Bod winnendBod = Schoenen.LstBod.OrderByDescending(b => b.Bedrag).FirstOrDefault();
                if (winnendBod != null)
                {
                    Console.WriteLine($"Het winnende bod voor {Schoenen.Naam} is {winnendBod.Koper.Naam} voor {winnendBod.Bedrag} euro.");
                }
                else
                {
                    Console.WriteLine($"Er zijn geen biedingen voor {Schoenen.Naam}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Vaas.NieuweBod(new Bod(Grealish, 22000));
                Vaas.NieuweBod(new Bod(Foden, 20050));
                Vaas.NieuweBod(new Bod(Silva, 30000));
                Vaas.Gesloten();

                Console.WriteLine($"De volgende biedingen zijn gedaan voor {Vaas.Naam}:");
                foreach (var bod in Vaas.LstBod)
                {
                    Console.WriteLine($"{bod.Koper.Naam} bood {bod.Bedrag} euro.");
                }
                Console.WriteLine($"Het winnende bod voor {Vaas.Naam} is {Vaas.LaaststeKoper.Naam} voor {Vaas.LstBod[Vaas.LstBod.Count - 1].Bedrag} euro.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            PrintWinners(Schoenen);
            PrintWinners(Vaas);
            PrintKoperItems(Foden);
            PrintKoperItems(Grealish);
            PrintKoperItems(Silva);
            Console.ReadLine();
        }

        static void PrintWinners(Item i)
        {
            if (i.IsVerkocht)
            {
                Console.WriteLine($"Het item: -{i.Naam}- werd gewonnen door: {i.LaaststeKoper.Naam} voor {i.LstBod[i.LstBod.Count - 1].Bedrag} euro.");
            }
            else
            {
                Console.WriteLine($"Het item: -{i.Naam}- is niet verkocht.");
            }
        }

        static void PrintKoperItems(Koper koper)
        {
            if (koper.LstItem.Count == 0)
            {
                Console.WriteLine($"{koper.Naam} heeft geen items gewonnen.");
                return;
            }

            Console.WriteLine($"{koper.Naam} heeft de volgende items in bezit:");
            for (int i = 0; i < koper.LstItem.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {koper.LstItem[i].Naam}");
            }
            Console.WriteLine();
        }
    }
}

