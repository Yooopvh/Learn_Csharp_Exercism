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
}
