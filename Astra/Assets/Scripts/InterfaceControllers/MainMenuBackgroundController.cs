using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackgroundController : MonoBehaviour
{
    private GameObject rank;
    private GameObject tile;
    public Sprite[] sprites;
    private Sprite sprite;
    void Start() {
        int i = Random.Range(0, sprites.Length-1);
        sprite = sprites[i];
        for (int a = 0; a < transform.childCount; a++) {
            rank  = transform.GetChild(a).gameObject;
            for (int x = 0; x < rank.transform.childCount; x++)
            {
                tile = rank.transform.GetChild(x).gameObject;
                tile.GetComponent<Image>().sprite = sprite;
                int z = Random.Range(1, 101);
                if (z == 10)
                {
                    i = Random.Range(0, sprites.Length - 1);
                    sprite = sprites[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
