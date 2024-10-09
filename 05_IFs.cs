using Code2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    static class AssemblyLine
    {
        public static double SuccessRate(int speed)
        {
            if (speed == 0) return 0;
            else if (speed <= 4) return 1;
            else if (speed <= 8) return 0.9;
            else if (speed == 9) return 0.8;
            else return 0.77;
        }

        public static double ProductionRatePerHour(int speed) => speed*221 * SuccessRate(speed);

        public static int WorkingItemsPerMinute(int speed) => (int) Math.Floor(ProductionRatePerHour(speed)/60);
    }

    //public static int Score(double x, double y)
    //{
    //    double dartRadious = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    //    if (dartRadious <= 1) return 10;
    //    else if (dartRadious <= 5) return 5;
    //    else if (dartRadious <= 10) return 1;
    //    else return 0;
    //}

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Triangle
    {
        public static bool IsScalene(double side1, double side2, double side3)
        {
            return (side1 != side2 && side2 != side3 && side1 != side3) && IsTriangle(side1,side2,side3);
        }

        public static bool IsIsosceles(double side1, double side2, double side3)
        {
            return (side1 == side2 ||  side2 == side3 || side3 == side1) && IsTriangle(side1,side2,side3);
        }

        public static bool IsEquilateral(double side1, double side2, double side3)
        {
            return (side1 == side2 && side2 == side3) && IsTriangle(side1, side2, side3);
        }

        public static bool IsTriangle(double side1, double side2, double side3)
        {
            if ((side1 > 0 && side2 > 0 && side3 > 0) && (side3 + side2 >= side1 && side1 + side2 >= side3 && side1 + side3 >= side2)) return true; return false;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Hamming
    {
        public static int Distance(string firstStrand, string secondStrand)
        {
            int result = 0;
            for (int i = 0; i < Math.Max(firstStrand.Length,secondStrand.Length); i++)
            {
                try
                {
                    result = firstStrand[i] != secondStrand[i] ? result + 1 : result;

                }catch (Exception) { throw new ArgumentException();  }
            }
            return result;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class CollatzConjecture
    {
        public static int Steps(int number)
        {
            int counter = 0;
            if (number < 1) { throw new ArgumentOutOfRangeException(); }
            while (number != 1)
            {
                number = number % 2 == 0 ? number/2 : number*3+1;
                counter++;
            }
            return counter;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Raindrops
    {
        public static string Convert(int number)
        {
            string result = "";
            
            if (number % 3 == 0) result += "Pling";
            if (number % 5 == 0) result += "Plang";
            if (number % 7 == 0) result += "Plong";
            if (result == "") result = number.ToString();

            return result;
        }

    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    public static class FoodChain
    {

        public static string Recite(int verseNumber)
        {
            string[] animalsList = new string[] { "fly", "spider", "bird", "cat", "dog", "goat", "cow", "horse" };
            string result = "";

            if (verseNumber == 8) result = "I know an old lady who swallowed a horse.\nShe's dead, of course!";
            else
            {
                result += $"I know an old lady who swallowed a {animalsList[verseNumber-1]}.\n";
                switch (verseNumber)
                {
                    case 2:
                        result += "It wriggled and jiggled and tickled inside her.\n";
                        break;
                    case 3:
                        result += "How absurd to swallow a bird!\n";
                        break;
                    case 4:
                        result += "Imagine that, to swallow a cat!\n";
                        break;
                    case 5:
                        result += "What a hog, to swallow a dog!\n";
                        break;
                    case 6:
                        result += "Just opened her throat and swallowed a goat!\n";
                        break;
                    case 7:
                        result += "I don't know how she swallowed a cow!\n";
                        break;
                }
                for (int i = verseNumber - 1;  i > 0; i--)
                {
                    result += $"She swallowed the {animalsList[i]} to catch the {animalsList[i-1]}";
                    if (i == 2) result += " that wriggled and jiggled and tickled inside her.\n";
                    else result +=".\n";
                }
                result += "I don't know why she swallowed the fly. Perhaps she'll die.";
            }
            return result;
        }

        public static string Recite(int startVerse, int endVerse)
        {
            string result = "";
            for (int i = startVerse; i <= endVerse; i++)
            {
                result += Recite(i);
                if (i != endVerse)
                {
                    result += "\n\n";
                }
            }
            return result;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Grep
    {
        public static string Match(string pattern, string flags, string[] files)
        {
            string result = "";
            List<(string, int, string)> fileLineTextMatches = new List<(string, int, string)>();

            foreach (string file in files)
            {
                string[] lines = File.ReadAllLines(file);

                for (int i = 0; i < lines.Length; i++)
                {

                    string lineText = lines[i];
                    string actualPattern = pattern;

                    if (flags.Contains("-x"))
                    {
                        actualPattern = "^" + actualPattern + "$";
                    }

                    Regex r;
                    if (flags.Contains("-i")) r = new Regex(actualPattern, RegexOptions.IgnoreCase);
                    else r = new Regex(actualPattern);

                    Match m = r.Match(lineText);

                    if ((m.Success && !flags.Contains("-v")) || (!m.Success && flags.Contains("-v")))
                    {
                        fileLineTextMatches.Add((file, i+1, lines[i]));
                    }
                    
                }
            }

            if (flags.Contains("-l"))
            {
                List<string> uniqueFileNames = fileLineTextMatches.Select(tuple => tuple.Item1).Distinct().ToList();

                foreach (string uniqueFileName in uniqueFileNames)
                {
                    result = result + uniqueFileName + "\n";
                }

            } else
            {
                
                foreach ((string,int,string) fileLineTextMatch in fileLineTextMatches)
                {
                    string lineResult = "";
                    if (files.Count() > 1) lineResult += fileLineTextMatch.Item1 + ":";
                    if (flags.Contains("-n")) lineResult += fileLineTextMatch.Item2 + ":";
                    lineResult += fileLineTextMatch.Item3 + "\n";
                    result += lineResult;
                }
            }
            result = result.Length > 0? result[0..(result.Length-1)] : result;
            return result;

        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Markdown
    {
        private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

        private static string Parse(string markdown, string delimiter, string tag)
        {
            var pattern = $"{delimiter}(.+){delimiter}";
            var replacement = $"<{tag}>$1</{tag}>";
            return Regex.Replace(markdown, pattern, replacement);
        }

        private static string ParseText(string markdown, bool list)
        {
            var parsedText = Parse(markdown, "__", "strong");
            parsedText = Parse(parsedText, "_", "em");   //Parse_(Parse__((markdown)));

            return list? parsedText : Wrap(parsedText, "p");
        }

        private static string ParseHeader(string markdown, bool list, out bool inListAfter)
        {
            var count = 0;

            for (int i = 0; i < markdown.Length; i++)
            {
                if (markdown[i] == '#')
                {
                    count += 1;
                }
                else
                {
                    break;
                }
            }

            if (count == 0 || count > 6)
            {
                inListAfter = list;
                return null;
            }

            var headerTag = $"h{count}";
            var headerHtml = Wrap(markdown.Substring(count + 1), headerTag);

            inListAfter = false;
            return list ? $"</ul>{headerHtml}" : headerHtml;
        }

        private static string ParseLineItem(string markdown, bool list, out bool inListAfter)
        {
            if (markdown.StartsWith("*"))
            {
                var innerHtml = Wrap(ParseText(markdown.Substring(2), true), "li");

                inListAfter = true;
                return list? innerHtml : $"<ul>{innerHtml}";
            }

            inListAfter = list;
            return null;
        }

        private static string ParseParagraph(string markdown, bool list, out bool inListAfter)
        {
            inListAfter = false;
            return list ? $"</ul>{ParseText(markdown, false)}" : ParseText(markdown, false); 
        }

        private static string ParseLine(string markdown, bool list, out bool inListAfter)
        {
            var result = ParseHeader(markdown, list, out inListAfter);

            if (result == null)
            {
                result = ParseLineItem(markdown, list, out inListAfter);
            }

            if (result == null)
            {
                result = ParseParagraph(markdown, list, out inListAfter);
            }

            if (result == null)
            {
                throw new ArgumentException("Invalid markdown");
            }

            return result;
        }

        public static string Parse(string markdown)
        {
            var lines = markdown.Split('\n');
            var result = "";
            var list = false;

            for (int i = 0; i < lines.Length; i++)
            {
                var lineResult = ParseLine(lines[i], list, out list);
                result += lineResult;
            }

            return list ? $"{result}</ul>" : result;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Say
    {
        
        private static readonly string[] _units = new string[] {"", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static readonly string[] _decenas = new string[] { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] _elevenToNineteen = new string[] {"ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] _scale = new string[] {"", "thousand ", "million ", "billion " };
        

        public static string InEnglish(long number)
        {
            string result = "";

            if (number < 0 || number > 999999999999) throw new ArgumentOutOfRangeException();
            else if (number == 0) return "zero";

            string strNumber = number.ToString();
            string auxStrNumber = strNumber;
            List<long> list = new List<long>();
            
            while (auxStrNumber.Length > 0)
            {
                Int64.TryParse(auxStrNumber[Math.Max(auxStrNumber.Length - 3,0)..(auxStrNumber.Length)], out long num);
                list.Add(num);
                auxStrNumber = auxStrNumber.Remove(Math.Max(auxStrNumber.Length - 3, 0));
            }

            for (int i = list.Count - 1; i >= 0; i--)  
            {
                string scale = list[i] > 0 ? _scale[i] : "";
                result += $"{stringizeNumberUnderThousand(list[i])} {scale}";
            };

            result = result.Trim();
            result = result.EndsWith('-') ? result[0..(result.Length-1)] : result;
            return result;
        }

        private static string stringizeNumberUnderThousand(long number)
        {
            if (number > 999) throw new Exception("The value can´t be higher than 999.");
            int centenas = number >= 100 ? (int)(number - number%100)/100:0;
            number -= centenas*100;
            int decenas = number >= 10 ? (int)(number - number%10)/10 : 0;
            number -= decenas*10;
            int unidades = (int)number;

            string strCentenas = centenas > 0 ? $"{_units[centenas]} hundred" : "";
            string strDecenas = decenas > 1 ? $"{_decenas[decenas - 1]}-" : "";
            string strUnidades = decenas == 1? _elevenToNineteen[unidades] : _units[unidades];

            return $"{strCentenas} {strDecenas}{strUnidades}".Trim();
        }
    }
}
