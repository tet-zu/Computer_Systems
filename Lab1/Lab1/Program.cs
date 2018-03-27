using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    class Program
    {

        public static int getNumberOfCharacters(string file)
        {
            int count = 0;
            using (var sr = new StreamReader(file))
            {
                while (sr.Read() != -1)
                    count++;
            }
            return count;
        }

        public static SortedDictionary<char, double> calculateFrequency(string file)
        {
            int numberOfAllCharacters = getNumberOfCharacters(file);
            double[] frequencies = new double[(int)char.MaxValue];
            SortedDictionary<char, double> resultFrequency = new SortedDictionary<char, double>();
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            string fileAll = File.ReadAllText(file);
            foreach (char t in fileAll)
            {
                frequencies[(int)t]++;
            }
            for (int i = 0; i < (int)char.MaxValue; i++)
            {
                if (frequencies[i] > 0)
                {
                    Console.WriteLine("Letter: {0}  Frequency: {1}",
                    (char)i,
                    System.Math.Round(frequencies[i] * 1.0 / numberOfAllCharacters, 5)
                    );
                    resultFrequency.Add((char)i, frequencies[i] * 1.0 / numberOfAllCharacters);
                }

            }
            return resultFrequency;
        }

        public static double calculateEntropy(SortedDictionary<char, double> frequencies)
        {
            double middleEntropy = 0;
            double log;
            foreach (char c in frequencies.Keys)
            {
                if (c > 0)
                {
                    log = Math.Log(frequencies[c], 2);
                    if (log != double.NaN && !double.IsInfinity(log))
                        middleEntropy -= frequencies[c] * log;
                }
            }
            Console.WriteLine("Entropy: " + (int)middleEntropy);
            return middleEntropy;
        }

        public static void calculateInfoInText(double entropy, double allCharacters)
        {
            double amountInfoInText = entropy * allCharacters;
            Console.WriteLine("Amount info in text: " + (int)amountInfoInText/8);
        }

        public static void generateOutput(string file)
        {
            int allCharacters = getNumberOfCharacters(file);
            SortedDictionary<char, double> frequences = calculateFrequency(file);
            double middleEntropy = calculateEntropy(frequences);
            calculateInfoInText(middleEntropy, allCharacters);
        }

        static void Main(string[] args)
        {
            string file1 = "Мені тринадцятий минало.txt",
                file2 = "Казка про ріпку.txt",
                file3 = "USB.txt",
                path = @"C:\users\zubko_t\Desktop\Lab1_CS\Lab_files\";
            generateOutput(path + file1);
            generateOutput(path + file2);
            generateOutput(path + file3);
            Console.ReadKey();
        }
    }
}
