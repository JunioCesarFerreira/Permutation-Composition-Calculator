using System;
using System.Threading;
using System.Collections.Generic;

namespace PermutationLibrary
{
    public static class SampleGenerator
    {
        public static string Cycles(int dim)
        {
            Random random = new Random();
            List<int> bag = new List<int>();
            for (int i = 0; i < dim; i++) bag.Add(i + 1);
            List<int[]> cycles = new List<int[]>();
            int k = 0;
            // building dimensional cycles
            do
            {
                int dcycle = random.Next(1, dim);
                k += dcycle;
                if (k > dim) dcycle -= (k - dim);
                cycles.Add(new int[dcycle]);
                Thread.Sleep(1);
            } while (k < dim);
            // building cycles
            foreach (int[] cycle in cycles)
            {
                for (int i = 0; i < cycle.Length; i++)
                {
                    int index = random.Next(0, bag.Count);
                    cycle[i] = bag[index];
                    bag.RemoveAt(index);
                }
            }
            // building text value
            string tmp = "";
            foreach (int[] cycle in cycles) tmp += "(" + string.Join(" ", cycle) + ")";
            return tmp;
        }
    }
}
