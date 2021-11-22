using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        
        agent.autoRepath = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.stoppingDistance = 10;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (Input.GetKey("z"))
        {
            //agent.Stop
        }
    }
}
