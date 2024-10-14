using System;
using System.Collections.Generic;
using System.Linq;
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
}
