using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] Transform target;
    public GameObject projectile;
    public float speed;
    public float projSpeed;
    private NavMeshAgent agent;
    public Rigidbody2D rb;
    private Vector3 StartScale;
    public GameObject ShootingPoint;
    private float reviveChance = 70;

    private bool isReviving;
    public int shootPeriod;
    private int shootCooldown = 0;
    private GameObject player;
    private GameObject clock;
    private EnemyHPController EHpContr;

    void Start()
    {
        isReviving = false;
        EHpContr = GetComponent<EnemyHPController>();
        rb = this.GetComponent<Rigidbody2D>();
        StartScale = transform.localScale;

        clock = GameObject.FindGameObjectWithTag("Clock");
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (EHpContr.hp<=0 && !isReviving)
        {
            if(Random.Range(0, 100) <= reviveChance)
            {
                StartCoroutine(Revive());
            }
            isReviving = true;
        }
        if (Input.GetKeyDown("k"))
        {
            Shoot();
        }
        GetComponent<SpriteRenderer>().sortingOrder = 3;
        Move();

        Recharge();

        if (player.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-StartScale.x, StartScale.y, StartScale.z);
        }
        else
        {
            transform.localScale = new Vector3(StartScale.x, StartScale.y, StartScale.z);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed/100);
    }

    private void Shoot()
    {
        GameObject SummonedProjectile = Instantiate(projectile, ShootingPoint.transform.position, transform.rotation);
        SummonedProjectile.GetComponent<PurpleProjectileController>().direction = Vector3.MoveTowards(ShootingPoint.transform.position, target.transform.position, projSpeed/10) - ShootingPoint.transform.position;
        SummonedProjectile.transform.SetParent(transform);
    }
    private void Recharge()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= clock.GetComponent<TicksCounter>().tickNumberChange;
        }
        if (shootCooldown <= 0)
        {
            shootCooldown = shootPeriod;
            Shoot();
        }
    }
    IEnumerator Revive()
    {
        yield return new WaitForSeconds(EHpContr.deathSeconds-0.01f);
        GameObject newAttempt = Instantiate(this.gameObject);
        newAttempt.GetComponent<EnemyHPController>().hp = newAttempt.GetComponent<EnemyHPController>().maxHp;
        newAttempt.GetComponent<GhostAI>().reviveChance = reviveChance - 20;
        isReviving = false;

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogError("Вы врезались в присрака");
        if (collision.gameObject.tag == "Player")
        {
            isReviving = true;
            StartCoroutine(Revive());
            EHpContr.hp = 0;

        }
    }*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isReviving = true;
            StartCoroutine(Revive());
            EHpContr.hp = 0;

        }
    }
}
