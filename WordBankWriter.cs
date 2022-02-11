using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace wordleSolver
{
    class WordBankWriter
    {
        public void WriteWordBankToFile(List<string> wordBank)
        {
            var textFile = @"D:\Documents\Programming\wordleWords2.txt";
            var wordBankList = WordBankToList(wordBank);

            using (StreamWriter writer = new StreamWriter(textFile))
            {
                foreach (var word in wordBankList)
                {
                    writer.WriteLine(word);
                }
                writer.Close();
            }
        }

        private List<string> WordBankToList(List<string> wordBank)
        {
            var newWordBank = new List<string>();

            foreach (var line in wordBank)
            {
                newWordBank.AddRange(line.Split(","));

            }

            for (int i = 0; i < newWordBank.Count; i++)
            {
                var newWord = "";
                foreach (var c in newWordBank[i])
                {
                    if (c != '"')
                    {
                        newWord += c;
                    }
                }
                newWordBank[i] = newWord;
            }

            return newWordBank;
        }
    }
}
