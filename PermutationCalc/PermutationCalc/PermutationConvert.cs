using System;
using System.Linq;
using System.Collections.Generic;

namespace PermutationCalc
{
    /// <summary>
    /// Provides methods for manipulating data between permutation classes
    /// </summary>
    internal static class PermutationConvert
    {
        /// <summary>
        /// Checks if text is a permutation in cycle notation
        /// </summary>
        /// <param name="text">text permutation. Example: (1 3 5)(2 4)</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        public static bool Check(string text, out string Message)
        {
            List<int> intList = new List<int>();

            #region build integers list
            string[] cycles = text.Split(')');
            foreach (string cycle in cycles)
            {
                if (cycle != null && cycle != "" && cycle.Length > 1)
                {
                    string tmp = cycle.Trim('(');
                    string[] values = tmp.Split(' ');
                    foreach (string value in values)
                    {
                        if (int.TryParse(value, out int result))
                        {
                            intList.Add(result);
                        }
                        else
                        {
                            Message = "Invalid value at \'" + value + "\'. Check the syntax.";
                            return false;
                        }
                    }
                }
            }
            #endregion

            #region checks if there are duplicates
            List<int> distinct = intList.Distinct().ToList();
            distinct.Sort();
            if (distinct.Count != intList.Count)
            {
                foreach (int n in distinct)
                {
                    int count = 0;
                    foreach (int m in intList)
                    {
                        if (m == n) count++;
                    }
                    if (count > 1)
                    {
                        Message = "The number \'" + n + "\' is repeated in the permutation cycles.";
                        return false;
                    }
                }
            }
            #endregion

            #region check for missing integers
            if (intList.Max() != intList.Count)
            {
                for (int i = 1; i <= intList.Count; i++)
                {
                    bool belongs = false;
                    foreach (int n in distinct)
                    {
                        if (n == i)
                        {
                            belongs = true;
                            break;
                        }
                    }
                    if (!belongs)
                    {
                        Message = "The number \'" + i + "\' is missing from the permutation cycles.";
                        return false;
                    }
                }
            }
            #endregion

            Message = "Successfully verified.";
            return true;
        }

        /// <summary>
        /// Convert text to cycle list
        /// </summary>
        /// <param name="text">text permutation. Example: (1 3 5)(2 4)</param>
        /// <returns>list of arrays represents cycles</returns>
        public static List<int[]> DecodeCycles(string text)
        {
            string[] cycles = text.Split(')');
            List<int[]> contentsCicles = new List<int[]>();
            foreach (string cycle in cycles)
            {
                if (cycle != null && cycle != "" && cycle.Length > 1)
                {
                    string tmp = cycle.Trim('(');
                    string[] values = tmp.Split(' ');
                    List<int> intList = new List<int>();
                    // try to build a cycle
                    foreach (string value in values)
                    {
                        if (int.TryParse(value, out int result))
                        {
                            intList.Add(result);
                        }
                        else
                        {
                            throw new Exception("Invalid value at \'" + value + "\'. Check the syntax.");
                        }
                    }
                    contentsCicles.Add(intList.ToArray());
                }
            }
            return contentsCicles;
        }

        /// <summary>
        /// Check if n is contained in array
        /// </summary>
        /// <param name="n"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private static bool ArrayContains(int n, int[] array)
        {
            foreach (int i in array)
            {
                if (i == n) return true;
            }
            return false;
        }

        /// <summary>
        /// Convert codomain to cycles
        /// </summary>
        /// <param name="codomain">codomain input</param>
        /// <returns>cycle list output</returns>
        public static List<int[]> Cycles(int[] codomain)
        {
            List<int[]> cycles = new List<int[]>();
            List<int> cycle = new List<int>();
            int n = 1; // n in domain
            for (int i = 0; i < codomain.Length; i++)
            {
                cycle.Add(n); // starts building a cycle
                int start = n;
                while (codomain[n - 1] != start) // as long as there is a next one
                {
                    cycle.Add(codomain[n - 1]);
                    n = codomain[n - 1];
                    i++;
                    if (i >= codomain.Length) break; // to avoid stack overflow
                }
                cycles.Add(cycle.ToArray()); // finishes building a cycle
                cycle.Clear();
                // finds next candidate to start next cycle
                for (int m = 1; m <= codomain.Length; m++)
                {
                    bool belongs = false;
                    foreach (int[] previous in cycles)
                    {
                        if (ArrayContains(m, previous))
                        {
                            belongs = true;
                            break;
                        }
                    }
                    if (!belongs) n = m;
                }
            }
            return cycles;
        }

        /// <summary>
        /// Convert text (cycles) to codomain array
        /// </summary>
        /// <param name="text">text permutation. Example: (1 3 5)(2 4)</param>
        /// <returns>array represents codomain permutation</returns>
        public static int[] Codomain(string text)
        {
            return Codomain(DecodeCycles(text));
        }

        /// <summary>
        /// Convert cycles to codomain
        /// </summary>
        /// <param name="cycles">list cycles input</param>
        /// <returns>array output</returns>
        public static int[] Codomain(List<int[]> cycles)
        {
            // find length of permutation
            int len = 0;
            foreach (int[] a in cycles)
            {
                foreach (int n in a)
                {
                    if (n > len) len = n;
                }
            }
            /*
             * build codomiain permutation p
             * domain   (1    2    3    ... len   )
             * codoamin (p(1) p(2) p(3) ... p(len))
             */
            int[] codomain = new int[len];
            for (int i = 1; i <= len; i++)
            {
                foreach (int[] cycle in cycles)
                {
                    bool end = false; // end where finded p(i)
                    for (int j = 0; j < cycle.Length; j++)
                    {
                        if (cycle[j] == i)
                        {
                            // apply cycle permutation
                            if (j == cycle.Length - 1) codomain[i - 1] = cycle[0];
                            else codomain[i - 1] = cycle[j + 1];
                            end = true;
                            break;
                        }
                    }
                    if (end) break;
                }
            }
            return codomain;
        }
    }
}
