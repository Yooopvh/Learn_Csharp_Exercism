using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class IsbnVerifier
    {
        public static bool IsValid(string number)
        {
            int multiplier = 10;
            int result = 0;
            for(int i = 0; i < number.Length;i++)
            {
                if (char.IsDigit(number[i]))
                {
                    result += (int) char.GetNumericValue(number[i]) * multiplier;
                    multiplier--;
                } else if ((i == number.Length - 1 && number[i] == 'X'))
                {
                    result += 10;
                    multiplier--;
                } else if (number[i] == '-')
                {
                    //nothing
                }
                else
                {
                    return false;
                }
            }
            return result%11 == 0 && multiplier == 0;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Minesweeper
    {
        public static string[] Annotate(string[] input)
        {
            int numRows = input.Length;
            if (numRows == 0) return input;
            int numCols = input[0].Length;
            char[][] matrixInput = input.ToList().Select(x => x.ToCharArray()).ToArray();
            for (int rowIndex = 0;rowIndex < numRows;rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < numCols; columnIndex++)
                {
                    int numMinesArround = 0;
                    for (int subRowIndex = -1; subRowIndex < 2; subRowIndex++)
                    {
                        for (int subColumnIndex = -1; subColumnIndex < 2; subColumnIndex++)
                        {
                            if (subRowIndex == 0 && subColumnIndex == 0) continue;
                            if (rowIndex + subRowIndex < 0 ||
                                rowIndex + subRowIndex >= numRows ||
                                columnIndex + subColumnIndex < 0 ||
                                columnIndex + subColumnIndex >= numCols) continue;
                            if (matrixInput[rowIndex + subRowIndex][columnIndex + subColumnIndex] == '*') numMinesArround++;
                        }
                    }
                    matrixInput[rowIndex][columnIndex] = (matrixInput[rowIndex][columnIndex] == ' ' && numMinesArround > 0) ? (char) ('0' +numMinesArround) : matrixInput[rowIndex][columnIndex];
                }
            }

            var opopo = matrixInput.Select(x => string.Concat(x)).ToArray();
            return matrixInput.Select(x => string.Concat(x)).ToArray();
        }
    }


    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Diamond
    {
        public static string Make(char target)
        {
            int numLetters = (int)(target - 'A' + 1);
            string lettersArray = new string(Enumerable.Range('A', numLetters).Select(i => (char)i).ToArray());
            string result = string.Empty;

            // Rows
            for (int i=0; i < lettersArray.Length; i++) 
            {
                string rowResult = string.Empty;
                char  c = lettersArray[i];
                rowResult += new string(' ',numLetters - i -1) + c + new string(' ',i);
                rowResult = rowResult.Reverse().Skip(1).Aggregate(rowResult,(result,x) => result += x);
                result += rowResult + "\n";
            }

            for (int i = (lettersArray.Length - 2); i >= 0; i--)
            {
                string rowResult = string.Empty;
                char c = lettersArray[i];
                rowResult += new string(' ', numLetters - i -1) + c + new string(' ', i);
                rowResult = rowResult.Reverse().Skip(1).Aggregate(rowResult, (result, x) => result += x);
                result += rowResult + "\n";
            }

            return result.Substring(0, result.Length - 1);
        }
    }


}
