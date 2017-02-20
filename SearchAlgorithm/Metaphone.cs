using System.Linq;
using System.Text;

namespace SearchAlgorithm
{
    public class Metaphone
    {
        // algorithm
        public static string GetMetaphoneCode(string word)
        {
            StringBuilder sbuilder = new StringBuilder();
            // 1. Удаляем все повторяющиеся соседние буквы, за исключением буквы C.
            bool[] flags = new bool[word.Length];
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (word[i] == word[i + 1] && word[i] != 'C')
                {
                    flags[i] = flags[i + 1] = true;
                }
            }
            for (int i = 0; i < flags.Length; i++)
            {
                if (!flags[i])
                {
                    sbuilder.Append(word[i]);
                }
            }

            // 2. Начало слова преобразовать по следующим правилам KN → N; GN → N; PN → N; AE → E; WR → R
            if (sbuilder.Length > 1)
            {
                string startString = sbuilder[0] + "" + sbuilder[1];
                if (startString.Equals("KN") || startString.Equals("GN") ||
                    startString.Equals("PN") || startString.Equals("AE") ||
                    startString.Equals("WR"))
                {
                    sbuilder.Remove(0, 1);
                }
            }
            // 3. Удаляем на конце букву B, если она идет после M.
            if (sbuilder.Length > 1 && sbuilder[sbuilder.Length - 2] == 'M' && sbuilder[sbuilder.Length - 1] == 'B')
            {
                sbuilder.Remove(sbuilder.Length - 1, 1);
            }

            // 4. Заменяем C по следующим правилам 
            // На Х: CIA → XIA, SCH → SKH, CH → XH; На S: CI → SI, CE → SE, CY → SY; На K: C → K
            sbuilder.Replace("CIA", "XIA");
            sbuilder.Replace("SCH", "SKH");
            sbuilder.Replace("CH", "XH");
            sbuilder.Replace("CI", "SI");
            sbuilder.Replace("CE", "SE");
            sbuilder.Replace("CY", "SY");
            sbuilder.Replace("C", "K");

            // 5. Заменяем D по следующим правилам
            // На J: DGE → JGE, DGY → JGY, DGI → JGY
            // На T: D → T
            sbuilder.Replace("DGE", "JGE");
            sbuilder.Replace("DGY", "JGY");
            sbuilder.Replace("DGI", "JGY");
            sbuilder.Replace("D", "T");

            // 6. Заменяем GH → H, если это буквосочетание стоит не в конце и не перед гласной.
            var charArray = sbuilder.ToString().ToCharArray();
            int cnt = 0;
            for (int i = 0; i < charArray.Length - 1; i++)
            {
                if (charArray[i] == 'G' && charArray[i + 1] == 'H' &&
                        (i < charArray.Length - 2 && !IsVowel(charArray[i + 2])))
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }

            // 7. Заменяем GN → N и GNED → NED, если эти буквосочетания стоят в конце.
            if (sbuilder.Length > 1 && sbuilder[sbuilder.Length - 2] == 'G' && sbuilder[sbuilder.Length - 1] == 'N')
            {
                sbuilder.Remove(sbuilder.Length - 2, 1);
            }
            if (sbuilder.Length > 3 && sbuilder[sbuilder.Length - 4] == 'G' && sbuilder[sbuilder.Length - 3] == 'N' &&
                sbuilder[sbuilder.Length - 2] == 'E' && sbuilder[sbuilder.Length - 1] == 'D')
            {
                sbuilder.Remove(sbuilder.Length - 4, 1);
            }

            // 8. Заменяем G по следующим правилам На J: GI → JI, GE → JE, GY → JY; на K: G → K
            sbuilder.Replace("GI", "JI");
            sbuilder.Replace("GE", "JE");
            sbuilder.Replace("GY", "JY");

            // 9. Удаляем все H, идущие после гласных, но не перед гласными.
            charArray = sbuilder.ToString().ToCharArray();
            cnt = 0;
            for (int i = 1; i < charArray.Length - 1; i++)
            {
                if (IsVowel(charArray[i - 1]) && charArray[i] == 'H' &&
                    i < charArray.Length - 1 && !IsVowel(charArray[i + 1]))
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }

            // 10. Выполняем последующие преобразования по правилам: CK → K; PH → F; Q → K; V → F; Z → S
            sbuilder.Replace("CK", "K");
            sbuilder.Replace("PH", "F");
            sbuilder.Replace("Q", "K");
            sbuilder.Replace("V", "F");
            sbuilder.Replace("Z", "S");

            // 11. Заменяем S на X: SH → XH; SIO → XIO; SIA → XIA
            sbuilder.Replace("SH", "XH");
            sbuilder.Replace("SIO", "XIO");
            sbuilder.Replace("SIA", "XIA");

            // 12. Заменяем T по следующим правилам На X: TIA → XIA, TIO → XIO; На 0: TH → 0; Удаляем: TCH → CH
            sbuilder.Replace("TIA", "XIA");
            sbuilder.Replace("TIO", "XIO");
            sbuilder.Replace("TH", "0");
            charArray = sbuilder.ToString().ToCharArray();
            cnt = 0;
            for (int i = 0; i < charArray.Length - 2; i++)
            {
                if (charArray[i] == 'T' && charArray[i + 1] == 'C' && charArray[i + 2] == 'H')
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }

            // 13. В начале слова преобразовать WH → W. Если после W нет гласной, то удалить W.
            if (sbuilder.Length > 1 && sbuilder[0] == 'W' && sbuilder[1] == 'H')
            {
                sbuilder.Remove(1, 1);
            }
            charArray = sbuilder.ToString().ToCharArray();
            cnt = 0;
            for (int i = 0; i < charArray.Length - 1; i++)
            {
                if (charArray[i] == 'W' && !IsVowel(charArray[i + 1]))
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }

            // 14. Если X в начале слова, то преобразовать X → S, иначе X → KS
            if(sbuilder.Length > 0 && sbuilder[0] == 'X')
            {
                sbuilder.Remove(0, 1);
                sbuilder.Insert(0, 'S');
            }
            else
            {
                sbuilder.Replace("X", "KS");
            }

            // 15. Удалить все Y, которые не находятся перед гласными.
            charArray = sbuilder.ToString().ToCharArray();
            cnt = 0;
            for (int i = 1; i < charArray.Length; i++)
            {
                if (charArray[i] == 'Y' && !IsVowel(charArray[i - 1]))
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }

            // 16. Удалить все гласные, кроме начальной.
            charArray = sbuilder.ToString().ToCharArray();
            cnt = 0;
            for (int i = 1; i < charArray.Length; i++)
            {
                if (IsVowel(charArray[i]))
                {
                    sbuilder.Remove(i - cnt, 1);
                    cnt++;
                }
            }
            return sbuilder.ToString();
        }

        public static int SimilarStrings(string qWord, string word)
        {
            string qCode = GetMetaphoneCode(qWord);
            string wCode = GetMetaphoneCode(word);
            int length = qCode.Length > wCode.Length ? wCode.Length : qCode.Length;
            int value = LevenshteinDistance.CalculateDistance(qCode.Substring(0, length), wCode.Substring(0, length));
            return (int) ((length - value) * 1.0 / length * 100);
        }

        private static bool IsVowel(char c)
        {
            char[] vowelArray = { 'A', 'E', 'Y', 'U', 'I', 'O' };
            return vowelArray.Contains(c);
        }
    }
}
