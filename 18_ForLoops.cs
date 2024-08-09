using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Rectangles
    {
        public static int Count(string[] rows)
        {
            int numOfRectangles = 0;
            List<int>[] vertexIndices = new List<int>[rows.Length];

            for (int j = 0; j < vertexIndices.Length; j++)
            {
                vertexIndices[j] = new List<int>();
            }

            for (int i = 0; i < rows.Length; i++) 
            {
                string row = rows[i];
                //vertexIndices.Select(i => new List<int>());

                int minIndex = row.IndexOf("+");
                while (minIndex != -1)
                {
                    vertexIndices[i].Add(minIndex);
                    minIndex = row.IndexOf("+", minIndex + "+".Length);
                }
            }

            for ( int i = 0; i< vertexIndices.Length; i++)  //Loop para los vértices superiores
            {
                for (int j = 0; j < vertexIndices[i].Count; j++)    
                {
                    for (int j2 = j+1; j2 < vertexIndices[i].Count; j2++)
                    {
                        int[] indicesInStudy = { vertexIndices[i][j], vertexIndices[i][j2] };

                        for (int k = i + 1; k < vertexIndices.Length; k++)  //Loop para los vértices inferiores 
                        {
                            if (vertexIndices[k].Contains(indicesInStudy[0]) && vertexIndices[k].Contains(indicesInStudy[1]))   //Si existen ambos índices en el límite inferior, se suma un rectángulo
                            {
                                bool validRectangle = true;

                                for (int l = i+1; l < k; l++)   //Check lateral sides
                                {
                                    if ((rows[l][indicesInStudy[0]] != '|') && (rows[l][indicesInStudy[0]] != '+') || 
                                        (rows[l][indicesInStudy[1]] != '|') && (rows[l][indicesInStudy[1]] != '+'))
                                    {
                                        validRectangle = false;
                                    }
                                }

                                for (int m = indicesInStudy[0]; m < indicesInStudy[1]; m++)
                                {
                                    if ((rows[i][m] != '-') && (rows[i][m] != '+') ||
                                        (rows[k][m] != '-') && (rows[k][m] != '+'))
                                    {
                                        validRectangle = false;
                                    }
                                }

                                if (validRectangle){
                                    numOfRectangles += 1;
                                }
                                //break;  //Salimos del loop para evitar que siga buscando en las siguientes filas
                            }
                        
                        }
                    }
                }
            }

            return numOfRectangles;
        }
    }
}
