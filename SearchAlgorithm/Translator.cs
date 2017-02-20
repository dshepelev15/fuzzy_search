using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithm
{
    public class Translator
    {
        public static string DoTranslate(string word)
        {
            word = word.ToUpper();
            word = ConvertEnglishWordToRussia(word);
            Dictionary<char, string> hello = new Dictionary<char, string>();
            hello.Add('A', "A");
            hello.Add('Б', "B");
            hello.Add('В', "V");
            hello.Add('Г', "G");
            hello.Add('Д', "D");
            hello.Add('Е', "E");
            hello.Add('Ё', "E");
            hello.Add('Ж', "J");
            hello.Add('З', "Z");
            hello.Add('И', "I");
            hello.Add('Й', "Y");
            hello.Add('К', "K");
            hello.Add('Л', "L");
            hello.Add('М', "M");
            hello.Add('Н', "N");
            hello.Add('О', "O");
            hello.Add('П', "P");
            hello.Add('Р', "R");
            hello.Add('С', "S");
            hello.Add('Т', "T");
            hello.Add('У', "U");
            hello.Add('Ф', "F");
            hello.Add('Х', "H");
            hello.Add('Ц', "TS");
            hello.Add('Ч', "CH");
            hello.Add('Ш', "SH");
            hello.Add('Щ', "SCH");
            hello.Add('Ъ', "");
            hello.Add('Ы', "YI");
            hello.Add('Ь', "");
            hello.Add('Э', "E");
            hello.Add('Ю', "YU");
            hello.Add('Я', "YA");
            hello.Add(' ', " ");
            hello.Add('-', " ");


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                string value;
                if (hello.TryGetValue(word[i], out value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(word[i]);
                }
            }
            return sb.ToString();
        }

        public static string ConvertEnglishWordToRussia(string word)
        {
            word = word.ToUpper();
            Dictionary<char, char> hello = new Dictionary<char, char>();
            hello.Add('Q', 'Й');
            hello.Add('W', 'Ц');
            hello.Add('E', 'У');
            hello.Add('R', 'К');
            hello.Add('T', 'Е');
            hello.Add('Y', 'Н');
            hello.Add('U', 'Г');
            hello.Add('I', 'Ш');
            hello.Add('O', 'Щ');
            hello.Add('P', 'З');
            hello.Add('[', 'Х');
            hello.Add(']', 'Ъ');
            hello.Add('A', 'Ф');
            hello.Add('S', 'Ы');
            hello.Add('D', 'В');
            hello.Add('F', 'А');
            hello.Add('G', 'П');
            hello.Add('H', 'Р');
            hello.Add('J', 'О');
            hello.Add('K', 'Л');
            hello.Add('L', 'Д');
            hello.Add(';', 'Ж');
            hello.Add('\'', 'Э');
            hello.Add('Z', 'Я');
            hello.Add('X', 'Ч');
            hello.Add('C', 'С');
            hello.Add('V', 'М');
            hello.Add('B', 'И');
            hello.Add('N', 'Т');
            hello.Add('M', 'Ь');
            hello.Add(',', 'Б');
            hello.Add('.', ' ');
            hello.Add('`', 'Ё');

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                char value;
                if (hello.TryGetValue(word[i], out value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(word[i]);
                }
            }
            return sb.ToString();
        }
    }
}
