using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    public int meleeDamagePeriod = 40;
    private int meleeDamageCooldown = 0;
    private GameObject player;
    private GameObject clock;

    void Start()
    {
        clock = GameObject.FindGameObjectWithTag("Clock");
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        
        //agent.autoRepath = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.stoppingDistance = 10;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        MeleeDamageReload();
        if (Input.GetKey("z"))
        {
            //agent.Stop
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MeleeDamage();
        }
    }

    private void MeleeDamage()
    {
        if(meleeDamageCooldown <= 0)
        {
            meleeDamageCooldown = meleeDamagePeriod;
            player.GetComponent<CharacterControllerScript>().hp-=1;
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
