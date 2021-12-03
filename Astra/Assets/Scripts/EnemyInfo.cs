using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int biomeId;
    public int spawnAttemptRate; //раз во сколько кадров спавнит с шансом, указанным в spawnChance
    public int spawnChance;
    public int maxAmount; //пока ни на что не влияет
}
