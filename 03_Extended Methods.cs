using Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
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

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

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

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

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

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class RealNumberExtension
    {
        public static double Expreal(this int realNumber, RationalNumber r) => Math.Pow(Math.Pow((double)realNumber, r.num), 1.0/r.den);
    }

    public struct RationalNumber
    {
        public int num;
        public int den;
        public RationalNumber(int numerator, int denominator)
        {
            num = numerator;
            den = denominator;
        }

        public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        {
            int newNum = (r1.num * r2.den + r1.den*r2.num);
            int newDen = r1.den * r2.den;
            if (newDen == 0) throw new ArgumentException("Can´t divide by 0.");
            return new RationalNumber(newNum, newDen).Reduce();
        }

        public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        {
            int newNum = (r1.num * r2.den - r1.den*r2.num);
            int newDen = r1.den * r2.den;
            if (newDen == 0) throw new ArgumentException("Can´t divide by 0.");
            return new RationalNumber(newNum, newDen).Reduce();
        }

        public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        {
            int newNum = r1.num * r2.num;
            int newDen = r1.den * r2.den;
            if (newDen == 0) throw new ArgumentException("Can´t divide by 0.");
            return new RationalNumber(newNum, newDen).Reduce();
        }

        public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        {
            int newNum = r1.num * r2.den;
            int newDen = r1.den * r2.num;
            if (newDen == 0) throw new ArgumentException("Can´t divide by 0.");
            return new RationalNumber(newNum, newDen).Reduce();
        }

        public RationalNumber Abs() => new RationalNumber(Math.Abs(num), Math.Abs(den)).Reduce();

        public RationalNumber Reduce()
        {
            int MCD = MaxCommonDivisor(num, den);
            RationalNumber result = den < 0?
                new RationalNumber(-num/MCD, -den/MCD) :
                new RationalNumber(num/MCD, den/MCD);
            return result;
        }

        public RationalNumber Exprational(int power) => power< 0 ?
                new RationalNumber((int)Math.Pow(den, -power), (int)Math.Pow(num, -power)).Reduce() :
                new RationalNumber((int)Math.Pow(num, power), (int)Math.Pow(den, power)).Reduce();


        public double Expreal(int baseNumber) => Math.Pow(baseNumber^num,1/den);

        private static int MaxCommonDivisor(int num, int den)
        {
            for (int i = Math.Max(Math.Abs(num), Math.Abs(den)); i >= 1;i--) 
            { 
                if (num % i == 0 && den % i == 0) return i;
            }
            return -1;
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    public struct ComplexNumber
    {
        private double real;
        private double imag;

        public ComplexNumber(int real)
        {
            this.real = real;
            this.imag = 0;
        }

        public ComplexNumber(double real, double imaginary)
        {
            this.real = real;
            this.imag = imaginary;
        }

        public double Real() => this.real;

        public double Imaginary() => this.imag;

        public ComplexNumber Mul(ComplexNumber other) => 
            new ComplexNumber(
                real*other.real - imag*other.imag, 
                imag*other.real + real * other.imag
            );

        public ComplexNumber Mul(int other) => Mul(new ComplexNumber(other,0));

        public ComplexNumber Add(ComplexNumber other) =>
            new ComplexNumber(
                real + other.real,
                imag + other.imag
            );
        public ComplexNumber Add(int other) => Add(new ComplexNumber(other,0));

        public ComplexNumber Sub(ComplexNumber other) =>
            new ComplexNumber(
                real - other.real,
                imag - other.imag
            );

        public ComplexNumber Sub(int other) => Sub(new ComplexNumber(other, 0));

        public ComplexNumber Div(ComplexNumber other) =>
            new ComplexNumber(
                (real*other.real + imag*other.imag)/(Math.Pow(other.real,2) + Math.Pow(other.imag,2)),
                (imag*other.real - real * other.imag)/(Math.Pow(other.real, 2) + Math.Pow(other.imag, 2))
            );

        public ComplexNumber Div(int other) => Div(new ComplexNumber(other, 0));

        public double Abs() => Math.Sqrt(Math.Pow(real,2)+Math.Pow(imag,2));

        public ComplexNumber Conjugate() => new ComplexNumber(real,-imag);

        public ComplexNumber Exp() => new ComplexNumber(Math.Exp(real)*Math.Cos(imag), Math.Exp(real)*Math.Sin(imag));
    }


}
