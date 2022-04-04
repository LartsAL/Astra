using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.AI;
public class MapGeneratorScript : MonoBehaviour
{
	public TicksCounter TC;
	public WorldNumberContainer WNC;
	public GameSaver GS;
	public Transform NavMesh;
	public GameObject[] grasstiles = new GameObject[10];
	public GameObject[] objects;
	public int size;
	[HideInInspector] public int[,] tileMatrix;
	[HideInInspector] public int[,] objectMatrix;
	public int worldNumber;

	private NavMeshSurface2d surface;

	private EnemySpawner ES;

	void Start()
    {
		ES = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawner>(); // !
		worldNumber = WNC.worldNumber;
		surface = GameObject.Find("NavMesh 2D").GetComponent<NavMeshSurface2d>();
		SaveData data = GS.LoadGame();
		GS.currentData = data;
		if (data == null || data.worlds[worldNumber] == null || data.worlds[worldNumber].tileMatrix == null || WNC.isRestarting[worldNumber])
		{
			GenerateNewWorld();
			WNC.isRestarting[worldNumber] = false;
		}
		else
		{
			tileMatrix = data.worlds[worldNumber].tileMatrix;
			objectMatrix = data.worlds[worldNumber].objectMatrix;
			
		}
		SaveTheWorld();

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				GameObject a =  Instantiate(grasstiles[tileMatrix[i, j]], new Vector2(i*1.6f, j*1.6f), grasstiles[tileMatrix[i, j]].transform.rotation);
				a.transform.SetParent(NavMesh);
			}
			
		}

		TransformTrees(tileMatrix, objectMatrix);
		Summon(tileMatrix, objectMatrix);
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if (objectMatrix[i, j] <= objects.Length)
				{
					GameObject instantiated = Instantiate(objects[objectMatrix[i, j]], new Vector2(i * 1.6f, j * 1.6f), objects[objectMatrix[i, j]].transform.rotation);
					instantiated.transform.SetParent(NavMesh);
				}
			}
			
		}

		surface.BuildNavMesh();

		ES.GetData();
	}
	void SaveTheWorld()
	{
		GS.currentData.worlds[worldNumber].tileMatrix = tileMatrix;
		GS.currentData.worlds[worldNumber].objectMatrix = objectMatrix;
		//WNC.isRestarting[worldNumber] = true;
	}
	public void GenerateNewWorld()
	{
		tileMatrix = GeneratetileMatrix(size);
		objectMatrix = new int[size, size];
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				objectMatrix[i, j] = 0;
			}
		}
		GenerateMultiplyObjects(tileMatrix, objectMatrix, 1, 10, 2, 15, 1, 0);
		GenerateMultiplyObjects(tileMatrix, objectMatrix, 10, 3, 10, 15, 1, 0);
		GenerateMultiplyObjects(tileMatrix, objectMatrix, 25, 10, 60, 15, 1, 3);
		GenerateMultiplyObjects(tileMatrix, objectMatrix, 1, 100, 7, 15, 0, 3);
		GenerateMultiplyObjects(tileMatrix, objectMatrix, 26, 10, 100, 15, 0, 3);
		GenerateSingleObject(tileMatrix, objectMatrix, 64, 1);
	}


	public void GenerateSingleObject(int [,] tileMatrix, int[,] objectMatrix, int id, int biomeid)
	{
		rand:
		int x = UnityEngine.Random.Range(0, tileMatrix.GetLength(0));
		int y = UnityEngine.Random.Range(0, tileMatrix.GetLength(1));
		if (tileMatrix[x, y] != biomeid || objectMatrix[x, y] != 0)
		{
			goto rand;
		}
		objectMatrix[x, y] = id;
		
	}
	public int[,] GeneratetileMatrix(int size)
	{
		int chosenNumber = 0;
		int[] biomeNumbers = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
		bool areBiomsGenerated = false;
		int centerx;
		int centery;
		int centerbiome = 0;
		int[,] matrix = new int[size, size];
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				matrix[i, j] = 0;

			}
		}
		for (int a = 0; a < 3; a++)
		{
			for (int b = 0; b < 3; b++) {
				while (true)
				{
					chosenNumber = UnityEngine.Random.Range(0, 10);
					centerbiome = biomeNumbers[chosenNumber];
					if (centerbiome != 0) { break; };
				}
				biomeNumbers[chosenNumber] = 0;
				centerx = UnityEngine.Random.Range(size / 12 + 1, size / 4 + 1) - 1 + a * size / 3;
				centery = UnityEngine.Random.Range(size / 12 + 1, size / 4 + 1) - 1 + b * size / 3;
				matrix[centerx, centery] = centerbiome * 11;
			}
		}

		while (!areBiomsGenerated)
		{
			areBiomsGenerated = true;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if ((i == 0) || (i == size - 1) || (j == 0) || j == size - 1)
					{
					}
					else
					{
						for (int biome = 1; biome < 10; biome++)
							if (matrix[i, j] == 11 * biome)
							{
								if (matrix[i - 1, j] == 0) { matrix[i - 1, j] = biome * 111; areBiomsGenerated = false; }
								if (matrix[i + 1, j] == 0) { matrix[i + 1, j] = biome * 111; areBiomsGenerated = false; }
								if (matrix[i, j - 1] == 0) { matrix[i, j - 1] = biome * 111; areBiomsGenerated = false; }
								if (matrix[i, j + 1] == 0) { matrix[i, j + 1] = biome * 111; areBiomsGenerated = false; }
								matrix[i, j] = biome;
							}

					}
				}
			}

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					for (int biome = 1; biome < 10; biome++)
					{
						if (matrix[i, j] == biome * 111)
						{
							matrix[i, j] = biome * 11;
						}
						if ((i == 0) || (i == size - 1) || (j == 0) || j == size - 1)
						{
							matrix[i, j] = 0;
						}


					}
				}
			}
		}

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				for (int biome = 1; biome < 10; biome++)
				{
					if (matrix[i, j] == biome * 11)
					{
						matrix[i, j] = biome;
					}


				}
			}
		}
		return matrix;
	}
	public static void GenerateMultiplyObjects(int[,] firstMatrix, int[,] secondMatrix, int id, int percentage, int fixedDistance, int fieldDistance, int fieldPercentage, int biomeid)
	{
		float one = 1.0f;
		float squareSize = one * secondMatrix.Length;
		float size = Mathf.Sqrt(squareSize);
		int distance = fixedDistance;
		UnityEngine.Random rnd = new UnityEngine.Random();
		int minusOne = 0;
		for (int i = 0; i < size; i++)
		{
			if (i > 0)
			{
				minusOne = 1;
			}
			for (int j = 0; j < size; j++)
			{
				if (firstMatrix[i, j] == biomeid || biomeid == 0) {
					if (distance <= 0 && secondMatrix[i, j] == 0 && secondMatrix[i-minusOne, j]!=id)
					{
						int randomNumber = UnityEngine.Random.Range(1, 101);
						if (randomNumber <= percentage)
						{
							if (firstMatrix[i, j] != 0)
							{
								secondMatrix[i, j] = id;
								distance = fixedDistance;
							}
						}
					}
					else
					{
						distance--;
					}
				}
			}
		}
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				int randomNumber = UnityEngine.Random.Range(1, 101);
				if (randomNumber <= fieldPercentage && secondMatrix[i, j] == id)
				{
					for (int x = 0; x < size; x++)
					{
						for (int y = 0; y < size; y++)
						{
							if (i - fieldDistance < x && i + fieldDistance > x && j - fieldDistance < y && j + fieldDistance > y && secondMatrix[x, y] == id)
							{
								int randomNumberTwo = UnityEngine.Random.Range(1, 101);
								if (randomNumberTwo <= 70)
								{
									secondMatrix[x, y] = 0;

								}
							}
						}
					}
				}
			}
		}
	}
	public void TransformTrees(int[,] tileMatrix, int[,] objectMatrix)
	{
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if(objectMatrix[i, j] == 1)
				{
					objectMatrix[i, j] = tileMatrix[i, j];
				}
			}
		}
	}
	public void Summon(int[,] tileMatrix, int[,] objectMatrix)
	{
		for (int i = 0; i < tileMatrix.GetLength(0); i++)
		{
			for (int j = 0; j < tileMatrix.GetLength(1); j++)
			{
				if (objectMatrix[i, j] == 64)
				{
					GameObject.FindGameObjectWithTag("Player").transform.position = 1.6f * new Vector3(i, j, 0);
				}
			}
		}
	}

	private void Update()
	{
		if(TC.tickNumber % 400 == 0 || Input.GetKeyDown("p"))
		{
			SaveTheWorld();
			GS.SaveGame(GS.currentData);
		}
	}
}