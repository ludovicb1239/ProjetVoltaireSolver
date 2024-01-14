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
        public Solver(string data) {
            reponses = new();
            try
            {
                isOK = fromString(data, out reponses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error -> {ex.Message}");
                isOK = false;
            }
        }
        public static bool fromString(string data, out List<string> reponses)
        {
            reponses = data.Split('"').ToList();
            reponses = reponses.Where(x => x.Contains("\\x3C")).ToList();

            for (int i = 0; i < reponses.Count; i++)
                reponses[i] = ProcessInput(reponses[i]);

            reponses = reponses.Where(s => s.Length >= 8 && s.Length <= 200).ToList();

            /*
            foreach (string reponse in reponses)
                Console.WriteLine("Data -> " + reponse);
            */
            Console.WriteLine($"Solver found {reponses.Count} Awnsers");

            return(reponses.Count > 5);
        }
        public bool GetBestMatch(string phrase, out string bestMatch)
        {
            foreach (string s in reponses)
            {
                int firstIndex = s.IndexOf("|");
                int lastIndex = s.LastIndexOf("|");
                string start = s.Substring(0, firstIndex);
                string end = s.Substring(lastIndex+1);
                if (AreSameStartAndEnd(phrase, start, end))
                {
                    bestMatch = s;
                    return true;
                }
            }
            bestMatch = "";
            return false;
        }
        static bool AreSameStartAndEnd(string str, string start, string end)
        {
            // Trim leading and trailing spaces
            start = start.Trim().Normalize();
            end = end.Trim().Normalize();
            str = str.Trim().Normalize();

            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            /*if (str.StartsWith(start, comp) || str.EndsWith(end, comp))
            {
                Console.WriteLine($"Comparing :\n\"{ConvertStringToAscii(str)}\"\n\"{ConvertStringToAscii(start)}\" \"{ConvertStringToAscii(end)}\"");
                Console.WriteLine($"Start equal : {str.StartsWith(start, comp)} End equal : {str.EndsWith(end, comp)}");
            }*/

            return str.StartsWith(start, comp) && str.EndsWith(end, comp);
        }
        static string ConvertStringToAscii(string input)
        {
            List<int> asciiList = new List<int>();

            foreach (char c in input)
            {
                // Convert each character to its ASCII value and add to the list
                asciiList.Add((int)c);
            }

            return string.Join(" ", asciiList);
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
