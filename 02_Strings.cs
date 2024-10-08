﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Strings
    {

    }

    static class LogLine
    {
        public static string Message(string logLine) 
        { 
            return logLine.Substring(logLine.IndexOf(':') + 1).Trim();
        }

        public static string LogLevel(string logLine)
        {
            int firstBracketIndex = logLine.IndexOf("[");
            int lastBracketIndex = logLine.IndexOf("]");

            return logLine.Substring(firstBracketIndex + 1, lastBracketIndex - firstBracketIndex - 1).ToLower();
        }

        public static string Reformat(string logLine)
        {
            return Message(logLine) + " (" + LogLevel(logLine) + ")";
        }
    }


    public static class Bob
    {
        public static string Response(string statement)
        {
            bool atLeastOneLetter = false;
            //foreach (char c in statement)
            //{
            //    if (char.IsLetter(c))
            //    {
            //        atLeastOneLetter = true;
            //    }
            //}
            atLeastOneLetter = statement.Any(char.IsLetter);
            if (statement == statement.ToUpper() && statement.Trim().EndsWith('?') && atLeastOneLetter) return "Calm down, I know what I'm doing!";
            else if (statement.Trim().EndsWith('?')) return "Sure.";
            else if (statement.Trim() == "" || statement is null) return "Fine. Be that way!";
            else if (statement == statement.ToUpper() && atLeastOneLetter) return "Whoa, chill out!";
            else return "Whatever.";

        }
    }



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

     
    public class Anagram
    {

        private string _baseWord;
        
        public Anagram(string baseWord)
        {
            _baseWord = baseWord.ToLower();
        }

        public string[] FindAnagrams(string[] potentialMatches)
        {
            List<string> solutions = new List<string>();          

            foreach (string auxPotentialMatch in potentialMatches)
            {
                bool isAnagram = true;
                string potentialMatch = auxPotentialMatch.ToLower();
                

                foreach (char character in _baseWord)
                {
                    if (_baseWord.Contains(character) && _baseWord.Count(f => f == character ) == potentialMatch.Count(f => f == character) && potentialMatch.Length == _baseWord.Length)
                    {
                        isAnagram = true;
                    }
                    else
                    {
                        isAnagram = false; break;
                    }
                }

                if (_baseWord == potentialMatch) isAnagram = false;

                if (isAnagram) { solutions.Add(auxPotentialMatch); }
                

            }

            return solutions.ToArray();
        }
    }



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class RunLengthEncoding
    {
        public static string Encode(string input)
        {
            //input = input.ToUpper();
            char? previousChar = null;
            int letterCounter = 1;
            string resultCode = "";
            foreach (char? letter in input)
            {
                if (previousChar == null)
                {
                    previousChar = letter;
                    continue;
                }
                if (letter == previousChar)
                {
                    letterCounter++;

                } else
                {
                    if (letterCounter < 2)
                    {
                        resultCode += previousChar;
                    }
                    else
                    {
                        resultCode = $"{resultCode}{letterCounter}{previousChar}";
                    }
                    letterCounter = 1;
                }
                previousChar = letter;
            };

            //Add the last letter
            if (letterCounter < 2)
            {
                resultCode += previousChar;
            }
            else
            {
                resultCode = $"{resultCode}{letterCounter}{previousChar}";
            }

            return resultCode;
        }

        public static string Decode(string input)
        {
            string resultCode = "";
            //Get the indices of letters
            byte[] asciiBytes = Encoding.ASCII.GetBytes(input);
            List<int> indexLetterBytes = new List<int>();
            for (int index = 0; index < input.Length; index ++)
            {
                if (asciiBytes[index]< 48 || asciiBytes[index] > 57)
                {
                    indexLetterBytes.Add(index);
                }
            }

            //Decode the input
            int previousIndex = -1;
            if (indexLetterBytes.Count() > 0)
            {
                foreach (int index in indexLetterBytes)
                {
                    int difference = index - previousIndex;

                    if (difference < 2)
                    {
                        resultCode += input[index];
                    } else
                    {
                        int number = Int32.Parse(input.Substring(previousIndex + 1, difference-1));

                        for (int i = 0; i<number; i ++)
                        {
                            resultCode += input[index];
                        }
                    }

                    previousIndex = index;
                }
            }

            return resultCode;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class RotationalCipher
    {
        public static string Rotate(string text, int shiftKey)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <='z'))
                {
                    char newLetter = (char) (c + shiftKey);
                    newLetter = ((c >= 'A' && c <= 'Z') && newLetter > 'Z') ? (char)((int)'A' + (int)newLetter - (int)'Z'-1) : newLetter;
                    newLetter = ((c >= 'a' && c <= 'z') && newLetter > 'z') ? (char)((int)'a' + (int)newLetter - (int)'z'-1) : newLetter;

                    stringBuilder.Append(newLetter);
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Acronym
    {
        public static string Abbreviate(string phrase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool nextLetterIsAcronim = true;
            foreach (char c in phrase)
            {
                if (nextLetterIsAcronim && char.IsLetter(c)) { stringBuilder.Append(char.ToUpper(c)); }
                nextLetterIsAcronim = (c == ' ' || c == '-'|| c == '_');
            }

            return stringBuilder.ToString();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class Pangram
    {
        public static bool IsPangram(string input)
        {
            int[] numOfEachLetter = new int[(int)'Z' - (int)'A' +1] ;
            input = input.ToUpper();
            foreach (char c in input)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    numOfEachLetter[c - (int)'A'] += 1;
                }
            }
            return numOfEachLetter.All(x => x > 0);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public static class Isogram
    {
        public static bool IsIsogram(string word) => word.ToLower().Where(x => x >= 'a' && x<='z').Distinct().Count() == word.ToLower().Where(x => x >= 'a' && x<='z').Count();
        //BETTER SOLUTION
    //=> word.ToLower().Where(char.IsLetter).Distinct().Count() == word.ToLower().Where(char.IsLetter).Count();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class ReverseString
    {
        public static string Reverse(string input) => new string(input.Reverse().ToArray());

    }
}

