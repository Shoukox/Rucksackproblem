using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rucksackproblem
{
    class Rucksack
    {
        public int m; //zahl der Gegenstanden in Rucksack
        public int k; //Rucksackskapazität (in kg)

        public List<RucksackGegenstand> gegenstanden = new List<RucksackGegenstand>();

        public Rucksack(int m, int k)
        {
            this.m = m;
            this.k = k;
        }
        private void _DatenEingeben(int count)
        {
            Console.WriteLine($"Gebe {m} Gewichte und Nutzen ein. ");
            for (int i = 0; i <= count - 1; i++)
            {
                int[] splittedParsedString = Console.ReadLine().Split(" ").Select(m => int.Parse(m)).ToArray();
                gegenstanden.Add(new RucksackGegenstand(splittedParsedString[0], splittedParsedString[1]));
            }
        }
        public void Solve()
        {
            _DatenEingeben(m);
            List<List<RucksackGegenstand[]>> allGegenstanden = new List<List<RucksackGegenstand[]>>();
            for (int i = 1; i <= m - 1; i++)
            {
                allGegenstanden.Add(new List<RucksackGegenstand[]>());
                Permutatation permutatation = new Permutatation();
                permutatation.perms = permutatation.Solve(Enumerable.Range(0, m), i).Select(m => m.ToArray()).ToList();
                //permutatation.Solve1(Enumerable.Range(0, m).ToArray(), i);
                foreach (int[] num in permutatation.perms)
                {
                    allGegenstanden[i - 1].Add(num.Select(m => gegenstanden[m]).ToArray());
                }
            }

            List<RucksackGegenstand[]> allGegenstanden_Vereinigt = new List<RucksackGegenstand[]>();
            foreach(var array in allGegenstanden)
            {
                for (int i = 0; i <= array.Count - 1; i++)
                {
                    allGegenstanden_Vereinigt.Add(array[i]);
                }
            }
            allGegenstanden_Vereinigt = allGegenstanden_Vereinigt.OrderByDescending(m => GetSumOfNutzen(m)).ToList();

            for (int i = 0; i <= allGegenstanden_Vereinigt.Count-1; i++)
            {
                if (GetSumOfGewicht(allGegenstanden_Vereinigt[i]) <= k)
                {
                    Console.WriteLine("Beste Variante ist gefunden: ");
                    foreach(var item in allGegenstanden_Vereinigt[i])
                    {
                        Console.WriteLine($"item - gewicht: {item.gewicht}, nutzen: {item.nutzen}");
                    }
                    return;
                }
            }

        }
        private int GetSumOfNutzen(RucksackGegenstand[] gegenstands) => gegenstands.Select(m => m.nutzen).Sum();
        private int GetSumOfGewicht(RucksackGegenstand[] gegenstands) => gegenstands.Select(m => m.gewicht).Sum();

        public class RucksackGegenstand
        {
            public int gewicht;
            public int nutzen;

            public RucksackGegenstand(int gewicht, int nutzen)
            {
                this.gewicht = gewicht;
                this.nutzen = nutzen;
            }
        }
        public class Permutatation
        {
            public List<int[]> perms = new List<int[]>();

            //public void Solve1(int[] arrayOfNumbers, int numbersCount)
            //{
            //    if (numbersCount == 1)
            //    {
            //        int[] arr = new int[arrayOfNumbers.Length];
            //        arrayOfNumbers.CopyTo(arr, 0);
            //        perms.Add(arr);
            //    }
            //    for (int i = 0; i <= numbersCount - 1; i++)
            //    {
            //        _Swap(ref arrayOfNumbers, i, numbersCount - 1);
            //        Solve1(arrayOfNumbers, numbersCount - 1);
            //        _Swap(ref arrayOfNumbers, i, numbersCount - 1);
            //    }
            //}

            public IEnumerable<IEnumerable<T>> Solve<T>(IEnumerable<T> list, int length)
            {
                if (length == 1) return list.Select(t => new T[] { t });
                return Solve(list, length - 1)
                    .SelectMany(t => list.Where(o => !t.Contains(o)),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
            }
            public void _Print()
            {
                foreach (int[] array in perms)
                {
                    foreach (int num in array)
                    {
                        Console.Write($"{num} ");
                    }
                    Console.WriteLine();
                }
            }
            private void _Swap(ref int[] a, int i, int j)
            {
                (a[i], a[j]) = (a[j], a[i]);
            }
        }
    }
}
