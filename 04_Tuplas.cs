using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class PhoneNumber
    {
        public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
        {
            string[] phoneElements = phoneNumber.Split('-');
            return (phoneElements[0] == "212" ? true : false , phoneElements[1] == "555" ? true : false , phoneElements[3]);
        }

        public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo) => phoneNumberInfo.IsFake;

    }



    //public static class PythagoreanTriplet
    //{
    //    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    //    {
    //        List<(int a, int b, int c)> Solutions = new List<(int a, int b, int c)> { };
    //        //IEnumerable<(int, int, int)> Solutions = new List<(int, int, int)>();
    //        for (int i = 1; i< (int) (sum/3)-1; i++)
    //        {
    //            for(int j = i+1; j< (int) 2*(sum/3); j++)
    //            {
    //                var auxTuple = (i, j, sum - i - j);
    //                if ((Math.Pow(i, 2) + Math.Pow(j, 2)) == Math.Pow(sum-i-j, 2)) { Solutions.Append(auxTuple);  }
    //            }
    //        }
    //        return Solutions;
    //    }
    //}

    public static class PythagoreanTriplet
    {
        public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
        {
            // if a reaches sum/3, then there is not point to continue
            // as b > a and c > b, so we overcome the sum
            for (int a = 1; a < sum/3; a++)
            {
                for (int b = a + 1; b < sum/2; b++)
                {
                    // c is fully determined by the
                    // condition we want it to apply
                    int c = sum - a - b;
                    // simply check the pythagorean relation
                    if (a*a + b * b == c * c)
                        yield return (a, b, c);
                }
            }
        }
    }

    public static class SaddlePoints
    {
        public static IEnumerable<(int, int)> Calculate(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int maxInRow = -1;
                var columnsToCheck = new List<int>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > maxInRow)
                    {
                        maxInRow =  matrix[i, j];
                        columnsToCheck = new List<int>() { j };
                    } else if (matrix[i,j] == maxInRow) {
                        columnsToCheck.Add(j);
                    }
                }

                foreach (int columnToCheck in columnsToCheck)
                {
                    bool minInColumn = true;
                    for (int row = 0; row < matrix.GetLength(0); row++)
                    {
                        if (row != i)
                        {
                            minInColumn = matrix[row, columnToCheck] < maxInRow ? false : minInColumn;
                        }
                    }

                    if (minInColumn && columnToCheck > -1) { yield return (i+1, columnToCheck+1); }
                }
                
            }
        }
    }


    public static class Dominoes
    {
        public static bool CanChain(IEnumerable<(int, int)> dominoes)
        {
            var resultado = new List<(int,int)>();
            var auxDominoes = new List<(int,int)>();
            try { 
            auxDominoes = dominoes.ToList();

            //resultado.Add(dominoes.ElementAt(0));
            //auxDominoes.RemoveAt(0);
            } catch { }

            int index = 0;
            int indexOfElementToDelete = -1;
            bool existsSolution = true;

            //while (auxDominoes.Count > 0)
            //{
            //    foreach ((int,int) element in auxDominoes.Reverse<(int,int)>())
            //    {
            //        indexOfElementToDelete = -1;

            //        if (resultado[index].Item2 == element.Item1)
            //        {
            //            resultado.Add((element.Item1, element.Item2));
            //            indexOfElementToDelete = auxDominoes.IndexOf(element);
            //            index++;
            //            break;
            //        }else if (resultado[index].Item2 == element.Item2)
            //        {
            //            resultado.Add((element.Item2, element.Item1));
            //            indexOfElementToDelete = auxDominoes.IndexOf(element);
            //            index++;
            //            break;
            //        }   
                    
            //    }

            //    if (indexOfElementToDelete >= 0)
            //    {
            //        auxDominoes.RemoveAt((int)indexOfElementToDelete);
            //    }
            //    else
            //    {
            //        existsSolution = false;
            //    }

                //if (!existsSolution)
                //{
                //    break;
                //}

           //}

            //try
            //{
            //    if (resultado[0].Item1 != resultado[resultado.Count-1].Item2)
            //    {
            //        existsSolution = false;
            //    }
            //} catch { }
           if (auxDominoes.Count > 0 )
            {
                return RecursiveDominoesChain(auxDominoes, resultado, index, indexOfElementToDelete);
            }
            else
            {
                return true;
            }

            

            //if (existsSolution)
            //{
            //    foreach ((int, int) value in resultado)
            //    {
            //        yield return value;
            //    }
            //}
        }

        public static bool RecursiveDominoesChain( List<(int, int)> auxDominoes2,  List<(int, int)> resultado2,  int index2, int indexOfElementToDelete2)
        {
            bool validSolution = false;
            List<(int, int)> auxDominoes = new List<(int, int)>(auxDominoes2);
            List<(int, int)> resultado = new List<(int, int)>(resultado2);
            int index = index2;
            int indexOfElementToDelete = indexOfElementToDelete2;

            if (indexOfElementToDelete >= 0)
            {
                auxDominoes.RemoveAt((int)indexOfElementToDelete);
            }

            if (resultado.Count > 0)
            {

                foreach ((int, int) element in auxDominoes)
                {
                    indexOfElementToDelete = -1;

                    if (resultado[index].Item2 == element.Item1)
                    {
                        resultado.Add((element.Item1, element.Item2));
                        indexOfElementToDelete = auxDominoes.IndexOf(element);
                        index++;
                        validSolution = RecursiveDominoesChain(auxDominoes, resultado, index, indexOfElementToDelete);
                        if (validSolution)
                        {
                            break;
                        }
                        resultado.RemoveAt((int)index);
                        index--;
                    }
                    else if (resultado[index].Item2 == element.Item2)
                    {
                        resultado.Add((element.Item2, element.Item1));
                        indexOfElementToDelete = auxDominoes.IndexOf(element);
                        index++;

                        validSolution = RecursiveDominoesChain(auxDominoes, resultado, index, indexOfElementToDelete);
                        if (validSolution)
                        {
                            break;
                        }
                        resultado.RemoveAt((int)index);
                        index--;
                    }

                }
            } else
            {
                resultado.Add(auxDominoes.ElementAt(0));
                index = 0;
                indexOfElementToDelete = 0;
                validSolution = RecursiveDominoesChain(auxDominoes, resultado, index, indexOfElementToDelete);
            }
             


            //if (auxDominoes.Count == 0 && resultado[0].Item1 == resultado[resultado.Count-1].Item2)
            if (validSolution == true || (auxDominoes.Count == 0 && resultado[0].Item1 == resultado[resultado.Count-1].Item2))
            {
                return true;
            }else
            {
                return false;
            }
        }
    }


    //-------------------------------------------------------------------------------------------------------------------------------


    public static class Knapsack
    {
        //public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
        //{
        //    (int weight, int value)[] itemsSorted = items.OrderByDescending(item => (item.value/item.weight)).ToArray();

        //    int actualWeight = 0;
        //    int actualValue = 0;    
        //    foreach ((int weight, int value) in itemsSorted)
        //    {
        //        if (weight < (maximumWeight - actualWeight))
        //        {
        //            actualWeight += weight;
        //            actualValue += value;
        //        }
        //    }

        //    return actualValue;
        //}

        //Recoursive



        //public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
        //{
        //    int maxValue = 0;
        //    int minWeight = 0;

        //    if (items.Length > 0)
        //    {
        //        minWeight = items.Min(item => item.weight);
        //    }

        //    (int weight,int value)[] auxList = items.OrderByDescending(item => item.value/item.weight).ToArray();

        //    MaxValueRecursive(maximumWeight, auxList,ref maxValue, 0, 0,minWeight);

        //    return maxValue;
        //}

        //public static int MaxValueRecursive(int maximumWeight, (int weight, int value)[] items,ref int maxValue, int actualWeight, int actualValue, int minWeight)
        //{
        //    foreach ((int weight, int value) item in items)
        //    {
        //        int newWeight = 0;
        //        int newValue = 0;
        //        List<(int weight, int value)> auxList = items.ToList();

        //        if (item.weight <= (maximumWeight - actualWeight))
        //        {
        //            newWeight = actualWeight + item.weight;
        //            newValue = actualValue + item.value;


        //            //(int weight, int value)[] auxitems = items.Where(x => x != item).ToArray();
        //            auxList.Remove(item);
        //            (int weight, int value)[] auxitems = auxList.ToArray();

        //            if (newValue > maxValue )
        //            {
        //                maxValue = newValue;
        //            }

        //            if (auxitems.Length > 0 && (maximumWeight - actualWeight) > minWeight)
        //            {
        //                MaxValueRecursive(maximumWeight, auxitems,ref maxValue,newWeight,newValue, minWeight);
        //            }
        //        } else
        //        {
        //            auxList.Remove(item);
        //        }

        //    }

        //    return maxValue;
        //}

        public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
        {

            //Solution
            //https://www.freecodecamp.org/news/how-to-use-dynamic-programming-to-solve-the-0-1-knapsack-problem/

            //Be careful!!! This lists doesn´t have 0 at the begining but the data table with the result does. Due to this it is needed to add a -1 to the indices of this arrays as the loop goes trough the indices of the data matrix
            int[] weights = items.Select(item => item.weight).ToArray();        //List of weights
            int[] values = items.Select(value => value.value).ToArray();        //List of Values

            int[,] data = new int[items.Length+1, maximumWeight+1];

            int n = items.Length;

            for (int itemNum = 0; itemNum <= n; itemNum++)
            {
                for (int capacity = 0; capacity <= maximumWeight; capacity++)
                {
                    if(itemNum == 0 || capacity == 0)
                    {
                        data[itemNum, capacity] = 0;
                    } else if (weights[itemNum-1] <= capacity)
                    {
                        data[itemNum, capacity] = Math.Max(values[itemNum-1] + data[itemNum-1,capacity - weights[itemNum-1]], data[itemNum-1,capacity]);
                    }
                    else
                    {
                        data[itemNum, capacity] = data[itemNum-1, capacity]; 
                    }
                }
            }

            return data[n, maximumWeight];
        }

    }


}
