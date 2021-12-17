using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private float angularSpeed;
    public bool canDamage;
    public int fixeddmg;
    public int dmg;
    public float requiredAngSpeed;
    public float bonusDamage;

    void Start()
    {
        
    }

    void Update()
    {
        if (angularSpeed >= requiredAngSpeed)
        {
            canDamage = true;
            dmg = fixeddmg + Mathf.RoundToInt((angularSpeed - requiredAngSpeed) * bonusDamage);
        }
        else
        {
            canDamage = false;
            dmg = fixeddmg;
        }
        angularSpeed = GetComponentInParent<SpinScript>().angularSpeed;
    }
}
