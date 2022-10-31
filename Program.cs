using System.Security.AccessControl;

namespace Rucksackproblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Wie viel Gegenstände wirst du haben? Eingabe m: ");
            int m = int.Parse(Console.ReadLine());
            Console.Write("Was ist unsere Rucksackkapazität? Eingabe k: ");
            int k = int.Parse(Console.ReadLine());

            Rucksack rucksack = new Rucksack(m, k);
            rucksack.Solve();
        }
    }
}