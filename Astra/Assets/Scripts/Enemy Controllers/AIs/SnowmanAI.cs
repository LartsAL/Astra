using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanAI : MonoBehaviour
{
    [SerializeField]  int degree;
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
    [SerializeField] private bool isCasting;

    public int shootPeriod;
    private int shootCooldown = 0;
    private GameObject player;
    private GameObject clock;
    private EnemyHPController EHpContr;
    public int castingSpeed;
    public int throwingSpeed;

    void Start()
    {
        isCasting = true;
        EHpContr = GetComponent<EnemyHPController>();
        rb = this.GetComponent<Rigidbody2D>();
        StartScale = transform.localScale;

        clock = GameObject.FindGameObjectWithTag("Clock");
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Rotate();

        if (!isShooting)
        {
            Recharge();
        }
        else
        {
            Shoot();
        }
    }
    // Update is called once per frame
    void Update()
    {

        

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
        if (isCasting)
        {
            if(degree < 120)
            {
                degree+=castingSpeed;
            }
            else
            {
                isCasting = false;
                GameObject SummonedProjectile = Instantiate(projectile, ShootingPoint.transform.position, transform.rotation);
                SummonedProjectile.GetComponent<ProjectileController>().direction = Vector3.MoveTowards(ShootingPoint.transform.position, target.transform.position, projSpeed / 10) - ShootingPoint.transform.position;
            }
        }
        else
        {
            if (degree > 0)
            {
                degree-=throwingSpeed;
            }
            else
            {
                isCasting = true;
                isShooting = false;
            }
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

    private void Rotate()
    {
        arm.transform.rotation = Quaternion.Euler(0, 0, -degree * koef);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
