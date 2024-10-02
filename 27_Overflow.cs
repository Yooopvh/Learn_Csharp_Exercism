using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class CentralBank
    {
        public static string DisplayDenomination(long @base, long multiplier)
        {
            try
            {
                long result = 0;
                checked { result = @base * multiplier; }
                return result.ToString();
            } catch (OverflowException ex) {
                return "*** Too Big ***";
            }
        }

        public static string DisplayGDP(float @base, float multiplier)
        {
            try
            {
                float result = 0;
                checked { result = @base * multiplier; }
                if (result == float.NegativeInfinity ||  result == float.PositiveInfinity) { throw new OverflowException(); }
                return result.ToString();
            }
            catch (OverflowException ex)
            {
                return "*** Too Big ***";
            }
        }

        public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
        {
            try
            {
                decimal result = 0;
                checked { result = salaryBase * multiplier; }
                return result.ToString();
            }
            catch (OverflowException ex)
            {
                return "*** Much Too Big ***";
            }
        }

        // ESTO SERÍA SI LO HICIESEMOS TODO CON LA MISMA FUNCIÓN
        //public static string DisplayValue<T>(T @base, T multiplier) where T : struct, IComparable, IConvertible, IFormattable
        //{
        //    try
        //    {
        //        dynamic baseValue = @base;
        //        dynamic multiplierValue = multiplier;
        //        dynamic result = baseValue * multiplierValue;
        //        return result.ToString();
        //    }
        //    catch (OverflowException)
        //    {
        //        return "*** Too Big ***";
        //    }
        //}
    }
}
