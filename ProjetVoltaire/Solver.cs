using System.Text.RegularExpressions;

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
                Console.WriteLine($"SolverError -> {ex.Message}");
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

            
            foreach (string reponse in reponses)
                Console.WriteLine("Data -> " + reponse);
            
            Console.WriteLine($"SolverInfo -> found {reponses.Count} Awnsers");

            return(reponses.Count > 5);
        }
        public bool GetBestMatch(string phrase, out string bestMatch)
        {
            foreach (string s in reponses)
            {
                string[] split = Regex.Replace(s, @"\|.*?\|", "+").Split("+");
                if (AreSameStartAndEnd(phrase, split))
                {
                    bestMatch = s;
                    return true;
                }
            }
            bestMatch = "";
            return false;
        }
        static bool AreSameStartAndEnd(string str, string[] contents)
        {
            // Trim leading and trailing spaces
            for (int i = 0; i < contents.Length; i++)
                contents[i] = contents[i].Trim().Normalize();
            str = str.Trim().Normalize();

            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            /*if (str.StartsWith(start, comp) || str.EndsWith(end, comp))
            {
                Console.WriteLine($"Comparing :\n\"{ConvertStringToAscii(str)}\"\n\"{ConvertStringToAscii(start)}\" \"{ConvertStringToAscii(end)}\"");
                Console.WriteLine($"Start equal : {str.StartsWith(start, comp)} End equal : {str.EndsWith(end, comp)}");
            }*/
            foreach(string s in contents)
            {
                if (!str.Contains(s, comp))
                    return false;
            }
            return true;
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
            
            pattern = "n\\x3CSUP\\x3Eos\\x3C/SUP\\x3E";
            replacement = "n°";
            input = Regex.Replace(input, pattern, replacement);

            pattern = "\\x3Cbr/\\x3Eou\\x3Cbr/\\x3E";
            replacement = "\"";
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
