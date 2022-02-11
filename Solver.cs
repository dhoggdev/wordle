using System;
using System.Collections.Generic;
using System.Linq;

namespace wordleSolver
{
    class Solver
    {
        public IEnumerable<string> GetPossibleSolutions(
            string included,
            string excluded,
            string known,
            List<string> unknown,
            List<string> wordBank)
        {
            var narrowList = wordBank
                .Where(word => included.Where(c => word.Contains(c)).Count() == included.Count())
                .Where(word => excluded.Where(c => word.Contains(c)).Count() == 0);

            for (var i = 0; i < known.Length; i ++)
            {
                if (known[i] != '*')
                {
                    narrowList = narrowList.Where(word => word[i].Equals(known[i]));
                }
            }

            foreach (var fstring in unknown)
            {
                for (var i = 0; i < fstring.Length; i++)
                {
                    if (fstring[i] != '*')
                    {
                        narrowList = narrowList.Where(word => !word[i].Equals(fstring[i]));
                    }
                }
            }

            return narrowList;
        }

        public IEnumerable<string> RecommendedGuesses(List<string> wordBank, string included, List<string> allWords)
        {
            var charFrequency = NonRequiredCharacterFrequency(wordBank, included);
            var sorted = from entry in charFrequency orderby entry.Value ascending select entry;
            var attemptToInclude = "";
            var charCount = 
                charFrequency.Keys.Count() > 5 
                ? 5 
                : charFrequency.Keys.Count();

            for (var i = 0; i < charCount; i++)
            {
                attemptToInclude += sorted.ElementAt(i).Key;
            }

            var bestAnswers = allWords.Where(word => attemptToInclude.Where(c => word.Contains(c)).Count() == attemptToInclude.Count());

            while(bestAnswers.Count() < 1)
            {
                attemptToInclude = attemptToInclude.Remove(attemptToInclude.Length - 1);
                bestAnswers = allWords.Where(word => attemptToInclude.Where(c => word.Contains(c)).Count() == attemptToInclude.Count());
            }

            bestAnswers = RestrictGuessesTo(5, bestAnswers);

            return bestAnswers;
        }

        private IEnumerable<string> RestrictGuessesTo(int limit, IEnumerable<string> guesses)
        {
            if (guesses.Count() > limit)
            {
                guesses = guesses.Take(limit);
            }

            return guesses;
        }

        private Dictionary<char, int> NonRequiredCharacterFrequency(List<string> wordBank, string included)
        {
            var wordsDictonary = new Dictionary<char, int>();
            
            foreach (var word in wordBank)
            {
                var singleWord = new List<char>();
                
                foreach (var c in word)
                {
                    if (included.Contains(c) || singleWord.Contains(c)) continue;
                        
                    singleWord.Add(c);
                }

                foreach (var c in singleWord)
                {
                    if (wordsDictonary.Keys.Contains(c))
                    {
                        wordsDictonary[c] += 1;
                    }
                    else
                    {
                        wordsDictonary.Add(c, 1);
                    }
                }
            }

            return wordsDictonary;
        }
    }
}
