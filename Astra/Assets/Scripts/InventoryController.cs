using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    List<GameObject> nearItems = new List<GameObject>();
    public Image[] slots;
    private GameObject[] items;
    public Image lightSlot;
    private int chosenSlot = 1;

    private float pickupCooldown;
    private bool canPickup = true;
    void Start()
    {
        items = new GameObject[9];
    }
    void Update()
    {
        pickupCooldown = 0.1f;
        ChooseSlot();
        lightSlot.rectTransform.position = slots[chosenSlot - 1].rectTransform.position;
        PickupItems();
        if (items[chosenSlot - 1] != null && Input.GetKey("q"))
        {
            DropItem(items[chosenSlot - 1]);
        }

    }
    private void ChooseSlot() {
        if (Input.GetKey("1") || Input.GetKey("2") || Input.GetKey("3") || Input.GetKey("4") || Input.GetKey("5") || Input.GetKey("6") || Input.GetKey("7") || Input.GetKey("8") || Input.GetKey("9"))
        {
            chosenSlot = int.Parse(Input.inputString);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            chosenSlot -= (int) Input.mouseScrollDelta.y;
            if (chosenSlot <= 0)
            {
                chosenSlot = 9;
            }
            if (chosenSlot >= 10)
            {
                chosenSlot = 1;
            }
        }
    }

    public void PickupItems()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            canPickup = false;
            foreach (Collider2D i in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1.5f))
            {
                nearItems.Add(i.gameObject);
            }

            float minRange = float.MaxValue;
            GameObject nearestObject = null;
            foreach (GameObject i in nearItems)
            {
                if ((i.transform.position - this.transform.position).sqrMagnitude < minRange && i.tag == "Item")
                {
                    minRange = (i.transform.position - this.transform.position).sqrMagnitude;
                    nearestObject = i;
                }
            }
            if (nearestObject != null)
            {
                if (items[chosenSlot - 1] != null)
                {
                    DropItem(items[chosenSlot - 1]);
                }
                items[chosenSlot - 1] = nearestObject;

                nearestObject.transform.position = slots[chosenSlot - 1].transform.position;
                nearestObject.transform.SetParent(slots[chosenSlot - 1].transform);
                //nearestObject.GetComponent<SpriteRenderer>().sortingOrder = 9;

                slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = nearestObject.GetComponent<SpriteRenderer>().sprite; // Ебать, оно не работает

                // Што это за костыли, ёбаный рот
                Color c = slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color;
                c.a = 1;
                slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color = c;

                nearestObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            nearItems.Clear();
            Invoke("PickupTimer", pickupCooldown);
        }
    }
    
    private void DropItem(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().sortingOrder = 0;

        slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;

        // Костылиииии
        Color c = slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color;
        c.a = 0;
        slots[chosenSlot - 1].gameObject.transform.GetChild(0).GetComponent<Image>().color = c;

        item.GetComponent<SpriteRenderer>().enabled = true;//

        item.transform.parent = null;
        item.transform.position = transform.position;
        items[chosenSlot - 1] = null;
    }

    private void PickupTimer()
    {
        canPickup = true;
    }
}
