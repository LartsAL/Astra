using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] Transform target;
    public float speed;
    private NavMeshAgent agent;
    public Rigidbody2D rb;
    private Vector3 StartScale;

    public int meleeDamagePeriod = 40;
    private int meleeDamageCooldown = 0;
    private GameObject player;
    private GameObject clock;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartScale = transform.localScale;

        clock = GameObject.FindGameObjectWithTag("Clock");
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 3;
        Move();

        MeleeDamageReload();

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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MeleeDamage();
        }
    }

    private void MeleeDamage()
    {
        if (meleeDamageCooldown <= 0)
        {
            meleeDamageCooldown = meleeDamagePeriod;
            player.GetComponent<CharacterControllerScript>().hp -= 1;
        }
    }
    private void MeleeDamageReload()
    {
        if (meleeDamageCooldown > 0)
        {
            meleeDamageCooldown -= clock.GetComponent<TicksCounter>().tickNumberChange;
        }
    }
}
