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
            
            var knownIncluded = "os";
            var knownExcluded = "haterpiu";
            var knownPosition = "**o*s";
            var unknownPosition = new List<string> { "*****" };

            var wordBank = reader.GetWordBank();
            var possibleWords = solver.GetPossibleSolutions(knownIncluded, knownExcluded, knownPosition, unknownPosition, wordBank);
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

