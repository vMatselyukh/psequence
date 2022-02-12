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

        public const int Modulo = 1000000007;

        public static long pSequences(int n, int p)
        {
            const int Modulo = 1000000007;
            SortedDictionary<int, int> sortedBaseDict = new SortedDictionary<int, int>();
            Dictionary<int, int> baseDict = new Dictionary<int, int>();
            Dictionary<int, int> diffDict = new Dictionary<int, int>();
            Dictionary<int, long> prevDict = new Dictionary<int, long>();
            Dictionary<int, long> nextDict = new Dictionary<int, long>();


            int squareRoot = (int)Math.Sqrt(p);

            for (int i = 1; i <= squareRoot; i++)
            {
                int division = p / i;

                sortedBaseDict[i] = division;
                prevDict[i] = 0;
                nextDict[i] = 0;

                if (!sortedBaseDict.ContainsKey(division))
                {
                    sortedBaseDict[division] = i;
                    prevDict[division] = 0;
                    nextDict[division] = 0;
                }
            }

            baseDict = new Dictionary<int, int>(sortedBaseDict);

            var baseDictKeys = baseDict.Keys.ToList();//baseDict.Keys.ToHashSet();
            var baseDictKeysCount = baseDict.Count;

            for (int i = 0; i < baseDictKeysCount; i++)
            {
                var currentKey = baseDictKeys[i];
                var previousKey = i == 0 ? 0 : baseDictKeys[i - 1];

                diffDict[i] = currentKey - previousKey;
            }


            for (int i = 2; i <= n; i++)
            {
                long previousValue = 0;
                prevDict = new Dictionary<int, long>(nextDict);

                for (int j = 0; j < baseDictKeysCount; j++)
                {
                    //if nothing in previous dict
                    if (i == 2)
                    {
                        if (baseDictKeys[j] == 1)
                        {
                            previousValue = baseDict[baseDictKeys[j]];
                            nextDict[baseDictKeys[j]] = previousValue > Modulo ? previousValue % Modulo : previousValue;
                        }
                        else
                        {
                            previousValue += diffDict[j] * baseDict[baseDictKeys[j]];
                            nextDict[baseDictKeys[j]] = previousValue > Modulo ? previousValue % Modulo : previousValue;
                        }
                    }
                    else
                    {
                        if (baseDictKeys[j] == 1)
                        {
                            previousValue = prevDict[p];
                            nextDict[baseDictKeys[j]] = previousValue > Modulo ? previousValue % Modulo : previousValue;
                        }
                        else
                        {
                            previousValue += diffDict[j] * prevDict[baseDictKeys[baseDictKeysCount - j - 1]];
                            nextDict[baseDictKeys[j]] = previousValue > Modulo ? previousValue % Modulo : previousValue;
                        }
                    }
                }

                //printDict(nextDict);
            }

            return nextDict[p];
        }

        public static long pSequencesV2(int n, int p)
        {
            SortedDictionary<int, int> sortedBaseDict = new SortedDictionary<int, int>();
            Dictionary<int, int> baseDict = new Dictionary<int, int>();
            Dictionary<int, int> diffDict = new Dictionary<int, int>();
            Dictionary<string, long> acumulativeDict = new Dictionary<string, long>();
            Dictionary<int, long> prevDict = new Dictionary<int, long>();
            Dictionary<int, long> nextDict = new Dictionary<int, long>();


            int squareRoot = (int)Math.Sqrt(p);

            for (int i = 1; i <= squareRoot; i++)
            {
                int division = p / i;

                sortedBaseDict[i] = division;

                if (!sortedBaseDict.ContainsKey(division))
                {
                    sortedBaseDict[division] = i;
                }
            }

            baseDict = new Dictionary<int, int>(sortedBaseDict);

            var baseDictKeys = baseDict.Keys.ToList();
            var baseDictKeysCount = baseDict.Count;

            for (int i = 0; i < baseDictKeysCount; i++)
            {
                var currentKey = baseDictKeys[i];
                var previousKey = i == 0 ? 0 : baseDictKeys[i - 1];

                diffDict[currentKey] = currentKey - previousKey;
            }

            return recursion(acumulativeDict, diffDict, baseDict, baseDictKeys, baseDictKeysCount, n + 1, 1);
        }

        public static long recursion(Dictionary<string, long> acumulativeDict, Dictionary<int, int> diffDict, Dictionary<int, int> baseDict, List<int> baseDictKeys, int baseDictKeysCount, int n, int p)
        {
            int currentKey = p;
            var key = $"{n},{currentKey}";
            if (acumulativeDict.ContainsKey(key))
            {
                return acumulativeDict[key];
            }
            else if (n == 2)
            {
                return baseDict[p];
            }


            long sum = 0;            

            int index = 0;

            currentKey = baseDictKeys[index];
            do
            {
                long recursionResult = diffDict[currentKey] * recursion(acumulativeDict, diffDict, baseDict, baseDictKeys, baseDictKeysCount, n - 1, currentKey);
                        
                if (recursionResult > Modulo)
                {
                    recursionResult %= Modulo;
                }
                    
                sum += recursionResult;

                index++;
            }
            while (index < baseDictKeysCount && (currentKey = baseDictKeys[index]) <= baseDict[p]);

            key = $"{n},{p}";
            acumulativeDict[key] = sum;

            return sum;
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
            //long result = Result.pSequences(899, 1178162386);
            //long result = Result.pSequences(899, 78162386);
            //long result = Result.pSequences(3, 4);
            //long result = Result.pSequences(3, 10); //147
            //long result = Result.pSequences(4, 10); //544

            //################  v2  ################
            //long result = Result.pSequences(899, 1178162386);
            long result = Result.pSequencesV2(899, 78162386);
            //long result = Result.pSequencesV2(3, 4);
            //long result = Result.pSequencesV2(3, 10); //147
            //long result = Result.pSequencesV2(4, 10); //544
            //long result = Result.pSequencesV2(500, 1000); //544

            Console.WriteLine($"result {result}");
        }
    }
}
