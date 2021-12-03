using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public List<int> priorities = new List<int>();
    public GameObject TestEnemy;
    public GameObject[] Enemies;
    private int[,] objectMatrix;
    private int[,] tileMatrix;
    private GameObject player;
    private GameObject clock;
    private int tileBiomeId;
    private int size;
    private int tickNumberChange;
    private int tickNumber;
    // Start is called before the first frame update
    void Start()
    {
        //objectMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().objectMatrix;
        //tileMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().tileMatrix;
        player = GameObject.FindGameObjectWithTag("Player");
        clock = GameObject.FindGameObjectWithTag("Clock");
        //size = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().size;
        for (int i=0; i<100; i++)
            {
                priorities.Add(i);
            }

        
    }

    // Update is called once per frame
    void Update()
    {
        tickNumberChange = clock.GetComponent<TicksCounter>().tickNumberChange;
        tickNumber = clock.GetComponent<TicksCounter>().tickNumber;
        if (tickNumberChange>0)
        {
            TryToSpawnAnyone();
        }
        if (Input.GetKeyDown("o"))
        {
            Spawn(TestEnemy, ChooseSpawnpoint());
        }
        tileBiomeId = tileMatrix[Mathf.RoundToInt(player.transform.position.x / 1.6f), Mathf.RoundToInt(player.transform.position.y / 1.6f)];
        
    }

    void Spawn(GameObject Enemy, Vector3 position)
    {
        GameObject spawned = Instantiate(Enemy, position, Quaternion.identity);
        
         priorities = priorities.OrderBy(x => x).ToList();
         spawned.GetComponent<NavMeshAgent>().avoidancePriority = priorities[0];
         priorities.RemoveAt(0);
        
        
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
