using System;
using System.Collections.Generic;
using System.Linq;

namespace wordleSolver
{
    class Solver
    {
        public List<string> GetPossibleSolutions(
            string included,
            string excluded,
            string knownPositions,
            List<string> ExcludedPositionsOfIncludedChars,
            List<string> wordBank)
        {
            return wordBank
                .Where(word => included.All(c => word.Contains(c)))
                .Where(word => excluded.All(c => !word.Contains(c)))
                .Where(word => CharsAppearInKnownPositions(word, knownPositions))
                .Where(word => CharsDoNotAppearInExcludedPositions(word, ExcludedPositionsOfIncludedChars))
                .ToList();
        }

        public IEnumerable<string> GetRecommendedGuesses(List<string> wordBank, string included, List<string> allWords)
        {
            var charFrequency = NonRequiredCharacterFrequency(wordBank, included);
            var sortedList = DictionaryToSortedList(charFrequency);

            return FindBestGuesses(allWords, sortedList);
        }

        private List<char> DictionaryToSortedList(Dictionary<char, int> charFrequency)
        {
            var sortedList = new List<char>();

            while(charFrequency.Keys.Count() > 0)
            {
                var maxValue = -1;
                char maxValueKey = '1';
                foreach (var key in charFrequency.Keys)
                {
                    if (charFrequency[key] > maxValue)
                    {
                        maxValue = charFrequency[key];
                        maxValueKey = key;
                    }
                }
                sortedList.Add(maxValueKey);
                charFrequency.Remove(maxValueKey);
            }

            return sortedList;
        }

        private IEnumerable<string> FindBestGuesses(IEnumerable<string> allWords, List<char> sortedCharFrequency)
        {
            var attemptToInclude = sortedCharFrequency.Take(5).ToList();

            var bestGuesses = allWords.Where(word => attemptToInclude.All(c => word.Contains(c)));

            while (bestGuesses.Count() < 1)
            {
                attemptToInclude = NextOptimalAttemptToInclude(attemptToInclude, sortedCharFrequency);
                bestGuesses = allWords.Where(word => attemptToInclude.All(c => word.Contains(c)));
            }

            return RestrictGuessesTo(5, bestGuesses);
        }

        private List<char> NextOptimalAttemptToInclude(
            List<char> attemptToInclude,
            List<char> sortedCharFrequency)
        {
            var includeMaxIndex = attemptToInclude.Count() - 1;
            if (sortedCharFrequency.IndexOf(attemptToInclude[includeMaxIndex]) == sortedCharFrequency.Count() - 1 )
            {
                attemptToInclude.RemoveAt(includeMaxIndex);
                return attemptToInclude;
            }

            var nextBestValue = sortedCharFrequency[sortedCharFrequency.IndexOf(attemptToInclude[includeMaxIndex]) + 1];
            attemptToInclude[includeMaxIndex] = nextBestValue;

            return attemptToInclude;
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

        private bool CharsAppearInKnownPositions(string word, string known)
        {
            for (var i = 0; i < known.Length; i++)
            {
                if (known[i] != '*')
                {
                    if (known[i] != word[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CharsDoNotAppearInExcludedPositions(string word, List<string> excludedPositionsOfIncludedChars)
        {
            foreach (var fstring in excludedPositionsOfIncludedChars)
            {
                for (var i = 0; i < fstring.Length; i++)
                {
                    if (fstring[i] != '*')
                    {
                        if (fstring[i] == word[i])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
