using System;
using System.Linq;
using System.Collections.Generic;

namespace PermutationLibrary
{
    /// <summary>
    /// Provides methods for manipulating data between permutation classes
    /// </summary>
    public static class PermutationConvert
    {
        #region Check text sintax methods
        /// <summary>
        /// Check for duplicate values in the list
        /// </summary>
        /// <param name="intList">list of all integers used in a notation</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        private static bool CheckDuplicated(List<int> intList, out string Message)
        {
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
            Message = "Successfully verified.";
            return true;
        }

        /// <summary>
        /// Check if any value is missing from the list
        /// </summary>
        /// <param name="intList">list of all integers used in a notation</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        private static bool CheckMissing(List<int> intList, out string Message)
        {
            if (intList.Max() != intList.Count)
            {
                for (int i = 1; i <= intList.Count; i++)
                {
                    bool belongs = false;
                    foreach (int n in intList)
                    {
                        if (n == i)
                        {
                            belongs = true;
                            break;
                        }
                    }
                    if (!belongs)
                    {
                        Message = "The number \'" + i + "\' is missing.";
                        return false;
                    }
                }
            }
            Message = "Successfully verified.";
            return true;
        }

        /// <summary>
        /// Apply CheckDuplicated and CheckMissing methods
        /// </summary>
        /// <param name="intList">list of all integers used in a notation</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        private static bool CheckDuplicatedAndMissing(List<int> intList, out string Message)
        {
            if (CheckDuplicated(intList, out Message))
            {
                if (CheckMissing(intList, out Message))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Checks if text is a permutation in canonical cycle notation
        /// </summary>
        /// <param name="text">text permutation in cycle notation. Example: (1 3 5)(2 4)</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        public static bool CheckCycleNotation(string text, out string Message)
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

            return CheckDuplicatedAndMissing(intList, out Message);
        }

        /// <summary>
        /// Checks if text is a permutation in one-line notation
        /// </summary>
        /// <param name="text">text permutation in one-line notation. Example: (3 4 5 2 1)</param>
        /// <param name="Message">out checking messsage</param>
        /// <returns>true if checked</returns>
        public static bool CheckOneLineNotation(string text, out string Message)
        {
            List<int> intList = new List<int>();

            #region build integers list
            string[] parts = text.Trim('(', ')').Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int value))
                {
                    intList.Add(value);
                }
                else
                {
                    Message = "Invalid value at \'" + parts[i] + "\'. Check the syntax.";
                    return false;
                }
            }
            #endregion

            return CheckDuplicatedAndMissing(intList, out Message);
        }
        #endregion

        #region Text converters
        /// <summary>
        /// Convert canonical cycle notation text to cycle list
        /// </summary>
        /// <param name="text">text permutation in cycle notation. Example: (1 3 5)(2 4)</param>
        /// <returns>list of arrays represents cycles</returns>
        public static List<int[]> CycleNotationToCycles(string text)
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
        /// Convert canonical cycle notation text to codomain array
        /// </summary>
        /// <param name="text">text permutation in cycle notation. Example: (1 3 5)(2 4)</param>
        /// <returns>array represents codomain permutation</returns>
        public static int[] CycleNotationToCodomain(string text)
        {
            return Codomain(CycleNotationToCycles(text));
        }

        /// <summary>
        /// Convert one-line notation text to codomain array
        /// </summary>
        /// <param name="text">text permutation in one-line notation. Example: (3 4 5 2 1)</param>
        /// <returns>array represents codomain permutation</returns>
        public static int[] OneLineNotationToCodomain(string text)
        {
            string[] parts = text.Trim('(', ')').Split(' ');
            int[] result = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int value))
                {
                    result[i] = value;
                }
                else
                {
                    throw new Exception("Invalid value at \'" + parts[i] + "\'. Check the syntax.");
                }
            }
            return result;
        }

        /// <summary>
        /// Convert one-line notation text to cycle list
        /// </summary>
        /// <param name="text"><text permutation in one-line notation. Example: (3 4 5 2 1)/param>
        /// <returns>list of arrays represents cycles</returns>
        public static List<int[]> OneLineNotationToCycles(string text)
        {
            return Cycles(OneLineNotationToCodomain(text));
        }
        #endregion

        #region Cycle(Codomain) to Codomain(Cycle) converters
        /// <summary>
        /// Check if n is contained in array
        /// </summary>
        /// <param name="n"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private static bool ArrayContains(int n, int[] array)
        {
            foreach (int i in array) if (i == n) return true;
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
        #endregion
    }
}
