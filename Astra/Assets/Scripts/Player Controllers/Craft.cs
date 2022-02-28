using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public GameObject result;
    public string[] materialTypes;
    public int[] materialAmounts;
    public string[] remainingMaterialTypes;
    public int[] remainingMaterialAmounts;
    public string description;
    public bool isAvailable;
    public InventoryController IC;
    public GameObject[] items;

    void Start()
    {
        //items = new GameObject[9];
        remainingMaterialAmounts = new int[materialAmounts.Length];
        remainingMaterialTypes = new string[materialTypes.Length];
        items = IC.items;
    }

    private void Update()
    {
        items = IC.items;
        //CheckAvailablity();
    }
    public void CheckAvailablity()
    {
        CopyArrays();
        for(int i=0; i<materialTypes.Length; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if (items[j] == null)
                {
                    continue;
                }
                if(items[j].GetComponent<ItemController>().type == materialTypes[i])
                {
                    remainingMaterialAmounts[i] -= items[j].GetComponent<ItemController>().amount;
                }
                if (remainingMaterialAmounts[i]<0)
                {
                    remainingMaterialAmounts[i] = 0;
                }
            }
        }
        isAvailable = true;
        for (int i = 0; i < materialAmounts.Length; i++)
        {
            if (remainingMaterialAmounts[i]!=0)
            {
                isAvailable = false;
            }
        }
    }
    private void CopyArrays()
    {
        for (int i = 0; i < materialTypes.Length; i++)
        {
            remainingMaterialTypes[i] = materialTypes[i];
            remainingMaterialAmounts[i] = materialAmounts[i];
        }
        /*for (int i = 0; i < 9; i++)
        {
            items[i] = IC.items[i].gameObject;
        }*/
    }
}
