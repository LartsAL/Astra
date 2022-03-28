using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public GameObject weapon;
    public string type;
    public int amount;
    public int maxAmount;
    public Text text;
    public Vector3 localScale;

    private void Update()
    {
        localScale = transform.localScale;
        text.text = amount.ToString();
        
    }
}
