using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Result
    {

        /*
         * Complete the 'pSequences' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. INTEGER n
         *  2. INTEGER p
         */

        public static long pSequences(int n, int p)
        {
            const int Modulo = 1000000007;
            SortedDictionary<int, int> baseDict = new SortedDictionary<int, int>();
            Dictionary<int, long> prevDict = new Dictionary<int, long>();
            Dictionary<int, long> nextDict = new Dictionary<int, long>();

            int squareRoot = (int)Math.Sqrt(p);

            for (int i = 1; i <= squareRoot; i++)
            {
                int division = p / i;

                baseDict[i] = division;
                prevDict[i] = 0;
                nextDict[i] = 0;

                if (!baseDict.ContainsKey(division))
                {
                    baseDict[division] = i;
                    prevDict[division] = 0;
                    nextDict[division] = 0;
                }
            }

            //printDict(baseDict);

            var baseDictKeys = baseDict.Keys;

            for (int i = 2; i <= n; i++)
            {
                long previousValue = 0;
                int previousKey = 0;
                prevDict = new Dictionary<int, long>(nextDict);

                foreach (var baseDictElement in baseDict)
                {
                    //if nothing in previous dict
                    if (i == 2)
                    {
                        if (baseDictElement.Key == 1)
                        {
                            previousValue = baseDictElement.Value;
                            nextDict[baseDictElement.Key] = previousValue % Modulo;
                        }
                        else
                        {
                            previousValue += (baseDictElement.Key - previousKey) * baseDictElement.Value;
                            nextDict[baseDictElement.Key] = previousValue % Modulo;
                        }
                    }
                    else
                    {
                        if (baseDictElement.Key == 1)
                        {
                            previousValue = prevDict[p];
                            nextDict[baseDictElement.Key] = previousValue % Modulo;
                        }
                        else
                        {
                            previousValue += (baseDictElement.Key - previousKey) * prevDict[baseDictElement.Value];
                            nextDict[baseDictElement.Key] = previousValue % Modulo;
                        }
                    }

                    previousKey = baseDictElement.Key;
                }

                //printDict(nextDict);
            }

            return nextDict[p];
        }

        public static void printNumber(List<int> nList, int n)
        {
            for (var i = 0; i < n; i++)
            {
                Console.Write(nList[i]);
            }
            Console.WriteLine();
        }

        public static void printDict(SortedDictionary<int, int> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                Console.WriteLine($"key: {dict.ElementAt(i).Key}");
                Console.WriteLine($"value: {dict.ElementAt(i).Value}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            long result = Result.pSequences(899, 1781623860);
            //long result = Result.pSequences(3, 4);
            //long result = Result.pSequences(3, 10);
            Console.WriteLine($"result {result}");
        }
    }
}
