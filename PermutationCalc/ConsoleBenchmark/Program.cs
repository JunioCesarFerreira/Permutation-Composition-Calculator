using System;
using PermutationLibrary;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace ConsoleBenchmark
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<MemoryBenchMarker>();
            Console.ReadKey();
        }
    }

    [MemoryDiagnoser]
    public class MemoryBenchMarker
    {
        public static int N = 7;
        private static readonly string[] samples1 =
        {
            Samples.input1_2,     // 0
            Samples.input1_4,     // 1
            Samples.input1_8,     // 2
            Samples.input1_16,    // 3 
            Samples.input1_32,    // 4
            Samples.input1_64,    // 5
            Samples.input1_128,   // 6
            Samples.input1_256,   // 7
            Samples.input1_1024,  // 8
            Samples.input1_2048,  // 9
            Samples.input1_4096,  // 10
            Samples.input1_8192,  // 11
            Samples.input1_16384, // 12
            Samples.input1_32768, // 13
            Samples.input1_65536  // 14
        };
        private static readonly string[] samples2 =
        {
            Samples.input2_2,
            Samples.input2_4,
            Samples.input2_8,
            Samples.input2_16,
            Samples.input2_32,
            Samples.input2_64,
            Samples.input2_128,
            Samples.input2_256,
            Samples.input2_1024,
            Samples.input2_2048,
            Samples.input2_4096,
            Samples.input2_8192,
            Samples.input2_16384,
            Samples.input2_32768,
            Samples.input2_65536
        };

        [Benchmark]
        public string Permutation_Cycles()
        {
            string input1 = samples1[N];
            string input2 = samples2[N];
            PermutationCycles p1 = new PermutationCycles(input1);
            PermutationCycles p2 = new PermutationCycles(input2);
            PermutationCycles p = PermutationCycles.Composition(p1, p2);
            return p.PrintCycles();
        }

        [Benchmark]
        public string Permutation_Codomain()
        {
            string input1 = samples1[N];
            string input2 = samples2[N];
            PermutationCodomain P1 = new PermutationCodomain(input1);
            PermutationCodomain P2 = new PermutationCodomain(input2);
            PermutationCodomain P = PermutationCodomain.Composition(P1, P2, out _);
            return P.PrintCycles();
        }
    }

}
