using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public Animator anim;
    public int maxHp;
    public int hp;
    private bool isVulnerable;
    private GameObject clock;
    private int tickNumberChange;
    private int tickNumber;
    private int vulCD; //длительность неуязвимости
    public int vulCDfixed;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        anim = this.gameObject.GetComponent<Animator>();
        clock = GameObject.FindGameObjectWithTag("Clock");
        vulCD = vulCDfixed;
        isVulnerable = true;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        tickNumberChange = clock.GetComponent<TicksCounter>().tickNumberChange;
        tickNumber = clock.GetComponent<TicksCounter>().tickNumber;
        if (hp <= 0 && isAlive)
        {
            Death();
        }
        if (!isVulnerable)
        {
            vulCD -= tickNumberChange;
            if (vulCD <= 0)
            {
                isVulnerable = true;
                vulCD = vulCDfixed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" && isVulnerable && collision.gameObject.GetComponent<WeaponScript>().canDamage)
        {
            hp -= collision.gameObject.GetComponent<WeaponScript>().dmg;
            isVulnerable = false;
            GetDamaged();
        }
    }


    public void Death()
    {
        isAlive = false;
        Destroy(GetComponent<BoxCollider2D>());
        GetComponent<EnemyInfo>().prefab.GetComponent<EnemyInfo>().curAmount -= 1;
        anim.SetTrigger("isDying");
        StartCoroutine("Die");
        
    }

    public void GetDamaged()
    {
        if (hp>0) {
            anim.SetTrigger("WeeWee");
        }
        //anim.SetBool("isGettingDamaged", true);
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.433f);
        Destroy(this.gameObject);
    }
}
