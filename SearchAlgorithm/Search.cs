using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithm
{
    public class Search
    {
        public static int[] DoSearch(string query, string word)
        {
            query = Translator.DoTranslate(query);
            word = Translator.DoTranslate(word);
            return Find(query, word);
        }
        private static int[] Find(string query, string word)
        {
            var queryByWordsList = new List<string>();
            var wordByWordsList = new List<WordClass>();
            query.Split(' ', '-').ToList().ForEach(o => { if (!o.Equals("") && !o.Equals(" ")) queryByWordsList.Add(o); });
            word.Split(' ', '-').ToList().ForEach(o => { if (!o.Equals("") && !o.Equals(" ")) wordByWordsList.Add(new WordClass((o).ToString())); });

            queryByWordsList.Sort((s1, s2) => s2.Length.CompareTo(s1.Length));
            int allDistance = 0;
            int allSimilarity = 0;
            for (int i = 0; i < queryByWordsList.Count; i++)
            {
                var simValue = -1;
                var distValue = Int32.MaxValue;
                var indexW = -1;
                for (int j = 0; j < wordByWordsList.Count; j++)
                {
                    if (wordByWordsList[j].isUsed) continue;
                    int similarity = Metaphone.SimilarStrings(queryByWordsList[i], wordByWordsList[j].Word);
                    if (similarity >= simValue)
                    {
                        int distance = DamerauLevenshteinDistance.CalculateDistance(queryByWordsList[i],wordByWordsList[j].Word);
                        if (distance <= distValue)
                        {
                            simValue = similarity;
                            distValue = distance;
                            indexW = j;
                        }
                    }
                }
                // Если из слова из запроса не оказалось свободного слова
                if (indexW == -1)
                {
                    allDistance += queryByWordsList[i].Length;
                }
                else
                {
                    wordByWordsList[indexW].isUsed = true;
                    allDistance += distValue;
                    allSimilarity += simValue;
                }
            }
            return new int[] {allDistance, allSimilarity};
        }
    }

    class WordClass
    {
        public string Word { get; set; }
        public bool isUsed { get; set; }
        public WordClass(string word)
        {
            Word = word;
            isUsed = false;
        }
    }
}
