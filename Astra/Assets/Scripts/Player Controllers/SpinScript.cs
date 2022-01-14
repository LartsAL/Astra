using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool isSpinning;
    public float angularSpeed;
    private float lastAngle;
    public GameObject weapon;
    public InventoryController ic;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ic = player.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            Spin();
        }
    }

    private void FixedUpdate()
    {
        angularSpeed = Mathf.Abs(lastAngle - transform.rotation.z) * 1000f;
        lastAngle = transform.rotation.z;
        //if (ic.items[ic.chosenSlot-1].GetComponent<WeaponReferenceScript>() != null)
        if (ic.items[ic.chosenSlot - 1] != null)
        {
            if (ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>() == null)
            {
                Destroy(weapon);
                weapon = null;
            }
            if (weapon == null && ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>().weapon != null || weapon != null && ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>().weapon.GetComponent<WeaponScript>().type != weapon.GetComponent<WeaponScript>().type)
            //if (weapon.GetComponent<WeaponScript>().type != ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>().weapon.GetComponent<WeaponScript>().type || (weapon == null && ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>().weapon != null))
            {
                Debug.LogWarning("Mr.Cum");
                Destroy(weapon);
                weapon = null;
                weapon = Instantiate(ic.items[ic.chosenSlot - 1].GetComponent<WeaponReferenceScript>().weapon);
                weapon.transform.SetParent(this.gameObject.transform);
                weapon.transform.localPosition = new Vector3(weapon.GetComponent<WeaponScript>().handOffset, 0, 0); // Ранее (0.5f, 0, 0)
                //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                //weapon.transform.localRotation = this.gameObject.transform.rotation;
                weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            Destroy(weapon);
            weapon = null;
        }
    }

    void Spin()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed);
    }
}
