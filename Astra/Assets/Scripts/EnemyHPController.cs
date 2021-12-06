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

    // Start is called before the first frame update
    void Start()
    {
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
        if (hp <= 0)
        {
            Debug.LogWarning("Плыли мы по морю, ветер мачту рвал");
            Death();
        }
        if (!isVulnerable)
        {
            vulCD -= tickNumberChange;
            if (vulCD < 0)
            {
                isVulnerable = true;
                vulCD = vulCDfixed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon" && isVulnerable)
        {
            hp -= collision.gameObject.GetComponent<WeaponScript>().dmg;
            isVulnerable = false;
            GetDamaged();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void GetDamaged()
    {

    }
}
