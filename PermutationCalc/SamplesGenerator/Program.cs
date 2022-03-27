using System;
using System.IO;
using PermutationLibrary;

namespace SamplesGenerator
{
    class Program
    {
        /// <summary>
        /// Sample generator for benchmark
        /// </summary>
        static void Main()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutputSamples.txt");
            StreamWriter streamWriter = new StreamWriter(path, false);

            for (int n = 2; n <= Math.Pow(2, 16); n *= 2)
            {
                Console.WriteLine("running dim=" + n.ToString());
                try
                {
                    string input1 = SampleGenerator.Cycles(n);
                    string input2 = SampleGenerator.Cycles(n);
                    streamWriter.WriteLine("public const string input1_" + n + "=\"" + input1 + "\";");
                    streamWriter.WriteLine("public const string input2_" + n + "=\"" + input2 + "\";");
                    streamWriter.WriteLine("");
                }
                catch (Exception Ex)
                {
                    streamWriter.Close();
                    Console.WriteLine("Error: " + Ex.Message);
                    Console.ReadKey();
                    break;
                }
            }
            streamWriter.Close();
        }
    }
}
