using System;
using System.Text;
using static System.Console;

namespace FlyweightPattern
{
    class Program
    {
        public class Sentence
        {
            private string plaintText;
            private WordToken token = new WordToken();

            public Sentence(string plainText)
            {
                this.plaintText = plainText;
            }

            public WordToken this[int index]
            {
                get
                {
                    var word = plaintText.Split(char.Parse(" "))[index];
                    token.Start = plaintText.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                    token.End = token.Start + word.Length - 1;

                    return token;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int i = 0; i < plaintText.Length; i++)
                {
                    var c = plaintText[i];
                    if (i >= token.Start && i <= token.End && token.Capitalize)
                    {
                        c = char.ToUpper(c);
                    }
                    sb.Append(c);
                }

                return sb.ToString();
            }

            public class WordToken
            {
                public bool Capitalize;
                public int Start;
                public int End;
            }
        }

        static void Main(string[] args)
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            WriteLine(sentence);
            ReadKey();
        }
    }
}
