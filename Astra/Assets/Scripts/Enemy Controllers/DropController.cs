using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{

    public GameObject[] items;
    public float[] chances;
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Drop();
        }
    }
    public void Drop()
    {
        for (int i=0; i<items.Length; i++)
        {
            float randomNumber = Random.Range(1, 101);
            if (randomNumber <= chances[i])
            {
                Instantiate(items[i], transform.position, transform.rotation);
            }
        }
    }
}
