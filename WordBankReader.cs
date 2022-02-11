using System.Collections.Generic;
using System.IO;

namespace wordleSolver
{
    class WordBankReader
    {
        public List<string> GetWordBank()
        {
            var textFile = @"D:\Documents\Programming\wordleWords.txt";

            var lines = new List<string>();

            if (!File.Exists(textFile)) return new List<string>();

            using (StreamReader file = new StreamReader(textFile))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();
            }

            return lines;
        }
    }
}
