using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image[] slots;
    public CraftsController CC;
    private int UpdatesAfterCrafting;

    void Start()
    {
        UpdatesAfterCrafting = 0; 
        //items = new GameObject[9];
        remainingMaterialAmounts = new int[materialAmounts.Length];
        remainingMaterialTypes = new string[materialTypes.Length];
        items = IC.items;
        slots = IC.slots;
    }

    private void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            CC.UpdateCrafts();
        }
        items = IC.items;
        if (UpdatesAfterCrafting>0)
        {
            CC.UpdateCrafts();
            UpdatesAfterCrafting -= 1;
            IC.UpdateAmountText();
        }
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
    }
    public void ExecuteCraft()
    {
        CopyArrays();
        for (int i = 0; i < materialTypes.Length; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if (items[j] == null)
                {
                    continue;
                }
                if (items[j].GetComponent<ItemController>().type == materialTypes[i])
                {
                    if(remainingMaterialAmounts[i] < items[j].GetComponent<ItemController>().amount)
                    {
                        items[j].GetComponent<ItemController>().amount -= remainingMaterialAmounts[i];
                        remainingMaterialAmounts[i] = 0;
                    }
                    else
                    {
                        remainingMaterialAmounts[i] -= items[j].GetComponent<ItemController>().amount;
                        Destroy(items[j]);
                        slots[j].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;

                        // Костылиииии
                        Color c = slots[j].gameObject.transform.GetChild(0).GetComponent<Image>().color;
                        c.a = 0;
                        slots[j].gameObject.transform.GetChild(0).GetComponent<Image>().color = c;
                    }
                    
                }
                if (remainingMaterialAmounts[i] <= 0)
                {
                    remainingMaterialAmounts[i] = 0;
                    break;
                }
            }
        }
        IC.CreateItem(result);
        CC.UpdateCrafts();
        CC.UpdateCrafts();
        UpdatesAfterCrafting = 10;
    }
}
