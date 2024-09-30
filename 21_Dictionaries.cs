using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class DialingCodes
    {
        public static Dictionary<int, string> GetEmptyDictionary() => new Dictionary<int, string>();

        public static Dictionary<int, string> GetExistingDictionary()
        {
            return new Dictionary<int, string>
            {
                [1] = "United States of America",
                [55] = "Brazil",
                [91] = "India"
            };
        }

        public static Dictionary<int, string> AddCountryToEmptyDictionary(int countryCode, string countryName)
        {
            return new Dictionary<int, string> { [44] = "United Kingdom" };
        }

        public static Dictionary<int, string> AddCountryToExistingDictionary(
            Dictionary<int, string> existingDictionary, int countryCode, string countryName)
        {
            existingDictionary[countryCode] = countryName;
            return existingDictionary;
        }

        public static string GetCountryNameFromDictionary(
            Dictionary<int, string> existingDictionary, int countryCode)
        {
            if (existingDictionary.ContainsKey(countryCode))
            {
                return existingDictionary[countryCode];
            } else
            {
                return string.Empty;
            }
        }

        public static bool CheckCodeExists(Dictionary<int, string> existingDictionary, int countryCode)
        {
            return existingDictionary.ContainsKey(countryCode);
        }

        public static Dictionary<int, string> UpdateDictionary(
            Dictionary<int, string> existingDictionary, int countryCode, string countryName)
        {
            if (existingDictionary.ContainsKey(countryCode))
            {
                existingDictionary[countryCode] = countryName;
            }
            return existingDictionary;
        }

        public static Dictionary<int, string> RemoveCountryFromDictionary(
            Dictionary<int, string> existingDictionary, int countryCode)
        {
            existingDictionary.Remove(countryCode);
            return existingDictionary;
        }

        public static string FindLongestCountryName(Dictionary<int, string> existingDictionary)
        {
            string longestCountryName = string.Empty;
            foreach (string countryName in existingDictionary.Values) 
            { 
                if (countryName.Length > longestCountryName.Length)
                {
                    longestCountryName = countryName;
                }
            }
            return longestCountryName;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------

    public class WordSearch
    {
        private List<List<char>> matrix = new List<List<char>>();
        int maxRow = 0;
        int maxCol = 0;
        public WordSearch(string grid)
        {
            int row = 0;
            int col = 0;
            List<char> rowList = new List<char>();
            foreach (char c in grid)
            {
                if (c == '\n')
                {
                    matrix.Add(rowList);
                    rowList = new List<char>();
                    row++;
                    col = 0;
                }
                else
                {
                    rowList.Add(c);
                    col++;
                }
                maxRow = row > maxRow ? row : maxRow;
                maxCol = col > maxCol ? col : maxCol;
            }
            matrix.Add(rowList);
        }

        public Dictionary<string, ((int, int), (int, int))?> Search(string[] wordsToSearchFor)
        {
            int searchedLetterIndex = 0;
            List<string> validWords = new List<string>();
            int actualRow = 0;
            int actualCol = 0;
            Dictionary<string, ((int, int), (int, int))?> result = new Dictionary<string, ((int, int), (int, int))?>();

            foreach(string word in wordsToSearchFor)
            {
                result[word] = null;
            }

            for (int row = 0; row <= maxRow; row++)
            {
                for(int col = 0; col < maxCol; col++)
                {
                    for (int updateRow = -1; updateRow < 2; updateRow++)
                    {
                        for(int updateCol = -1;updateCol < 2; updateCol++)
                        {
                            actualCol = col;
                            actualRow = row;
                            searchedLetterIndex = 0;
                            validWords = wordsToSearchFor.ToList();

                            if (updateRow != 0 || updateCol != 0)
                            {
                                while (actualRow <= maxRow && actualCol < maxCol && actualRow >= 0 && actualCol >=0 && validWords.Count > 0)
                                {
                                    List<string> wordsToRemove = new List<string>();
                                    foreach (string word in validWords)
                                    {
                                        if (word[searchedLetterIndex] != matrix[actualRow][actualCol])
                                        {
                                            wordsToRemove.Add(word);
                                        }
                                        else if (word.Length - 1 == searchedLetterIndex)
                                        {
                                            result[word] = ((col + 1, row + 1), (actualCol + 1, actualRow + 1));
                                            wordsToRemove.Add(word);
                                        }
                                    }
                                    foreach (string word in wordsToRemove)
                                    {
                                        validWords.Remove(word);
                                    }
                                    if (validWords.Count == 0)
                                    {
                                        break;
                                    }
                                    actualCol += updateCol;
                                    actualRow += updateRow;
                                    searchedLetterIndex++;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
