using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public string type;
    public int biomeId;
    public int spawnAttemptRate; // Раз во сколько тиков спавнит с шансом, указанным в spawnChance
    public int spawnChance;
    public int maxAmount; // Максимальное кол-во противников
    public int curAmount; // Текущее кол-во противников
}
