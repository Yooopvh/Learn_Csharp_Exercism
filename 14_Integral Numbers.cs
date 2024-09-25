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
}
