using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject TestEnemy;
    private int[,] objectMatrix;
    private int[,] tileMatrix;
    private GameObject player;
    private int tileBiomeId;
    private int size;
    // Start is called before the first frame update
    void Start()
    {
        //objectMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().objectMatrix;
        //tileMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().tileMatrix;
        player = GameObject.FindGameObjectWithTag("Player");
        //size = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().size;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (tileMatrix != null)
        {
            Debug.LogWarning("Wth");
        }
        if (Input.GetKeyDown("o"))
        {
            Debug.LogWarning("wow 0-0");
            Spawn(TestEnemy, ChooseSpawnpoint());
        }
        tileBiomeId = tileMatrix[Mathf.RoundToInt(player.transform.position.x / 1.6f), Mathf.RoundToInt(player.transform.position.y / 1.6f)];
        
    }

    void Spawn(GameObject Enemy, Vector3 position)
    {           
            Instantiate(Enemy, position, Quaternion.identity);
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
        //int tileBiomeId = 3;
        return new Vector3(point.x, point.y, 0);    
    }

    public void GetData()
    {
        objectMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().objectMatrix;
        tileMatrix = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().tileMatrix;
        size = GameObject.FindGameObjectWithTag("Generator").GetComponent<MapGeneratorScript>().size;
    }
}
