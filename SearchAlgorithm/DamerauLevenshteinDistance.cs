using System;

namespace SearchAlgorithm
{
    public class DamerauLevenshteinDistance
    {
        public static int CalculateDistance(string s, string t)
        {
            s = s.ToLower();
            t = t.ToLower();
            int[,] array = new int[s.Length + 1, t.Length + 1];
            for (int i = 0; i <= s.Length; i++)
            {
                for (int j = 0; j <= t.Length; j++)
                {
                    if (Math.Min(i, j) == 0)
                    {
                        array[i, j] = Math.Max(i, j);
                        continue;
                    }
                    int a = array[i, j - 1] + 1;
                    int b = array[i - 1, j] + 1;
                    int c = array[i - 1, j - 1] + ((s[i - 1].Equals(t[j - 1])) ? 0 : 1);
                    int d = Int32.MaxValue;
                    if (i > 1 && j > 1 && s[i - 2].Equals(t[j - 1]) && s[i - 1].Equals(t[j - 2]))
                    {
                        d = array[i - 2, j - 2] + 1;
                    }
                    array[i, j] = Min(a, b, c, d);
                }
            }
            return array[s.Length, t.Length];
        }

        private static int Min(int a, int b, int c, int d)
        {
            if (a <= b && a <= c && a <= d) return a;
            if (b <= a && b <= c && b <= d) return b;
            if (c <= a && c <= b && a <= d) return c;
            return d;
        }
    }
}
