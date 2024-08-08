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

}
