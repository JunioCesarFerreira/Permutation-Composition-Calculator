using System;
using System.Collections.Generic;

namespace PermutationCalc
{
    /// <summary>
    /// Permutation by codomain or using two-line (or one-line) notation
    /// </summary>
    public class PermutationCodomain
    {
        private readonly int[] codomain;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Codomain">Codomain one-line notation</param>
        public PermutationCodomain(int[] Codomain)
        {
            codomain = Codomain;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">text canonical cycle permutation. Example: (1 3 5)(2 6)(4)</param>
        public PermutationCodomain(string text)
        {
            codomain = PermutationConvert.Codomain(text);
        }

        /// <summary>
        /// Print permutation cycles
        /// </summary>
        /// <returns></returns>
        public string PrintCycles()
        {
            string tmp = "";
            List<int[]> cycleList = PermutationConvert.Cycles(codomain);
            foreach (int[] cycle in cycleList)
            {
                tmp += "(" + string.Join(" ", cycle) + ")";
            }
            return tmp;
        }

        /// <summary>
        /// Perform composition between two permutations (result=p2*p1)
        /// </summary>
        /// <param name="p1">first permutation to be applied</param>
        /// <param name="p2">second permutation</param>
        /// <param name="textOperation">Text operation in two-line notation</param>
        /// <returns>result=p2*p1</returns>
        public static PermutationCodomain Compose(PermutationCodomain p1, PermutationCodomain p2, out string textOperation)
        {
            if (p1.codomain.Length == p2.codomain.Length)
            {
                int[] result = new int[p1.codomain.Length];
                int[] domain = new int[p1.codomain.Length];
                for (int i = 0; i < p1.codomain.Length; i++)
                {
                    domain[i] = i + 1;
                    for (int j = 0; j < p2.codomain.Length; j++)
                    {
                        if (p1.codomain[i] == j + 1)
                        {
                            result[i] = p2.codomain[j];
                            break;
                        }
                    }
                }
                textOperation = 
                      "P1:    (" + Join(domain) + ")\r\n"
                    + "       (" + Join(p1.codomain) + ")\r\n"
                    + "P2:    (" + Join(domain) + ")\r\n" 
                    + "       (" + Join(p2.codomain) + ")\r\n"
                    + new string('-', 3 * domain.Length + 8) + "\r\n"
                    + "P2*P1: (" + Join(domain) + ")\r\n"
                    + "       (" + Join(result) + ")";

                return new PermutationCodomain(result);
            }
            else
            {
                throw new Exception("Permutations have different domains.");
            }
        }

        /// <summary>
        /// Join values to two-line notation
        /// </summary>
        /// <param name="values">values</param>
        /// <returns>text</returns>
        private static string Join(int[] values)
        {
            if (values != null || values.Length > 0)
            {
                string result = values[0].ToString("D2");
                for (int i=1; i<values.Length; i++)
                {
                    result += " " + values[i].ToString("D2");
                }
                return result;
            }
            else return "";
        }
    }
}
