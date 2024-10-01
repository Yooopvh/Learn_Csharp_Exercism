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

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class ParallelLetterFrequency
    {
        // Esta 
        public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
        {
            Dictionary<char,int> result = new Dictionary<char,int>();
            Parallel.For('a', 'z', letterIndex =>
            {
                int num = texts.SelectMany(text => text.ToLower()).Count(x => ((char)letterIndex) == x);
                if (num > 0)
                {
                    Console.WriteLine((char)letterIndex);
                    try
                    {
                        result.Add((char)letterIndex, num);
                    }
                    catch
                    {
                    };
                }
                
            });
            Parallel.For('ß', 'ÿ', letterIndex =>
            {
                if (letterIndex != 247) //÷ symbol
                {
                    int num = texts.SelectMany(text => text.ToLower()).Count(x => ((char)letterIndex) == x);
                    if (num > 0)
                    {
                        result.Add((char)letterIndex, num);
                    }
                }
            });
            return result;
        }

        //  SOLUCIÓN COMUNIDAD
        //public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
        //{
        //    return texts
        //        .AsParallel()
        //        .SelectMany(text => text.ToLower().Select(c => c))
        //        .Where(c => char.IsLetter(c))
        //        .GroupBy(c => c)
        //        .ToDictionary(c => c.Key, c => c.Count());
        //}
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public enum Owner
    {
        None,
        Black,
        White 
    }

    public class GoCounting
    {
        private List<List<char>> board = new List<List<char>>();
        int maxRow = 0;
        int maxColumn = 0;
        
        public GoCounting(string input)
        {
            List<char> rowChars = new List<char>();
            int row = 1; int col = 0;
            foreach (char letter in input)
            {
                if (letter != '\n'){
                    rowChars.Add(letter);
                    col++;
                    if (col > maxColumn) maxColumn = col;
                } else
                {
                    board.Add(rowChars);
                    rowChars = new List<char>();
                    row++;
                    col = 0;
                }
                if (row > maxRow) maxRow = row;
            }
            board.Add(rowChars);   
        }

        public Tuple<Owner, HashSet<(int, int)>> Territory((int, int) coord)
        {
            Tuple<Owner, HashSet<(int, int)>> result;
            HashSet<(int, int)> occupiedCells = new HashSet<(int, int)> ();

            if (coord.Item1 < 0 || coord.Item2 < 0 || coord.Item1 > maxColumn-1 || coord.Item2 > maxRow-1)
            {
                throw new ArgumentException();
            }

            List<(int,int)> itemsToStudy = new List<(int,int)> ();
            if (board[coord.Item2][coord.Item1] == ' ')
            {
                itemsToStudy.Add(coord);
            }
            bool isValid = true;
            char? ownerInStudy = null;
            int i = 0;

            while (itemsToStudy.Except(occupiedCells).ToList().Count() > 0 && itemsToStudy.Count() > 0)
            {
                (int,int) itemInStudy = itemsToStudy[i];
                i++;
                occupiedCells.Add(itemInStudy);

                // Celda superior
                checkCell(itemInStudy, 0, -1,ref isValid,ref ownerInStudy,ref itemsToStudy);
                // Celda derecha
                checkCell(itemInStudy, 1, 0, ref isValid, ref ownerInStudy, ref itemsToStudy);
                // Celda inferior
                checkCell(itemInStudy, 0, 1, ref isValid, ref ownerInStudy, ref itemsToStudy);
                // Celda izquierda
                checkCell(itemInStudy, -1, 0, ref isValid, ref ownerInStudy, ref itemsToStudy);


                //if (!isValid) { break; }
            }

            Owner owner;
            if (!isValid || ownerInStudy == null)
            {
                owner = Owner.None;
            } else
            {
                if (ownerInStudy == 'W')
                {
                    owner = Owner.White;
                } else
                {
                    owner = Owner.Black;
                }
            }

            return new Tuple<Owner, HashSet<(int, int)>>(owner, occupiedCells);
        }

        public Dictionary<Owner, HashSet<(int, int)>> Territories()
        {
            Dictionary<Owner, HashSet<(int, int)>> result = new Dictionary<Owner, HashSet<(int, int)>> 
            {
                {Owner.Black, new HashSet<(int, int)>() },
                {Owner.White, new HashSet<(int, int)>() },
                {Owner.None, new HashSet<(int, int)>() }
            };

            for (int row = 0; row < maxColumn; row++)
            {
                for (int col = 0; col < maxRow; col++)
                {
                    Tuple<Owner, HashSet<(int, int)>>  cellResult = Territory((row,col));
                    Owner owner = cellResult.Item1;
                    
                    foreach ((int,int) element in  cellResult.Item2)
                    {
                        if (!result[owner].Contains(element))
                        {
                            result[owner].Add(element);
                        }
                    }
                }
            }

            return result;
        }

        private void checkCell((int, int) itemInStudy, int varX, int varY,ref bool isValid, ref char? ownerInStudy, ref List<(int, int)> itemsToStudy)
        {
            if ((itemInStudy.Item1 + varX) >= 0 && 
                (itemInStudy.Item2 + varY) >= 0 && 
                (itemInStudy.Item1 + varX) < maxColumn && 
                (itemInStudy.Item2 + varY) < maxRow)
            {
                char value = board[itemInStudy.Item2 + varY][itemInStudy.Item1 + varX];
                if (value == ' ')
                {
                    itemsToStudy.Add((itemInStudy.Item1 + varX, itemInStudy.Item2 + varY));
                }
                else
                {
                    if (ownerInStudy == null)
                    {
                        ownerInStudy = value;
                    }
                    else
                    {
                        isValid = ownerInStudy != value ? false : isValid;
                    }

                }
            }
        }
    }
}
