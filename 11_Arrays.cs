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
}
