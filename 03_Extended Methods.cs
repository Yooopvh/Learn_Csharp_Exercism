using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class LogAnalysis
    {
        // TODO: define the 'SubstringAfter()' extension method on the `string` type
        public static string SubstringAfter(this string originalString, string stringToFind) => originalString.Substring(originalString.IndexOf(stringToFind) + stringToFind.Length);

        // TODO: define the 'SubstringBetween()' extension method on the `string` type

        public static string SubstringBetween(this string originalString, string delimeter1, string delimeter2)
        {
            int indexDelimeter1 = originalString.IndexOf(delimeter1);
            int indexDelimeter2 = originalString.IndexOf(delimeter2);

            return originalString.Substring(indexDelimeter1 + delimeter1.Length, indexDelimeter2 - (indexDelimeter1 + delimeter1.Length));

            //Better option
            //originalString.Split(delimeter1).Last().Split(delimeter2).First();
        }

        // TODO: define the 'Message()' extension method on the `string` type

        public static string Message(this string originalString) => originalString.Substring(originalString.IndexOf(": " +1));

        // TODO: define the 'LogLevel()' extension method on the `string` type

        public static string LogLevel(this string originalString) => originalString.Substring(1, originalString.IndexOf(')')-1);
    }


    public static class RomanNumeralExtension
    {
        public static string ToRoman(this int value)
        {
            string romanNumber = "";

            while (value > 0)
            {
                if (value >= 1000){
                    romanNumber += "M";
                    value -= 1000;
                } else if ( value >= 900)
                {
                    romanNumber += "CM";
                    value -= 900;
                }else if( value >= 500)
                {
                    romanNumber += "D";
                    value -= 500;
                }else if( value >= 400)
                {
                    romanNumber += "CD";
                    value -= 400;
                }else if ( value >= 100)
                {
                    romanNumber += "C";
                    value  -= 100;
                }else if (value >= 90)
                {
                    romanNumber += "XC";
                    value -=90;
                }else if (value >= 50)
                {
                    romanNumber += "L";
                    value -= 50;
                }else if(value >= 40)
                {
                    romanNumber += "XL";
                    value -= 40;
                }else if(value >= 10)
                {
                    romanNumber += "X";
                    value -= 10;
                }else if (value >= 9)
                {
                    romanNumber += "IX";
                    value -= 9;
                }else if (value >= 5)
                {
                    romanNumber += "V";
                    value -= 5;
                }else if(value >= 4)
                {
                    romanNumber += "IV";
                    value -= 4;
                }
                else
                {
                    romanNumber+= "I";
                    value -= 1;
                }
            }

            return romanNumber;
        }
    }

    public static class AccumulateExtensions
    {
        public static IEnumerable<U> Accumulate<T, U>(this IEnumerable<T> collection, Func<T, U> func)
        {

            List<U> result = new List<U>();

            foreach (T collectionElement in collection)
            {
                U collectionElementResult = func(collectionElement);
                result.Add(collectionElementResult);
            }

            return result;
        }
    }

}
