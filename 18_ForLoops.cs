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


    public static class Transpose
    {
        public static string String(string input)
        {
            //Detectamos los finales de línea

            int fila = 0;
            int columna = 0;
            int maxColumna = 0;

            string resultString = "";

            //Dimensiones de la matriz
            foreach (char c in input)
            {
                if (c == '\n')
                {
                    fila++;
                    columna = 0;
                    continue;
                }

                columna++;
                if (columna > maxColumna)
                {
                    maxColumna = columna;
                }
            }

            fila++;

            //Generamos matriz de esas dimensiones
            char[,] transposedCharacters = new char[maxColumna, fila];

            int auxFila = 0;
            int auxColumna = 0;

            //Guardamos valores en la matriz
            foreach (char c in input)
            {
                if (c == '\n')
                {
                    auxFila++;
                    auxColumna = 0;
                    continue;
                }

                transposedCharacters[auxColumna, auxFila] = c;
                auxColumna++;
            }

            //Generamos el string solución
            for (int i = 0; i < maxColumna; i++)
            {
                for (int j = 0; j<fila; j++)
                {
                    if (transposedCharacters[i, j] != '\0')
                    {
                        while (resultString.Length - resultString.LastIndexOf('\n') <= j)
                        {
                            resultString +=' ';
                        }
                        resultString += transposedCharacters[i, j];
                    }
                }
                resultString += "\n";
            }

            if (resultString.Length > 0)
            {
                resultString = resultString.Remove(resultString.Length - 1);
                resultString = resultString.Trim();
            }


            return resultString;
        }


        //Solución de la comunidad
        //public static string String(string input)
        //{
        //    var rows = input.Split('\n');
        //    var maxLineLength = rows.Max(x => x.Length);
        //    var transposed = new string[maxLineLength];
        //    for (var i = 0; i < rows.Length; i++)
        //    {
        //        for (var j = 0; j < rows[i].Length; j++)
        //            transposed[j] += rows[i][j];
        //        var remainderRowsMaximumLength = rows.Skip(i).Max(x => x.Length);
        //        for (var k = rows[i].Length; k < remainderRowsMaximumLength; k++)
        //            transposed[k] += " ";
        //    }

        //    return string.Join("\n", transposed).TrimEnd();
        //}
    }
}
