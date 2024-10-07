using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Code
{
    public class LogParser
    {
        public bool IsValidLine(string text) => new Regex(@"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]").IsMatch(text);

        public string[] SplitLogLine(string text) => new Regex(@"<(^|\*|=|-)+>").Split(text);

        public int CountQuotedPasswords(string lines) => Regex.Matches(lines, @""".*password.*""", RegexOptions.IgnoreCase).Count;
        //{
        //    Regex re = new Regex(@"""(?i).*password.*""");  //(?i) = Case insensitive desde ese punto en adelante. Para anularlo (?-i)
        //    int count = 0;
        //    string[] splitedLines = lines.Split("\n");
        //    foreach (string line in splitedLines)
        //    {
        //        count += re.IsMatch(line) ? 1 : 0;
        //    }
        //    return count;
        //}

        public string RemoveEndOfLineText(string line) => new Regex(@"end-of-line\d+").Replace(line,string.Empty);

        public string[] ListLinesWithPasswords(string[] lines)
        {
            string[] result = new string[lines.Length];
            Regex re = new Regex(@"(?i)(password\S+)");
            int i = 0;
            foreach (string line in lines)
            {
                if (re.IsMatch(line))
                {
                    result[i] = $"{re.Match(line).Value}: {line}";
                }else
                {
                    result[i] = $"--------: {line}";
                }
                i++;
            }

            return(result);
        }
    }
}
