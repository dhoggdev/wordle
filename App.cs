using System;
using System.Collections.Generic;

namespace wordleSolver
{
    class App
    {
       public static void Main()
        {
            var reader = new WordBankReader();
            var solver = new Solver();
            
            var knownIncluded = "atur";
            var knownExcluded = "pioshe";
            var knownPosition = "**t**";
            var excludedPosition = new List<string> { "*a*ur" };

            var wordBank = reader.GetWordBank();
            var possibleWords = solver.GetPossibleSolutions(knownIncluded, knownExcluded, knownPosition, excludedPosition, wordBank);
            var recommendedGuesses = solver.RecommendedGuesses(possibleWords, knownIncluded, wordBank);

            WriteListToConsole(possibleWords);
            Console.WriteLine("Recommended Guesses: ");
            WriteListToConsole(recommendedGuesses);
           
        }

        public static void WriteListToConsole(IEnumerable<string> list)
        {
            foreach (var word in list)
            {
                Console.WriteLine(word);
            }
        }
    }
}

