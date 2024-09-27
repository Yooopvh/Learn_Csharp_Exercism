using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Identifier
    {
        // Este ejercicio se corresponde con la parte de Chars pero también con la de String Builder
        public static string Clean(string identifier)
        {
            // Reemplazar ' ' con '_'
            StringBuilder sb = new StringBuilder(identifier);
            sb.Replace(' ', '_');
            identifier = sb.ToString();

            // Buscar Control Characters y reemplazar con CTRL
            List<int> controlCharIndices = new List<int>();
            for (int i = 0; i < identifier.Length; i++)
            {
                if (char.IsControl(identifier[i]))
                {
                    controlCharIndices.Add(i);
                }
            }

            foreach (int j in controlCharIndices)
            {
                sb.Remove(j,1);
                sb.Insert(j, "CTRL");

                identifier = sb.ToString();
                controlCharIndices = new List<int>();
                for (int i = 0; i < identifier.Length; i++)
                {
                    if (char.IsControl(identifier[i]))
                    {
                        controlCharIndices.Add(i);
                    }
                }
            }
            identifier = sb.ToString();

            //kebab-case to camelCase
            int indexOfDash = identifier.IndexOf('-');

            while (indexOfDash != -1)
            {
                sb.Remove(indexOfDash, 1);
                sb[indexOfDash] = char.ToUpper(sb[indexOfDash]);
                identifier = sb.ToString();
                indexOfDash = identifier.IndexOf('-');
            }

            // Omit characters that are not letters
            int adjust = 0;
            int position = 0;
            foreach (char c in identifier)
            {
                if (!char.IsLetter(c) && c != '_')
                {
                    sb.Remove(position-adjust,1);
                    adjust++;
                    
                }
                position++;
            }

            identifier = sb.ToString();

            // Omit Greek lower case letters
            adjust = 0;
            position = 0;
            foreach (char c in identifier)
            {
                if ((int) c >= 945 && (int) c <= 969)
                {
                    sb.Remove(position-adjust, 1);
                    adjust++;

                }
                position++;
            }
            return sb.ToString();
        }
    }

    // MUCH BETTER SOLUTION
    //public static class Identifier
    //{
    //    private static bool IsGreekLowercase(char c) => (c >= 'α' && c <= 'ω');

    //    public static string Clean(string identifier)
    //    {
    //        var stringBuilder = new StringBuilder();
    //        var isAfterDash = false;
    //        foreach (var c in identifier)
    //        {
    //            stringBuilder.Append(c switch
    //            {
    //                _ when IsGreekLowercase(c) => default,
    //                _ when isAfterDash => char.ToUpperInvariant(c),
    //                _ when char.IsWhiteSpace(c) => '_',
    //                _ when char.IsControl(c) => "CTRL",
    //                _ when char.IsLetter(c) => c,
    //                _ => default,
    //            });
    //            isAfterDash = c.Equals('-');
    //        }
    //        return stringBuilder.ToString();
    //    }
    //}


}
