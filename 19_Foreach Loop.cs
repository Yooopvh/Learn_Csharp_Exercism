﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Code
{
    public static class ScaleGenerator
    {
        
        private static List<string> getScale(string tonic)
        {
            List<string> result = new List<string>();
            bool isFlatScale = false;
            string[] FLAT = { "d", "g", "c", "f", "F" };
            if (tonic.EndsWith('b') ||  FLAT.Contains(tonic))
            {
                result = new List<string> { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };
            } else
            {
                result = new List<string> { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            }

            return result;
        }

        public static string[] Chromatic(string tonic)
        {
            string[] resultString = new string[12];
           
            List<string> scale = getScale(tonic);

            int tonicIndex = scale.IndexOf(tonic);

            for (int i = 0; i<12; i++)
            {
                resultString[i] = scale[(tonicIndex+i)%12];
            }

            return resultString;

        }

        public static string[] Interval(string tonic, string pattern)
        {
            List<string> scale =getScale(tonic);

            int indexInScale = scale.IndexOf(char.ToUpper(tonic[0]) + tonic.Substring(1));

            string[] result = new string[pattern.Length+1];

            result[0] = scale[indexInScale];

            int indexInPattern = 0;

            foreach (char c in pattern)
            {
                switch (c)
                {
                    case 'm':
                        indexInScale++;
                        indexInPattern++;
                        result[indexInPattern] = scale[indexInScale%12];
                        break;
                    case 'M':
                        indexInScale += 2;
                        indexInPattern++;
                        result[indexInPattern] = scale[indexInScale%12];
                        break;
                    case 'A':
                        indexInScale += 3;
                        indexInPattern++;
                        result[indexInPattern] = scale[indexInScale%12];
                        break;     
                }
            }

            return result;
        }


        public static class FlattenArray
        {
            public static IEnumerable Flatten(IEnumerable input)
            {
                foreach (object element in input)
                {
                    if ( element is IEnumerable auxElement)
                    {
                        foreach (var nestedElement in Flatten(auxElement))
                        {
                            yield return nestedElement;
                        }
                    }
                    else
                    {
                        if (element != null)
                        {
                            yield return element;
                        }
                    }
                };
            }
        }

    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    public static class AtbashCipher
    {
        public static string Encode(string plainValue)
        {
            StringBuilder sb = new StringBuilder();
            plainValue = plainValue.ToLower();
            int i = 0;
            foreach (char c in plainValue)
            {
                if (i == 5)
                {
                    sb.Append(' ');
                    i = 0;
                }
                if (char.IsLetter(c))
                {
                    int difference = c-'a';
                    sb.Append((char)('z'- difference));
                    i++;
                }
                else if (char.IsDigit(c))
                {
                    sb.Append(c);
                    i++;
                }
            }
            return sb.ToString().Trim();
        }

        public static string Decode(string encodedValue)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in encodedValue)
            {
                if (char.IsLetter(c))
                {
                    int difference = 'z'-c;
                    sb.Append((char)('a' + difference));
                }
                else if (char.IsDigit(c))
                {
                    sb.Append(c);
                }

            }
            return sb.ToString();
        }
    }
}
