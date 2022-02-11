using System.Collections.Generic;
using System.IO;

namespace wordleSolver
{
    class WordBankWriter
    {
        //only used once to write a usable text file for word bank
        public void WriteWordBankToFile(List<string> wordBank)
        {
            var textFile = @"D:\Documents\Programming\wordleWords2.txt";
            var wordBankList = ParseWordBankIntoUsableList(wordBank);

            using (StreamWriter writer = new StreamWriter(textFile))
            {
                foreach (var word in wordBankList)
                {
                    writer.WriteLine(word);
                }
                writer.Close();
            }
        }

        private List<string> ParseWordBankIntoUsableList(List<string> wordBank)
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
