using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class SpiralMatrix
    {
        public static int[,] GetMatrix(int size)
        {
            int[,] solutionMatrix = new int[size,size];

            int value = 1;
            int index = 0;
            bool ascending = true;

            for ( int i = 0; i < size; i++ )
            {
                int maxValueIndex = size - 1 - (int)Math.Ceiling((double)i/2);
                int minValueIndex = 0 + (int)Math.Floor((double)i/2);
                // Horizontal values
                if (ascending )
                {
                    for (int j = minValueIndex;  j <= maxValueIndex; j++ )
                    {
                        solutionMatrix[minValueIndex,j] = value;
                        value++;

                        //Check if the center has been reached
                        if (minValueIndex == Math.Floor(((double) size+1)/2) && j == Math.Ceiling((double)size/2))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = maxValueIndex; j >= minValueIndex; j--)
                    {
                        solutionMatrix[size - minValueIndex-1, j] = value;
                        value++;

                        //Check if the center has been reached
                        if (minValueIndex == Math.Floor(((double)size+1)/2) && j == Math.Ceiling((double)size/2))
                        {
                            break;
                        }
                    }
                }

                //Vertical values

                maxValueIndex = size - 1 - (int)Math.Floor((double)(i+1)/2);
                minValueIndex = 0 + (int)Math.Ceiling((double)(i+1)/2);
                // Horizontal values
                if (ascending)
                {
                    for (int j = minValueIndex; j <= maxValueIndex; j++)
                    {
                        solutionMatrix[j, (size-minValueIndex)] = value;
                        value++;
                    }
                }
                else
                {
                    for (int j = maxValueIndex; j >= minValueIndex; j--)
                    {
                        solutionMatrix[j, minValueIndex-1] = value;
                        value++;
                    }
                }


                //Switch
                ascending = ascending ? false : true;

            }

            return solutionMatrix;
        }

       
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------


    public class Clock : IEquatable<Clock>
    {
        private int _hours;
        private int _minutes;

        public Clock(int hours, int minutes)
        {
            int auxHours = (hours + (minutes - (minutes%60))/60)%24;
            _hours = auxHours < 0 ? 24 + auxHours : auxHours ;
            if (minutes%60 < 0)
            {
                _minutes = 60 + minutes%60;
                _hours = _hours - 1;
            } else
            {
                _minutes = minutes % 60;
            }
        }

        public Clock Add(int minutesToAdd)
        {
            int resultMinutes = (_minutes + minutesToAdd)%60;
            int resultHours = (_hours + (_minutes+minutesToAdd - (_minutes+minutesToAdd)%60)/60)%24;
            return new Clock(resultHours, resultMinutes);
        }

        public bool Equals(Clock? other) => (_hours == other._hours && _minutes == other._minutes);

        public Clock Subtract(int minutesToSubtract)
        {
            int resultMinutes = (_minutes - minutesToSubtract)%60 < 0 ? 60 + (_minutes - minutesToSubtract)%60 : (_minutes - minutesToSubtract)%60;
            int hoursToSubstract = (_minutes-minutesToSubtract - (_minutes-minutesToSubtract)%60)/60;
            hoursToSubstract = _minutes-minutesToSubtract < 0? hoursToSubstract - 1 : hoursToSubstract ;
            int resultHours = (_hours + hoursToSubstract)%24;
            return new Clock(resultHours, resultMinutes);
        }

        public override string ToString()
        {
            return $"{_hours:00}:{_minutes:00}";
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public enum Classification
    {
        Perfect,
        Abundant,
        Deficient
    }

    public static class PerfectNumbers
    {
        public static Classification Classify(int number)
        {
            if (number <= 0) throw new ArgumentOutOfRangeException("number");
            int aliquotSum = 0;
            for (int i = 1; i <= number/2; i++) 
            {
                aliquotSum += number%i==0 ? i : 0;
            }

            if (aliquotSum == number) return Classification.Perfect;
            else if (aliquotSum > number) return Classification.Abundant;
            else return Classification.Deficient;
        }
    }

    // COMUNITY SOLUTION
    //public enum Classification
    //{
    //    Perfect = 0,
    //    Abundant = 1,
    //    Deficient = -1
    //}
    //public static class PerfectNumbers
    //{
    //    public static Classification Classify(int number) =>
    //        (Classification)Enumerable.Range(1, number - 1).Where(i => number % i == 0).Sum().CompareTo(number);
    //}


    //----------------------------------------------------------------------------------------------------------------------------------------------------

    public static class SumOfMultiples
    {
        public static int Sum(IEnumerable<int> multiples, int max)
        {
            HashSet<int> values = new HashSet<int>();

            foreach (int multiple in multiples.Where(x =>x != 0))
            {
                for (int i = multiple; i<=max; i += multiple)
                {
                     values.Add(i);
                }
            }

            return values.Sum();
        }
    }
}
