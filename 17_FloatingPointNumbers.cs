using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class BookStore
    {
        public static decimal Total(IEnumerable<int> books)
        {
            double[] discounts = new double[] { 0, 0.05, 0.1, 0.2, 0.25 };

            int[] numOfBooksOfEachType = new int[5];

            foreach (int book in books)
            {
                numOfBooksOfEachType[book-1] +=1;
            }

            numOfBooksOfEachType = numOfBooksOfEachType.OrderByDescending(x => x).ToArray();

            int NumOfGroups = numOfBooksOfEachType.Max();

            int maxNumOfBookInGroup = 5 - numOfBooksOfEachType.Where(x => x == 0).Count();

            int totalNumOfBooks = numOfBooksOfEachType.Sum();

            int[] groups = new int[NumOfGroups];
            int[] groupsShared = new int[NumOfGroups];
            int[] groupsOptimum = new int[NumOfGroups];

            decimal result = decimal.MaxValue;

            foreach (int amountOfBooksOfEachType in numOfBooksOfEachType)
            {
                bool[] emptyGroups = new bool[NumOfGroups]; //Grupo al que se puede añadir un libro todavía
                emptyGroups = emptyGroups.Select(x => x = true).ToArray();  //En promer lugar, el nuevo tipo de libro se puede añadir a cualquier grupo


                for (int i = 0; i < amountOfBooksOfEachType; i++)   //para cada libro de un mismo tipo
                {
                    decimal auxResult = decimal.MaxValue;
                    int bestIndex = -1;
                    int[] groupsAux = (int[])groupsOptimum.Clone();

                    for (int j = 0; j < NumOfGroups; j++)       //para cada grupo de libros
                    {
                        if (emptyGroups[j])     //Si un libro del mismo tipo no ha sido añadido todavía
                        {
                            groupsAux[j] +=1;

                            if ((decimal)groupsAux.Select(x => x*8*(1-discounts[x = x ==0 ? 0 : x-1])).Sum() < auxResult)   //Evaluamos si añadir el libro a este grupo es mejor que al que teníamos guardado anteriormente como mejor
                            {
                                auxResult = (decimal)groupsAux.Select(x => x*8*(1-discounts[x = x ==0 ? 0 : x-1])).Sum();
                                groupsOptimum = (int[])groupsAux.Clone();
                                bestIndex = j;
                            }

                            groupsAux[j] -=1;
                        }
                    }

                    emptyGroups[bestIndex] = false;
                }
            }

            return (decimal)groupsOptimum.Select(x => x*8*(1-discounts[x-1])).Sum();
        }
    }
}
