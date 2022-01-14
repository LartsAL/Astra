using UnityEngine;
using System;

namespace CHarp_Shell {
public class BiomeGenerator : MonoBehaviour
{
        public int matrixSize = 36;
        public GameObject[] tiles;

        public void Start()
        {
            Generate(tiles);
        }
               
        public void Generate(GameObject[] tiles) {
            start:
            
            int[,] matrix = new int[matrixSize, matrixSize];
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = 0;
                }
            }
            
            int x, y;
            int n = 0;
            int[] order = new int[9];
            for (int i = 0; i < order.Length; i++)
            {
                order[i] = i + 1;
            }
            for (int i = 0; i < order.Length; i++)
            {
                int j = UnityEngine.Random.Range(0, order.Length);
                int tmp = order[j];
                order[j] = order[i];
                order[i] = tmp;
            }
            
            for (int k1 = 1; k1 <= 3; k1++)
            {
                for (int k2 = 1; k2 <= 3; k2++)
                {
                    x = matrix.GetLength(0) / 3 * k1;
                    y = matrix.GetLength(1) / 3 * k2;
                    x = UnityEngine.Random.Range(x - x / k1 + x / k1 / 6, x - x / k1 / 6);
                    y = UnityEngine.Random.Range(y - y / k2 + y / k2 / 6, y - y / k2 / 6);
                    matrix[x, y] = order[n];
                
                    n++;
                }
            }

            for (int i = 0; i < order.Length; i++)
            {
                int j = UnityEngine.Random.Range(0, order.Length);
                int tmp = order[j];
                order[j] = order[i];
                order[i] = tmp;
            }

            bool filled = false;
            
            while (!filled)
            {
                foreach (int biome in order)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            if (matrix[i, j] == biome)
                            {
                                if (j - 1 >= 0 && matrix[i, j - 1] == 0)
                                {
                                    matrix[i, j - 1] = matrix[i, j] * 11;
                                }
                                if (i + 1 < matrix.GetLength(0) && matrix[i + 1, j] == 0)
                                {
                                    matrix[i + 1, j] = matrix[i, j] * 11;
                                }
                                if (j + 1 < matrix.GetLength(1) && matrix[i, j + 1] == 0)
                                {
                                    matrix[i, j + 1] = matrix[i, j] * 11;
                                }
                                if (i - 1 >= 0 && matrix[i - 1, j] == 0)
                                {
                                    matrix[i - 1, j] = matrix[i, j] * 11;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] % 11 == 0)
                            {
                                matrix[i, j] = matrix[i, j] / 11;
                            }
                        }
                    }
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 0)
                        {
                            filled = false;
                            break;
                        }
                        else
                        {
                            filled = true;
                        }
                    }
                }
            }
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        goto start;
                    }
                }
            }

            TilesRender(matrix);
    }

        public void TilesRender(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Instantiate(tiles[0], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 2)
                    {
                        Instantiate(tiles[1], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 3)
                    {
                        Instantiate(tiles[2], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 4)
                    {
                        Instantiate(tiles[3], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 5)
                    {
                        Instantiate(tiles[4], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 6)
                    {
                        Instantiate(tiles[5], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 7)
                    {
                        Instantiate(tiles[6], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 8)
                    {
                        Instantiate(tiles[7], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                    else
                    if (matrix[i, j] == 9)
                    {
                        Instantiate(tiles[8], new Vector3(i * 1.6f, j * 1.6f, 0), new Quaternion());
                    }
                }
            }
        }
    }
}