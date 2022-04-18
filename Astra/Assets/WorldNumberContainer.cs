using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNumberContainer : MonoBehaviour
{
    public int worldNumber;
    public bool[] isRestarting = new bool[3];

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
