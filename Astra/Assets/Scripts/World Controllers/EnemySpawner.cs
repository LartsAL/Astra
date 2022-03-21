using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //private List<GameObject> aliveEnemies = new List<GameObject>(); // Список живых врагов, влияет на блокировку спавна новых врагов
    public List<int> priorities = new List<int>(); // Надо, без этого они толкаются
    public GameObject[] Enemies;
    private int[,] objectMatrix;
    private int[,] tileMatrix;
    private GameObject player;
    private GameObject clock;
    private int tileBiomeId;
    private int size;
    private int tickNumberChange;
    private int tickNumber;
 
    void Start()
    {
        foreach (GameObject i in Enemies)
        {
            i.GetComponent<EnemyInfo>().curAmount = 0;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        clock = GameObject.FindGameObjectWithTag("Clock");
        for (int i=0; i<100; i++)
        {
            priorities.Add(i);
        }
    }

    void Update()
    {
        tickNumberChange = clock.GetComponent<TicksCounter>().tickNumberChange;
        tickNumber = clock.GetComponent<TicksCounter>().tickNumber;
        if (tickNumberChange>0)
        {
            TryToSpawnAnyone();
        }
        tileBiomeId = tileMatrix[Mathf.RoundToInt(player.transform.position.x / 1.6f), Mathf.RoundToInt(player.transform.position.y / 1.6f)];
        
    }

    void Spawn(GameObject Enemy, Vector3 position)
    {
        if (Enemy.GetComponent<EnemyInfo>().curAmount < Enemy.GetComponent<EnemyInfo>().maxAmount)
        {
            GameObject spawned = Instantiate(Enemy, position, Quaternion.identity);
            Enemy.GetComponent<EnemyInfo>().curAmount++;
            spawned.GetComponent<EnemyInfo>().prefab = Enemy;
            priorities = priorities.OrderBy(x => x).ToList();
            spawned.GetComponent<NavMeshAgent>().avoidancePriority = priorities[0];
            priorities.RemoveAt(0);
        }
    }
    Vector3 ChooseSpawnpoint()
    {
        choose:
        Vector2 point = new Vector2(Random.insideUnitCircle.x * 32 + player.transform.position.x, Random.insideUnitCircle.y * 32 + player.transform.position.y);
        if(Mathf.Sqrt(Mathf.Pow((point.x - player.transform.position.x), 2) + Mathf.Pow((point.y - player.transform.position.y), 2)) <= 17.6f)
        {
            goto choose;
        }
        if (point.x>size*1.6 || point.y>size*1.6 || point.x < 0 || point.y < 0)
        {
            goto choose;
        }
        return new Vector3(point.x, point.y, 0);    
    }

    public void GetData()
    {
        objectMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().objectMatrix;
        tileMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().tileMatrix;
        size = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().size;
    }

    public void TryToSpawnAnyone()
    {
        foreach (GameObject i in Enemies)
        {
            int randomNumber = Random.Range(1, 100);
            if(tickNumber % i.GetComponent<EnemyInfo>().spawnAttemptRate == 0 && randomNumber < i.GetComponent<EnemyInfo>().spawnChance && tileBiomeId == i.GetComponent<EnemyInfo>().biomeId)
            {
                Spawn(i, ChooseSpawnpoint());
            }
        }
    }
}
