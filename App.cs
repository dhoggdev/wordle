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
            
            var knownIncluded = "urqe";
            var knownExcluded = "aosblnt";
            var knownPosition = "*****";
            var excludedPosition = new List<string> { "uru*e", "*q***" };

            var wordBank = reader.GetWordBank();
            var possibleWords = solver.GetPossibleSolutions(knownIncluded, knownExcluded, knownPosition, excludedPosition, wordBank);
            var recommendedGuesses = solver.GetRecommendedGuesses(possibleWords, knownIncluded, wordBank);

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

