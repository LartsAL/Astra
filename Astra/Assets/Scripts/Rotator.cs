using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject character;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = character.transform.position;
    }
}
