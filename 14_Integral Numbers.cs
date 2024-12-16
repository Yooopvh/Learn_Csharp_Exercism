using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class TelemetryBuffer
    {
        public static byte[] ToBuffer(long reading)
        {
            bool isNegative = reading < 0;
            bool isSigned;
            switch (reading)
            {
                case > uint.MaxValue:
                    isSigned = true; break;
                case > int.MaxValue:
                    isSigned = false; break;
                case > ushort.MaxValue:
                    isSigned = true; break;
                case >= 0:
                    isSigned = false; break;
                default:
                    isSigned = true;break;
            }

            double numBits;
            if (isNegative)
            {
                numBits = Math.Log2(Math.Abs(reading + 1));
            } else
            {
                numBits = Math.Log2(reading);
            }    

            if (isNegative == true) { numBits += 1; }
            if (numBits%8 == 0 && numBits != 64 ) { numBits += 1; }

            sbyte numBytesNeeded =  (sbyte) Math.Ceiling((numBits/8));

            numBytesNeeded = (sbyte) Math.Pow(2,Math.Ceiling(Math.Log2(numBytesNeeded)));

            numBytesNeeded = Math.Max(numBytesNeeded,(sbyte) 2);

            byte[] solution = new byte[9];

            byte[] longBitArray = BitConverter.GetBytes(reading);

            solution[0] = BitConverter.GetBytes(isSigned ? (256 - numBytesNeeded) : numBytesNeeded)[0];

            for (int i = 0; i < 8; i++)
            {
                if (i<numBytesNeeded)
                {
                    solution[i+1] = longBitArray[i];
                }
                else
                {
                    solution[i+1] = 0;
                }
                
            }

            return solution;

        }

        public static long FromBuffer(byte[] buffer)
        {
            int dataType = buffer[0];

            switch (dataType)
            {
                case 2: return BitConverter.ToUInt16(buffer, 1); break;
                case 4: return BitConverter.ToUInt32(buffer, 1); break;
                case 254: return BitConverter.ToInt16(buffer, 1); break;
                case 252: return BitConverter.ToInt32(buffer, 1); break;
                case 248: return BitConverter.ToInt64(buffer, 1); break;
                default: return 0;

            }
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public static class PrimeFactors
    {
        public static long[] Factors(long number)
        {
            List<long> result = new List<long>();

            while (number > 1)
            {
                for (int i = 2; i <= number; i++)
                {
                    if ((number%i) == 0)
                    {
                        result.Add(i);
                        number /= i;
                        break;
                    }
                }
            }
            return result.ToArray();
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class VariableLengthQuantity
    {
        public static uint[] Encode(uint[] numbers)
        {
            List<uint> allValues = new List<uint>();
            foreach (uint number in numbers)
            {
                if (number == 0)
                {
                    allValues.Add(0);
                    continue;
                }
                List<uint> values = new List<uint>();
                uint auxNumber = number;
                while (auxNumber > 0)
                {
                    // Obtenemos los 7 últimos bits
                    values.Add(auxNumber & 0b_111_1111);
                    auxNumber >>= 7;
                }

                uint mask = 0b_0000_0000;
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] |= mask;
                    mask = 0b_1000_0000;

                }

                allValues.AddRange(values.ToArray().Reverse());
            }

            return allValues.ToArray();
        }

        public static uint[] Decode(uint[] bytes)
        {
            List<uint> allValues = new List<uint>();
            uint decodedNumber = 0;
            bool isLast = false;    
            foreach (uint number in bytes)
            {
                if (number > 0xFF) throw new InvalidOperationException();
                isLast = (number & 0b_1000_0000) == 0? true : false;
                uint numberWithoutFlag = number & 0b_0111_1111;
                decodedNumber <<= 7;
                decodedNumber |= numberWithoutFlag;
                
                if (isLast)
                {
                    allValues.Add(decodedNumber);
                    decodedNumber = 0;
                }
            }

            if (!isLast ) throw new InvalidOperationException(); 

            return allValues.ToArray();
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static class LargestSeriesProduct
    {
        public static long GetLargestProduct(string digits, int span)
        {
            if (span > digits.Length || span < 0) throw new ArgumentException();
            List<int> values = digits.ToCharArray().Select(x => (int)x - (int)'0').ToList();
            if (values.Any(x => x > 9 || x < 0)) throw new ArgumentException();
            int maxResult = 0;
            for (int i = 0; i <= digits.Length - span;  i++)
            {
                List<int> studyRange = values.GetRange(i, span);
                int result = 1;
                foreach (int value in studyRange) result *= value;
                if (result > maxResult) maxResult = result;
            }
            return maxResult;
        }
    }
}
