using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetVoltaire
{
    public class Solver
    {
        List<string> reponses = new();
        public bool isOK = false;
        public Solver() {
            reponses = new();
            try
            {
                string data = File.ReadAllText("data.txt", System.Text.Encoding.UTF8);
                reponses = data.Split('"').ToList();
                reponses = reponses.Where(x => x.Contains("\\x3C")).ToList();

                for (int i = 0; i < reponses.Count; i++)
                    reponses[i] = ProcessInput(reponses[i]);

                reponses = reponses.Where(s => s.Length >= 8 && s.Length <= 200).ToList();

                foreach (string reponse in reponses)
                    Console.WriteLine("Data -> " + reponse);

                isOK = reponses.Count > 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error -> {ex.Message}");
                isOK = false;
            }
        }
        public bool GetBestMatch(string phrase, out string bestMatch)
        {
            List<int> possibilites = GetCloseMatchesIndexes(phrase, reponses);

            if (possibilites.Count != 0)
            {
                bestMatch = reponses[possibilites[0]];
                return true;
            }
            else
            {
                bestMatch = "";
                return false;
            }
        }
        private List<int> GetCloseMatchesIndexes(string word, List<string> possibilities, int n = 3, double cutoff = 0.65)
        {
            if (!(n > 0)) throw new ArgumentException("n must be > 0");

            if (!(cutoff >= 0.0 && cutoff <= 1.0)) throw new ArgumentException("cutoff must be in [0.0, 1.0]");

            List<Tuple<double, int>> result = new List<Tuple<double, int>>();
            foreach (var pair in possibilities.Select((x, idx) => new { Value = x, Index = idx }))
            {
                double ratio = CalculateRatio(word, pair.Value);
                if (ratio >= cutoff)
                {
                    result.Add(Tuple.Create(ratio, pair.Index));
                }
            }

            // Move the best scorers to the head of the list
            result = result.OrderByDescending(pair => pair.Item1).Take(n).ToList();

            // Strip scores for the best n matches
            return result.Select(pair => pair.Item2).ToList();
        }
        private static double CalculateRatio(string word, string target)
        {
            int[,] distanceMatrix = new int[word.Length + 1, target.Length + 1];

            for (int i = 0; i <= word.Length; i++)
            {
                for (int j = 0; j <= target.Length; j++)
                {
                    if (i == 0)
                    {
                        distanceMatrix[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        distanceMatrix[i, j] = i;
                    }
                    else
                    {
                        int cost = (word[i - 1] == target[j - 1]) ? 0 : 1;
                        distanceMatrix[i, j] = Math.Min(
                            Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1),
                            distanceMatrix[i - 1, j - 1] + cost
                        );
                    }
                }
            }

            int maxLen = Math.Max(word.Length, target.Length);
            double similarityRatio = 1.0 - (double)distanceMatrix[word.Length, target.Length] / maxLen;
            return similarityRatio;
        }
        private static string ProcessInput(string input)
        {
            // Use regular expressions to find and replace the specified patterns
            string pattern = @"\\x3C(B)\\x3E(.*?)\\x3C/B\\x3E";
            string replacement = "|$2|";
            input = Regex.Replace(input, pattern, replacement);

            pattern = @"\\x27";
            replacement = "'";
            input = Regex.Replace(input, pattern, replacement);

            pattern = @"\\x26#x2011;";
            replacement = "-";
            input = Regex.Replace(input, pattern, replacement);

            pattern = "\"";
            replacement = "";
            input = Regex.Replace(input, pattern, replacement);

            pattern = @"\\xA0";
            replacement = " ";
            string result = Regex.Replace(input, pattern, replacement);

            return result;
        }

    }
}
