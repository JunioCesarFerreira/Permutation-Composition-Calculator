using System;
using System.Collections;
using System.Collections.Generic;

namespace PermutationCalc
{
    /// <summary>
    /// Permutation by cycles or using cycle notation
    /// </summary>
    public class PermutationCycles
    {
        // Permutation cycles
        private readonly List<int[]> cycleList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">text canonical cycle permutation. Example: (1 3 5)(2 6)(4)</param>
        public PermutationCycles(string text)
        {
            cycleList = PermutationConvert.DecodeCycles(text);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public PermutationCycles()
        {
            cycleList = new List<int[]>();
        }
        /// <summary>
        /// Print permutation cycles
        /// </summary>
        /// <returns></returns>
        public string PrintCycles()
        {
            string tmp = "";
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
        /// <returns>result=p2*p1</returns>
        public static PermutationCycles Compose(PermutationCycles p1, PermutationCycles p2)
        {
            PermutationCycles permutation = new PermutationCycles();

            MarkedElements[] M1 = new MarkedElements[p1.cycleList.Count];
            for (int i = 0; i < M1.Length; i++) M1[i] = new MarkedElements(p1.cycleList[i]);

            MarkedElements[] M2 = new MarkedElements[p2.cycleList.Count];
            for (int i = 0; i < M2.Length; i++) M2[i] = new MarkedElements(p2.cycleList[i]);

            foreach (MarkedElements m1 in M1)
            {
                foreach (MarkedElement I in m1)
                {
                    if (!I.Mark)
                    {
                        foreach (MarkedElements m2 in M2)
                        {
                            if (m2.Contain(I.Value))
                            {
                                List<int> newCicle = new List<int>();
                                int p = I.Value;
                                do
                                {
                                    if (!ListContains(p, newCicle))
                                    {
                                        newCicle.Add(p);
                                        MarkedElements.Marked(p, M1);
                                        int n = MarkedElements.NextFind(p, M1);
                                        p = MarkedElements.NextFind(n, M2);
                                    }
                                    else break;
                                } while (p != I.Value);
                                permutation.cycleList.Add(newCicle.ToArray());
                            }
                        }
                    }
                }
            }
            return permutation;
        }

        /// <summary>
        /// Check if n is contained in list
        /// </summary>
        /// <param name="n"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static bool ListContains(int n, List<int> list)
        {
            foreach (int i in list)
            {
                if (i == n) return true;
            }
            return false;
        }

        #region Inner classes used to compute the composition
        /// <summary>
        /// Represents a marked element (integer)
        /// </summary>
        private class MarkedElement
        {
            public int Value { get; set; }
            public bool Mark { get; private set; } = false;

            public MarkedElement(int value)
            {
                Value = value;
            }

            public void Marked()
            {
                Mark = true;
            }
        }

        private class MarkedElements : IEnumerable
        {
            private readonly MarkedElement[] markedElements;

            #region Indexer and Enumerator
            public MarkedElement this[int index]
            {
                get => markedElements[index];
                set => markedElements[index] = value;
            }

            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < markedElements.Length; i++)
                {
                    yield return markedElements[i];
                }
            }
            #endregion

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="values">Array initial values</param>
            public MarkedElements(int[] values)
            {
                markedElements = new MarkedElement[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    markedElements[i] = new MarkedElement(values[i]);
                }
            }

            /// <summary>
            /// Check if n is contained in markedElements
            /// </summary>
            /// <param name="n">integer value</param>
            /// <returns>true if contain n</returns>
            public bool Contain(int n)
            {
                foreach (MarkedElement element in markedElements)
                {
                    if (element.Value == n)
                    {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Marked value n in elements
            /// </summary>
            /// <param name="n"></param>
            public void Marked(int n)
            {
                foreach (MarkedElement element in markedElements)
                {
                    if (element.Value == n)
                    {
                        element.Marked();
                        break;
                    }
                }
            }

            /// <summary>
            /// Marked n in elements of indicaded array
            /// </summary>
            /// <param name="n"></param>
            /// <param name="markedArrays"></param>
            public static void Marked(int n, MarkedElements[] markedArrays)
            {
                foreach (MarkedElements markedArray in markedArrays)
                {
                    if (markedArray.Contain(n))
                    {
                        markedArray.Marked(n);
                        break;
                    }
                }
            }

            /// <summary>
            /// Find next
            /// </summary>
            /// <param name="n"></param>
            /// <param name="markedArrays"></param>
            /// <returns></returns>
            public static int NextFind(int n, MarkedElements[] markedArrays)
            {
                foreach (MarkedElements markedArray in markedArrays)
                {
                    if (markedArray.Next(n, out int NF))
                    {
                        return NF;
                    }
                }
                throw new Exception("Invalid data. Could not find value " + n + " in permutation.");
            }

            /// <summary>
            /// Search for the next one if it is in the current elements
            /// </summary>
            /// <param name="n">scope element</param>
            /// <param name="NF">next finded</param>
            /// <returns>true if finded</returns>
            public bool Next(int n, out int NF)
            {
                for (int i = 0; i < markedElements.Length; i++)
                {
                    if (markedElements[i].Value == n)
                    {
                        NF = markedElements[CiclicNextIndex(i, markedElements.Length)].Value;
                        return true;
                    }
                }
                NF = -1;
                return false;
            }
            private int CiclicNextIndex(int index, int length)
            {
                int tmp = index + 1;
                while (tmp >= length) tmp -= length;
                return tmp;
            }
        }
        #endregion
    }
}