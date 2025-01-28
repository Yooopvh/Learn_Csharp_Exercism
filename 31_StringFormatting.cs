using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class HighSchoolSweethearts
    {
        public static string DisplaySingleLine(string studentA, string studentB) => $"{studentA,-30}♡{studentB,30}";

        public static string DisplayBanner(string studentA, string studentB)
        {
            return $@"     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**{studentA,-10}  +  {studentB,10}**
 **                       **
   **                   **
     **               **
       **           **
         **       **
           **   **
             ***
              *";
        }

        public static string DisplayGermanExchangeStudents(string studentA, string studentB, DateTime start, float hours) => $"{studentA} and {studentB} have been dating since {string.Format(CultureInfo.GetCultureInfo("de-DE"),"{0:d} - that´s {1:N} hours",start,hours)}";

    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Tournament
    {
        public static void Tally(Stream inStream, Stream outStream)
        {
            Dictionary<string, Dictionary<string, int>> results = new Dictionary<string, Dictionary<string, int>>();
            using (StreamReader sr = new StreamReader(inStream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(';');
                    string team1 = data[0];
                    string team2 = data[1];
                    string result = data[2];

                    if (!results.Keys.Contains(team1)) results.Add(team1, InitializeDict(new Dictionary<string, int>()));
                    if (!results.Keys.Contains(team2)) results.Add(team2, InitializeDict(new Dictionary<string, int>()));

                    results[team1]["MP"] +=1;
                    results[team2]["MP"] +=1;

                    switch (result)
                    {
                        case "win":
                            results[team1]["W"] +=1;
                            results[team2]["L"] +=1;
                            results[team1]["P"] +=3;
                            break;
                        case "loss":
                            results[team2]["W"] +=1;
                            results[team1]["L"] +=1;
                            results[team2]["P"] +=3;
                            break;
                        case "draw":
                            results[team1]["D"] +=1;
                            results[team2]["D"] +=1;
                            results[team1]["P"] +=1;
                            results[team2]["P"] +=1;
                            break;
                    }
                }

                results = results.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                results = results.OrderByDescending(x => x.Value["P"]).ToDictionary(x => x.Key, x => x.Value);

                // Write results
                using (StreamWriter sw = new StreamWriter(outStream))
                {
                    sw.Write($"{"Team",-30} | MP |  W |  D |  L |  P");

                    foreach (KeyValuePair<string,Dictionary<string,int>> team in results){
                        string team_name = team.Key;
                        Dictionary<string,int> team_results = team.Value;

                        sw.WriteLine();

                        sw.Write($"{team_name,-30} | {team_results["MP"],2} | {team_results["W"],2} | {team_results["D"],2} | {team_results["L"],2} | {team_results["P"],2}");
                    }
                }
            };
        }


        private static Dictionary<string,int> InitializeDict(Dictionary<string,int> dictToInitialize)
        {
            dictToInitialize.Add("MP", 0);
            dictToInitialize.Add("W", 0);
            dictToInitialize.Add("D", 0);
            dictToInitialize.Add("L", 0);
            dictToInitialize.Add("P", 0);

            return dictToInitialize;
        }
    }
}
