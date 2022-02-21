using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public Animator anim;
    public float deathSeconds;
    public int maxHp;
    public int hp;
    private bool isVulnerable;
    private GameObject clock;
    private int tickNumberChange;
    private int tickNumber;
    private int vulCD; //длительность неуязвимости
    public int vulCDfixed;
    private bool isAlive;
    private TicksCounter TCounter;
    private DropController dc;

    // Start is called before the first frame update
    void Start()
    {
        dc = GetComponent<DropController>();
        isAlive = true;
        anim = this.gameObject.GetComponent<Animator>();
        clock = GameObject.FindGameObjectWithTag("Clock");
        vulCD = vulCDfixed;
        isVulnerable = true;
        hp = maxHp;
        TCounter = clock.GetComponent<TicksCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        tickNumberChange = TCounter.tickNumberChange;
        tickNumber = TCounter.tickNumber;
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
        if (collision.gameObject.tag == "Weapon" && isVulnerable && collision.gameObject.GetComponent<WeaponScript>().canDamage && isAlive)
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
        Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
        GetComponent<EnemyInfo>().prefab.GetComponent<EnemyInfo>().curAmount -= 1;
        anim.SetTrigger("isDying");
        StartCoroutine(Die());
        
    }

    public void GetDamaged()
    {
        if (hp>0 && isAlive) {
            anim.SetTrigger("WeeWee");
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(deathSeconds);
        if (dc!=null)
        {
            dc.Drop();
        }
        Destroy(this.gameObject);
    }
}
