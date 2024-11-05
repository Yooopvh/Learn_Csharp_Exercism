using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class BirdCount
    {
        private int[] _birdsPerDay;

        public BirdCount(int[] birdsPerDay)
        {
            this._birdsPerDay = birdsPerDay;
        }

        public static int[] LastWeek() => new int[] {0,2,5,3,7,8,4};


        public int Today() => _birdsPerDay[6];


        public void IncrementTodaysCount() => _birdsPerDay[6]++;

        public bool HasDayWithoutBirds()
        {
            bool anyDay0Birds = false;
            foreach (int birdsCount in _birdsPerDay)
            {
                if (birdsCount == 0) 
                {
                    anyDay0Birds = true;
                }
            }
            return anyDay0Birds;
        }

        public int CountForFirstDays(int numberOfDays)
        {
            int count = 0;
            for (int i = 0; i < numberOfDays; i++)
            {
                count += _birdsPerDay[i];
            }
            return count;
        }

        public int BusyDays()
        {
            int numOfBussyDays = 0;
            foreach (int birdsCount in _birdsPerDay)
            {
                if (birdsCount >= 5)
                {
                    numOfBussyDays += 1;
                }
            }
            return numOfBussyDays;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class AllYourBase
    {
        public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
        {
            if (inputBase <= 1 || outputBase <= 1) throw new ArgumentException();
            if (inputDigits.Any(x => x < 0) || inputDigits.Any(x => x >= inputBase)) throw new ArgumentException();
            int value = 0;
            for (int i = inputDigits.Length - 1; i >= 0; i--) { 
                value += inputDigits[i] * (int) Math.Pow(inputBase,inputDigits.Length - 1 -i);
            }
            int digitsNeeded = 1;
            while (Math.Pow(outputBase, digitsNeeded) <= value) digitsNeeded++;
            int[] result = new int[digitsNeeded];
            for (int i = digitsNeeded-1; i >= 0; i--)
            {
                int actualDigit = (int)((value/Math.Pow(outputBase, i) - value %Math.Pow(outputBase, i)/Math.Pow(outputBase,i)));
                result[digitsNeeded - 1 - i] = actualDigit;
                value -= (int) (actualDigit * Math.Pow(outputBase,i));
            }
            return result;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class OcrNumbers
    {
        private static readonly Dictionary<string,int> numberCodes = new Dictionary<string, int>()
        {
            {" _ \n| |\n|_|\n   ",0 },
            {"   \n  |\n  |\n   ",1 },
            {" _ \n _|\n|_ \n   ",2 },
            {" _ \n _|\n _|\n   ",3 },
            {"   \n|_|\n  |\n   ",4 },
            {" _ \n|_ \n _|\n   ",5 },
            {" _ \n|_ \n|_|\n   ",6 },
            {" _ \n  |\n  |\n   ",7 },
            {" _ \n|_|\n|_|\n   ",8 },
            {" _ \n|_|\n _|\n   ",9 }
        };

        public static string Convert(string input)
        {
            char[]inputPerLines = input.ToCharArray();
            int row = 0;
            int col = 0;

            //Get a matrix of characters 
            int numRows = input.ToCharArray().Count(x => x == '\n')+1;
            int numColumns = input.Split('\n')[0].ToCharArray().Count();
            if (numRows%4 != 0) throw new ArgumentException();
            if (numColumns%3 != 0) throw new ArgumentException();

            char[,] charMatrix = new char[numRows,numColumns];
            foreach (char c in inputPerLines)
            {
                if (c == '\n')
                {
                    row++;
                    col = 0;
                } else
                {
                    charMatrix[row, col] = c;
                    col++;
                }
            }

            List<string> stringsToDecode = new List<string>();
            int actualRow = 0;
            int actualCol = 0;
            //Divide that characters into groups of 4 by 3.
            for (int auxRow = 0; auxRow < numRows; auxRow += 4)
            {
                while (actualCol < numColumns)
                {
                    string numberCodedString = "";
                    for (int i = 0+auxRow; i < auxRow + 4; i++)
                    {
                        for (int auxCol = actualCol; auxCol < actualCol + 3; auxCol++)
                        {
                            numberCodedString += charMatrix[i, auxCol].ToString();
                        }
                        numberCodedString += '\n';

                    }

                    stringsToDecode.Add(numberCodedString[0..(numberCodedString.Length - 1)]);
                    actualCol += 3;
                }
                actualCol = 0;
                stringsToDecode.Add(",");
            }

            stringsToDecode.RemoveAt(stringsToDecode.Count - 1);

            //Decode the srings
            string result = "";
            foreach (string numberCodedString in stringsToDecode)
            {
                if (numberCodes.ContainsKey(numberCodedString)) result += numberCodes[numberCodedString].ToString();
                else if (numberCodedString == ",") result += numberCodedString;
                else result += "?";
            }

            return result;
        }
    }

}
