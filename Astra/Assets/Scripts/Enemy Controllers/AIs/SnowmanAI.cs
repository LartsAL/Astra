using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanAI : MonoBehaviour
{
    int degree;
    private int koef;
    [SerializeField] Transform target;
    public GameObject projectile;
    public GameObject arm;
    public float speed;
    public float projSpeed;
    public Rigidbody2D rb;
    private Vector3 StartScale;
    public GameObject ShootingPoint;
    private bool isShooting;

    public int shootPeriod;
    private int shootCooldown = 0;
    private GameObject player;
    private GameObject clock;
    private EnemyHPController EHpContr;

    void Start()
    {
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

        if (!isShooting)
        {
            Recharge();
        }
        else
        {
            Shoot();
        }

        if (player.transform.position.x - transform.position.x < 0)
        {
            koef = 1;
        }
        else
        {
            koef=-1;
        }
        transform.localScale = new Vector3(StartScale.x*koef, StartScale.y, StartScale.z);
    }


    private void Shoot()
    {
        degree++;
        if (degree<120)
        {
            arm.transform.rotation = Quaternion.Euler(0, 0, -degree*koef);
        }
        else
        {
            Debug.LogError("Бебра!");
            GameObject SummonedProjectile = Instantiate(projectile, ShootingPoint.transform.position, transform.rotation);
            SummonedProjectile.GetComponent<PurpleProjectileController>().direction = Vector3.MoveTowards(ShootingPoint.transform.position, target.transform.position, projSpeed / 10) - ShootingPoint.transform.position;
            SummonedProjectile.transform.SetParent(transform);
            isShooting = false;
            degree = 0;
            arm.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
            isShooting = true;
        }
    }

}
